<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/AdminNavigation.Master" AutoEventWireup="true" CodeBehind="Admin_MainPage.aspx.cs" Inherits="testing.Pages.Admin.Admin_MainPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="stylesheets" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     
     <div class="main-content">
         <div class="container">
             <h1>DASHBOARD</h1>
             <br>

             <asp:LoginView ID="LoginView1" runat="server"></asp:LoginView>
             <!--Start your code here-->
             <div class="dashcontainer">
                 <div class="box-container">
                     <div class="box box-1">
                         <h2>Total Sales</h2>
                         <p>RM <asp:Label ID="lblTotalSales" runat="server" Text="0.00"></asp:Label></p>
                     </div>
                     <div class="box box-2">
                         <h2>Total New Orders</h2>
                         <p><asp:Label ID="lblNewOrders" runat="server" Text="0"></asp:Label></p>
                     </div>
                     <div class="box box-3">
                         <h2>Total New Reservations</h2>
                         <p><asp:Label ID="lblNewReservavtion" runat="server" Text="0"></asp:Label></p>
                     </div>
                 </div>
                 <div class="graph-box">
                     <h2>Sales Graph For Last Week</h2>
                     <canvas id="myChart" width="400" height="200"></canvas>
                     <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
                 </div>
             </div>

             <script>

                 var ctx = document.getElementById('myChart').getContext('2d');
                 var myChart = new Chart(ctx, {
                     type: 'bar',//or line
                     data: {
                         labels: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
                         datasets: [{
                             label: 'Sales',
                             data: <%= salesDataJson%>,
                             backgroundColor: 'rgba(255, 99, 132, 0.2)',
                             borderColor: 'rgba(255, 99, 132, 1)',
                             borderWidth: 1
                         }]
                     },
                     options: {
                         title: {
                             display: true,
                             text: 'Sales for the last week'
                         },
                         scales: {
                             y: {
                                 beginAtZero: true
                             }
                         }
                     }
                 });

             </script>
             <!--end-->
         </div>
     </div>

    


</asp:Content>
