<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="COZYCOOK.Pages.User.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Stylesheets" runat="server">
    <link rel="stylesheet" type="text/css" href="/CSS/login.css">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            height: 53px;
            width: 83px;
        }

        .auto-style3 {
            height: 53px;
            width: 587px;
        }

        .auto-style4 {
            width: 587px;
        height: 80px;
    }

        .single-input {
            width: 100%;
            padding: 16px 10px;
            border: 1px solid rgba(0, 0, 0, 0.3521);
            outline: none;
            background: transparent;
            margin-bottom: 10px;
            font-size: 15px;
            color: black;
        }

        .submit-button {
            float: right;
            border: none;
            outline: none;
            background: var(--greycolor);
            font-size: 22px;
            border-radius: 35px;
            text-align: center;
            position: relative;
            overflow: hidden;
            cursor: pointer;
            padding: 1%;
        }

        .auto-style5 {
            width: 142px;
            padding: 2%;
            vertical-align: central;
        height: 80px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="main-parent">
        <div class="form-wrap">
            <asp:Login ID="Login1" runat="server" CreateUserUrl="~/Pages/User/signup.aspx"
                CreateUserText="Don't have account? Sign up here !" ForeColor="Black" Font-Names="Work Sans" CssClass="logintable" DestinationPageUrl="~/index.aspx" ValidatorTextStyle-ForeColor="Red" LoginButtonText="LOGIN" FailureText="Login failed. Please check your username and password and try again.">
                <LayoutTemplate>
                    <table cellpadding="1" cellspacing="0" style="border-collapse: collapse;">
                        <tr>
                            <td>
                                <table class="auto-style1" cellpadding="0" style="padding: 2%; color: #000000; font-family: 'work Sans'; margin-right: auto; margin-left: auto;">
                                    <tr>
                                        <td align="center" colspan="2" style="padding: 0.5% 0.5% 2% 0.5%; font-size: 2em; font-weight: 350; color: #211f20; font-family: 'work Sans';">LOGIN</td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="auto-style2">
                                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" ForeColor="#211F20" Font-Names="Work Sans">User Name:&nbsp;</asp:Label>
                                        </td>
                                        <td class="auto-style3">
                                            <asp:TextBox ID="UserName" runat="server" CssClass="single-input" Font-Names="Work Sans" ForeColor="#211F20" Width="100%"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1" BorderStyle="None" ForeColor="Red">* Required Field</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="auto-style5">
                                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" ForeColor="#211F20" Font-Names="Work Sans">Password:&nbsp;</asp:Label>
                                        </td>
                                        <td class="auto-style4">
                                            <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="single-input" Font-Names="Work Sans" ForeColor="#211F20" Width="100%"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1" ForeColor="Red">* Required Field</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding: 2%; color: #211F20;">
                                            <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." ForeColor="#211F20" Font-Names="Work Sans" BackColor="#B9B5B7" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2" style="color: Red;">
                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">
                                            <asp:Button runat="server" CommandName="Login" Text="LOGIN" ValidationGroup="Login1" class="submit-button" BackColor="Silver" Font-Names="Work Sans" ForeColor="White" BorderStyle="None" OnClick="Unnamed1_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:HyperLink ID="CreateUserLink" runat="server" NavigateUrl="~/Pages/User/signup.aspx" Font-Names="Work Sans" ForeColor="#211F20">Don&#39;t have account? Sign up here !</asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
            </asp:Login>
        </div>
    </div>
</asp:Content>
