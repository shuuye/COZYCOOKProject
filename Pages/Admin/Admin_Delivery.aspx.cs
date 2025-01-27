using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COZYCOOK.Pages.Admin
{
    public partial class Admin_Delivery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void Status_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList statusDropDown = (DropDownList)sender;

            GridViewRow row = (GridViewRow)statusDropDown.NamingContainer;

            int deliveryId = Int32.Parse((statusDropDown.NamingContainer as GridViewRow).Cells[0].Text);

            string sql = @"UPDATE Deliverys SET status = @Status WHERE delivery_id = @DeliveryID";

            string strCon = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            using (SqlConnection con = new SqlConnection(strCon))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Status", statusDropDown.SelectedValue);
                    cmd.Parameters.AddWithValue("@DeliveryID", deliveryId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }

            DeliveryList.DataBind();

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find the Literal control in the current row
                Literal litMenuItems = (Literal)e.Row.FindControl("litMenuItems");

                // Check if the control is found
                if (litMenuItems != null)
                {
                    // Get the menu items string from the DataSource
                    string menuItems = DataBinder.Eval(e.Row.DataItem, "MenuItems") as string;

                    // Set the menu items string to the Literal control
                    litMenuItems.Text = menuItems;
                }
            }
        }


    }
}