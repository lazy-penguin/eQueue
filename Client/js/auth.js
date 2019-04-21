token = undefined;
userName = "exampleUserName";
userId = 1;

$(document).ready(function () {
    if (this.token === undefined) {
        //document.location = "./Login.html"
        //    token = getTemporaryUserToken()
    }
});

function getTemporaryUserToken() {
    var req = new XMLHttpRequest();
    req.open("GET", serverIp + "/users/login", false);
    try {
        req.send();
        if (xhr.status == 200) {
            resp = JSON.parse(req.responseText);
            return resp.token;
        }
    }
    catch { }
    return null;
}