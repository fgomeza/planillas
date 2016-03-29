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

var displayAlerts = function () {
    var alert = $('.field-validation-error').closest('.alert');
    alert.show();
}

var fadeOutAlerts = function () {
    $(".alert").addClass("in").fadeOut(4500);
}

var swapOpenCloseSideMenuIcons = function () {
    // swap open/close side menu icons 
    $('[data-toggle=collapse]').click(function () {
        // toggle icon
        $(this).find("i").toggleClass("glyphicon-chevron-right glyphicon-chevron-down");
    });
}

/* Bootstrap feedback for client-side unobtrusive validation */
var setupUnobtrusiveValidationForBootstrap = function () {
    var defaultOptions = {
        validClass: 'has-success',
        errorClass: 'has-error',
        highlight: function (element, errorClass, validClass) {
            $(element).closest(".form-group")
                .removeClass(validClass)
                .addClass(errorClass);
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).closest(".form-group")
                .removeClass(errorClass);
        }
    };

    $.validator.setDefaults(defaultOptions);

    $.validator.unobtrusive.options = {
        errorClass: defaultOptions.errorClass,
        validClass: defaultOptions.validClass,
    };
}

var addAnimationForDropdowns = function () {
    $('nav .dropdown').on('show.bs.dropdown', function (e) {
        $(this).find('.dropdown-menu').first().stop(true, true).slideDown();
    })
    $('nav .dropdown').on('hide.bs.dropdown', function (e) {
        $(this).find('.dropdown-menu').first().stop(true, true).slideUp();
    })
}

$(document).ready(function () {
    //fadeOutAlerts();
    //swapOpenCloseSideMenuIcons();
    displayAlerts();
    addAnimationForDropdowns();
});

$(setupUnobtrusiveValidationForBootstrap(jQuery));
