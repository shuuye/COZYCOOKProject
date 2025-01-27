<%@ Page Title="FAQs" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="faq.aspx.cs" Inherits="COZYCOOK.Pages.User.faq" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Stylesheets" runat="server">
     <link rel="stylesheet" type="text/css" href="/CSS/faq.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cartPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

     <main class="FAQmain">
     <div class="FAQ">
         <hr />
         <br /><br />
         <h2>FAQ</h2>
     </div>
     <div class="FAQcontent">
         <h4>What are your opening hour?</h4>
         <p>
             Our opening hours are as follows: <br /><br />

             Monday - Friday:<br /><br />
             Breakfast service: 7:00 AM - 10:00 AM <br />
             Lunch / Dinner: 11:00 AM - 10:00 PM<br /><br />
             Weekend (Saturday and Sunday):<br /><br />

             Brunch: 9:00 AM - 11:00 AM<br />
             Lunch / Dinner: 1:00 PM - 10:00 PM<br />
         </p>
         <h4>Are reservations required?</h4>
         <p>
             Whether you're planning a special celebration or simply want to secure a table during peak hours, booking
             in advance allows us to accommodate your needs efficiently.<br /><br />
             Please feel free to contact us to make a reservation, and our team will be delighted to assist you.
         </p>
         <h4>What payment methods do you accept?</h4>
         <p>We accept cash, credit/debit cards, and e-wallet too.</p>
         <h4>Do you have parking facilities?</h4>
         <p>
             Yes, we have parking facilities available nearby. <br /><br />
             Our parking area is conveniently located within walking distance, ensuring ease of access for diners.
         </p>
         <h4>Can I host private events at your restaurant?</h4>
         <p>
             Yes, we offer event spaces and catering services for private parties, celebrations, and corporate
             events.<br /><br />
             Our event spaces are designed to accommodate a variety of gatherings, whether it's an intimate birthday party, a lavish wedding reception, or a corporate luncheon. <br /><br />
             From elegant banquet halls to cozy private dining rooms, we have versatile spaces that can be customized to suit your event requirements.
         </p>
     </div>
 </main>

</asp:Content>
