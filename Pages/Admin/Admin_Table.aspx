<%@ Page Title="Table Management" Language="C#" MasterPageFile="~/AdminNavigation.Master" AutoEventWireup="true" CodeBehind="Admin_Table.aspx.cs" Inherits="COZYCOOK.Pages.Admin.Admin_Table" %>

<asp:Content ID="Content1" ContentPlaceHolderID="stylesheets" runat="server">
    <link rel="stylesheet" type="text/css" href="/CSS/Admin_Table.css" />
    <style type="text/css">
        .auto-style1 {
            width: 10px;
        }
        .auto-style2 {
            width: 618px;
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

};

</script>

<body class="MainPage">
            <div class="main-content">
            <div class="container">
                <h1>Table Management</h1>
                <br />
                <table class="timeDisplay">
                    <!-- Time display table -->
                    <tr>
                        <th>Table Available for</th>
                        <td> </td>
                    </tr>
                     <tr>
                         <td>Reservation ID:</td>
                         <td>
                             <asp:Label ID="lblReserveID" runat="server" Text=""></asp:Label>
                         </td>
                     </tr>
                     <tr>
                         <td>Customer Name:</td>
                         <td>
                             <asp:Label ID="lblCusName" runat="server" Text=""></asp:Label>
                         </td>
                     </tr> 
                    <tr>
                        <td class="date">
                            Date:
                        </td>
                        <td class="dateSlot">
                            <asp:Label ID="lblDate" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="time">
                            Time:
                        </td>
                        <td class="timeSlot">
                            <asp:Label ID="lblTime" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                </table>
                <p>*Select the table type from the customers preferences</p>
                <br />
                <p>Table Size Details :</p>
                <!-- Table size details table -->
                <table class="table">
                    <tr class="table_th">
                        <th>Small (No.Pax)</th>
                        <th>Medium (No.Pax)</th>
                        <th>Big (No.Pax)</th>
                    </tr>
                    <tr>
                        <td>4-6px</td>
                        <td>6-11px</td>
                        <td>12-18px</td>
                    </tr>
                </table>
                <p>*Please select the table size by the no of pax.</p>
                <p>&nbsp;</p>
                <asp:Label ID="lblSuccessMessage" runat="server" Text=""></asp:Label><br/>
                <asp:Button ID="btnBack" runat="server" Text="Back Previous Page" PostBackUrl="~/Pages/Admin/Admin_ReservationPage.aspx" Height="42px" Width="207px" />

            <div class="ViewDeliveryPanel">
             <div class="auto-style1">
            <!-- Add GridView to display table data -->
            <table class="table">
                <td>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Height="300px" Width="1081px" CellPadding="8" CellSpacing="5" >
                <Columns>
                    <asp:BoundField DataField="table_No" HeaderText="Table No." InsertVisible="False" ReadOnly="true"/>
                    <asp:BoundField DataField="type" HeaderText="Table Type" ReadOnly="true"/>
                    <asp:BoundField DataField="size" HeaderText="Table Size" ReadOnly="true" />

                    <asp:TemplateField HeaderText="Status" SortExpression="Status">
                        <EditItemTemplate>
                            <asp:DropDownList ID="StatusDropDown" runat="server" AutoPostBack="False" OnSelectedIndexChanged="Status_SelectedIndexChanged">
                                <asp:ListItem Text="--" Value="New"></asp:ListItem>
                                <asp:ListItem Text="Available" Value="Available"></asp:ListItem>
                                <asp:ListItem Text="Unavailable" Value="Unavailable"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Status" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField ButtonType="Button" ShowEditButton="True" ShowHeader="True" EditText="Update" UpdateText="Confirm" CancelText="Back" HeaderText="#"/>
                    
                  <asp:TemplateField HeaderText="Add">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkAddTable" runat="server" Text="Add" CommandName="AddTable" CommandArgument='<%# Eval("table_No") + "," + Request.QueryString["reservationID"] %>' OnCommand="lnkAddTable_Command" ForeColor="White" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

         </table>
            <!-- Define SqlDataSource for data binding and updating -->
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>" 
                SelectCommand="SELECT * FROM Tables"
                UpdateCommand="UPDATE Tables SET status = @Status WHERE table_No = @table_No">
                <UpdateParameters>
                    <asp:Parameter Name="Status" Type="String" />
                    <asp:Parameter Name="table_No" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
            </div>
        </div>
    </div>
</div>
</body>
</asp:Content>
