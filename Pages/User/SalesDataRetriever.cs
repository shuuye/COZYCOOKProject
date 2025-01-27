using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Configuration;

public class SalesDataRetriever
{
    public List<double> GetWeeklySalesData()
    {

        string connectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
        List<double> salesData = new List<double>();

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Query to retrieve total sales for each day of the week
                string query = @"
                    WITH Weekdays AS (
                    SELECT 1 AS weekday_number, 'Sunday' AS weekday_name
                    UNION ALL SELECT 2, 'Monday'
                    UNION ALL SELECT 3, 'Tuesday'
                    UNION ALL SELECT 4, 'Wednesday'
                    UNION ALL SELECT 5, 'Thursday'
                    UNION ALL SELECT 6, 'Friday'
                    UNION ALL SELECT 7, 'Saturday'
                ) 
                SELECT COALESCE(SUM(o.total_amount), 0) AS total_sales, w.weekday_number, 
                       w.weekday_name, 
                       CASE 
                           WHEN COUNT(o.order_date) = 0 THEN 
                               DATEADD(DAY, w.weekday_number - DATEPART(WEEKDAY, GETDATE()) - 7, CAST(GETDATE() AS DATE))
                           ELSE
                               DATEADD(DAY, DATEDIFF(DAY, 0, o.order_date), 0)
                       END AS order_date
                FROM Weekdays w
                LEFT JOIN dbo.Orders o ON DATEPART(weekday, o.order_date) = w.weekday_number 
                AND o.order_date >= DATEADD(DAY, -6 - DATEPART(WEEKDAY, GETDATE()), CAST(GETDATE() AS DATE))
                AND o.order_date < DATEADD(DAY, 1 - DATEPART(WEEKDAY, GETDATE()), CAST(GETDATE() AS DATE))
                GROUP BY w.weekday_number, w.weekday_name,o.order_date
                ORDER BY w.weekday_number;
                ";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Initialize sales data for all weekdays as 0
                    salesData = new List<double> { 0, 0, 0, 0, 0, 0, 0 };
                    List<string> dates = new List<string>();

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int weekday = Convert.ToInt32(reader["weekday_number"]) - 1; // Adjust for 0-based index
                            double totalSales = Convert.ToDouble(reader["total_sales"]);
                            salesData[weekday] = totalSales; // Update sales data for the corresponding weekday

                            string orderDate = Convert.ToDateTime(reader["order_date"]).ToString("yyyy-MM-dd");
                            dates.Add(orderDate); // Add formatted date to the list
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions here
            Console.WriteLine("Error retrieving sales data: " + ex.Message);
        }

        return salesData;
    }
}
