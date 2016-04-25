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

        testingApp.action('locations', 'get').then(function (response) {
            response = response.data;
            var mappedData = $.map(response, function (item) { return new Location(item); });
            self.locations(mappedData);
        });

    }

    return new ViewModel();
});