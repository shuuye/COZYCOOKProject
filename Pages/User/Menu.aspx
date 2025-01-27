<%@ Page Title="Menu" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="menu.aspx.cs" Inherits="COZYCOOK.Pages.User.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Stylesheets" runat="server">
    <link rel="stylesheet" type="text/css" href="/CSS/Menu.css" />
    <link rel="stylesheet" type="text/css" href="/CSS/cart.css" />


</asp:Content>
<asp:Content ID="cartcontent" ContentPlaceHolderID="cartPlaceHolder" runat="server">
    <img src="/Pic_iconfolder/cart-icon.svg" alt="cart-icon" id="cart-icon">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <asp:LoginView ID="LoginView1" runat="server"></asp:LoginView>
        <div class="cart">
            <h2 class="cart-title">Your Cart</h2>
            <div class="cart-content">

                <asp:Repeater ID="rptCart" runat="server" OnItemCommand="rptCart_ItemCommand">
                    <ItemTemplate>
                        <div class="cart-box">

                            <img src='<%# Eval("productImage") %>' alt="menuImage" class="cart-img">
                            <div class="detail-box">
                                <div class="cart-product-title"><%# Eval("productName") %></div>
                                <div class="cart-price">RM <%# Eval("productPrice") %></div>
                                
                                <asp:TextBox ID="txtquantity" class="cart-quantity" runat="server" TextMode="Number" 
                                    Text="1" AutoPostBack="True" OnTextChanged="txtquantity_TextChanged" OnDataBinding="txtquantity_DataBinding"
                                    CommandArgument='<%# Eval("menu_id") %>'
                                    ></asp:TextBox>
                                <asp:ImageButton ID="removecart" class="remove-cart" alt="remove-cart" runat="server" 
                                    CommandName="RemoveCart" CommandArgument='<%# Eval("menu_id") %>'  CausesValidation="False"
                                    ImageUrl="~/Pic_iconfolder/remove-cart.svg" />
                                
                            </div>

                        </div>
                    </ItemTemplate>
                </asp:Repeater>

            </div>
            <div class="total">
                <div class="total-title">Total</div>
                <div class="total-price">RM 
                    <asp:Label ID="lblTotal" runat="server" Text="RM 0.00"></asp:Label></div>
            </div>

            <asp:Button ID="btnBuyNow" class="btn-buy" runat="server" Text="Buy Now" UseSubmitBehavior="False" OnClick="btnBuyNow_Click" />
            

            <img src="/Pic_iconfolder/close-cart.svg" alt="close-cart" id="close-cart">
            <script src="/Scripts/cart.js"></script>
        </div>




        <!-- Food Products Repeater -->
        <section class="shop-container">
            <h2 class="section-title">Food</h2>
            <div class="shop-content clearfix">
                <asp:Repeater ID="rptFood" runat="server" OnItemCommand="rptFood_ItemCommand">
                    <ItemTemplate>
                        <div class="product-box">
                            <img src='<%# Eval("productImage") %>' alt='<%# Eval("productName") %>' class="product-img">
                            <h2 class="product-title"><%# Eval("productName") %></h2>
                            <p class="product-description"><%# Eval("productDescription") %></p>
                            <span class="price">RM <%# Eval("productPrice") %></span>
                            
                            <asp:ImageButton ID="addCartFood" runat="server" CommandName="Select" CommandArgument='<%# Eval("menu_id") %>' ImageUrl="~/Pic_iconfolder/add-cart.svg" alt="cart-icon" class="add-cart"/>
                            
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </section>

        <!-- Define item template for food products -->

        <!-- Beverage Products Repeater -->
        <section class="shop-container">
            <h2 class="section-title">Beverage</h2>
            <div class="shop-content clearfix">
                <asp:Repeater ID="rptBeverage" runat="server" OnItemCommand="rptBeverage_ItemCommand">
                    <ItemTemplate>
                        <div class="product-box">
                            <img src='<%# Eval("productImage") %>' alt='<%# Eval("productName") %>' class="product-img">
                            <h2 class="product-title"><%# Eval("productName") %></h2>
                            <p class="product-description"><%# Eval("productDescription") %></p>
                            <span class="price">RM <%# Eval("productPrice") %></span>
                             <asp:ImageButton ID="addCartBeverage" CommandName="Select" CommandArgument='<%# Eval("menu_id") %>' runat="server" ImageUrl="~/Pic_iconfolder/add-cart.svg" alt="cart-icon" class="add-cart"/>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </section>
        <!-- Define item template for beverage products -->
    </main>

</asp:Content>

