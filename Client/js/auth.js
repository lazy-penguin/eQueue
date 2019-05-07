restApiUrl = document.location.origin + "/REST";
user = undefined;

function getCookie(name) {
    var matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}

function getTemporaryUser() {
    var req = new XMLHttpRequest();
    req.open("GET", restApiUrl + "/users/signup", false);
    try {
        req.send();
        if (xhr.status == 200)
            return JSON.parse(req.responseText);
    }
    catch { }
    return null;
}

function getUserByToken(token) {
    var req = new XMLHttpRequest();
    req.open("GET", restApiUrl + "/users/login?token=" + token, false);
    try {
        req.send();
        if (xhr.status == 200)
            return JSON.parse(req.responseText);
    }
    catch { }
    return null;
}

token = getCookie("token");
if (token !== undefined) {
    user = getUserByToken(token);
}
if (token === undefined) {
    user = getTemporaryUser();
    // if (user === null)
    //     document.location = "/Login";
}