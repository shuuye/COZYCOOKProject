using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml.Linq;
using System.Web.Security;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace testing.Pages.User
{
    public partial class reservation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Clear existing items
                pax.Items.Clear();

                // Add custom pax values up to 15
                for (int i = 1; i <= 15; i++)
                {
                    pax.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }

                // Optionally, you can set a default selected item
                pax.SelectedIndex = 0;
            }
            MembershipUser userName = Membership.GetUser();
            nametxt.Text = userName.UserName;
            emailip.Text = userName.Email;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int customerId;
                string date = dateip.Text;
                string time = timeip.Text;
                string paxdb = pax.Text;
                string phone = phoneip.Text;
                String userName = Membership.GetUser()?.UserName;
                

                // Check if user is logged in
                if (string.IsNullOrEmpty(userName))
                {
                    throw new Exception("User session expired. Please log in again to make a reservation.");
                }

                // Check if date is valid
                if (string.IsNullOrEmpty(date))
                {
                    throw new Exception("Please select a reservation date.");
                }

                // Check if time is valid
                if (string.IsNullOrEmpty(time))
                {
                    throw new Exception("Please select a reservation time.");
                }

                // Check if pax is selected
                if (string.IsNullOrEmpty(paxdb))
                {
                    throw new Exception("Please select the number of guests for the reservation.");
                }

               

                string getCustIDsql = @"SELECT customer_id FROM Customers WHERE name = @username";

                string sql = @"INSERT INTO Reservations(reserve_date, customer_id, reserve_time, pax, phone) VALUES(@date,@cust_id, @time, @pax, @phone); SELECT SCOPE_IDENTITY();";

                string strCon = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
                using (SqlConnection con = new SqlConnection(strCon))
                {

                    using (SqlCommand cmd = new SqlCommand(getCustIDsql, con))
                    {
                        cmd.Parameters.AddWithValue("@username", userName);

                        con.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            customerId = Convert.ToInt32(result);
                        }
                        else
                        {
                            // Handle the case where the user ID is not found
                            throw new Exception("User ID not found. Please make sure you are logged in.");
                        }
                        con.Close();

                        customerId = Convert.ToInt32(result);

                    }

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.Parameters.AddWithValue("@cust_id", customerId);
                        cmd.Parameters.AddWithValue("@time", time);
                        cmd.Parameters.AddWithValue("@pax", paxdb);
                        cmd.Parameters.AddWithValue("@phone", phone);


                        con.Open();
                        int reservationId = Convert.ToInt32(cmd.ExecuteScalar());
                        string encryptedUserName = Encrypt(userName);
                        string encryptedEmail = Encrypt(emailip.Text);
                        string encryptedReservationId = Encrypt(reservationId.ToString());
                        string encryptedPhone = Encrypt(phone);

                        con.Close();

                        Response.Redirect("bookingSummary.aspx?reservationId=" + HttpUtility.UrlEncode(encryptedReservationId) + "&name=" + HttpUtility.UrlEncode(encryptedUserName) + "&email=" + HttpUtility.UrlEncode(encryptedEmail) + "&phone=" + HttpUtility.UrlEncode(encryptedPhone));
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL-related exceptions
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Database error: " + ex.Message + ". Please try again later or contact support.')", true);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('An error occurred: " + ex.Message + "')", true);
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