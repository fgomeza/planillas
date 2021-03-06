﻿define(['jquery', 'app/testing', 'viewModels/title', 'app/driver'], function ($, app, title, driver) {

    var enviar = function () {
        var $container = $("#result")
        $container.html("");
        if ($("#fromForm").is(':checked')) {
            app.actionFromForm($("#group").val(), $("#operation").val(), JSON.parse("{" + $("#args").val() + "}"), function (res) {
                $container.html(JSON.stringify(res, null, 2))
            });
        }
        else {
            app.action($("#group").val(), $("#operation").val(), JSON.parse("[" + $("#args").val() + "]"), function (res) {
                $container.html(JSON.stringify(res, null, 2))
            });
        }
    };

    function Controller() {

        this.init = function () {
            var $containerElement = $('#testsSection');

            $('#testSubmitBttn').click(function (event) {
                event.preventDefault();
                var timeout = driver.startLoadingTimeout(500);
                enviar();
                driver.stopLoadingTimeout(timeout);
            });

            $containerElement.parent().fadeIn();

            title.partialViewTitle('Backdoor');
        }
    }

    return new Controller();
});
