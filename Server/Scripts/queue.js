M.AutoInit();

queueInfo = null;
queueUsers = null;

queueNameLabel = document.getElementById("queue-name");
queueExpiresLabel = document.getElementById("queue-expires");
queueUsersDiv = document.getElementById("queue-users");
userQueueEntry = document.getElementById("user-queue-entry");
userQueueEntryName = document.getElementById("user-queue-entry-name");
takePlaceButton = document.getElementById("take-place");
exchangePlaceButton = document.getElementById("exchange-place");
changeNameButton = document.getElementById("change-name");
userNameEdit = document.getElementById("modal-username");

function resetUserName() {
    userNameEdit.value = user.name;
}

function loadQueueInfo() {
    queueLink = document.location.href.split("/")[4]
    queueInfo = executeRestApiRequest("GET", `queue/queue?link=${queueLink}`, getToken());
    queueNameLabel.innerText = queueInfo.Name;
    queueExpiresLabel.innerText = "expires in " +
        queueInfo.Timer.toLocaleString("en-US", {
            year: 'numeric',
            month: 'long',
            day: 'numeric',
            weekday: 'long',
            hour: 'numeric',
            minute: 'numeric',
            second: 'numeric'
        });
}

function loadQueueUsers() {
    document.querySelectorAll('.queue-order-user-element').forEach(el => queueUsersDiv.removeChild(el));
    if (queueUsersDiv.contains(userQueueEntry))
        queueUsersDiv.removeChild(userQueueEntry);
    takePlaceButton.classList.remove("disabled");
    exchangePlaceButton.classList.remove("disabled");
    queueUsers = executeRestApiRequest("GET", `order/users?queueId=${queueInfo.Id}`, getToken());
    queueUsers.forEach(userData => createUserElement(userData));
    if (queueUsers.length > 0 && queueUsers[queueUsers.length - 1].Id === user.Id)
        exchangePlaceButton.classList.add("disabled");
}

function createUserElement(userData) {
    if (userData.Id === user.Id) {
        userQueueEntry.hidden = false;
        takePlaceButton.classList.add("disabled");
        userQueueEntryName.innerText = user.Name;
        queueUsersDiv.appendChild(userQueueEntry);
        return;
    }
    userElement = document.createElement('div');
    userElement.classList.add('collection-item');
    userElement.classList.add('deep-purple');
    userElement.classList.add('lighten-4');
    userElement.classList.add('queue-order-user-element')
    userElement.innerText = userData.Login
    queueUsersDiv.appendChild(userElement);
}

function takePlace() {
    executeRestApiRequest("GET", `order/getin?queueId=${queueInfo.Id}`, getToken());
    loadQueueUsers();
}

function leaveOrder() {
    executeRestApiRequest("DELETE", `order/exit?queueId=${queueInfo.Id}`, getToken());
    loadQueueUsers();
}

function swap() {
    userPos = -1;
    while (queueUsers[++userPos].Id !== user.Id);
    executeRestApiRequest("GET", `order/swap?userB=${queueUsers[userPos + 1].Id}&id=${queueInfo.Id}`, getToken());
    loadQueueUsers();
}

$('.modal').modal();
user = initAuthData();
loadQueueInfo();
loadQueueUsers();