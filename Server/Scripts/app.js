function initAuthData() {
    if (getToken() !== undefined) {
        user = executeRestApiRequest("GET", "user/signin", getToken());
    }
    if (getToken() === undefined || user === null) {
        user = executeRestApiRequest("GET", "user/signup")
        setToken(user.Token);
        if (user === null)
            document.location = "/Login";
    }

    document.getElementById("temporary-login-text").innerText = user.IsTemporary ? "temporary " : "";
    document.getElementById("username").innerText = user.Name;
    if (user.IsTemporary)
        document.getElementById("signup").classList.remove("hide");
    else
        document.getElementById("signin").innerText = "change user"

    return user;
}

function getCookie(name) {
    var matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}

function getToken() {
    return getCookie("token");
}

function setToken(token) {
    document.cookie = `token=${token}`;
}

function executeRestApiRequest(method, url, token, body) {
    var req = new XMLHttpRequest();
    req.open(method, document.location.origin + "/REST/" + url, false);
    req.setRequestHeader("Authorization", token)
    req.setRequestHeader("Content-Type", "application/json; charset=UTF-8")
    req.send(JSON.stringify(body));
    if (req.status == 200)
        return JSON.parse(req.responseText);
    return null;
}