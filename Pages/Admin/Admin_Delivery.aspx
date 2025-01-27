<%@ Page Title="Delivery Management" Language="C#" MasterPageFile="~/AdminNavigation.Master" AutoEventWireup="true" CodeBehind="Admin_Delivery.aspx.cs" Inherits="COZYCOOK.Pages.Admin.Admin_Delivery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="stylesheets" runat="server">
    <link rel="stylesheet" type="text/css" href="/CSS/Admin_OrderNav.css" />
    <link rel="stylesheet" type="text/css" href="/CSS/Admin_Table.css" />
    <style type="text/css">
        .auto-style1 {
            margin-top: 2%;
            padding: 2%;
            width: 131%;
            box-shadow: -4px -5px 9px 0px rgba(0, 0, 0, 0.22);
            -webkit-box-shadow: -4px -5px 9px 0px rgba(0, 0, 0, 0.22);
            -moz-box-shadow: -4px -5px 9px 0px rgba(0, 0, 0, 0.22);
            max-height: 550px;
            overflow: scroll;
            scrollbar-width: none;
        }
        .auto-style2 {
            list-style-type: none;
            margin: 0;
            padding: 0;
            overflow: hidden;
            background-color: #333;
            position: sticky;
            top: 0;
            width: 125%;
        }
        .auto-style3 {
            margin-top: 2%;
            padding: 2%;
            width: 113%;
            box-shadow: -4px -5px 9px 0px rgba(0, 0, 0, 0.22);
            -webkit-box-shadow: -4px -5px 9px 0px rgba(0, 0, 0, 0.22);
            -moz-box-shadow: -4px -5px 9px 0px rgba(0, 0, 0, 0.22);
            max-height: 550px;
            overflow: scroll;
            scrollbar-width: none;
        }
        .auto-style4 {
            width: 1337px;
        }
        .auto-style5 {
            position: relative;
            background-color: #eee;
            min-height: 127vh;
            top: 0;
            left: 72px;
            transition: all 0.5s ease;
            width: calc(100% - 80px);
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script>
    let btn = document.querySelector('#btn')
    let sidebar = document.querySelector('.sidebar')

    btn.onclick = function () {
        sidebar.classList.toggle('active');
    };

    function removeRow() {
        var gridView = document.getElementById('<%= DeliveryList.ClientID %>'); // Get the GridView element
        var rows = gridView.getElementsByTagName('tr'); // Get all rows in the GridView

        for (var i = 0; i < rows.length; i++) {
            var cells = rows[i].getElementsByTagName('td'); // Get all cells in the current row

            // Check if the row contains the status cell and its value is "Finish"
            if (cells.length > 0 && cells[2].innerHTML.trim() === "Finish") {
                // Remove the entire row
                gridView.deleteRow(i);
                break; // Exit the loop after removing the row
            }
        }
    }

}
</script>

<body>
          <div class="auto-style5">
            <div class="container">
                <h1 class="auto-style4">Order Management</h1>
                <!--Start your code here-->
                <ul class="auto-style2">
                    <li class="OrderNavSelect"><a href="Admin_OrderPage.aspx">All Orders</a></li>
                    <li class="OrderNavSelect"><a href="Admin_KitchenPage.aspx">Confirmed Order</a></li>
                    <li class="OrderNavSelect"><a class="active" href="Admin_Delivery.aspx">Delivery</a></li>
                </ul>
                <!--end-->
                <div class="auto-style3">
                <div class="auto-style1">
                     <table class="table">
                         <td>
                    <asp:GridView ID="DeliveryList" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="DeliveryID" Width="1161px" OnRowDataBound="GridView1_RowDataBound">

                        <Columns>
                            <asp:BoundField DataField="DeliveryID" HeaderText="Delivery ID" InsertVisible="False" ReadOnly="True" SortExpression="DeliveryID" />
                            <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" InsertVisible="False" ReadOnly="True"/>
                            <asp:TemplateField HeaderText="Delivery Status" SortExpression="Status">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="StatusDropDown" runat="server" AutoPostBack="False" OnSelectedIndexChanged="Status_SelectedIndexChanged">
                                        <asp:ListItem Text="--" Value="New"></asp:ListItem>
                                        <asp:ListItem Text="Finish" Value="Finish"></asp:ListItem>
                                        <asp:ListItem Text="On the Way" Value="On the Way"></asp:ListItem>
                                        <asp:ListItem Text="Preparing" Value="Preparing"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                 <ItemTemplate>
                                     <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                 </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Menu">
                                <ItemTemplate>
                                    <ul>
                                      <asp:Literal ID="litMenuItems" runat="server"></asp:Literal>
                                    </ul>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:BoundField DataField="address" HeaderText="Address" InsertVisible="False" ReadOnly="True" SortExpression="Address" />
                            <asp:CommandField ButtonType="Button" ShowEditButton="True" ShowHeader="True" EditText="Update" UpdateText="Confirm" CancelText="Back" />
                        </Columns>
                    </asp:GridView>
                             </td>
                        </table>

                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"
                        SelectCommand="SELECT 
                        D.delivery_id AS DeliveryID,
                        D.first_name AS CustomerName,
                        D.address AS Address,
                        D.status AS Status,
                        STUFF((SELECT ', ' + M.productName + ' x' + CAST(OD.quantity AS VARCHAR(10))
                               FROM Orders O
                               INNER JOIN Order_Details OD ON O.order_id = OD.order_id
                               INNER JOIN Menus M ON OD.menu_id = M.menu_id
                               WHERE D.delivery_id = O.delivery_id
                               FOR XML PATH('')), 1, 2, '') AS MenuItems
                        FROM 
                            Deliverys D;"
                        UpdateCommand="UPDATE Deliverys SET status = @Status WHERE delivery_id = @DeliveryID">
                        <UpdateParameters>
                            <asp:Parameter Name="Status" Type="String" />
                            <asp:Parameter Name="DeliveryID" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>

                </div>
            </div>
        </div>
    </div>
</body>

</asp:Content>
