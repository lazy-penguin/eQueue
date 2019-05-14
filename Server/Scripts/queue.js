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
    queueInfo = executeRestApiRequest("GET", "user/queues", token);
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
    users.forEach(userData => createUserElement(userData));
    if (users[users.length - 1].id == user.id)
        exchangePlaceButton.classList.add("disabled");
}

function createUserElement(userData) {
    if (userData.id == user.id) {
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
    userElement.innerText = userData.name
    queueUsersDiv.appendChild(userElement);
}

$('.modal').modal();
loadQueueInfo();
loadQueueUsers();