
var onDeviceReady = function () {
    $.ajax(
        {
            type: 'POST', url: 'https://eq.jandslab.com/token',
            data: $.param({ grant_type: 'password', username: 'hanhead@gmail.com', password: '123Qwer@' }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        }
    ).done(function (data) {
        consolewriteline(data);
        sessionStorage.setItem('token', data);
    });
};

var consolewriteline = function (log) {
    var $console = $('#console');
    $console.prepend(`<div>${log}</div>`);
}

document.addEventListener('deviceready', onDeviceReady, false);
