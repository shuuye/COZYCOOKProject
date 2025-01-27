<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="testing._index" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <main>

        <div id="mainBanner">
            <div class="mainBannerText">
                <h2 style="font-weight: 500;">Indulge in a culinary journey of exquisite flavors crafted with passion and expertise, where every dish tells a story of tradition and innovation. </h2>
                <br>
                <h5 style="font-weight: 100;">Experience the essence of fine dining in an ambiance that blends elegance with warmth, creating memorable moments with every visit.</h5>
                <br>
                <br>
                <button class="bannerButton">
                    <a href="/Pages/User/Menu.aspx">Order Now</a>
                </button>

            </div>

            <video id="mainBannerVd" src="Pic_Homepic/Video/mainBanner.mp4" autoplay muted loop></video>

        </div>


        <div class="our-dish">

            <div class="our-dish-box">
                <img src="Pic_Homepic/model1.jpg" alt="Model show 2">
                <div class="caption">
                    <h4>Master of Authentic Flavors</h4>
                    <br>
                    <p>Meet our esteemed chef, renowned for their mastery of authentic Chinese culinary techniques. With years of experience honing their skills in the kitchens of China's diverse regions, our chef brings a wealth of knowledge and tradition to every dish. From hand-pulled noodles to delicate dim sum, each creation is a testament to their dedication to preserving the rich heritage of Chinese cuisine.</p>
                </div>
            </div>

            <div class="our-dish-box">
                <img src="Pic_Homepic/model3.jpg" alt="Model show 3">
                <div class="caption">
                    <h4>Innovator of Fusion Delights</h4>
                    <br>
                    <p>Our chef is a visionary artist, blending the time-honored flavors of Chinese cooking with modern innovation. Drawing inspiration from both traditional recipes and contemporary culinary trends, they craft dishes that are as visually stunning as they are delectable. With a keen eye for balance and harmony, our chef creates an unforgettable dining experience that seamlessly fuses the past with the present.</p>
                </div>
            </div>

            <div class="our-dish-box">
                <img src="Pic_Homepic/model2.jpg" alt="store pic"></a>
                <div class="caption">
                    <h4>Ambassador of Culinary Excellence</h4>
                    <br>
                    <p>At the helm of our kitchen is a true ambassador of Chinese gastronomy, whose passion for food knows no bounds. Trained in the finest culinary institutions and mentored by some of the most esteemed chefs in China, our chef brings a refined palate and unparalleled expertise to every dish. From classic favorites to inventive creations, each plate reflects their commitment to excellence and their unwavering dedication.</p>
                </div>
            </div>
        </div>
    </main>


</asp:Content>
