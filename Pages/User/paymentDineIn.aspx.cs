using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Policy;

namespace COZYCOOK.Pages.User
{
    public partial class paymentDineIn : System.Web.UI.Page
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
            try
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
            catch (Exception ex)
            {
                ShowAlert("Error occurred while fetching cart items: " + ex.Message);
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

                    // Get the customer_id from the session
                    int customerId = GetCustomerId();

                    // Delete the record from the Carts table
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

                    // Rebind the data to the Repeater control
                    BindMenu();
                    UpdateTotal();
                }
            }
            catch (Exception ex)
            {
                ShowAlert("Error occurred while removing item from cart: " + ex.Message);
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
                // Cast the sender object to a TextBox
                TextBox txtQuantity = (TextBox)sender;

                RepeaterItem item = (RepeaterItem)txtQuantity.NamingContainer;

                ImageButton removeCartBtn = (ImageButton)item.FindControl("removecart");

                // Get the menu_id and new quantity value
                int menuId = Convert.ToInt32(removeCartBtn.CommandArgument);
                int newQuantity = Convert.ToInt32(txtQuantity.Text);

                // Get the customerId from the session
                int customerId = GetCustomerId();

                // Create a SQL query to update the quantity in the Carts table
                string sqlQuery = "UPDATE Carts SET quantity = @quantity WHERE customer_id = @customerId AND menu_id = @menuId";


                string strCon = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, con))
                    {
                        // Add parameters to the SQL command object
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
            try
            {
                // Retrieve the customer ID from the LoginView control
                int customerId = GetCustomerId();

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
                            if (!string.IsNullOrEmpty(totalString))
                            {
                                if (float.TryParse(totalString, out total))
                                {
                                    // Update the lblTotal control's Text property with the updated total value
                                    lblTotal.Text = string.Format("RM {0:0.00}", total);
                                    // Calculate 10% of the total
                                    float serviceCharge = total * 0.1f;
                                    lblService.Text = string.Format("RM {0:0.00}", serviceCharge);
                                    // Calculate the grand total (total + service charge)
                                    float grandTotal = total + serviceCharge;

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
            catch (Exception ex)
            {
                // Handle exceptions and display error message
                ShowAlert("An error occurred while updating the total: " + ex.Message);
            }
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            int customerId = GetCustomerId();

            if (customerId != -1) // Ensure valid customer ID
            {
                string connectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;

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
                     INSERT INTO [dbo].[Orders] ([order_date], [order_time], [order_status])
                        VALUES (
                            FORMAT(GETDATE(), 'dd/MM/yyyy'), -- Using FORMAT function to get date in 'dd/MM/yyyy' format
                            FORMAT(GETDATE(), 'HH:mm:ss'),   -- Using FORMAT function to get time in 'HH:mm:ss' format,
                            'NEW'
                        );";

                        using (SqlCommand cmd = new SqlCommand(addNewOrderSql, con))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        // Retrieve the newly inserted order ID
                        int orderId;
                        string getOrderIdSql = "SELECT SCOPE_IDENTITY();"; // Retrieve the last identity value inserted into an identity column
                        using (SqlCommand cmd = new SqlCommand(getOrderIdSql, con))
                        {
                            orderId = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Update order_id in Order_Details table
                        string updateOrderDetailsSql = "UPDATE [dbo].[Order_Details] SET [order_id] = @OrderId WHERE [order_id] IS NULL;";
                        using (SqlCommand cmd = new SqlCommand(updateOrderDetailsSql, con))
                        {
                            cmd.Parameters.AddWithValue("@OrderId", orderId);
                            cmd.ExecuteNonQuery();
                        }

                        // Calculate total amount from order details
                        double totalAmount = CalculateTotalAmount(con, orderId);

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
                        String paymentMethod = ddlPaymentMethod.Text;
                        // Assuming lblGrandTotal.Text contains "RM 55.00"
                        string grandTotalText = lblGrandTotal.Text;

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


                        // Your order processing logic is complete
                        // You may add further actions here, such as sending confirmation emails, etc.
                    }

                    // Redirect to a success page or display a success message
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "confirmRedirect", "if(confirm('Your order has been placed successfully, Enjoy your meal! Redirecting to the home page.')) { window.location.href = 'thankyou.aspx'; } else { window.location.href = 'thankyou.aspx'; }", true);
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


        private double CalculateTotalAmount(SqlConnection connection, int orderId)
        {
            double totalAmount = 0.0;

            // SQL statement to calculate total amount
            string calculateTotalAmountSql = "SELECT SUM(subtotal) FROM [dbo].[Order_Details] WHERE [order_id] = @OrderId;";

            // Create a new SQL command
            using (SqlCommand command = new SqlCommand(calculateTotalAmountSql, connection))
            {
                // Add parameter to the command
                command.Parameters.AddWithValue("@OrderId", orderId);

                // Execute the command and get the total amount
                object result = command.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    totalAmount = Convert.ToDouble(result);
                }
            }

            return totalAmount;
        }
        private void ShowAlert(string message)
        {
            string script = $"<script>alert('{message}');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", script);
        }
    }


}
