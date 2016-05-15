define(['knockout'], function (ko) {
    shouter = new ko.subscribable();

    function titleViewModel() {
        var self = this;
        self.partialViewTitle = ko.observable();
        self.applicationName = ko.observable();
        self.applicationTitle = ko.computed(function () {
            if (self.applicationName() && self.partialViewTitle()) {
                return self.applicationName() + " - " + self.partialViewTitle();
            }
            if (self.applicationName() || self.partialViewTitle()) {
                return self.applicationName() || self.partialViewTitle();
            }
        });
        shouter.subscribe(function (value) {
            partialViewTitle(value);
        }, this, 'title');
    }

    return new titleViewModel();
});
