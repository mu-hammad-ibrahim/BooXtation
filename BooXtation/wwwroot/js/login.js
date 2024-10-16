
    document.addEventListener('DOMContentLoaded', function() {
    const loginForm = document.getElementById('loginForm');

    loginForm.addEventListener('submit', function(event) {
        event.preventDefault(); 

    const formData = new FormData(loginForm);
    const xhr = new XMLHttpRequest();

    xhr.open('POST', loginForm.action, true);
    xhr.setRequestHeader('X-Requested-With', 'XMLHttpRequest');

    xhr.onload = function() {
            if (xhr.status >= 200 && xhr.status < 400) {
                const response = JSON.parse(xhr.responseText);
    if (response.redirect) {
        window.location.href = response.redirect;
                } else if (response.error) {
        alert(response.error);
                }
            } else {
        alert('An error occurred while processing your request.');
            }
        };

    xhr.send(formData);
    });
});

