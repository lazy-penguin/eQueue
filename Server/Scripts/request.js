restApiUrl = document.location.origin + "/REST/";

function executeRestApiRequest(method, url, token, body) {
    var req = new XMLHttpRequest();
    req.open(method, restApiUrl + url, false);
    req.setRequestHeader("Authorization", token)
    req.setRequestHeader("Content-Type", "application/json; charset=UTF-8")
    try {
        req.send(JSON.stringify(body));
        if (req.status == 200)
            return JSON.parse(req.responseText);
    }
    catch { }
    return null;
}