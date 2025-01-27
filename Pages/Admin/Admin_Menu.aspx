<%@ Page Title="Menu Management" Language="C#" MasterPageFile="~/AdminNavigation.Master" AutoEventWireup="true" CodeBehind="Admin_Menu.aspx.cs" Inherits="COZYCOOK.Pages.Admin.Admin_Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="stylesheets" runat="server">

    <link rel="stylesheet" type="text/css" href="/CSS/Admin_Menu.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        // Function to toggle the floating window
        function toggleFloatingWindow() {
            var floatingWindow = document.getElementById('floatingWindow');
            if (floatingWindow.style.display === 'none' || !floatingWindow.style.display) {
                floatingWindow.style.display = 'block';
                // Store the state in sessionStorage
                sessionStorage.setItem('floatingWindowVisible', 'true');
            } else {
                floatingWindow.style.display = 'none';
                // Update the state in sessionStorage
                sessionStorage.setItem('floatingWindowVisible', 'false');
            }
        }

        // Function to close the floating window
        function closeFloatingWindow() {
            var floatingWindow = document.getElementById('floatingWindow');
            floatingWindow.style.display = 'none';
            // Update the state in sessionStorage
            sessionStorage.setItem('floatingWindowVisible', 'false');
        }

        // Function to check and display the floating window on page load
        window.onload = function () {
            var floatingWindowVisible = sessionStorage.getItem('floatingWindowVisible');
            if (floatingWindowVisible === 'true') {
                var floatingWindow = document.getElementById('floatingWindow');
                floatingWindow.style.display = 'block';
            }
        }




    </script>

    <div class="main-content">
        <div class="container">

            <h1 style="color:black;">Menu Management</h1>


            <div class="ViewMenuListPanel">
                <h2 style="color:black; padding-bottom: 1%;">All Menu</h2>
                <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateEditButton="True" DataKeyNames="menu_id" AutoGenerateColumns="False" CssClass="gridview" BackColor="White" BorderColor="Black" BorderStyle="None" BorderWidth="1px" CellPadding="7" Width="100%">
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <HeaderStyle CssClass="gridview-header" BackColor="#333333" Font-Bold="True" ForeColor="#FFFFFF" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <RowStyle CssClass="gridview-row" BackColor="White" ForeColor="black" />
                    <AlternatingRowStyle CssClass="gridview-alternating-row" BorderColor="#663300" />
                    <SelectedRowStyle CssClass="gridview-selected-row" BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                    <EditRowStyle CssClass="gridview-edit-row" BackColor="#FFFF99" />
                    <Columns>
                        <asp:BoundField DataField="menu_id" HeaderText="ID" ReadOnly="True" InsertVisible="False" SortExpression="menu_id">
                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Image" SortExpression="productImage" InsertVisible="False">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("productImage") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="TextBox1" ErrorMessage="Image Path is required" Font-Italic="True" Font-Size="13px" ForeColor="Red"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Image ID="Image1" runat="server" Height="100px" ImageUrl='<%# Eval("productImage") %>' Width="130px" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Height="100px" Width="120px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name" SortExpression="productName">
                            <EditItemTemplate>
                                <asp:TextBox ID="Nametxt" runat="server" Text='<%# Bind("productName") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Nametxt" Display="Dynamic" ErrorMessage="Product name is Empty" Font-Italic="True" Font-Size="13px" ForeColor="Red"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("productName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Price" SortExpression="productPrice">
                            <EditItemTemplate>
                                <asp:TextBox ID="Pricetxt" runat="server" Text='<%# Bind("productPrice") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="Pricetxt" Display="Dynamic" ErrorMessage="Product Price cannot be Empty" Font-Italic="True" Font-Size="13px" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="Pricetxt" Display="Dynamic" ErrorMessage="Only accept 1-9999" Font-Italic="True" Font-Size="13px" ForeColor="Red" Type="Double" MaximumValue="9999" MinimumValue="1"></asp:RangeValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("productPrice") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" SortExpression="productDescription">
                            <EditItemTemplate>
                                <asp:TextBox ID="DescriptionTxt" runat="server" Text='<%# Bind("productDescription") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ErrorMessage="Description cannot be Empty" Font-Italic="True" Font-Size="13px" ForeColor="Red" ControlToValidate="DescriptionTxt"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("productDescription") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category" SortExpression="productCategory">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("productCategory") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" SortExpression="productStatus">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("productStatus") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                    </Columns>

                    <SortedAscendingCellStyle BackColor="#FEFCEB" />
                    <SortedAscendingHeaderStyle BackColor="#AF0101" />
                    <SortedDescendingCellStyle BackColor="#F6F0C0" />
                    <SortedDescendingHeaderStyle BackColor="#7E0000" />

                </asp:GridView>
                <br />
                <asp:Button ID="UpdateBtn" runat="server" OnClick="Button1_Click" Text="Update Status" BackColor="#990033" BorderColor="White" Font-Names="Arial" ForeColor="White" Font-Bold="True" Height="41px" />
                <asp:Label ID="Label1" runat="server" ForeColor="Lime" Font-Names="Arial"></asp:Label>
                <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT menu_id, productName, productPrice, productImage, productDescription, productCategory, productStatus FROM Menus" UpdateCommand="UPDATE [Menus] Set [productName]=@productName, [productPrice]=@productPrice, [productDescription]=@productDescription, [productCategory]=@productCategory, [productImage]=@productImage Where [menu_id]=@menu_id" DeleteCommand="DELETE from [Menus] Where [menu_id]=@menu_id">
                    <DeleteParameters>
                        <asp:Parameter Name="menu_id"></asp:Parameter>
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="productName"></asp:Parameter>
                        <asp:Parameter Name="productPrice"></asp:Parameter>
                        <asp:Parameter Name="productDescription"></asp:Parameter>
                        <asp:Parameter Name="productCategory"></asp:Parameter>
                        <asp:Parameter Name="productImage"></asp:Parameter>
                        <asp:Parameter Name="menu_id"></asp:Parameter>
                    </UpdateParameters>
                </asp:SqlDataSource>


                <!-- Form for adding a new product -->

                <div>
                    <!-- "+" Button -->
                    <button type="button" class="add-product-button" onclick="toggleFloatingWindow()">+</button>
                    &nbsp;
                </div>

                <div id="floatingWindow" class="floating-window" style="display: none">
                    <div class="modal-content">
                        <h2>Add New Product</h2>
                        <hr />

                        <br />

                        <div class="form-group">
                            <label for="productNameTextBox">Product Name:</label>
                            <asp:TextBox ID="productNameTextBox" runat="server"></asp:TextBox>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Product Name is Empty" ControlToValidate="productNameTextBox" Display="Dynamic" ForeColor="Red" ValidationGroup="AddProductValidation"></asp:RequiredFieldValidator>

                        </div>
                        <div class="form-group">
                            <label for="productPriceTextBox">Product Price:</label>
                            <asp:TextBox ID="productPriceTextBox" runat="server" TextMode="Number"></asp:TextBox>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="productPriceTextBox" Display="Dynamic" ErrorMessage="Product Price is Empty" ForeColor="Red" ValidationGroup="AddProductValidation"></asp:RequiredFieldValidator>
                            &nbsp;<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="productPriceTextBox" Display="Dynamic" ErrorMessage="must be 1 to 9999" ForeColor="Red" MaximumValue="9999" MinimumValue="1" Type="Double"></asp:RangeValidator>

                        </div>
                        <div class="form-group">
                            <label for="productDescriptionTextBox">Product Description:</label>
                            <asp:TextBox ID="productDescriptionTextBox" runat="server" TextMode="MultiLine"></asp:TextBox>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="productDescriptionTextBox" Display="Dynamic" ErrorMessage="Product Description is Empty" ForeColor="Red" ValidationGroup="AddProductValidation"></asp:RequiredFieldValidator>

                        </div>
                        <div class="form-group">
                            <label for="productCategoryDropDown">Product Category:</label>
                            <asp:DropDownList ID="productCategoryDropDown" runat="server">
                                <asp:ListItem Text="Dishes" Value="Dishes" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Beverage" Value="Beverage"></asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="productCategoryDropDown" ErrorMessage="Product Category is Empty" ForeColor="Red" ValidationGroup="AddProductValidation"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <asp:FileUpload ID="productImageFileUpload" runat="server" />
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="productImageFileUpload" Display="Dynamic" ErrorMessage="Product Image is Empty" ForeColor="Red" ValidationGroup="AddProductValidation"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="addProductButton" runat="server" Text="Add Product" CssClass="btn btn-primary" OnClick="AddProductButton_Click" ValidationGroup="AddProductValidation" BackColor="Blue" ForeColor="White" Height="50px" Width="110px" Font-Bold="True" Font-Italic="False" />
                            &nbsp;&nbsp;
        <button type="button" class="cancel-button" onclick="closeFloatingWindow()" style="padding: 14px; background-color: #0000FF; color: #FFFFFF; font-family: Arial; font-weight: bold">Cancel</button>

                        </div>

                        <div class="form-group">
                            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
