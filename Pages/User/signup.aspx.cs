using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COZYCOOK.Pages.User
{
    public partial class signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
        {
            try
            {
                MembershipUser user = Membership.GetUser(CreateUserWizard1.UserName);

                string name = user.UserName;
                string email = user.Email;
                DateTime date = DateTime.Now;

                string sql = @"INSERT INTO Customers(email, name, registration_date) VALUES(@email, @name, @date); ";

                string strCon = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@date", date);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        int customerId = GetLatestCustomerId();
                        Session["custID"] = customerId;
                        con.Close();

                        Response.Redirect("/index.aspx");
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

        private void ShowAlert(string message)
        {
            // Display an alert message to the user using JavaScript
            string script = $"<script>alert('{message}');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", script);
        }

        private int GetLatestCustomerId()
        {
            int latestCustomerId = 0;

            string connectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            string query = "SELECT TOP 1 customer_id FROM Customers ORDER BY customer_id DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        latestCustomerId = Convert.ToInt32(result);
                    }
                    connection.Close();
                }
            }

            return latestCustomerId;
        }
    }
}
    
