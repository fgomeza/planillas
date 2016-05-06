define(['jquery', 'knockout', 'app/driver'], function ($, ko, app) {
    function Role(data) {
        var data = data || {};
        this.id = ko.observable();
        this.name = ko.observable();
        this.groups = ko.observableArray();
        this.update(data);
    }

    ko.utils.extend(Role.prototype, {
        update: function (data) {
            this.id(data.id);
            this.name(data.name);

            var mappedData = $.map(data.groups, function (item) { return new Group(item); });
            this.groups(mappedData);
        }
    });

    function Group(data) {
        data = data || {};
        this.id = ko.observable();
        this.description = ko.observable();
        this.privileges = ko.observableArray();
        this.update(data);
    }

    ko.utils.extend(Group.prototype, {
        update: function (data) {
            this.id(data.id);
            this.description(data.description);

            var mappedData = $.map(data.privileges, function (item) { return new Privilege(item); });
            this.privileges(mappedData);
        }
    });

    function Privilege(data) {
        data = data || {};
        this.id = ko.observable();
        this.description = ko.observable();
        this.active = ko.observable();
        this.update(data);

        this.active.subscribe(function (newValue) {
            console.log(newValue);
        });
    }

    ko.utils.extend(Privilege.prototype, {
        update: function (data) {
            this.id(data.id);
            this.description(data.description);
            this.active(data.active);
        }
    });

    function ViewModel() {
        var self = this;

        self.strings = {
            createRoleLink: "Crear Rol"
        };

        self.roles = ko.observableArray();
        self.isFormOpen = ko.observable(false);
        self.isEditMode = ko.observable(false);
        self.isCreateMode = ko.observable(false);
        self.editingObject = ko.observable();
        self.formTitle = ko.computed(function () {
            return self.isEditMode() ? 'Editar Rol' : 'Registrar un nuevo rol';
        });

        self.openForm = function () {
            self.isFormOpen(true);
        }

        self.closeForm = function () {
            self.editingObject();
            self.isEditMode(false);
            self.isCreateMode(false);
            self.isFormOpen(false);
        }

        self.openCreateForm = function () {
            self.isEditMode(false);
            self.editingObject(new Role());
            self.openForm();
        }

        self.openEditForm = function (data, event) {
            self.editingObject(data);
            self.isEditMode(true);
            self.isCreateMode(false);
            self.openForm();
        }

        self.openGroup = function (data, event) {
            var $target = $(event.target);

            $target.parent().find('.collapse.in').collapse('hide');
            $target.next().collapse('toggle');
        }

        self.submitDelete = function () { }
        self.submitCreate = function () { }
        self.submitChanges = function () { }

        self.loading = app.consumeAPI('roles', 'get').done(function (data) {
            var mappedData = $.map(data, function (item) { return new Role(item); });
            self.roles(mappedData);
            return self.roles;
        });

    }

    return new ViewModel();
});