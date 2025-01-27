<%@ Page Title="Confirmed Order" Language="C#" MasterPageFile="~/AdminNavigation.Master" AutoEventWireup="true" CodeBehind="Admin_KitchenPage.aspx.cs" Inherits="testing.Pages.Admin.Admin_KitchenPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="stylesheets" runat="server">
    <link rel="stylesheet" type="text/css" href="/CSS/Admin_OrderNav.css">
    <link rel="stylesheet" type="text/css" href="/CSS/Admin_KitchenPage.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main-content">
        <div class="container">
            <h1>Order Management</h1>
            <!--Start your code here-->

            <ul class="OrderNav">
                <li class="OrderNavSelect"><a href="/Pages/Admin/Admin_OrderPage.aspx">All Orders</a></li>
                <li class="OrderNavSelect"><a class="active" href="/Pages/Admin/Admin_KitchenPage.aspx">Confirmed Order</a></li>
                <li class="OrderNavSelect"><a href="/Pages/Admin/Admin_Delivery.aspx">Delivery</a></li>

                <li>
                    <br />
                </li>

            </ul>


            <div class="ViewConfirmedOrderPanel">
                <h2 id="OrderTitle">All Confirmed Order</h2>
                <div id="orderContainer" class="orderContainer">
                    <asp:ListView ID="LVOrder" runat="server" DataKeyNames="order_id" DataSourceID="dsConfirmedOrder" OnItemDataBound="LVOrder_ItemDataBound" OnSelectedIndexChanged="LVOrder_SelectedIndexChanged">
                        <EmptyDataTemplate>
                            <table style="">
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>

                        <ItemTemplate>
                            <td runat="server" style="vertical-align: top;">
                                <table class="PrepareList" style="width:350px;">
                                    <thead>
                                        <tr runat="server" class="PrepareDetails OrderTime">
                                            <th>
                                                <h3>Order Time</h3>
                                            </th>
                                            <td>
                                                <asp:Label ID="order_timeLabel" runat="server" Text='<%# Eval("order_time") %>' /></td>
                                        </tr>
                                        <tr runat="server" class="PrepareDetails OrderID">
                                            <th>
                                                <h3>Order ID</h3>
                                            </th>
                                            <td>
                                                <asp:Label ID="order_idLabel" runat="server" Text='<%# Eval("order_id") %>' /></td>
                                        </tr>
                                        <tr runat="server" class="PrepareDetails head acquire">
                                            <th>
                                                <h3>Acquire</h3>
                                            </th>
                                            <td>
                                                <asp:Label ID="acquire_stateLabel" runat="server" Text='<%# Eval("acquire_state") %>' /></td>
                                        </tr>
                                    </thead>
                                    <tbody class="dishesDetails">
                                        <tr>
                                            <th colspan="2">Dishes</th>
                                        </tr>
                                        <tr runat="server">
                                            <td colspan="2">
                                                <asp:SqlDataSource ID="dsOrderMenu" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"
                                                    SelectCommand="SELECT Order_Details.quantity, Menus.productName FROM Menus INNER JOIN Order_Details ON Menus.menu_id = Order_Details.menu_id INNER JOIN Orders ON Order_Details.order_id = Orders.order_id
                   WHERE Order_Details.order_id = @order_id">
                                                    <SelectParameters>
                                                        <asp:Parameter Name="order_id" Type="Int32" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>

                                                <asp:DataList ID="DLOrderMenu" runat="server" DataSourceID="dsOrderMenu">
                                                    <ItemTemplate>
                                                        <tr runat="server">
                                                            <td class="quantity">
                                                                <asp:Label ID="quantityLabel" runat="server" Text='<%# Eval("quantity") %>' />
                                                            </td>
                                                            <td class="MenuName">
                                                                <asp:Label ID="productNameLabel" runat="server" Text='<%# Eval("productName") %>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>

                                    </tbody>

                                    <tfoot>
                                        <tr runat="server" class="PrepareDetails selection">
                                            <td>
                                                <asp:Button ID="finishBtn" class="selectionbtn finish"  runat="server" Text="Finish" OnClick="finishBtn_Click" />
                                                
                                            </td>
                                            <td>
                                                <asp:Button ID="cancelBtn" class="selectionbtn cancel"  runat="server" Text="Cancel" OnClick="cancelBtn_Click" />
                                                
                                            </td>
                                        </tr>
                                    </tfoot>
                                </table>

                            </td>
                        </ItemTemplate>


                        <LayoutTemplate>
                            <div id="itemPlaceholderContainer">
                                <table runat="server" class="PrepareList">
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </div>

                        </LayoutTemplate>

                    </asp:ListView>
                    <asp:SqlDataSource ID="dsConfirmedOrder" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>" SelectCommand="SELECT
    DISTINCT o.order_id,
    o.order_time,
    CASE
        WHEN o.delivery_id IS NOT NULL THEN 'Delivery (' + CAST(o.delivery_id AS NVARCHAR(10)) + ')'
        WHEN o.reservation_id IS NOT NULL THEN 'Reservation (Table ' + CAST(t.table_No AS NVARCHAR(10)) + ')'
        ELSE 'DINE-IN'
    END AS acquire_state
FROM
    Orders AS o
LEFT JOIN
    Order_Details AS od ON o.order_id = od.order_id
LEFT JOIN
    Reservations AS r ON o.reservation_id = r.reservation_id
LEFT JOIN
    Tables AS t ON r.table_id = t.table_No
WHERE
    o.order_status = 'Confirm'
"></asp:SqlDataSource>

                    


                </div>


            </div>
        </div>

    </div>
</asp:Content>
