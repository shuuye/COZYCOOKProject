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
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Membership.DeleteUser("jim", true);
            //Membership.DeleteUser("shuye1121", true);
            //Roles.CreateRole("admin");
            //Roles.AddUserToRole("admin", "admin");
        }

        protected void RememberMe_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            string username = Login1.UserName; // Get the username of the logged-in user

            // Query the customer table to retrieve the customer ID based on the username
            int customerId = GetCustomerIdByUsername(username); // Replace GetCustomerIdByUsername with your logic to retrieve the customer ID based on the username

            // Create a session variable to store the customer ID
            Session["custID"] = customerId;

        }

       

        // Method to retrieve the customer ID based on the username
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


    }
}