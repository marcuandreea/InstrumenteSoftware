document.addEventListener("DOMContentLoaded", function() {
    const loginForm = document.querySelector("form");
    loginForm.addEventListener("submit", function(event) {
        event.preventDefault(); // Prevent the default form submission

        // Get the input values
        const username = document.querySelector("input[type='username']").value;
        const password = document.querySelector("input[type='password']").value;
        localStorage.setItem("isLogged", false);

        // Perform validation (this is just a basic example, you can add more validation as needed)
        if (username == "andreea.lore@yahoo.com" && password == "12345678") {
            // Store the user information in localStorage
            localStorage.setItem("username", username);
            localStorage.setItem("isLogged", true);
            localStorage.setItem("accountType", "Administrator");



            // Redirect to the user page
            window.location.href = "user.html";
        } else {
            alert("Detalii de autentificare incorecte!");
        }
    });
});