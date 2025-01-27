using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Diagnostics.Eventing.Reader;

namespace COZYCOOK.Pages.Admin
{
    public partial class Admin_Menu : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\cozyCook.mdf;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void AddProductButton_Click(object sender, EventArgs e)
        {
            if (productImageFileUpload.HasFile)
            {

                // Get the product name
                string productName = productNameTextBox.Text;


                // Check if the product name already exists in the database
                if (IsDuplicateProduct(productName))
                {
                    // Display an error message to the user
                    lblmsg.Text = "Product name already exists. Please enter a different product name.";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    return; // Exit the function without adding the product
                }

                if (productImageFileUpload.PostedFile.ContentType == "image/jpeg")
                {
                    int filesize;
                    filesize = productImageFileUpload.PostedFile.ContentLength;

                    if (filesize <= 51200)
                    {
                        // If the product name is not a duplicate, proceed to insert the new product
                        string str = productImageFileUpload.FileName;
                        productImageFileUpload.PostedFile.SaveAs(Server.MapPath("/Pic_ProductPic/" + productCategoryDropDown.SelectedItem.Text + "/" + str));
                        string imgpath = "/Pic_ProductPic/" + productCategoryDropDown.SelectedItem.Text + "/" + str.ToString();
                        string mainconn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                        using (SqlConnection sqlcon = new SqlConnection(mainconn))
                        {
                            SqlCommand sqlcmd = new SqlCommand("INSERT INTO [dbo].[Menus] (productName, productPrice, productDescription, productCategory, productImage) VALUES (@productName, @productPrice, @productDescription, @productCategory, @productImage)", sqlcon);
                            sqlcmd.Parameters.AddWithValue("@productName", productNameTextBox.Text);
                            sqlcmd.Parameters.AddWithValue("@productPrice", productPriceTextBox.Text);
                            sqlcmd.Parameters.AddWithValue("@productDescription", productDescriptionTextBox.Text);
                            sqlcmd.Parameters.AddWithValue("@productCategory", productCategoryDropDown.SelectedItem.Text.ToString());
                            sqlcmd.Parameters.AddWithValue("@productImage", imgpath);
                            sqlcon.Open();
                            sqlcmd.ExecuteNonQuery();
                            productNameTextBox.Text = "";
                            productPriceTextBox.Text = "";
                            productDescriptionTextBox.Text = "";
                            productCategoryDropDown.SelectedIndex = 0;
                            lblmsg.Text = "Product added successfully!";
                            lblmsg.ForeColor = System.Drawing.Color.Green;


                        }
                    }
                    else
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "File Size exceeds 50KB";
                        lblmsg.ForeColor = System.Drawing.Color.Red;

                    }
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "File other than jpeg not accepted";
                    lblmsg.ForeColor = System.Drawing.Color.Red;

                }
                    
                }
               


            }
                
        

        

        // Function to check if the product name already exists in the database
        private bool IsDuplicateProduct(string productName)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection sqlcon = new SqlConnection(mainconn))
            {
                SqlCommand sqlcmd = new SqlCommand("SELECT COUNT(*) FROM [dbo].[Menus] WHERE productName = @productName", sqlcon);
                sqlcmd.Parameters.AddWithValue("@productName", productName);
                sqlcon.Open();
                int count = (int)sqlcmd.ExecuteScalar();
                return count > 0; // Return true if the product name already exists, false otherwise
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox status = (row.Cells[7].FindControl("CheckBox1") as CheckBox);
                int menu_id = Convert.ToInt32(row.Cells[1].Text);
                if (status.Checked)
                {
                    updaterow(menu_id, "true");

                }
                else
                {
                    updaterow(menu_id, "false");
                }
            }
        }

        private void updaterow(int menu_id, String markstatus)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string updateStatus = "Update Menus set productStatus='" + markstatus + "' where menu_id=" + menu_id;
            SqlConnection con = new SqlConnection(mainconn);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = updateStatus;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            Label1.Text = "Data Has Been Saved Successfully";
        }

        protected void Dishes_CheckedChanged(object sender, EventArgs e)
        {

        }
    }

}


