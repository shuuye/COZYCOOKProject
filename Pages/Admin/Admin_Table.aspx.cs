using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Adapters;

namespace COZYCOOK.Pages.Admin
{
    public partial class Admin_Table : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();

                // Check if reservation ID and customer name are passed in the query string
                if (Request.QueryString["reservationID"] != null && Request.QueryString["customerName"] != null)
                {
                    // Retrieve reservation ID and customer name from query string

                    string reservationID = Request.QueryString["reservationID"];
                    string customerName = Server.UrlDecode(Request.QueryString["customerName"]);

                    String decryreservationID = Decrypt(reservationID);
                    String decrypcusomerName = Decrypt(customerName);

                    // Display reservation ID and customer name in labels
                    lblReserveID.Text = decryreservationID;
                    lblCusName.Text = decrypcusomerName;
                }

                DateTime currentDateAndTime = DateTime.Now;

                // Set date to lblDate
                lblDate.Text = currentDateAndTime.ToString("MM/dd/yyyy");

                // Set time to lblTime
                lblTime.Text = currentDateAndTime.ToString("hh:mm:ss tt");
            }
        }

        protected void lnkAddTable_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "AddTable")
            {
                string[] arguments = e.CommandArgument.ToString().Split(',');
                int tableNo = Convert.ToInt32(arguments[0]);

                // Decrypt the encrypted reservation ID
                string encryptedReservationID = arguments[1];
                int reservationID = Convert.ToInt32(Decrypt(encryptedReservationID));

                // Update the reservation with the selected table number in the database
                UpdateReservationTable(reservationID, tableNo);

                // Display success message
                lblSuccessMessage.Text = string.Format("Reservation ID {0} has been successfully assigned to Table {1}.", reservationID, tableNo);

                // Refresh the GridView
                GridView1.DataBind();
            }
        }

        protected void UpdateReservationTable(int reservationID, int tableNo)
        {
            // Method to update the reservation with the selected table number in the database
            string connectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Reservations SET table_id = @tableNo WHERE reservation_id = @reservationID", con))
                {
                    cmd.Parameters.AddWithValue("@tableNo", tableNo);
                    cmd.Parameters.AddWithValue("@reservationID", reservationID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        protected void Status_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList statusDropDown = (DropDownList)sender;
                GridViewRow row = (GridViewRow)statusDropDown.NamingContainer;


                int tableNo = Int32.Parse((statusDropDown.NamingContainer as GridViewRow).Cells[0].Text);

                string sql = @"UPDATE Tables SET status = @Status WHERE table_No = @table_No";

                string strCon = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;

                using (SqlConnection con = new SqlConnection(strCon))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Status", statusDropDown.SelectedValue);
                        cmd.Parameters.AddWithValue("@table_No", tableNo);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }
                }

                BindGridView();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occur");
            }
        }

        private void BindGridView()
        {
            try
            {
                string sql = "SELECT table_No, type, size, status FROM Tables";

                string strCon = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;

                using (SqlConnection con = new SqlConnection(strCon))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        con.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable tableData = new DataTable();
                        adapter.Fill(tableData);

                        foreach (DataRow row in tableData.Rows)
                        {
                            // Check if status is "Unavailable"

                            string status = row["status"].ToString();
                            // Log status value to console or output window
                            Console.WriteLine("Status for table No. " + row["table_No"] + ": " + status);

                            if (row["Status"].ToString() == "Unavailable")
                            {
                                // Find the index of the "Assign" hyperlink column
                                int assignColumnIndex = GridView1.Columns.IndexOf(GridView1.Columns
                                    .OfType<HyperLinkField>()
                                    .FirstOrDefault(col => col.HeaderText == "Table Assignment"));

                                // Disable the "Assign" hyperlink for this row
                                if (assignColumnIndex >= 0)
                                {
                                    GridViewRow gridRow = GridView1.Rows[tableData.Rows.IndexOf(row)];
                                    HyperLink assignLink = (HyperLink)gridRow.Cells[assignColumnIndex].Controls[0];
                                    assignLink.Enabled = false;
                                    assignLink.CssClass = "disabled-link";
                                    assignLink.NavigateUrl = "#";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occurred: " + ex.Message);
            }
        }
        public string Encrypt(string clearText)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes("YourEncryptionKey", new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        // Decrypt function
        public string Decrypt(string cipherText)
        {
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes("YourEncryptionKey", new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }


    }
}

