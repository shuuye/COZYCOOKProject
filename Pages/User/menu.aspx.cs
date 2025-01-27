using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.ComponentModel;



namespace COZYCOOK.Pages.User
{
    public partial class Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindMenu();
                }
                catch (Exception ex)
                {
                    string errorMessage = $"An unexpected error occurred while loading the menu: {ex.Message}. Please try again later.";
                    ShowAlert(errorMessage);
                }
            }
        }

        protected void BindMenu()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;

            // Query to retrieve food products
            string foodQuery = "SELECT * FROM Menus WHERE productCategory = 'Dishes' AND productStatus = 1 ORDER BY productName";

            // Query to retrieve beverage products
            string beverageQuery = "SELECT * FROM Menus WHERE productCategory = 'Beverage' AND productStatus = 1  ORDER BY productName";

            string cartQuery = "SELECT m.menu_id, m.productName, m.productImage, m.productPrice, c.quantity " +
                "FROM Menus m INNER JOIN Carts c ON m.menu_id = c.menu_id " +
                "WHERE c.customer_id = @customerId";


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                //bind Cart
                int customerId = Convert.ToInt32(Session["custID"]);
                SqlCommand cartCmd = new SqlCommand(cartQuery, con);
                cartCmd.Parameters.AddWithValue("@customerId", customerId);
                SqlDataAdapter cartDa = new SqlDataAdapter(cartCmd);
                DataTable cartDt = new DataTable();
                cartDa.Fill(cartDt);
                rptCart.DataSource = cartDt;
                rptCart.DataBind();
                UpdateTotal();

                // Bind food products
                SqlDataAdapter foodDa = new SqlDataAdapter(foodQuery, con);
                DataTable foodDt = new DataTable();
                foodDa.Fill(foodDt);
                rptFood.DataSource = foodDt;
                rptFood.DataBind();

                // Bind beverage products
                SqlDataAdapter beverageDa = new SqlDataAdapter(beverageQuery, con);
                DataTable beverageDt = new DataTable();
                beverageDa.Fill(beverageDt);
                rptBeverage.DataSource = beverageDt;
                rptBeverage.DataBind();
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


        protected void rptFood_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    int menuId = Convert.ToInt32(e.CommandArgument);

                    // Get the customer_id from the session
                    int customerId = Convert.ToInt32(Session["custID"]);
                    bool exists = CheckIfRecordExists(customerId, menuId);

                    if (!exists)
                    {
                        // Insert the record into the Carts table
                        string sql = @"INSERT INTO Carts(customer_id, menu_id, quantity) VALUES(@customer_id, @menu_id, @quantity);";

                        string strCon = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(strCon))
                        {
                            using (SqlCommand cmd = new SqlCommand(sql, con))
                            {
                                cmd.Parameters.AddWithValue("@customer_id", customerId);
                                cmd.Parameters.AddWithValue("@menu_id", menuId);
                                cmd.Parameters.AddWithValue("@quantity", 1);

                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The item has been added to your cart.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The item is already in your cart.');", true);
                    }
                }
                BindMenu();
            }
            catch (Exception ex)
            {
                string errorMessage = $"An unexpected error occurred while adding the item to cart: {ex.Message}. Please try again later.";
                ShowAlert(errorMessage);
            }
        }

        protected void rptBeverage_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    int menuId = Convert.ToInt32(e.CommandArgument);

                    // Get the customer_id from the session
                    int customerId = Convert.ToInt32(Session["custID"]);
                    bool exists = CheckIfRecordExists(customerId, menuId);

                    if (!exists)
                    {
                        // Insert the record into the Carts table
                        string sql = @"INSERT INTO Carts(customer_id, menu_id, quantity) VALUES(@customer_id, @menu_id, @quantity);";

                        string strCon = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(strCon))
                        {
                            using (SqlCommand cmd = new SqlCommand(sql, con))
                            {
                                cmd.Parameters.AddWithValue("@customer_id", customerId);
                                cmd.Parameters.AddWithValue("@menu_id", menuId);
                                cmd.Parameters.AddWithValue("@quantity", 1);

                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The item has been added to your cart.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The item is already in your cart.');", true);
                    }

                }
                BindMenu();
            }
            catch (Exception ex)
            {
                string errorMessage = $"An unexpected error occurred while adding the item to cart: {ex.Message}. Please try again later.";
                ShowAlert(errorMessage);
            }
        }

        private bool CheckIfRecordExists(int customerId, int menuId)
        {
            string sql = "SELECT COUNT(*) FROM Carts WHERE customer_id = @customer_id AND menu_id = @menu_id";

            string strCon = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            using (SqlConnection con = new SqlConnection(strCon))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@customer_id", customerId);
                    cmd.Parameters.AddWithValue("@menu_id", menuId);

                    con.Open();
                    int count = (int)cmd.ExecuteScalar();
                    con.Close();

                    return count > 0;
                }
            }
        }

        protected void rptCart_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RemoveCart")
                {
                    int menuId = Convert.ToInt32(e.CommandArgument);

                    // Get the customer_id from the session
                    int customerId = Convert.ToInt32(Session["custID"]);

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
                }
            }
            catch (Exception ex)
            {
                string errorMessage = $"An unexpected error occurred while removing the item from cart: {ex.Message}. Please try again later.";
                ShowAlert(errorMessage);
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
                int customerId = Convert.ToInt32(Session["custID"]);

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
                if (reader.Read())
                {
                    // Update the lblTotal control's Text property with the updated total value
                    lblTotal.Text = string.Format("{0:0.00}", reader["total"]);

                }
                else

                    reader.Close();
            }
        }

        protected void btnBuyNow_Click(object sender, EventArgs e)
        {

            try
            {
                string totalString = lblTotal.Text;
                if (string.IsNullOrEmpty(totalString))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "confirmRedirect", "if(confirm('Your cart is empty now, try adding something to your cart.')) { window.location.href = 'Menu.aspx'; } else { window.location.href = 'Menu.aspx'; }", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "confirmRedirect", "if(confirm('Are you sure to place your order now ?')) {alert('Processing...'); window.location.href = '/Pages/User/selectDineOpt.aspx'; } else {alert('Cancel to place order.')  }", true);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = $"An unexpected error occurred while processing your order: {ex.Message}. Please try again later.";
                ShowAlert(errorMessage);
            }
        }

        private void ShowAlert(string message)
        {
            // Display an alert message to the user using JavaScript
            string script = $"<script>alert('{message}');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", script);
        }
    }

}
