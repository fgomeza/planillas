// Write your Javascript code.

function action(group, operation, args, callback) {
    $.ajax({
        url: "/api/action/"+group+"/"+operation,
        type: "POST",
        data: JSON.stringify(args),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) { callback(JSON.parse(data));},
        error: function () { callback({ status: 'ERROR', error: 404, detail: 'Sin respuesta del servidor' });}
    });
}

function actionMethod(group, operation, method, args, callback) {
    $.ajax({
        url: "/api/action/" + group + "/" + operation+"/"+method,
        type: "POST",
        data: JSON.stringify(args),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) { callback(JSON.parse(data)); },
        error: function () { callback({ status: 'ERROR', error: 404, detail: 'Sin respuesta del servidor' }); }
    });
}

function actionString(group, operation, args, callback) {
    args = "[" + args + "]";
    var x = JSON.parse(args);
    $.ajax({
        url: "/api/action/" + group + "/" + operation,
        type: "POST",
        data: args,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) { callback(JSON.stringify(JSON.parse(data),null,2)); },
        error: function () { callback(JSON.stringify({ status: 'ERROR', error: 404, detail: 'Sin respuesta del servidor' },null,2)); }
    });
}


$(document).ready(function () {
    $(".alert").addClass("in").fadeOut(4500);

    /* swap open/close side menu icons */
    $('[data-toggle=collapse]').click(function () {
        // toggle icon
        $(this).find("i").toggleClass("glyphicon-chevron-right glyphicon-chevron-down");
    });
});
