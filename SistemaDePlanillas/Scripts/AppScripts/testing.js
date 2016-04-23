define(['jquery'], function ($) {

    var testingApp = {};

    testingApp.action = function (group, operation, args, callback) {
        if (typeof args === 'function') {
            callback = args;
            args = [];
        }
        var xhr = {
            url: "/api/action/" + group + "/" + operation,
            type: "POST",
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
            url: "/api/action/" + group + "/" + operation + "/" + method,
            type: "POST",
            data: JSON.stringify(args),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: callback,
            error: function (error) { callback({ status: 'ERROR', error: error.status, detail: error.statusText }); }
        });
    }

    testingApp.consumeAPI = function (group, operation, args, callback) {
        if (typeof args === 'function') {
            callback = args;
            args = {};
        }
        var xhr = {
            url: "/api/action/" + group + "/" + operation,
            type: "POST",
            data: args,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: callback,
            error: function (error) { callback({ status: 'ERROR', error: error.status, detail: error.statusText }); }
        };
        $.ajax(xhr);
        console.log('requesting ' + xhr.url + ' args=' + xhr.data);
    }

    testingApp.formToJSON = function ($formElement) {
        return $formElement.serializeArray().reduce(function (data, x) {
            data[x.name] = x.value;
            return data;
        }, {});
    }

    testingApp.formToArray = function (formID) {
        return $(formID).serializeArray().map(function (current, index, array) {
            return isNaN(current.value) ? current.value : parseFloat(current.value);
        }, []);
    }

    return testingApp;

});

