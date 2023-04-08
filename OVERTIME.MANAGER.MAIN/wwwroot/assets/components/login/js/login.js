function ShowError(input, message) {
    let parent = input.parentElement;
    let small = parent.querySelector('small');
    small.innerText = message;
}

function ShowSuccess(input, message) {
    let parent = input.parentElement;
    let small = parent.querySelector('small');
    small.innerText = '';
}

function checkEmptyError(listInput) {
    let isEmptyError = false;
    listInput.forEach(input => {
        input.value = input.value.trim();
        if (!input.value) {
            isEmptyError = true;
            ShowError(input, 'not be empty!');
        }
        else {
            ShowSuccess(input);
        }
    });

    return isEmptyError;
}

var user = document.getElementById("user");
var pass = document.getElementById("password");
var singup_btn = document.querySelector(".submit");

singup_btn.addEventListener('click', function (e) {
    checkEmptyError([user, pass]);
});