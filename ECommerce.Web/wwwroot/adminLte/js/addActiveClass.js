let urlLink = window.location.pathname;

if (urlLink.includes("Create")) {
    let index = urlLink.indexOf('/Create');
    urlLink = urlLink.slice(0, index);
}
if (urlLink.includes("/Edit")) {
    let index = urlLink.indexOf('/Edit');
    urlLink = urlLink.slice(0, index);
}

let test = document.querySelector(`[href="${urlLink}"]`);
if (test.classList.contains("nav-link")) {
    test.classList.add("active");
}