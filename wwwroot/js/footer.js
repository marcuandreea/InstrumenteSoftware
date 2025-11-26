document.addEventListener("DOMContentLoaded", function() {
    const footer = document.querySelector(".footer");

    function checkFooterVisibility() {
        const scrollTop = window.scrollY || document.documentElement.scrollTop;
        const windowHeight = window.innerHeight;
        const documentHeight = document.documentElement.scrollHeight;

        if (documentHeight <= windowHeight || scrollTop + windowHeight >= documentHeight) {
            // Content height is less than or equal to the viewport height or at the bottom of the page
            footer.style.display = "flex";
        } else {
            // Not at the bottom of the page
            footer.style.display = "none";
        }
    }

    // Initial check
    checkFooterVisibility();

    // Check on scroll
    window.addEventListener("scroll", checkFooterVisibility);

    // Check on resize
    window.addEventListener("resize", checkFooterVisibility);
});