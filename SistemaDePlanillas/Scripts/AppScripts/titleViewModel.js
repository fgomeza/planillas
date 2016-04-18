define(['knockout'], function (ko) {
    shouter = new ko.subscribable();

    function titleViewModel() {
        var self = this;
        self.partialViewTitle = ko.observable();
        self.applicationName = ko.observable("Sistema de planillas");
        self.applicationTitle = ko.computed(function () {
            return self.applicationName() + " - " + self.partialViewTitle();
        });
        shouter.subscribe(function (value) {
            partialViewTitle(value);
        }, this, 'title');
    }

    return new titleViewModel();
});
