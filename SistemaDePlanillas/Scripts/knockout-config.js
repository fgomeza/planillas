define('appViewModel', ['ko'], function (ko) {
    function mainViewModel() {
        var self = this;
        self.partialViewTitle = ko.observable();
        self.applicationName = "Sistema de planillas";
        self.applicationTitle = ko.computed(function () {
            return self.applicationName() + " - " + self.partialViewTitle();
        })
    }

    var appViewModel = new mainViewModel();

    ko.applyBindings(appViewModel);

    return appViewModel;
});
