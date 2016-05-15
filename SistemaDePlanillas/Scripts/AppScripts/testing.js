define(['jquery'], function ($) {

    var testingApp = {};

    testingApp.action = function (group, operation, args, callback) {
        if (typeof args === 'function') {
            callback = args;
            args = [];
        }
        var xhr = {
            url: '/api/action/fromCall/' + group + '/' + operation,
            type: 'POST',
            data: JSON.stringify(args),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: callback,
            error: function (error) { callback({ status: 'ERROR', error: error.status, detail: error.statusText }); }
        };
        console.log('requesting ' + xhr.url + ' args=' + xhr.data);
        return $.ajax(xhr);
    }

    testingApp.actionFromForm = function (group, operation, args, callback) {
        if (typeof args === 'function') {
            callback = args;
            args = {};
        }
        var xhr = {
            url: '/api/action/fromForm/' + group + '/' + operation,
            type: 'POST',
            data: JSON.stringify(args),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: callback,
            error: function (error) { callback({ status: 'ERROR', error: error.status, detail: error.statusText }); }
        };
        console.log('requesting ' + xhr.url + ' args=' + xhr.data);
        return $.ajax(xhr);
    }

    testingApp.actionMethod = function (group, operation, method, args, callback) {
        $.ajax({
            url: '/api/action/' + group + '/' + operation + '/' + method,
            type: 'POST',
            data: JSON.stringify(args),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: callback,
            error: function (error) { callback({ status: 'ERROR', error: error.status, detail: error.statusText }); }
        });
    }

    return testingApp;

});

