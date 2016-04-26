define(['jquery', 'knockout', 'app/testing'], function ($, ko, app) {
    function Location(data) {
        var data = data || {};
        this.id = ko.observable(data.Id);
        this.name = ko.observable(data.Name);
        this.callPrice = ko.observable(data.CallPrice);
        this.capitalization = ko.observable(data.Capitalization);
        this.lastPayroll = ko.observable(data.LastPayroll);
        this.currentPayroll = ko.observable(data.CurrentPayroll);
        this.active = ko.observable(data.Active);
    }

    function ViewModel() {
        var self = this;

        self.locations = ko.observableArray();

        self.loading = app.consumeAPI('locations', 'get').done(function (data) {
            var mappedData = $.map(data, function (item) { return new Location(item); });
            self.locations(mappedData);
            return self.locations;
        });

    }

    return new ViewModel();
});