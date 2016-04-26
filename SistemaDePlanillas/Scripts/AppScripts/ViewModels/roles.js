define(['jquery', 'knockout', 'app/testing'], function ($, ko, app) {
    function Role(data) {
        var data = data || {};
        this.id = ko.observable(data.id);
        this.name = ko.observable(data.name);
    }

    function ViewModel() {
        var self = this;

        self.roles = ko.observableArray();

        self.loading = app.consumeAPI('roles', 'get').done(function (data) {
            var mappedData = $.map(data, function (item) { return new Role(item); });
            self.roles(mappedData);
            return self.roles;
        });

    }

    return new ViewModel();
});