define(['jquery', 'knockout'], function ($, ko) {

    function AppHandler() {

        var self = this;

        this.consumeAPI = function (group, operation, args) {
            args = args || {};
            var xhr = {
                url: '/api/action/fromForm/' + group + '/' + operation,
                type: 'POST',
                data: JSON.stringify(args),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
            };

            console.log('requesting ' + xhr.url + ' args = ' + xhr.data);

            return $.ajax(xhr).then(function (response) {
                var $deferred = $.Deferred(function (deferred) {
                    console.log('ajax ' + xhr.url, response);
                    return (response.status === 'OK') ?
                        deferred.resolve(response.data) :
                        deferred.reject(response);
                });

                return $deferred.promise();
            });
        }

        this.formToJSON = function ($formElement) {
            return $formElement.serializeArray().reduce(function (data, x) {
                data[x.name] = x.value;
                return data;
            }, {});
        }

        this.formToArray = function (formID) {
            return $(formID).serializeArray().map(function (current, index, array) {
                return isNaN(current.value) ? current.value : parseFloat(current.value);
            }, []);
        }

        this.showError = function (error) {
            var errorText = error.detail || error.status + ': ' + error.statusText;
            $('#errorText').html(errorText);
            $('#errorHolder').slideDown();
            var timeout = setTimeout(function () {
                $('#errorHolder').slideUp();
            }, 10000);

            $('#errorholder').on('close.bs.alert', function () {
                clearTimeout(timeout);
            });
        }

        this.showLoading = function () {
            $('#divLoading').fadeIn();
        }

        this.hideLoading = function () {
            $('#divLoading').hide();
        }

        this.startLoadingTimeout = function (miliseconds) {
            return setTimeout(function () {
                self.showLoading();
            }, miliseconds);
        }

        this.stopLoadingTimeout = function (loadingTimeout) {
            clearTimeout(loadingTimeout);
            self.hideLoading();
        }

        this.urlParameterExists = function (parameter) {
            var url = window.location.href;

            if (url.indexOf("?" + parameter + "=") != -1)
                return true;
            else if (url.indexOf("&" + parameter + "=") != -1)
                return true;

            return false;
        }

        this.EditableObject = function(data) {
            data = data || {};
            var self = this;
            $.each(data, function (property) {
                if ($.isArray(property))
                    self[property] = ko.observableArray();
                else
                    self[property] = ko.observable();
            });
            this.update(data)
        }

        ko.utils.extend(self.EditableObject.prototype, {
            update: function (data) {
                var self = this;
                $.each(data, function (propertyName, value) {
                    self[propertyName](value);
                });
            }
        });

    }

    return new AppHandler();

});