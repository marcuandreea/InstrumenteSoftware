document.addEventListener("DOMContentLoaded", function() {
    const isLogged = localStorage.getItem("isLogged") === "true";
    const userLink = document.getElementById("loginVerify");

    if (isLogged) {
        userLink.href = "user.html";
    } else {
        userLink.href = "login.html";
    }
});