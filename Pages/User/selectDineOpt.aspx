<%@ Page Title="Dining Choice " Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="selectDineOpt.aspx.cs" Inherits="COZYCOOK.Pages.User.selectDineOpt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Stylesheets" runat="server">
    <link href="../../CSS/selectDineOpt.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cartPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

     <main id="main">
        
        <div class="bannerHeader">
            <h3>Select your dining option:</h3><br>
            <a href="Menu.aspx"> < Back</a>
        </div>
        <div class="buttoncontainer">
            <button class="bannerButton">
                <a href="paymentDineIn.aspx">Dine-In</a>
            </button>
            <button class="bannerButton">
                <a href="paymentDelivery.aspx">Delivery</a>
            </button>
        </div>
    </main>


</asp:Content>
