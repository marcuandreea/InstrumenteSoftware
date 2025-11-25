document.addEventListener("DOMContentLoaded", function() {
    const username = localStorage.getItem("username");
    const accountType = localStorage.getItem("accountType");

    if (username) {
        document.getElementById("username").textContent = username;
    }

    if (accountType) {
        document.getElementById("accountType").textContent = accountType;
    }

    document.getElementById("logout").addEventListener("click", function() {
        localStorage.removeItem("username");
        localStorage.removeItem("accountType");
        localStorage.setItem("isLogged", false);
        window.location.href = "login.html";
        alert("You have been logged out successfully!");
    });
});