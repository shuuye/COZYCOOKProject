<%@ Page Title="Reservation Management" Language="C#" MasterPageFile="~/AdminNavigation.Master" AutoEventWireup="true" CodeBehind="Admin_ReservationPage.aspx.cs" Inherits="testing.Pages.Admin.Admin_ReservationPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="stylesheets" runat="server">
    <link rel="stylesheet" type="text/css" href="/CSS/Admin_OrderNav.css">
    <link rel="stylesheet" type="text/css" href="/CSS/Admin_ReservavtionPage.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main-content">
        <div class="container">
            <h1>Reservavtion Management</h1>
            <!--Start your code here-->


            <div class="ViewReservavtionPanel">
                <h2 id="ReservavtionTitle">All Reservavtion</h2>

                <asp:GridView ID="ReservavtionList" runat="server" class="ReservavtionList" AutoGenerateColumns="False" DataSourceID="dsReservation" GridLines="Horizontal" OnRowCommand="ReservavtionList_RowCommand" DataKeyNames="reservation_id" RowStyle-BackColor="White">
                    <Columns>
                        <asp:BoundField ItemStyle-CssClass="ReservavtionDetails ReservavtionID" DataField="reservation_id" HeaderText="Reservation ID" InsertVisible="False" ReadOnly="True" SortExpression="reservation_id">
                            <ItemStyle CssClass="ReservavtionDetails ReservavtionID"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField ItemStyle-CssClass="ReservavtionDetails Name" DataField="customer_name" HeaderText="Customer Name" ReadOnly="True" SortExpression="customer_name">
                            <ItemStyle CssClass="ReservavtionDetails Name"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField ItemStyle-CssClass="ReservavtionDetails Phone" DataField="phone" HeaderText="Contact No." SortExpression="phone">
                            <ItemStyle CssClass="ReservavtionDetails Phone"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField ItemStyle-CssClass="ReservavtionDetails Date" DataField="reserve_date" HeaderText="Reservation Date" ReadOnly="True" SortExpression="reserve_date" DataFormatString="{0:dd MMM yyyy}">
                            <ItemStyle CssClass="ReservavtionDetails Date"></ItemStyle>
                        </asp:BoundField>

                        <asp:BoundField ItemStyle-CssClass="ReservavtionDetails Time" DataField="reserve_time" HeaderText="Reserve Time" SortExpression="reserve_time">
                            <ItemStyle CssClass="ReservavtionDetails Time"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField ItemStyle-CssClass="ReservavtionDetails Pax" DataField="pax" HeaderText="Pax" SortExpression="pax">
                            <ItemStyle CssClass="ReservavtionDetails Pax"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField ItemStyle-CssClass="ReservavtionDetails Status" DataField="status" HeaderText="Status" ReadOnly="True" SortExpression="status">
                            <ItemStyle CssClass="ReservavtionDetails Status"></ItemStyle>
                        </asp:BoundField>
                        
                        <asp:TemplateField HeaderText="Assign Table" ItemStyle-ForeColor="Black">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkAssignTable" runat="server" Text="Assign Table" CommandName="AssignTable" CommandArgument='<%# Eval("reservation_id") + "," + Eval("customer_name") %>' ForeColor="Black" Font-Overline="False" BorderWidth="1px" BorderStyle="Outset" BackColor="#F4F4F4" BorderColor="Black" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:ButtonField ButtonType="Button" CommandName="CancelReservation" Text="Cancel" />


                    </Columns>
                    <EmptyDataTemplate>
                        There&#39;s no reservation record yet
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#242121" />
                </asp:GridView>





                <asp:SqlDataSource ID="dsReservation" runat="server" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\cozyCook.mdf;Integrated Security=True" ProviderName="System.Data.SqlClient"
                    SelectCommand="SELECT r.reservation_id , c.name AS 'customer_name', r.phone , r.reserve_date , r.reserve_time , r.pax ,
                        CASE WHEN r.table_id IS NULL THEN 'New Reservation' 
                        WHEN r.table_id &gt; 0 THEN 'Table ' + CAST(r.table_id AS NVARCHAR(10)) 
                        ELSE 'Cancelled' END AS status 
                        FROM Reservations AS r 
                        LEFT OUTER JOIN Customers AS c ON r.customer_id = c.customer_id 
                        ORDER BY r.reserve_date DESC, r.reserve_time DESC"></asp:SqlDataSource>





            </div>


            <!--end-->
        </div>
    </div>
</asp:Content>
