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
    public partial class User_Delivery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private int GetCustomerId()
        {
            if (Session["custID"] != null && int.TryParse(Session["custID"].ToString(), out int customerId))
            {
                return customerId;
            }
            else
            {
                // Handle invalid session or missing customer ID
                Response.Redirect("~/Login.aspx"); // Redirect to login page
                return -1; // Or any default value indicating error
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Phone text box validation
            string phoneNumber = txtPhoneNum.Text.Trim();

            // Validate if the phone number contains only numbers
            if (!System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^[0-9]+$"))
            {
                // Display error message for alphabetic characters
                ErrorMessageLabel.Text = "*No alphabets allowed in the phone number!";
                return;
            }

            // Validate if the phone number is within the acceptable range
            // You can define your specific range here, e.g., minimum and maximum length
            if (phoneNumber.Length < 7 || phoneNumber.Length > 11)
            {
                // Display error message for out-of-range numbers
                ErrorMessageLabel.Text = "*Invalid Phone Number range!";
                return;
            }

            // Retrieve form data
            string firstName = txtName.Text;
            string address = txtAddress.Text;
            String phoneNum = txtPhoneNum.Text;
            string status = "Preparing";
            string state = ddlCity.Text;
            int postcode = Convert.ToInt32(ddlPostcode.SelectedValue);
            DateTime deliveryDate = Convert.ToDateTime(txtDateTime.Text);
            TimeSpan deliveryTime = deliveryDate.TimeOfDay;

            // Insert into database
            string connectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Deliverys (first_name,address,phone, status, state, postcode, delivery_date, delivery_time) " +
                               "VALUES (@FirstName,@Address, @PhoneNum,@Status, @State, @Postcode, @DeliveryDate, @DeliveryTime)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@PhoneNum", phoneNum);
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@State", state);
                    command.Parameters.AddWithValue("@Postcode", postcode);
                    command.Parameters.AddWithValue("@DeliveryDate", deliveryDate);
                    command.Parameters.AddWithValue("@DeliveryTime", deliveryTime);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            // Retrieve the newly inserted delivery ID directly
            int deliveryId = GetLatestDeliveryId();



            int customerId = GetCustomerId();

            if (customerId != -1) // Ensure valid customer ID
            {

                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        // Add order details to Order_Details table
                        string addOrderDetailsql = @"
                    INSERT INTO [dbo].[Order_Details] ( [menu_id], [quantity], [subtotal])
                    SELECT 
                        C.[menu_id],
                        C.[quantity],
                        C.[quantity] * M.[productPrice] AS subtotal
                    FROM 
                        [dbo].[Carts] C
                    JOIN 
                        [dbo].[Menus] M ON C.[menu_id] = M.[menu_id]
                    WHERE 
                        C.[customer_id] = @customer_id;";

                        using (SqlCommand cmd = new SqlCommand(addOrderDetailsql, con))
                        {
                            cmd.Parameters.AddWithValue("@customer_id", customerId);
                            cmd.ExecuteNonQuery();
                        }

                        // Add new order to Orders table
                        string addNewOrderSql = @"
                             INSERT INTO [dbo].[Orders] ([order_date], [order_time],[delivery_id], [order_status])
                                VALUES (
                                    FORMAT(GETDATE(), 'dd/MM/yyyy'), -- Using FORMAT function to get date in 'dd/MM/yyyy' format
                                    FORMAT(GETDATE(), 'HH:mm:ss'),   -- Using FORMAT function to get time in 'HH:mm:ss' format,
                                    @delivery_id,
                                    'NEW'
                                );";

                        using (SqlCommand cmd = new SqlCommand(addNewOrderSql, con))
                        {
                            cmd.Parameters.AddWithValue("@delivery_id", deliveryId);
                            cmd.ExecuteNonQuery();
                        }

                        // Retrieve the newly inserted order ID
                        int orderId = GetLatestOrderId();

                        // Update order_id in Order_Details table
                        string updateOrderDetailsSql = "UPDATE [dbo].[Order_Details] SET [order_id] = @OrderId WHERE [order_id] IS NULL;";
                        using (SqlCommand cmd = new SqlCommand(updateOrderDetailsSql, con))
                        {
                            cmd.Parameters.AddWithValue("@OrderId", orderId);
                            cmd.ExecuteNonQuery();
                        }

                        double totalAmount = 0;
                        
                        string encryptedTotal = Request.QueryString["total"];
                        
                        string total = Decrypt(HttpUtility.UrlDecode(encryptedTotal));

                        // Check if the total amount is present in the query string
                        if (!string.IsNullOrEmpty(total))
                        {
                            // Retrieve the total amount from the query string
                            string totalString = total;

                            if (totalString.StartsWith("RM"))
                            {
                                totalString = totalString.Substring(2); // Remove the first two characters ("RM")
                            }

                            // Attempt to parse the total amount as a double
                            if (double.TryParse(totalString, out totalAmount))
                            {

                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "confirmRedirect", "if(confirm('Total Amount value expired, please place the order again.')) { window.location.href = 'paymentDelivery.aspx'; } else { window.location.href = 'Menu.aspx'; }", true);
                            }
                        }
                        else
                        {

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "confirmRedirect", "if(confirm('Total Amount value expired, please place the order again.')) { window.location.href = 'paymentDelivery.aspx'; } else { window.location.href = 'Menu.aspx'; }", true);
                        }

                        // Update total_amount in Orders table
                        string updateTotalAmountSql = "UPDATE [dbo].[Orders] SET [total_amount] = @TotalAmount WHERE [order_id] = @OrderId;";
                        using (SqlCommand cmd = new SqlCommand(updateTotalAmountSql, con))
                        {
                            cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                            cmd.Parameters.AddWithValue("@OrderId", orderId);
                            cmd.ExecuteNonQuery();
                        }
                        string deleteCartRecordsSql = "DELETE FROM [dbo].[Carts] WHERE [customer_id] = @customer_id;";
                        using (SqlCommand cmd = new SqlCommand(deleteCartRecordsSql, con))
                        {

                            try
                            {
                                cmd.Parameters.AddWithValue("@customer_id", customerId);
                                cmd.ExecuteNonQuery();

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error deleting cart records: " + ex.Message);
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "confirmMessage", "if (confirm( Error deleting cart records:  '" + ex.Message + "')) { alert('Start confirmed.'); } else { alert('Start cancelled.'); }", true);
                            }
                        }

                        string updatePaymentSql = @" INSERT INTO [dbo].[Payments] ([order_id], [payment_date], [payment_time], [payment_method], [amount_paid])" +
                            "    VALUES (@OrderId," +
                            " FORMAT(GETDATE(), 'dd/MM/yyyy')," +
                            "FORMAT(GETDATE(), 'HH:mm:ss'),   @PaymentMethod, @AmountPaid);";

                        float roundedGrandTotal = 0.0f;
                        
                        string encryptedPaymentMethod = Request.QueryString["paymentMethod"];// Decrypt parameters
                        string paymentMethod = Decrypt(HttpUtility.UrlDecode(encryptedPaymentMethod));
                        if (!string.IsNullOrEmpty(paymentMethod))
                        {
                            // Retrieve the payment method from the query string
                            paymentMethod = Decrypt(HttpUtility.UrlDecode(encryptedPaymentMethod));
                            string grandTotalText = totalAmount.ToString();

                            // Remove the currency symbol and any whitespace
                            string numericPart = grandTotalText.Replace("RM", "").Trim();

                            // Parse the remaining string as a floating-point number
                            if (float.TryParse(numericPart, out float grandTotalFloat))
                            {
                                roundedGrandTotal = (float)Math.Round(grandTotalFloat, 2);
                            }
                            else
                            {
                                Console.WriteLine("Failed to parse grand total.");
                            }

                            using (SqlCommand cmd = new SqlCommand(updatePaymentSql, con))
                            {

                                try
                                {
                                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                                    cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                                    cmd.Parameters.AddWithValue("@AmountPaid", roundedGrandTotal);
                                    cmd.ExecuteNonQuery();

                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Error adding payment record: " + ex.Message);
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "confirmMessage", "if (confirm( Error adding payment record:  '" + ex.Message + "')) { alert('Start confirmed.'); } else { alert('Start cancelled.'); }", true);
                                }
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "confirmRedirect", "if(confirm('Payment Method invalid, please place the order again.')) { window.location.href = 'paymentDelivery.aspx'; } else { window.location.href = 'Menu.aspx'; }", true);
                        }

                    }

                    // Redirect to a success page or display a success message
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "confirmRedirect", "if(confirm('Your order has been placed successfully, Enjoy your meal!')) { window.location.href = 'User_Dstatus.aspx'; } else { window.location.href = 'User_Dstatus.aspx'; }", true);
                }
                catch (Exception ex)
                {
                    // Handle exceptions, log errors, or display error messages
                    // For simplicity, we're redirecting to an error page here
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "confirmMessage", "if (confirm('got error'" + ex.Message + ")) { alert('Start confirmed.'); } else { alert('Start cancelled.'); }", true);
                }
            }
            else
            {
                // Handle invalid customer ID, perhaps by redirecting to the login page
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "confirmMessage", "if (confirm('login got problem')) { alert('Start confirmed.'); } else { alert('Start cancelled.'); }", true);
            }
        }
        // Function to retrieve the latest delivery ID from the database
        private int GetLatestDeliveryId()
        {
            int latestDeliveryId = 0;

            string connectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            string query = "SELECT TOP 1 delivery_id FROM Deliverys ORDER BY delivery_id DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        latestDeliveryId = Convert.ToInt32(result);
                    }
                    connection.Close();
                }
            }

            return latestDeliveryId;
        }

        private int GetLatestOrderId()
        {
            int orderid = 0;

            string connectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            string query = "SELECT TOP 1 order_id FROM Orders ORDER BY order_id DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        orderid = Convert.ToInt32(result);
                    }
                    connection.Close();
                }
            }

            return orderid;
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