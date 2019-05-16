function signIn() {
    username = document.getElementById("user-name").value;
    password = document.getElementById("user-password").value;
    user = executeRestApiRequest("POST", `user/signin?login=${username}&password=${password}`);
    if (user === null) {
        alert("sign in error");
        return;
    }
    setToken(user.Token);
    document.location.href = "Main"
}