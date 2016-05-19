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

        self.selectedObject = null;
        self.groups = null;

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
            self.isFormOpen(false);
            self.isEditMode(false);
            self.isCreateMode(false);
            self.editingObject(null);
        }

        self.openCreateForm = function () {
            self.editingObject(new Role({groups: self.groups}));
            self.isEditMode(false);
            self.isCreateMode(true);
            self.openForm();
        }

        self.openEditForm = function (data, event) {
            var jsData = ko.toJS(data);
            self.editingObject(new Role(jsData));
            self.selectedObject = data;
            self.isEditMode(true);
            self.isCreateMode(false);
            self.openForm();
        }

        self.openGroup = function (data, event) {
            var $target = $(event.target);

            $target.parent().find('.collapse.in').collapse('hide');
            $target.next().collapse('toggle');
        }

        self.submitDelete = function () {
            var obj = ko.toJS(self.editingObject);
            var args = { id: obj.id };
            app.consumeAPI("roles", "remove", args).done(function (data) {
                self.roles.destroy(self.selectedObject);
            }).fail(function (error) {
                console.error(error);
                app.showError(error);
            }).always(function () {
                self.closeForm();
            });
        }

        self.submitCreate = function () {
            var obj = ko.toJS(self.editingObject);
            var args = { name: obj.name, operations: createOperationsList(obj) };
            app.consumeAPI("roles", "add", args).done(function (data) {
                console.log("it worked!", data);
            }).fail(function (error) {
                console.error(error);
                app.showError(error);
            }).always(function () {
                self.closeForm();
            });
        }

        self.submitChanges = function () {
            var obj = ko.toJS(self.editingObject);
            var args = { id: obj.id, name: obj.name, operations: createOperationsList(obj) };
            app.consumeAPI("roles", "modify", args).done(function (data) {
                var edited = ko.toJS(self.editingObject());
                self.selectedObject.update(edited);
            }).fail(function (error) {
                console.error(error);
                app.showError(error);
            }).always(function () {
                self.closeForm();
            });
        }

        function createOperationsList(obj) {
            var operations = [];
            for (var i = 0; i < obj.groups.length; ++i) {
                var group = obj.groups[i];
                for (var j = 0; j < group.privileges.length; ++j) {
                    var privilege = group.privileges[j];
                    if (privilege.active == true) {
                        operations.push(privilege.id);
                    }
                }
            }

            return operations;
        }

        self.loading = $.when(self.loading, loadRoles(), loadGroups());

        function loadRoles() {
            return app.consumeAPI('roles', 'get/active').done(function (data) {
                var mappedData = $.map(data, function (item) { return new Role(item); });
                self.roles(mappedData);
                return mappedData;
            }).fail(function (error) {
                console.error(error);
                app.showError(error);
            });
        }

        function loadGroups() {
            return app.consumeAPI('roles', 'get/groups').done(function (data) {
                self.groups = data.groups;
                return data.groups;
            }).fail(function (error) {
                console.error(error);
                app.showError(error);
            });
        }

    }

    return new ViewModel();
});