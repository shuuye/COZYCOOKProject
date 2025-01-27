using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace testing.Pages.Admin
{
    public partial class Admin_KitchenPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LVOrder_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem item = (ListViewDataItem)e.Item;
                DataRowView rowView = (DataRowView)item.DataItem;
                int orderId = Convert.ToInt32(rowView["order_id"]);

                // Find the SqlDataSource control within the ListView item
                SqlDataSource dsOrderMenu = (SqlDataSource)e.Item.FindControl("dsOrderMenu");

                // Set the parameter value programmatically
                dsOrderMenu.SelectParameters["order_id"].DefaultValue = orderId.ToString();
            }
        }

        protected void LVOrder_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        protected void finishBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var button = (Button)sender;
                button.Text = "✓";

                Button finishBtn = (Button)sender;
                ListViewItem item = (ListViewItem)finishBtn.NamingContainer;
                ListViewDataItem lvItem = (ListViewDataItem)item;
                int index = lvItem.DisplayIndex;

                // Retrieve the order_id from the ListViewItem's data item
                int orderId = Convert.ToInt32(LVOrder.DataKeys[index].Value);

                // Update the order status in the database
                UpdateOrderStatus(orderId, "Finished");
                LVOrder.DataBind();
            }
            catch (FormatException ex)
            {
                // Handle format exception (e.g., if order ID is not a valid integer)
                string errorMessage = "Invalid order ID format exist in the database.";
                ShowAlert(errorMessage);
            }
            catch (InvalidOperationException ex)
            {
                // Handle invalid operation exceptions
                string errorMessage = "An error occurred while processing the request. Please try again.";
                ShowAlert(errorMessage);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                string errorMessage = $"An unexpected error occurred: {ex.Message}. Please contact support for assistance.";
                ShowAlert(errorMessage);
            }
        }

        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Button cancelBtn = (Button)sender;
                ListViewItem item = (ListViewItem)cancelBtn.NamingContainer;
                ListViewDataItem lvItem = (ListViewDataItem)item;
                int index = lvItem.DisplayIndex;

                // Retrieve the order_id from the ListViewItem's data item
                int orderId = Convert.ToInt32(LVOrder.DataKeys[index].Value);

                // Update the order status in the database
                UpdateOrderStatus(orderId, "Cancelled");
            }
            catch (FormatException ex)
            {
                // Handle format exception (e.g., if order ID is not a valid integer)
                string errorMessage = "Invalid order ID format exist in the database.";
                ShowAlert(errorMessage);
            }
            catch (InvalidOperationException ex)
            {
                // Handle invalid operation exceptions
                string errorMessage = "An error occurred while processing the request. Please try again.";
                ShowAlert(errorMessage);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                string errorMessage = $"An unexpected error occurred: {ex.Message}. Please contact support for assistance.";
                ShowAlert(errorMessage);
            }
        }

        private void UpdateOrderStatus(int orderId, string status)
        {
            try
            {
                string strCon = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    string updateQuery = "UPDATE Orders SET order_status = @status WHERE order_id = @orderId";
                    using (SqlCommand command = new SqlCommand(updateQuery, con))
                    {
                        command.Parameters.AddWithValue("@status", status);
                        command.Parameters.AddWithValue("@orderId", orderId);
                        con.Open();
                        command.ExecuteNonQuery();
                    }
                }
                LVOrder.DataBind();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                string errorMessage = $"A database error occurred: {ex.Message}. Please try again later.";
                ShowAlert(errorMessage);
            }
            catch (InvalidOperationException ex)
            {
                // Handle invalid operation exceptions
                string errorMessage = "An error occurred while processing the request. Please try again.";
                ShowAlert(errorMessage);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                string errorMessage = $"An unexpected error occurred: {ex.Message} . Please contact support for assistance.";
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