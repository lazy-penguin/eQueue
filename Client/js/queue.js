M.AutoInit();

serverIp = "http://127.0.0.1:1337";
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

$(document).ready(function () {
    $('.modal').modal();
    loadQueueInfo();
    loadQueueUsers();
});

function resetUserName() {
    userNameEdit.value = userName;
}

function loadQueueInfo() {
    queueInfo = {
        name: "Queue Example",
        expires: new Date('2019-12-17T03:24:00')
    };
    queueNameLabel.innerText = queueInfo.name;
    queueExpiresLabel.innerText = "expires in " +
        queueInfo.expires.toLocaleString("en-US", {
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
    userQueueEntry.hidden = true;
    takePlaceButton.classList.remove("disabled");
    exchangePlaceButton.classList.remove("disabled");
    users = [
        { id: 3, name: "Maria" },
        { id: 1, name: "Alex" },
        { id: 2, name: "John" }];
    users.forEach(user => createUserElement(user));
    if (users[users.length - 1].id == userId)
        exchangePlaceButton.classList.add("disabled");
}

function createUserElement(user) {
    if (user.id == userId) {
        userQueueEntry.hidden = false;
        takePlaceButton.classList.add("disabled");
        userQueueEntryName.innerText = user.name;
        queueUsersDiv.removeChild(userQueueEntry);
        queueUsersDiv.appendChild(userQueueEntry);
        return;
    }
    userElement = document.createElement('div');
    userElement.classList.add('collection-item');
    userElement.classList.add('deep-purple');
    userElement.classList.add('lighten-4');
    userElement.innerText = user.name
    queueUsersDiv.appendChild(userElement);
}