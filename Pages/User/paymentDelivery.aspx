<%@ Page Title="Delivery CheckOut" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="paymentDelivery.aspx.cs" Inherits="COZYCOOK.Pages.User.paymentDelivery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Stylesheets" runat="server">

    <link rel="stylesheet" type="text/css" href="/CSS/cart.css">
    <link rel="stylesheet" type="text/css" href="/CSS/cartReview.css">
    <link rel="stylesheet" type="text/css" href="/CSS/payment.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cartPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">


    <div class="payment-content">

        <div class="CheckOut">
            <h2>Delivery Checkout</h2>
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
                                            <div>
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

            <br>
            <br>
            <p>Promo Code:</p>
            <asp:TextBox ID="txtPromoCode" runat="server"></asp:TextBox>
            <asp:Button ID="applybtn" runat="server" Text="Apply" CssClass="button" BackColor="#C33A27" ForeColor="White" OnClick="applybtn_Click" />

            <p id="PromoCodeResult"></p>

            <div class="deliveryFee">
                <div class="deliveryFee-title">Applied Promotion (Minus - <asp:Label ID="lblPercentage" runat="server" Text=""></asp:Label>): </div>
                <div class="deliveryFee-price" id="Promotion">
                    RM 
                <asp:Label ID="lblPromotion" runat="server" Text="0.00"></asp:Label>
                </div>
            </div>

            <div class="deliveryFee">
                <div class="deliveryFee-title">Delivery Fee: </div>
                <div class="deliveryFee-price" id="DeliveryFee">
                    
                    <asp:Label ID="lblService" runat="server" Text="0.00"></asp:Label>
                </div>
            </div>

            <div class="total">
                <div class="total-title">Total: </div>
                <div class="total-price" id="DelTotal">
                    
                     <asp:Label ID="lblGrandTotal" runat="server" Text="0.00" />
                </div>
            </div>

            <div class="button">
                <asp:Button ID="btnBack" runat="server" Text="Back" PostBackUrl="~/Pages/User/selectDineOpt.aspx" CssClass="button" ForeColor="Black" />
                <asp:Button ID="btnProceed" runat="server" Text="Proceed" CssClass="button"  BackColor="#C33A27" OnClick="btnProceed_Click" />
                <br>
                <br>
            </div>
        </div>
    </div>
  

</asp:Content>
