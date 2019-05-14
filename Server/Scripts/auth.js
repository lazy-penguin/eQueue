user = undefined;

function getCookie(name) {
    var matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}

token = getCookie("token");
if (this.token !== undefined) {
    user = executeRestApiRequest("GET", "user/signin", token);
}
if (this.token === undefined || user === null) {
    user = executeRestApiRequest("GET", "user/signup")
    document.cookie = `token=${user.Token}`;
    if (user === null)
        document.location = "/Login";
}

document.getElementById("temporary-login-text").innerText = user.IsTemporary ? "temporary " : "";
document.getElementById("username").innerText = user.Name;
document.getElementById("signup").hidden = !user.IsTemporary;