let javafname = JSON.parse(localStorage.getItem("lfname"));
let javalname = JSON.parse(localStorage.getItem("llname"));
let javaemail = JSON.parse(localStorage.getItem("lemail"));
let javapass = JSON.parse(localStorage.getItem("lpass"));
let javacpass = JSON.parse(localStorage.getItem("lcpass"));

if (!javaemail || !javaemail.length) { //if nothings then create array 
    javafname = [];
    javalname = [];
    javaemail = [];
    javapass = [];
    javacpass = [];
}


const submit_button = document.querySelector(".button");
submit_button.onclick = () => {

    const fname = document.getElementById("fname").value;
    const lname = document.getElementById("lname").value;
    const email = document.getElementById("email").value;
    const pass = document.getElementById("pass").value;
    const cpass = document.getElementById("cpass").value;

    //Store data in web browser store


    //condition
    if (fname == "" && lname == "" && email == "" && pass == "" && cpass == "") {
        alert("input field has no value!");
    }
    else {
        let haveemail = 0;
        for (var i = 0; i < javaemail.length; i++) {
            if (javaemail[i] == email) {
                haveemail = 1;
                break;
            } else {
                haveemail = 0;
            }
        }
        if (haveemail == 1) {
            alert("Email is used by other user. Please register your account with a new email address");
        } else {
            if (pass.length >= 6 && pass.length <= 20) {
                if (pass !== cpass) {
                    alert("Password not matching!,Error");
                }
                else {
                    alert("Register successful!");

                    javafname.push(fname);
                    javalname.push(lname);
                    javaemail.push(email);
                    javapass.push(pass);
                    javacpass.push(cpass);


                    tmpfname = JSON.stringify(javafname);
                    tmplname = JSON.stringify(javalname);
                    tmpemail = JSON.stringify(javaemail);
                    tmppass = JSON.stringify(javapass);
                    tmpcpass = JSON.stringify(javacpass);


                    localStorage.setItem("lfname", tmpfname);
                    localStorage.setItem("llname", tmplname);
                    localStorage.setItem("lemail", tmpemail);
                    localStorage.setItem("lpass", tmppass);
                    localStorage.setItem("lcpass", tmpcpass);

                    setTimeout(() => {
                        location.href = "login.html";
                    }, 5000)
                }
            } else {
                alert("Input min six digit password!");
            }
        }


    }
}

const login = document.querySelector(".login");
login.onclick = (e) => {
    e.preventDefault();
    // cautch the value which is type user login page
    const emailAddress = document.getElementById("emailAddress").value;
    const passWord = document.getElementById("passWord").value;


    // let"s get value in localstorage which store user in registration field
    const Email = JSON.parse(localStorage.getItem("lemail"));
    const Password = JSON.parse(localStorage.getItem("lcpass"));


    if (emailAddress == "" && passWord == "") {

        alert("input field has no value!");
    }else {
        let wrongpass = 0;
        let wrongemail = 0;
        for (var i = 0; i < Email.length; i++) {
            if (Email[i] == emailAddress) {
                wrongemail = 0;
                console.log(Email[i])
                if (Password[i] == passWord) {
                    wrongpass = 0;
                    console.log(Password[i])
                    location.href = "index.html"
                    localStorage.setItem("currentEmail", javaemail[i]);
                    localStorage.setItem("currentUser", javafname[i]);
                    alert("Login Successfully");
                    break;                    

                } else {
                    wrongpass = 1;
                }
            } else {
                wrongemail = 1;   
            }


        }
        if(wrongemail == 1 && wrongpass ==1){
            alert("Wrong Password or Email");
        }else if(wrongpass == 1){
            alert("Incorrect Password");
        }

    }
};

