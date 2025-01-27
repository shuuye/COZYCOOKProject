using COZYCOOK.Pages.User;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace testing.Pages.Admin
{
    public partial class Admin_MainPage : System.Web.UI.Page
    {
        public static string salesDataJson;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                // Retrieve sales data
                SalesDataRetriever dataRetriever = new SalesDataRetriever();
                var salesData = dataRetriever.GetWeeklySalesData();
                

                // Convert sales data to JavaScript array
                var serializer = new JavaScriptSerializer();
                salesDataJson = serializer.Serialize(salesData);
                


                // Assuming connection string is stored in a web.config file
                string connectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;

                // SQL query to retrieve total sales
                string query = "SELECT SUM(total_amount) AS total_sales FROM dbo.Orders";

                // Create connection and command objects
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            decimal totalSales = Convert.ToDecimal(result);
                            lblTotalSales.Text = totalSales.ToString("0.00");
                        }
                        else
                        {
                            // Handle case where there are no sales
                            lblTotalSales.Text = "0.00";
                        }
                    }
                }


                string CountQuery = "SELECT COUNT(*) AS new_orders_count FROM dbo.Orders WHERE order_status = 'New'";

                // Create connection and command objects
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(CountQuery, connection))
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            int newOrdersCount = Convert.ToInt32(result);
                            lblNewOrders.Text = newOrdersCount.ToString();
                        }
                        else
                        {
                            // Handle case where there are no new orders
                            lblNewOrders.Text = "0";
                        }
                    }
                }

                // SQL query to retrieve total number of new reservations
                string reservavtionQuery = "SELECT COUNT(*) AS new_reservations_count FROM dbo.Reservations WHERE table_id IS NULL";

                // Create connection and command objects
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(reservavtionQuery, connection))
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            int newReservationsCount = Convert.ToInt32(result);
                            lblNewReservavtion.Text = newReservationsCount.ToString();
                        }
                        else
                        {
                            // Handle case where there are no new reservations
                            lblNewReservavtion.Text = "0";
                        }
                    }
                }



            }
        }

    }
}