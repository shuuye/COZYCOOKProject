<%@ Page Title="Delivery Status" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User_Dstatus.aspx.cs" Inherits="testing.Pages.User.User_Dstatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Stylesheets" runat="server">
    <link rel="stylesheet" type="text/css" href="/CSS/User_Delivery.css" />
    <link rel="stylesheet" type="text/css" href="/CSS/User_Dstatus.css" />
    <link rel="stylesheet" href="/CSS/date-picker.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <body>
        <main class="statusmain">
            <div class="delivery-head">
                <h3>Delivery Status</h3>
            </div>

            <div class="container">
                <ul class="progressbar">
                    <li>
                        <p>Order Sent</p>
                        <i class='bx bx-notepad'></i>
                    </li>
                    <li>
                        <p>Kitchen Cooking</p>
                        <i class='bx bxs-store'></i>
                    </li>
                    <li>
                        <p>On the Way</p>
                        <i class='bx bx-building-house'></i>
                    </li>
                </ul>
            </div>
            <br />
            <div class="contentP">
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                <p>Hello, Your Order Have Successfully !! Please be Patient for your delicious Meal -.- !!</p>
                <p>Delivery ID :<asp:Label ID="lblDeliveryId" runat="server" ForeColor="Black"></asp:Label></p>
                <p>The Current Process at :<asp:Label ID="lblStatus" runat="server"></asp:Label></p>
                

                <!-- Add buttons with onclick event handlers -->
                <div class="btn-container">
                    <asp:Button runat="server" ID="btnMakeNewOrder" CssClass="btn" Text="Make New Order" OnClick="btnMakeNewOrder_Click" PostBackUrl="~/Pages/User/Menu.aspx" />
                    <asp:Button runat="server" ID="btnReceiveOrder" CssClass="btn" Text="Receive Order" OnClick="btnReceiveOrder_Click" PostBackUrl="~/Pages/User/thankyou.aspx" />
                </div>
            </div>
        </main>
</body>

</asp:Content>
