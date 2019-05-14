function register(username, password) {
    userData = {
        id: user.id,
        token: user.token,
        name: username,
        password: password
    }
    fetch(restApiUrl + "/users/signup",
        {
            method: 'POST',
            'Content-Type': "application/json",
            data: JSON.stringify(userData)
        })
        .then((response) => response.json())
        .then((body) => { user = body; })
}