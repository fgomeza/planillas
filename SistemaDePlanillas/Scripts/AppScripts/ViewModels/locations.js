define(['jquery', 'knockout', 'app/testing'], function ($, ko, testingApp) {
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

        testingApp.consumeAPI('locations', 'get').done(function (response) {
            var data = response.data;
            var mappedData = $.map(data, function (item) { return new Location(item); });
            self.locations(mappedData);
        });

    }

    return new ViewModel();
});