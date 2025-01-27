<%@ Page Title="Thank You" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="thankyou.aspx.cs" Inherits="COZYCOOK.Pages.User.thankyou" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Stylesheets" runat="server">
    <link rel="stylesheet" type="text/css" href="/CSS/thankyou.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cartPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <main class="thxMsgMain">
        <div class="thxMsg">
            <h3>Thank you for your order! Your payment is confirmed, and we're on it.
                <br />
                Enjoy your meal!</h3>
            <asp:Button ID="btnBack" CssClass="btnBack" runat="server" Text="Back to menu page" OnClick="btnBack_Click" PostBackUrl="~/Pages/User/Menu.aspx" ForeColor="White" Height="75px" Width="266px" BackColor="#C33A27" />
        </div>
    </main>
</asp:Content>
