<%@ Page Title="Sign Up" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="COZYCOOK.Pages.User.signup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Stylesheets" runat="server">
    <link rel="stylesheet" type="text/css" href="../../CSS/signup.css">
    <style>
        .single-input {
            width: 100%;
            padding: 15px 15px;
            border: 1px solid rgba(0, 0, 0, 0.3521);
            outline: none;
            margin-bottom: 10px;
            font-size: 15px;
            color: black;
        }

        .auto-style1 {
            height: 20px;
        }

        .auto-style2 {
        }

        .auto-style3 {
           width:100%;
        }

        .submit-button {
            float: right;
            border: none;
            outline: none;
            background: var(--greycolor);
            color: #fff;
            font-size: 22px;
            border-radius: 35px;
            text-align: center;
            position: relative;
            overflow: hidden;
            cursor: pointer;
            padding: 1%;
        }
        .auto-style6 {
            width: 42px;
            padding:2%;
           vertical-align: top
        }
        .auto-style7 {
           
            width: 238px;
            padding-right: 14%;
            padding-bottom:2%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main-parent">
        <div class="form-wrap">
            <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" CancelDestinationPageUrl="~/Pages/User/login.aspx" ContinueDestinationPageUrl="~/Pages/User/login.aspx" Width="100%" DisplayCancelButton="True" CancelButtonText="Back to LOGIN" OnCreatedUser="CreateUserWizard1_CreatedUser">
                <CreateUserButtonStyle BackColor="Silver" BorderStyle="None" Font-Names="Work Sans" ForeColor="White" CssClass="submit-button " />
                <WizardSteps>
                    <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                        <ContentTemplate>
                            <table class="auto-style3">
                                <tr>
                                    <td align="center" colspan="2" class="auto-style1" style="font-family: 'work Sans'; font-size: 25px; font-variant: small-caps; margin-bottom: 2%; text-align: center; color: #211F20; padding-bottom: 3%;">Sign Up for Your New Account</td>
                                </tr>
                                <tr>
                                    <td align="right" class="auto-style6">
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" Font-Names="Work Sans" ForeColor="#211F20">User Name:</asp:Label>
                                        <br />
                                    </td>
                                    <td class="auto-style7">
                                        <asp:TextBox ID="UserName" CssClass="single-input" runat="server" Font-Names="Work Sans" ForeColor="#211F20"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="auto-style6">
                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" Font-Names="Work Sans" ForeColor="#211F20">Password:</asp:Label>
                                        <br />
                                    </td>
                                    <td class="auto-style7">
                                        <asp:TextBox ID="Password" CssClass="single-input" runat="server" TextMode="Password" Font-Names="Work Sans" ForeColor="#211F20"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="auto-style6">
                                        <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword" Font-Names="Work Sans" ForeColor="#211F20">Confirm Password:</asp:Label>
                                        <br />
                                    </td>
                                    <td class="auto-style7">
                                        <asp:TextBox ID="ConfirmPassword" CssClass="single-input" runat="server" TextMode="Password" Font-Names="Work Sans" ForeColor="#211F20"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword" ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="auto-style6">
                                        <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email" Font-Names="Work Sans" ForeColor="#211F20">E-mail:</asp:Label>
                                    </td>
                                    <td class="auto-style7">
                                        <asp:TextBox ID="Email" CssClass="single-input" runat="server" Font-Names="Work Sans" ForeColor="#211F20"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" ErrorMessage="E-mail is required." ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match." ValidationGroup="CreateUserWizard1" ForeColor="Red"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" style="color: Red;">
                                        <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:CreateUserWizardStep>
                    <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td align="center" style="font-family: 'work Sans'; font-size: 25px; font-variant: small-caps; color: #211F20; padding-bottom: 2%; font-weight: 300; width: 100%;">Complete</td>
                                </tr>
                                <tr>
                                    <td style="font-family: 'work Sans'; width: 100%; color: #211F20; padding-bottom: 6%;">Your account has been successfully created.</td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Button ID="ContinueButton" class="submit-button" runat="server" CausesValidation="False" CommandName="Continue" Text="Continue" ValidationGroup="CreateUserWizard1" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:CompleteWizardStep>
                </WizardSteps>
                <CancelButtonStyle BorderStyle="None" ForeColor="#211F20" />
            </asp:CreateUserWizard>
        </div>
    </div>
</asp:Content>
