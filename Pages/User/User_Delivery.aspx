<%@ Page Title="Delivery" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User_Delivery.aspx.cs" Inherits="testing.Pages.User.User_Delivery" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
    <link rel="stylesheet" type="text/css" href="/CSS/User_Delivery.css" />
    <link rel="stylesheet" href="/CSS/date-picker.css">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <body>
        <div class="delivery-head">
            <h3>Delivery Details</h3>
            <p>*Select a Delivery Date and Time</p>
            <br />
             <a href="paymentDelivery.aspx"> &lt; Back</a>
            <br />
        </div>

        <div class="delivery-form">
            <br />
            <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName" Text="Name:"></asp:Label>
            <asp:TextBox ID="txtName" runat="server" required maxlength="20" title="Enter your full name."></asp:TextBox>
            <br />
            <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" Text="Email:"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" required title="Enter email with correct format."></asp:TextBox>
            <br />
            <asp:Label ID="lblDateTime" runat="server" AssociatedControlID="txtDateTime" Text="Date and Time:"></asp:Label>
            <asp:TextBox ID="txtDateTime" runat="server" TextMode="DateTimeLocal" required></asp:TextBox>
            <br />
            <asp:Label ID="lblAddress" runat="server" AssociatedControlID="txtAddress" Text="Address:"></asp:Label>
            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" required maxlength="150"></asp:TextBox>
            <br />
            <asp:Label ID="Label1" runat="server" AssociatedControlID="txtAddress" Text="Phone Number:"></asp:Label>
            <asp:TextBox ID="txtPhoneNum" runat="server" TextMode="MultiLine" placeholder="e.g., 018 1234567" required pattern="[0-9]+" title="Phone number should only contain numbers."></asp:TextBox>
            <br />
            <asp:Label ID="lblPostcode" runat="server" AssociatedControlID="ddlPostcode" Text="Postcode:" ForeColor="White"></asp:Label>
            <asp:DropDownList ID="ddlPostcode" runat="server" required ForeColor="Black" BackColor="#666666">
                <asp:ListItem Text="Select Postcode" Value="" />
                <asp:ListItem Text="56000" Value="56000" />
                <asp:ListItem Text="53000" Value="53000" />
                <asp:ListItem Text="43000" Value="43000" />
                <asp:ListItem Text="32000" Value="32000" />
                <asp:ListItem Text="12000" Value="12000" />
                <asp:ListItem Text="66000" Value="66000" />
            </asp:DropDownList>
            <br />
            <asp:Label ID="lblCity" runat="server" AssociatedControlID="ddlCity" Text="City:" ForeColor="White"></asp:Label>
            <asp:DropDownList ID="ddlCity" runat="server" required ForeColor="Black" BackColor="#666666">
                <asp:ListItem Text="Select City" Value="" />
                <asp:ListItem Text="Penang" Value="Penang" />
                <asp:ListItem Text="Johor" Value="Johor" />
                <asp:ListItem Text="Melaka" Value="Melaka" />
            </asp:DropDownList>
            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Confirm Details" OnClick="btnSubmit_Click"/>
            <asp:Label ID="ErrorMessageLabel" runat="server" Text=""></asp:Label>
        </div>
  

</body>

</asp:Content>
