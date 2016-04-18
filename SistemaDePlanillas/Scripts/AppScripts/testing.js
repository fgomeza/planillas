define(['jquery'], function ($) {

    var testingApp = {};

    testingApp.action = function (group, operation, args, callback) {
        if (typeof args === 'function') {
            callback = args;
            args = '';
        }
        args = "[" + args + "]";
        var xhr = {
            url: "/api/action/" + group + "/" + operation,
            type: "POST",
            data: args,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) { callback(data); },
            error: function () { callback({ status: 'ERROR', error: 404, detail: 'Sin respuesta del servidor' }); }
        };
        $.ajax(xhr);
        console.log('requesting ' + xhr.url + ' args=' + xhr.data);
    }

    testingApp.actionMethod = function (group, operation, method, args, callback) {
        $.ajax({
            url: "/api/action/" + group + "/" + operation + "/" + method,
            type: "POST",
            data: JSON.stringify(args),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) { callback(JSON.parse(data)); },
            error: function () { callback({ status: 'ERROR', error: 404, detail: 'Sin respuesta del servidor' }); }
        });
    }

    testingApp.actionString = function (group, operation, args, callback) {
        args = '[' + args + ']';
        $.ajax({
            url: '/api/action/' + group + '/' + operation,
            type: 'POST',
            data: args,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) { callback(data); },
            error: function () { callback(JSON.stringify({ status: 'ERROR', error: 404, detail: 'Sin respuesta del servidor' }, null, 2)); }
        });
    }

    return testingApp;

});

