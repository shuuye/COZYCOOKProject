using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace testing.Pages.Admin
{
    public partial class Admin_ReservationPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ReservavtionList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "CancelReservation")
                {
                    int rowIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = ReservavtionList.Rows[rowIndex];
                    int reservationId = Convert.ToInt32(ReservavtionList.DataKeys[row.RowIndex].Value);

                    // Update the reservation table_id to 0 in the database
                    UpdateReservationTableId(reservationId);
                }

                if (e.CommandName == "AssignTable")
                {
                    string[] arguments = e.CommandArgument.ToString().Split(',');
                    string reservationID = arguments[0];
                    string customerName = arguments[1];

                    string encryptreserveID = Encrypt(reservationID);
                    string encryptName = Encrypt(customerName);

                    // Redirect to Admin_Table page with query string parameters
                    Response.Redirect("Admin_Table.aspx?reservationID=" + encryptreserveID + "&customerName=" + Server.UrlEncode(encryptName));
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                string errorMessage = $"An error occurred: {ex.Message}. Please try again later.";
                ShowAlert(errorMessage);
            }
        }

        private void UpdateReservationTableId(int reservationId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string updateQuery = "UPDATE Reservations SET table_id = 0 WHERE reservation_id = @ReservationId";
                    SqlCommand command = new SqlCommand(updateQuery, connection);
                    command.Parameters.AddWithValue("@ReservationId", reservationId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                // Refresh the GridView after the update
                ReservavtionList.DataBind();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                string errorMessage = $"Database error: {ex.Message}. Please try again later.";
                ShowAlert(errorMessage);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                string errorMessage = $"An unexpected error occurred: {ex.Message}. Please contact support for assistance.";
                ShowAlert(errorMessage);
            }


        }
        private void ShowAlert(string message)
        {
            // Display an alert message to the user using JavaScript
            string script = $"<script>alert('{message}');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", script);
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