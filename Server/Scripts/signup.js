M.AutoInit();

function passwordValidation() {
    if ($("#user-password").val() != $("#user-password-confirm").val()) {
        $("#user-password-confirm").removeClass("valid").addClass("invalid");
        $("#signup").addClass("disabled");
    }
    else {
        $("#user-password-confirm").removeClass("invalid").addClass("valid");
        $("#signup").removeClass("disabled");
    }
}
$("#user-password").on("focusout", passwordValidation);
$("#user-password-confirm").on("keyup", passwordValidation);

function signUp() {
    username = $("#user-name").val();
    password = document.getElementById("user-password").value;
    signUpOk = executeRestApiRequest("POST", `user/signup?login=${username}&password=${password}`, getToken());
    if (!signUpOk) {
        alert("sign up error");
        return;
    }
    document.location.href = "Main"
}