﻿define(['jquery', 'knockout', 'app/driver', 'viewModels/locations', 'viewModels/roles'], function ($, ko, app, locations, roles) {
    function User(data) {
        var self = this;
        this.id = ko.observable();
        this.name = ko.observable();
        this.username = ko.observable();
        this.roleId = ko.observable();
        this.roleName = ko.observable();
        this.locationId = ko.observable();
        this.locationName = ko.observable();
        this.email = ko.observable();
        this.password = ko.observable();
        this.cache = function () { };

        this.roleId.subscribe(function (newValue) {
            self.roleName(getNameFromId(roles.roles(), newValue));
        });
        this.locationId.subscribe(function (newValue) {
            self.locationName(getNameFromId(locations.locations(), newValue));
        });
        this.update(data);
    }

    ko.utils.extend(User.prototype, {
        update: function (data) {
            data = data || {};
            this.id(data.id);
            this.name(data.name);
            this.username(data.username);
            this.roleId(data.roleId);
            this.locationId(data.locationId);
            this.email(data.email);

            this.cache.lastestData = data;
            this._destroy = data.active === false;
        },
        revert: function () {
            this.update(this.cache.lastestData)
        },
        commit: function () {
            this.cache.lastestData = ko.toJS(this);
        }
    });

    var getNameFromId = function (list, id) {
        var item = ko.utils.arrayFirst(list, function (item) {
            return item.id() == id;
        });

        return item ? item.name() : '';
    };

    function UsersViewModel() {
        var self = this;

        self.selectedRow = null;
        self.locations = null;
        self.roles = null;

        self.users = ko.observableArray([]);
        self.isEditMode = ko.observable(false);
        self.selectedObject = ko.observable();
        self.editingObject = ko.observable();
        self.newUserObj = ko.observable(new User());

        self.activeUsers = ko.computed(function () {
            return ko.utils.arrayFilter(self.users(), function (user) { return !user._destroy; });
        }, this);
        self.usersVisible = ko.computed(function () {
            return self.activeUsers().length > 0;
        }, this);

        self.strings = {
            id: 'Id',
            name: 'Nombre',
            username: 'Usuario',
            password: 'Contraseña',
            role: 'Rol',
            location: 'Sede',
            email: 'Correo electrónico',
            active: 'Activo',
            createUserLink: 'Crear un nuevo usuario',
            createUserModalTitle: 'Creación de usuarios',
            closeModalButton: 'Cerrar',
            SaveModalButton: 'Guardar cambios'
        };

        self.openEditForm = function (data, event) {
            self.selectedObject(data);
            self.editingObject(new User(ko.toJS(data)));
            self.selectedRow = $(event.target.closest('tr'));
            self.selectedRow.addClass('highlight');
            self.isEditMode(true);
            $('body').animate({ scrollTop: $('#usersEditSection').offset().top }, 'slow');
        };

        self.closeForm = function () {
            self.selectedObject(null);
            self.editingObject(null);
            self.isEditMode(false);
            $('body').animate({ scrollTop: 0 }, 'slow');
            self.selectedRow.removeClass('highlight');
            self.selectedRow = null;
        };

        self.submitChanges = function (data) {
            var obj = ko.toJS(self.editingObject);
            var args = { id: obj.id, name: obj.name, username: obj.username, email: obj.email, role: obj.roleId, location: obj.locationId };
            app.consumeAPI('users', 'modify', args).done(function (data) {
                var edited = ko.toJS(self.editingObject());
                self.selectedObject().update(edited);
                return data;
            }).fail(function (error) {
                app.showError(error);
                return error;
            }).always(function (response) {
                self.closeForm();
            });
        };

        self.submitDelete = function (data) {
            var args = { id: self.editingObject().id() };
            app.consumeAPI('users', 'remove', args).done(function (data) {
                self.users.destroy(self.selectedObject());
                return data;
            }).fail(function (error) {
                app.showError(error);
                return error;
            }).always(function (response) {
                self.closeForm();
            });
        }

        self.submitCreate = function (data) {
            var obj = ko.toJS(self.newUserObj());
            var args = { name: obj.name, username: obj.username, password: obj.password, email: obj.email, role: obj.roleId };
            app.consumeAPI('users', 'add', args).done(function (data) {
                self.users.push(new User(data));
                self.newUserObj(new User());
                return data;
            }).fail(function (error) {
                app.showError(error);
                return error;
            }).always(function (response) {
                $('#createUserModal').modal('hide');
            });
        }

        self.usersGrid = new ko.simpleGrid.viewModel({
            data: self.users,
            columns: [
                { headerText: "name", rowText: "name", isVisible:true },
                { headerText: "username", rowText: "username", isVisible: true },
                { headerText: "role", rowText: "roleName", isVisible: true },
                { headerText: "location", rowText: "locationName", isVisible: true },
                { headerText: "email", rowText: "email", isVisible: true }
            ],
            pageSize: -1
        });

        function editButton() {
            return "<a href=\"#\" data-bind=\"click: $root.openEditForm\">\
                        <span class=\"glyphicon glyphicon-pencil\"></span>\
                    </a>";
        }

        self.loading = $.when(roles.loading, locations.loading).
            then(function () {
                self.roles = roles.roles;
                self.locations = locations.locations;
                return app.consumeAPI('users', 'get');
            }).done(function (data) {
                var mappedData = $.map(data, function (item) { return new User(item); });
                self.users(mappedData);
            }).fail(function (error) {
                app.showError(error);
                return error;
            });
    };

    return new UsersViewModel();
});