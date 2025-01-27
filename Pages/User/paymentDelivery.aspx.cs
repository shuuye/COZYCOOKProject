using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace COZYCOOK.Pages.User
{
    public partial class paymentDelivery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindMenu();
                }

            }
            catch (Exception ex)
            {
                string errorMessage = $"An unexpected error occurred while loading the menu: {ex.Message}. Please try again later.";
                ShowAlert(errorMessage);
            }

        }

        protected void BindMenu()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;


            string cartQuery = "SELECT m.menu_id, m.productName, m.productImage, m.productPrice, c.quantity " +
                "FROM Menus m INNER JOIN Carts c ON m.menu_id = c.menu_id " +
                "WHERE c.customer_id = @customerId";


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                //bind Cart
                int customerId = GetCustomerId();



                SqlCommand cartCmd = new SqlCommand(cartQuery, con);
                cartCmd.Parameters.AddWithValue("@customerId", customerId);
                SqlDataAdapter cartDa = new SqlDataAdapter(cartCmd);
                DataTable cartDt = new DataTable();
                cartDa.Fill(cartDt);
                rptCart.DataSource = cartDt;
                rptCart.DataBind();
                UpdateTotal();

            }
        }

        protected void txtquantity_DataBinding(object sender, EventArgs e)
        {
            TextBox txtQuantity = (TextBox)sender;
            RepeaterItem item = (RepeaterItem)txtQuantity.NamingContainer;
            DataRowView row = (DataRowView)item.DataItem;
            int quantity = Convert.ToInt32(row["quantity"]);
            txtQuantity.Text = quantity.ToString();
        }
        protected void rptCart_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RemoveCart")
                {
                    int menuId = Convert.ToInt32(e.CommandArgument);
                    int customerId = GetCustomerId();
                    string sql = @"DELETE FROM Carts WHERE customer_id = @customer_id AND menu_id = @menu_id;";
                    string strCon = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(strCon))
                    {
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("@customer_id", customerId);
                            cmd.Parameters.AddWithValue("@menu_id", menuId);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    BindMenu();
                    UpdateTotal();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = $"An unexpected error occurred while removing the item from cart: {ex.Message}. Please try again later.";
                ShowAlert(errorMessage);
            }
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
                Response.Redirect("/Login.aspx"); // Redirect to login page
                return -1; // Or any default value indicating error
            }
        }
        protected void txtquantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtQuantity = (TextBox)sender;
                RepeaterItem item = (RepeaterItem)txtQuantity.NamingContainer;
                ImageButton removeCartBtn = (ImageButton)item.FindControl("removecart");
                int menuId = Convert.ToInt32(removeCartBtn.CommandArgument);
                int newQuantity = Convert.ToInt32(txtQuantity.Text);
                int customerId = GetCustomerId();
                string sqlQuery = "UPDATE Carts SET quantity = @quantity WHERE customer_id = @customerId AND menu_id = @menuId";
                string strCon = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, con))
                    {
                        sqlCmd.Parameters.AddWithValue("@quantity", newQuantity);
                        sqlCmd.Parameters.AddWithValue("@customerId", customerId);
                        sqlCmd.Parameters.AddWithValue("@menuId", menuId);
                        con.Open();
                        sqlCmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                BindMenu();
            }
            catch (Exception ex)
            {
                string errorMessage = $"An unexpected error occurred while updating the cart item quantity: {ex.Message}. Please try again later.";
                ShowAlert(errorMessage);
            }
        }

        private void UpdateTotal()
        {
            // Retrieve the customer ID from the LoginView control
            int customerId = Convert.ToInt32(Session["custID"]);

            // Query the database to retrieve the updated total value
            string query = "SELECT SUM(productPrice * quantity) AS total FROM Carts JOIN Menus ON Carts.menu_id = Menus.menu_id WHERE customer_id = @customerId";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@customerId", customerId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    if (reader.Read())
                    {
                        float total;
                        string totalString = reader["total"].ToString();
                        String promotionText = lblPromotion.Text;
                        float discountPercentage = 0;
                        float discountAmount = 0;
                        if (!string.IsNullOrEmpty(totalString))
                        {
                            if (float.TryParse(totalString, out total))
                            {

                                if (promotionText.EndsWith("%"))
                                {
                                    string percentageString = promotionText.Substring(0, promotionText.Length - 1);
                                    if (float.TryParse(percentageString, out discountPercentage))
                                    {
                                        
                                        // Calculate the discount amount
                                         discountAmount = (total * discountPercentage);


                                        // Update lblPromotion with the discounted price
                                        lblPromotion.Text = string.Format("{0:0.00}", discountAmount);
                                    }
                                    else
                                    {
                                        // Handle parsing error
                                        Console.WriteLine("Failed to parse discount percentage.");
                                    }
                                }
                                else
                                {
                                    // Handle invalid format
                                    Console.WriteLine("Invalid promotion format.");
                                }

                                // Update the lblTotal control's Text property with the updated total value
                                lblTotal.Text = string.Format("RM {0:0.00}", total);
                                // Calculate 10% of the total
                                float serviceCharge = 5;
                                lblService.Text = string.Format("RM {0:0.00}", serviceCharge);
                                // Calculate the grand total (total + service charge)
                                float grandTotal = total + serviceCharge - discountAmount;

                                // Update lblGrandTotal with the grand total
                                lblGrandTotal.Text = string.Format("RM {0:0.00}", grandTotal);
                            }
                        }
                        else
                        {
                            // If no rows were returned by the query, handle it here
                            lblTotal.Text = "0.00";
                            lblService.Text = "0.00";
                            lblGrandTotal.Text = "0.00";
                            // Add a debugging message to see if this block is executed
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "confirmRedirect", "if(confirm('Your cart is empty now, try adding something to your cart.')) { window.location.href = 'Menu.aspx'; } else { window.location.href = 'Menu.aspx'; }", true);


                        }

                    }
                }

                reader.Close();
            }
        }

        protected void applybtn_Click(object sender, EventArgs e)
        {
            try
            {
                string promotionCode = txtPromoCode.Text;
                string query = @"
                SELECT [discount_percentage], [start_date], [end_date]
                FROM [dbo].[Promotions]
                WHERE [promotion_id] = @PromotionId;";

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PromotionId", promotionCode);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        float discountPercentage = (float)reader["discount_percentage"];
                        DateTime startDate = (DateTime)reader["start_date"];
                        DateTime endDate = (DateTime)reader["end_date"];
                        DateTime currentDate = DateTime.Today;
                        if (currentDate >= startDate && currentDate <= endDate)
                        {
                            lblPercentage.Text = (discountPercentage * 100).ToString() + "%";
                            lblPromotion.Text = discountPercentage.ToString() + "%";
                            applybtn.Enabled = false;
                            UpdateTotal();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "invalidPromoMessage", "if (confirm('This promotion is not valid for the current date. Do you want to continue?')) { alert('Promotion not applied.'); } else { alert('Promotion cancelled.'); };", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "invalidPromoCodeMessage", "alert('Invalid promotion code. Please try again.');", true);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = $"An unexpected error occurred while applying the promotion: {ex.Message}. Please try again later.";
                ShowAlert(errorMessage);
            }
        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            try
            {
                string paymentMethod = ddlPaymentMethod.Text;
                string total = lblGrandTotal.Text;
                string encryptedPaymentMethod = Encrypt(paymentMethod);
                string encryptedTotal = Encrypt(total);
                string queryString = $"?paymentMethod={HttpUtility.UrlEncode(encryptedPaymentMethod)}&total={HttpUtility.UrlEncode(encryptedTotal)}";
                Response.Redirect("~/Pages/User/User_Delivery.aspx" + queryString);
            }
            catch (Exception ex)
            {
                string errorMessage = $"An unexpected error occurred while processing the payment: {ex.Message}. Please try again later.";
                ShowAlert(errorMessage);
            }
        }

        private void ShowAlert(string message)
        {
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


    }
}