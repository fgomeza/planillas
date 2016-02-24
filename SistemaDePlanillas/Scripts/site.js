// Write your Javascript code.

var action = function (group, operation, args, callback) {
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

var action = function (group, operation, method, args, callback) {
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

var actionString = function (group, operation, args, callback) {
    args = "[" + args + "]";
    var x = JSON.parse(args);
    $.ajax({
        url: "/api/action/" + group + "/" + operation,
        type: "POST",
        data: args,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) { callback(data); },
        error: function () { callback({ status: 'ERROR', error: 404, detail: 'Sin respuesta del servidor' }); }
    });
}
