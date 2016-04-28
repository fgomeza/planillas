define(['jquery', 'knockout', 'app/testing'], function ($, ko, app) {
    function Role(data) {
        var data = data || {};
        this.id = ko.observable();
        this.name = ko.observable();
        this.privileges = ko.observableArray();
        this.update(data);
    }

    ko.utils.extend(Role.prototype, {
        update: function (data) {
            var self = this;
            this.id(data.id);
            this.name(data.name);

            var mappedData = $.map(data.privileges, function (value, key) {
                var obj = { group: key, operations: value };
                return new Privilege(obj);
            });
            this.privileges(mappedData);
        }
    });

    function Privilege(data) {
        var self = this;
        this.group = ko.observable();
        this.operations = ko.observableArray();
        this.update(data);
    }

    ko.utils.extend(Privilege.prototype, {
        update: function (data) {
            this.group(data.group);
            this.operations(data.operations);
        }
    });

    function ViewModel() {
        var self = this;

        self.roles = ko.observableArray();

        self.openGroup = function (data, event) {
            var $target = $(event.target);
            $target.parent().find('.collapse.in').collapse('hide');
            $target.next().collapse('toggle');
        }

        self.loading = app.consumeAPI('roles', 'get').done(function (data) {
            var mappedData = $.map(data, function (item) { return new Role(item); });
            self.roles(mappedData);
            return self.roles;
        });

    }

    return new ViewModel();
});