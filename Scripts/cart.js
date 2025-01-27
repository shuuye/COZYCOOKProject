const cartIcon = document.querySelector("#cart-icon");
const cart = document.querySelector(".cart");
const closeCart = document.querySelector("#close-cart");

// open cart 
cartIcon.onclick = () => {
    cart.classList.add("active");
}

//close cart 
closeCart.onclick = () => {
    cart.classList.remove("active");
}

//check if cart working is
if (document.readyState == "loading") {
    document.addEventListener("DOMContentLoaded", ready);
} else {
    ready();
}

// call the needed function
function ready() {
  
}

function on(x) {
    document.getElementById("background").style.display = "block";
    var overlaypage = document.getElementsByClassName("overlay");

    document.getElementsByClassName("overlay")[x].style.display = "block";

}

function off() {
    document.getElementById("background").style.display = "none";
    var overlaypage = document.getElementsByClassName("overlay");

    for (let i = 0; i <= overlaypage.length; i++) {
        document.getElementsByClassName("overlay")[i].style.display = "none";
    }

}
