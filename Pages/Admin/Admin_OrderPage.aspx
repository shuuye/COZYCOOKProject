<%@ Page Title="All Orders" Language="C#" MasterPageFile="~/AdminNavigation.Master" AutoEventWireup="true" CodeBehind="Admin_OrderPage.aspx.cs" Inherits="testing.Pages.Admin.Admin_OrderPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="stylesheets" runat="server">
    <link rel="stylesheet" type="text/css" href="/CSS/Admin_OrderNav.css">
    <link rel="stylesheet" type="text/css" href="/CSS/Admin_OrderPage.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="main-content">
        <div class="container">
            <h1>Order Management</h1>
            <!--Start your code here-->

            <ul class="OrderNav">
                <li class="OrderNavSelect"><a class="active" href="/Pages/Admin/Admin_OrderPage.aspx">All Orders</a></li>
                <li class="OrderNavSelect"><a href="/Pages/Admin/Admin_KitchenPage.aspx">Confirmed Order</a></li>
                <li class="OrderNavSelect"><a href="/Pages/Admin/Admin_Delivery.aspx">Delivery</a></li>

            </ul>
            <div class="ViewOrderPanel">
                <h2 id="OrderTitle">All Order</h2>

                <asp:GridView ID="OrderList" CssClass="OrderList" runat="server" AutoGenerateColumns="False" DataKeyNames="OrderID" DataSourceID="dsAllOrder">
                    <Columns>
                        <asp:BoundField ItemStyle-CssClass="OrderDetails OrderID" DataField="OrderID" HeaderText="Order ID" InsertVisible="False" ReadOnly="True" SortExpression="OrderID" ItemStyle-Width="5%">
                            <ItemStyle CssClass="OrderDetails OrderID"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField ItemStyle-CssClass="OrderDetails Address" DataField="Address" HeaderText="Address" ReadOnly="True" SortExpression="Address" ItemStyle-Width="38%">
                            <ItemStyle CssClass="OrderDetails Address"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField ItemStyle-CssClass="OrderDetails Phone" DataField="Phone" HeaderText="Phone" ReadOnly="True" SortExpression="Phone" ItemStyle-Width="10%">
                            <ItemStyle CssClass="OrderDetails Phone"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField ItemStyle-CssClass="OrderDetails Dishes" DataField="Dishes" HeaderText="Dishes" ReadOnly="True" SortExpression="Dishes" ItemStyle-Width="10%">
                            <ItemStyle CssClass="OrderDetails Dishes"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField ItemStyle-CssClass="OrderDetails Price" DataField="Price" HeaderText="Price" ReadOnly="True" SortExpression="Price" ItemStyle-Width="8%">
                            <ItemStyle CssClass="OrderDetails Price" HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField ItemStyle-CssClass="OrderDetails Acquire" DataField="Acquire" HeaderText="Acquire" ReadOnly="True" SortExpression="Acquire" ItemStyle-Width="8%">
                            <ItemStyle CssClass="OrderDetails Acquire" HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Status" SortExpression="Status" ItemStyle-Width="11%">
                            <EditItemTemplate>
                                <asp:DropDownList ID="Status" runat="server" AutoPostBack="False" OnSelectedIndexChanged="Status_SelectedIndexChanged">
                                    <asp:ListItem Text="--" Value="New"></asp:ListItem>
                                    <asp:ListItem Text="Confirmed" Value="Confirm"></asp:ListItem>
                                    <asp:ListItem Text="Canceled" Value="Canceled"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>' CssClass="status-label"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Button" ShowEditButton="True" ShowHeader="True" EditText="Update Status" UpdateText="Confirm" CancelText="Back" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </asp:GridView>

                <asp:SqlDataSource ID="dsAllOrder" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>" SelectCommand="SELECT 
                        O.order_id AS OrderID,
                        CASE 
                            WHEN O.delivery_id IS NOT NULL THEN D.address
                            ELSE NULL
                        END AS Address,
                        CASE 
                            WHEN O.delivery_id IS NOT NULL THEN D.phone
                            ELSE NULL
                        END AS Phone,
                        (
                            SELECT 
                                STRING_AGG(CONCAT(M.productName, ' X', OD.quantity), CHAR(13) + CHAR(10)) 
                            FROM 
                                Order_Details OD 
                                INNER JOIN Menus M ON OD.menu_id = M.menu_id 
                            WHERE 
                                OD.order_id = O.order_id
                        ) AS Dishes,
                        (
                            SELECT 
                                SUM(OD.quantity * M.productPrice) 
                            FROM 
                                Order_Details OD 
                                INNER JOIN Menus M ON OD.menu_id = M.menu_id 
                            WHERE 
                                OD.order_id = O.order_id
                        ) AS Price,
                        CASE 
                            WHEN O.delivery_id IS NOT NULL THEN 'Delivery'
                            ELSE 'Dine-In'
                        END AS Acquire,
                        O.order_status AS Status
                    FROM 
                        Orders O
                    LEFT JOIN 
                        Deliverys D ON O.delivery_id = D.delivery_id
                    ORDER BY 
                    O.order_id DESC ;"
                    UpdateCommand="UPDATE Orders SET order_status = @Status WHERE order_id = @OrderID">
                    <UpdateParameters>
                        <asp:Parameter Name="Status" Type="String" />
                        <asp:Parameter Name="OrderID" Type="Int32" />
                    </UpdateParameters>
                </asp:SqlDataSource>



            </div>
        </div>
    </div>


</asp:Content>
