using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace testing.Pages.User
{
    public partial class User_Dstatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Call a method to get the latest delivery details
                 GetLatestDeliveryDetails();
                
            }

        }

        protected void GetLatestDeliveryDetails()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            string query = "SELECT TOP 1 delivery_id, status FROM Deliverys ORDER BY delivery_id DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Get the delivery ID and status from the database
                        int deliveryId = Convert.ToInt32(reader["delivery_id"]);
                        string status = Convert.ToString(reader["status"]);

                        // Set the values to the label controls
                        lblDeliveryId.Text = deliveryId.ToString();
                        lblStatus.Text = status;
                    }
                    else
                    {
                        // Handle case where no delivery record is found
                        lblDeliveryId.Text = "N/A";
                        lblStatus.Text = "N/A";
                    }

                    reader.Close();
                }
            }
        }

        protected void btnMakeNewOrder_Click(object sender, EventArgs e)
        {
            //Response.Redirect("product.html");
        }

        protected void btnReceiveOrder_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Thanks for Choosing Us, Enjoy Meal !');", true);
        }

    }
}