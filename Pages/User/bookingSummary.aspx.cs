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

namespace testing.Pages.User
{
    public partial class bookingSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Request.QueryString["reservationId"] != null)
                    {
                        
                        string encryptedReservationId = Request.QueryString["reservationId"];
                        string reservationId = Decrypt(HttpUtility.UrlDecode(encryptedReservationId));

                        string sql = @"SELECT * FROM Reservations WHERE reservation_id = @reservationId";

                        string strCon = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
                        SqlConnection con = new SqlConnection(strCon);
                        SqlCommand cmd = new SqlCommand(sql, con);

                        cmd.Parameters.AddWithValue("@reservationId", reservationId);

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                
                                string encryptedUserName = Request.QueryString["name"];
                                string encryptedEmail = Request.QueryString["email"];
                                string encryptedPhone = Request.QueryString["phone"];

                                
                                string userName = Decrypt(HttpUtility.UrlDecode(encryptedUserName));
                                string email = Decrypt(HttpUtility.UrlDecode(encryptedEmail));
                                string phone = Decrypt(HttpUtility.UrlDecode(encryptedPhone));

                                bookingID.Text = reservationId;
                                bookingDate.Text = bookingDate.Text = ((DateTime)reader["reserve_date"]).ToString("d MMMM yyyy");
                                bookingTime.Text = reader["reserve_time"].ToString();
                                bookingPax.Text = reader["pax"].ToString();
                                bookingName.Text = userName;
                                bookingEmail.Text = email;
                                bookingPhone.Text = phone;
                            }
                        }
                        reader.Close();

                        con.Close();
                    }
                    else
                    {
                        bookingID.Text += "No Booking found";
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                string errorMessage = "An unexpected error occurred while retrieving booking details.";
                ShowAlert(errorMessage);
            }
        }
        private void ShowAlert(string message)
        {
            // Display an alert message to the user using JavaScript
            string script = $"<script>alert('{message}');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", script);
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




