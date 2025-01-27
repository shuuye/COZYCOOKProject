using COZYCOOK.Pages.User;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace testing
{
    public partial class _index : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string username = User.Identity.Name; // Get the username of the logged-in user

                if(!string.IsNullOrEmpty(username) )
                {
                    // Query the customer table to retrieve the customer ID based on the username
                    int customerId = GetCustomerIdByUsername(username); // Replace GetCustomerIdByUsername with your logic to retrieve the customer ID based on the username

                    if (customerId != 0)
                    {
                        // Create a session variable to store the customer ID
                        Session["custID"] = customerId;
                    }
                    else
                    {
                        string errorMessage = "Invalid username. Please enter a valid username.";
                        ShowAlert(errorMessage);
                    }
                }

               
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                string errorMessage = $"Database error occurred: {ex.Message}. Please try again later.";
                ShowAlert(errorMessage);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                string errorMessage = $"An unexpected error occurred: {ex.Message}. Please contact support for assistance.";
                ShowAlert(errorMessage);
            }

        }

        private int GetCustomerIdByUsername(string username)
        {
            int customerId = 0; // Default value

            // SQL query to retrieve the customer ID based on the username
            string sql = "SELECT customer_id FROM Customers WHERE name = @username";

            string strCon = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            using (SqlConnection con = new SqlConnection(strCon))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);

                    con.Open();
                    object result = cmd.ExecuteScalar();
                    con.Close();

                    if (result != null)
                    {
                        customerId = Convert.ToInt32(result);
                    }
                }
            }

            return customerId;
        }

        private void ShowAlert(string message)
        {
            // Display an alert message to the user using JavaScript
            string script = $"<script>alert('{message}');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", script);
        }

    }
}