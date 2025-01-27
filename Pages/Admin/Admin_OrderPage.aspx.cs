using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace testing.Pages.Admin
{
    public partial class Admin_OrderPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }



        protected void Status_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList statusDropDown = (DropDownList)sender;
                int orderId = Int32.Parse((statusDropDown.NamingContainer as GridViewRow).Cells[0].Text);
                string selectedStatus = statusDropDown.SelectedValue;

                string updateQuery = @"UPDATE Orders SET order_status = @Status WHERE order_id = @OrderID";
                string connectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Status", selectedStatus);
                        command.Parameters.AddWithValue("@OrderID", orderId);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            // No rows were updated, which indicates that the order ID does not exist
                            throw new Exception("Order ID not found.");
                        }
                    }
                }

                OrderList.DataBind();
            }
            catch (FormatException)
            {
                // Handle format exception (e.g., if order ID is not a valid integer)
                string errorMessage = "Invalid order ID format exist in database.";
                ShowAlert(errorMessage);
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                string errorMessage = $"A database error occurred: {ex.Message}. Please try again later.";
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
    }

   
}