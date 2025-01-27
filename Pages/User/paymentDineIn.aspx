<%@ Page Title="Dine-In CheckOut " Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="paymentDineIn.aspx.cs" Inherits="COZYCOOK.Pages.User.paymentDineIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Stylesheets" runat="server">
    <link rel="stylesheet" type="text/css" href="/CSS/cart.css">
    <link rel="stylesheet" type="text/css" href="/CSS/cartReview.css">
    <link rel="stylesheet" type="text/css" href="/CSS/payment.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cartPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:LoginView ID="LoginView1" runat="server"></asp:LoginView>
    <div class="paymentBody">
        <div class="payment-content">
            <div class="CheckOut">
                <h2>Dine-In Checkout</h2>
            </div>

            <div class="checkoutContent">
                <div class="OrderSummary">
                    <h3>Order Summary</h3>
                </div>
                <div class="OrderDetail">
                    <h3>
                        <br>
                        <br>
                        Cart</h3>
                    <div id="cartReview">


                        <div id="receipt-display">
                            <div class="payment-box">

                                <asp:Repeater ID="rptCart" runat="server" OnItemCommand="rptCart_ItemCommand">
                                    <ItemTemplate>
                                        <div class="payment-cart-box">
                                            <img src='<%# Eval("productImage") %>' alt="menuImage" class="cart-img">
                                            <div class="detail-box">
                                                <div class="cart-product-title"><%# Eval("productName") %></div>
                                                <div class="cart-price">RM <%# Eval("productPrice") %></div>

                                                <div class="cart-quantity">
                                                    <div class="displayqty">Quantity: </div>
                                                    <asp:TextBox ID="txtquantity" class="cart-quantity" runat="server" TextMode="Number"
                                                        Text="1" AutoPostBack="True" OnTextChanged="txtquantity_TextChanged" OnDataBinding="txtquantity_DataBinding"
                                                        CommandArgument='<%# Eval("menu_id") %>'></asp:TextBox>
                                                </div>
                                                <div >
                                                    <asp:ImageButton ID="removecart" class="remove-cart" alt="remove-cart" runat="server"
                                                        CommandName="RemoveCart" CommandArgument='<%# Eval("menu_id") %>' CausesValidation="False"
                                                        ImageUrl="~/Pic_iconfolder/remove-cart.svg" ImageAlign="Right" CssClass="cart-remove" Height="40px" />
                                                </div>
                                            </div>

                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </div>

                            <div class="total">
                                <div class="total-title">Sub-Total: </div>
                                <div class="total-price">
                                    RM
                                    <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>
                                </div>
                            </div>
                        </div>


                    </div>


                </div>
                


                <div class="paymentDetail">
                    <h3>Payment Details</h3>
                    <label for="PaymentMethod">
                        <br>
                        Payment Method:</label><br>
                  
                    <asp:DropDownList ID="ddlPaymentMethod" runat="server" Height="28px" Width="126px">
                        <asp:ListItem>Cash</asp:ListItem>
                        <asp:ListItem>Credit Card</asp:ListItem>
                        <asp:ListItem Value="TNG"></asp:ListItem>
                        <asp:ListItem Value="GrabPay"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                

                <div class="deliveryFee">
                    <div class="deliveryFee-title">Service Charge@10%: </div>
                    <div class="deliveryFee-price" id="serviceCharge">RM 
                        <asp:Label ID="lblService" runat="server" Text="0"></asp:Label></div>
                </div>
                <div class="total">
                    <div class="total-title">Total (Inc. tax): </div>
                    <div class="total-price" id="Total">RM 
                        <asp:Label ID="lblGrandTotal" runat="server" Text="0"></asp:Label>
                    </div>
                </div>

                <div class="button">
                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="button" PostBackUrl="~/Pages/User/selectDineOpt.aspx" ForeColor="Black" />
                    <asp:Button ID="btnPlaceOrder" runat="server" Text="Place Order" CssClass="button" BackColor="#C33A27" OnClick="btnPlaceOrder_Click" />
                    <!--onclick="placeOrder()-->
                    <br>
                    <br>
                </div>
            </div>

        </div>
    </div>

   

</asp:Content>
