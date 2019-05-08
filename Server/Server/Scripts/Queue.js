var parsedUrl = document.location.href.split("/");
var id = parsedUrl[parsedUrl.length - 1];
fetch(document.location.origin + "/REST/Queue/Name?Id=" + id, { method: 'GET' })
    .then((response) => response.json())
    .then((body) => { document.getElementById("queue-name").innerText = "Queue name is " + body; })
