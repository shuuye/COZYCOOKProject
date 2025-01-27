<%@ Page Title="Make Reservation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reservation.aspx.cs" Inherits="testing.Pages.User.reservation" %>


<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
    <link rel="stylesheet" type="text/css" href="/CSS/reservation.css" />
    <link rel="stylesheet" href="/CSS/date-picker.css">
    </asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        

        function openPopUp(event) {
            var closestHavePopUp = event.target.closest('.havePopUp');
            if (closestHavePopUp) {
                var popupConfirm = closestHavePopUp.querySelector('.popupConfirm');
                if (popupConfirm) {
                    popupConfirm.classList.add("show");
                    return;
                }
            }
            console.error("Could not find .popupConfirm");
        }


        function closePopupbox(event) {
            var popupConfirm = event.currentTarget.closest('.popupConfirm');
            popupConfirm.classList.remove("show");
        }

        function cancelReserve() {
            var confirmed = confirm("Are you sure you want to cancel your reservation?");
            if (confirmed) {
                window.open("index.aspx", "_self");
                document.getElementById("bookingForm").reset(); // Reset the form fields
            }
        }


    </script>


    <main id="reserve">
        <!-- ~~PUT YOUR CODE HERE ONLY , change the main id is ok ------------------------------------------------------------ -->
        <asp:LoginView ID="LoginView1" runat="server"></asp:LoginView>
        <div class="bookingBanner">
            <h1>Booking</h1>
        </div>
        <div class="bookingContent">

            <table class="bookingDetails">
                <tr>
                    <td class="havePopUp date">

                        <label for="date">Date</label>
                        <asp:TextBox ID="dateip" class="date-picker" ClientIDMode="Static" runat="server" AutoCompleteType="Disabled"></asp:TextBox>

                        <script src="/Scripts/date-picker.js"></script>
                    </td>

                    <td class="havePopUp time">
                        <label for="time">Time</label>
                        <div onclick="openPopUp(event)">
                            <asp:TextBox ID="timeip" runat="server" ClientIDMode="Static" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                        <div class="popupConfirm">
                            <div class="popupConfirm-content">
                                <div class="time-picker">
                                    <div class="time-slot">
                                        <label for="time-slot">Select Time:</label>
                                        <select id="time-slot">
                                            <!-- Options will be dynamically added here using JavaScript -->
                                        </select>
                                    </div>
                                </div>
                                <button class="closePopup" onclick="closePopupbox(event)">Close</button>
                            </div>
                        </div>

                    </td>

                    <td class="pax">
                        <label for="pax">Pax</label>
                        <asp:DropDownList ID="pax" runat="server"  DataTextField="size" DataValueField="size"></asp:DropDownList>
                        
                    </td>

                </tr>
                <tr>
                    <td class="name">
                        <label for="name">Name</label>
                        <asp:TextBox ID="nametxt" runat="server" Enabled="False"></asp:TextBox>
                    </td>
                    <td class="email">
                        <label for="email">Email Address</label>
                        <asp:TextBox ID="emailip" runat="server" Enabled="False"></asp:TextBox>

                    </td>
                    <td class="phone">
                        <label for="phone">Phone</label>
                        <asp:TextBox ID="phoneip" runat="server" placeholder="e.g., 01X XXXXXXX (do not require '-' and space)"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidateEmptyText="True" ErrorMessage="The phone format is incorrect." ControlToValidate="phoneip" Font-Italic="True" ValidationExpression="^01[1-9]\d{7,8}$" SetFocusOnError="True"></asp:RegularExpressionValidator>
                    </td>
                </tr>
            </table>
            <div class="bookingFormButtons">
                <asp:Button ID="btnSubmit" class="submitButton" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="submitButton" />
                <asp:Button ID="btnCancel" class="cancelButton" runat="server" Text="Cancel" />
            </div>
            <%-- onclick="cancelReserve()"--%>



            <div class="historyPanel">
                <h3>History</h3>

                <asp:GridView ID="reservationTable" runat="server" AutoGenerateColumns="False" DataKeyNames="Booking ID" DataSourceID="dsReserveHistory" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="8" ForeColor="Black" GridLines="Horizontal" Font-Names="Work Sans" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="Booking ID" HeaderText="Booking ID" InsertVisible="False" ReadOnly="True" SortExpression="Booking ID" />
                        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                        <asp:BoundField DataField="Time" HeaderText="Time" SortExpression="Time" />
                        <asp:BoundField DataField="Pax" HeaderText="Pax" SortExpression="Pax" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                        <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                        <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" SortExpression="Status" />
                    </Columns>
                    <EditRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <EmptyDataTemplate>
                        No record found in current account.
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                    <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" ForeColor="Black" />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#242121" />
                </asp:GridView>

                <asp:SqlDataSource ID="dsReserveHistory" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>" SelectCommand="SELECT
    r.reservation_id AS 'Booking ID',
    FORMAT(r.reserve_date, 'dd MMM yyyy') AS 'Date',
    r.reserve_time AS 'Time',
    r.pax AS 'Pax',
    c.name AS 'Name',
    c.email AS 'Email',
    r.phone AS 'Phone',
    CASE
        WHEN t.table_No IS NULL THEN 'Pending'
        WHEN t.table_No = 0 THEN 'Cancelled'
        ELSE 'Confirmed: Table ' + CAST(t.table_No AS NVARCHAR(10))
    END AS 'Status'
FROM
    Reservations r
JOIN
    Customers c ON r.customer_id = c.customer_id
LEFT JOIN
    Tables t ON r.table_id = t.table_No
WHERE
    r.customer_id = @customer_id
ORDER BY reservation_id DESC;
">
                    <SelectParameters>
                        <asp:SessionParameter Name="customer_id" SessionField="custID" />
                    </SelectParameters>
                </asp:SqlDataSource>

                
            </div>

        </div>




        <!-- ~~ STOP PUTTING YOUR CODE AFTER HERE ------------------------------------------------------------------------------- -->
    </main>
</asp:Content>
