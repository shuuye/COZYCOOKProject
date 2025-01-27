// Define an array of admin accounts
const adminAccounts = [
    { AdminID: "1122", passWord: "123456" },
    { AdminID: "2233", passWord: "123456" },
    { AdminID: "5566", passWord: "123456" }
];

// Function to handle form submission
const handleFormSubmit = (event) => {
    event.preventDefault(); // Prevent form submission
    const AdminID = document.getElementById("AdminID").value;
    const passWord = document.getElementById("passWord").value;

    if (AdminID === "" || passWord === "") {
        alert("Please don't leave empty fields.");
    } else {
        let isAdminValid = false;

        for (let i = 0; i < adminAccounts.length; i++) {
            if (adminAccounts[i].AdminID === AdminID && adminAccounts[i].passWord === passWord) {
                isAdminValid = true;
                break;
            }
        }

        if (isAdminValid) {
            // Admin ID and password are valid
            alert("Welcome back!");
            window.location.href = "Admin_MainPage.html";
        } else {
            // Admin ID or password is invalid
            alert("Invalid Admin ID or password.");
        }
    }
};

// Attach form submission handler to the button click event
const submitButton = document.querySelector(".login");
submitButton.addEventListener("click", handleFormSubmit);
