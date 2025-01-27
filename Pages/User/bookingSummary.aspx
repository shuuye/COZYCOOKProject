<%@ Page Title="Booking Summary" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="bookingSummary.aspx.cs" Inherits="testing.Pages.User.bookingSummary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Stylesheets" runat="server">
    <link rel="stylesheet" type="text/css" href="/CSS/reservation.css">
    <link rel="stylesheet" type="text/css" href="/CSS/bookingSummary.css" />
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <script>

        function openPopUp(event) {
            console.log("popupConfirm");
            var popupConfirm = event.target.closest('.havePopUp').querySelector('.popupConfirm');
            console.log(popupConfirm);
            popupConfirm.classList.add("show");

        }

        function closePopupbox(event) {
            var popupConfirm = event.currentTarget.closest('.popupConfirm');
            popupConfirm.classList.remove("show");
        }




     </script>

    <main id="reserve">
        <!-- ~~PUT YOUR CODE HERE ONLY , change the main id is ok ------------------------------------------------------------ -->
        <div class="bookingBanner">
            <h1>Booking Summary</h1>
        </div>
        <div class="bookingSummary" id="bookingSummary">
            <table>
                <tr>
                   <th>Booking ID:</th>
                    <td><asp:Label ID="bookingID" runat="server" Text="Reservation Id: " ForeColor="Black"></asp:Label></td>
                </tr>
                <tr>
                    <th>Date:</th>
                    <td ><asp:Label ID="bookingDate" runat="server" Text="Reservation Date: " ForeColor="Black"></asp:Label></td>
                </tr>
                <tr>
                    <th>Time:</th>
                    <td><asp:Label ID="bookingTime" runat="server" Text="Reservation Time: " ForeColor="Black"></asp:Label></td>
                </tr>
                <tr>
                    <th>Pax:</th>
                    <td ><asp:Label ID="bookingPax" runat="server" Text="Pax: " ForeColor="Black"></asp:Label></td>
                </tr>
                <tr>
                    <th>Name:</th>
                    <td ><asp:Label ID="bookingName" runat="server" Text="Name: " ForeColor="Black"></asp:Label></td>
                </tr>
                <tr>
                    <th>Email:</th>
                    <td ><asp:Label ID="bookingEmail" runat="server" Text="Email: " ForeColor="Black"></asp:Label></td>
                </tr>
                <tr>
                    <th>Phone:</th>
                    <td ><asp:Label ID="bookingPhone" runat="server" Text="Phone: " ForeColor="Black"></asp:Label></td>
                </tr>
                <tr>
                    <th>Status:</th>
                    <td ><asp:Label ID="bookingStatus" runat="server" Text="Pending " ForeColor="Black"></asp:Label></td>
                </tr>
            </table>
        </div>

        <!-- ~~ STOP PUTTING YOUR CODE AFTER HERE ------------------------------------------------------------------------------- -->
    </main>
</asp:Content>
