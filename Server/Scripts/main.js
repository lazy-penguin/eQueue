M.AutoInit();

restApiUrl = document.location.origin + "/REST";
queues = document.getElementById("queues");

function loadQueues() {
    var req = new XMLHttpRequest();
    req.open("GET", serverIp + "/queue", false);
    req.setRequestHeader('Authorization', `Token ${token}`);
    try {
        req.send();
        if (xhr.status == 200) {
            resp = JSON.parse(req.responseText);
            resp.queues.forEach(queue => {
                createQueueElement(queue.name);
            });
        }
    }
    catch { }
}

function createQueueElement(queueName) {
    newQueueElement = document.createElement('a');
    newQueueElement.href = "Queue/" + queueName;
    newQueueElement.classList.add('collection-item');
    newQueueElement.classList.add('deep-purple');
    newQueueElement.classList.add('lighten-4');
    newQueueElement.classList.add('black-text');
    newQueueElement.text = queueName
    queues.appendChild(newQueueElement);
}

function createQueue() {
    queueName = $('#queueName')[0].value;
    queueExpiresDate = $('#datepicker')[0].value;
    queueExpiresTime = $('#timepicker')[0].value;
    queueExpiresDateTime = null;
    if (queueExpiresDate || queueExpiresTime) {
        if (!queueExpiresTime)
            queueExpiresDateTime = new Date(queueExpiresDate);
        else if (!queueExpiresDate) {
            dateNow = new Date();
            queueExpiresDateTime = Date(`${dateNow.getMonth()} ${dateNow.getDay()} ${dateNow.getFullYear()} ${queueExpiresTime}:00`);
        } else
            queueExpiresDateTime = new Date(queueExpiresDate + " " + queueExpiresTime + ":00");
    }
    if (queueName.trim() === "")
        return;

    var req = new XMLHttpRequest();
    req.open("POST", serverIp + "/queue/", false);
    req.setRequestHeader('Authorization', `Token ${token}`);
    try {
        req.send(({ name: queueName, expires: queueExpiresDateTime }));
        if (xhr.status == 200) {
            createQueueElement(queueName);
            $('#queueName')[0].value = null;
            $('#datepicker')[0].value = null;
            $('#timepicker')[0].value = null;
        }
    }
    catch { }
}

function changeUser() {
    document.location = "./Login.html"
}

$("#datepicker").datepicker({ container: $("body"), autoClose: true });
$("#timepicker").timepicker({ container: "body", autoClose: true, twelveHour: false });
$('.modal').modal();
$('#username')[0].innerText = user.Name;
loadQueues();