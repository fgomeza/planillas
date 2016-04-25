define(['jquery', 'knockout', 'app/testing'], function ($, ko, testingApp) {
    function Role(data) {
        var data = data || {};
        this.id = ko.observable(data.id);
        this.name = ko.observable(data.name);
    }

    function ViewModel() {
        var self = this;

        self.roles = ko.observableArray();

        testingApp.action('roles', 'get').then(function (response) {
            response = response.data;
            var mappedData = $.map(response, function (item) { return new Role(item); });
            self.roles(mappedData);
        });

    }

    return new ViewModel();
});