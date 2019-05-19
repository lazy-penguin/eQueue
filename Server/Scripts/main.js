M.AutoInit();
queuesDiv = document.getElementById("queues");

function createQueueElement(queue) {
    newQueueElement = document.createElement('a');
    newQueueElement.href = "Queue/" + queue.Link;
    newQueueElement.classList.add('collection-item');
    newQueueElement.classList.add('deep-purple');
    newQueueElement.classList.add('lighten-4');
    newQueueElement.classList.add('black-text');
    newQueueElement.text = queue.Name
    queuesDiv.appendChild(newQueueElement);
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
            queueExpiresDateTime =
                new Date(`${dateNow.getMonth() + 1} ${dateNow.getDate()} ${dateNow.getFullYear()} ${queueExpiresTime}`);
        } else
            queueExpiresDateTime = new Date(queueExpiresDate + " " + queueExpiresTime + ":00");
    }
    console.log(queueExpiresDateTime);
    if (queueName.trim() === "")
        return;

    queue = executeRestApiRequest("POST", "queue/create", getToken(),
        {
            UserId: user.Id,
            Name: queueName,
            UserNickname: user.Name,
            Link: "",
            Timer: queueExpiresDateTime

        });
    if (queue != null) {
        createQueueElement(queue);
        $('#queueName')[0].value = null;
        $('#datepicker')[0].value = null;
        $('#timepicker')[0].value = null;
    }
}

function joinQueue() {
    url = $("#queue-link")[0].value;
    if (true === executeRestApiRequest("GET", `queue/join?link=${url.split("/")[4]}`, getToken()))
        document.location = url;
}

function changeUser() {
    document.location = "./Login.html"
}

initAuthData();
$("#datepicker").datepicker({ container: $("body"), autoClose: true });
$("#timepicker").timepicker({ container: "body", autoClose: true, twelveHour: false });
$('.modal').modal();
$('#username')[0].innerText = user.Name;
queues = executeRestApiRequest("GET", "user/queues", getToken());
queues.forEach(queue => createQueueElement(queue));