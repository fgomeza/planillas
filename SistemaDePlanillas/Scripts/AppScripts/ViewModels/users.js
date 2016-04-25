define(['jquery', 'knockout', 'app/testing'], function ($, ko, app) {
    function User(data) {
        this.id = ko.observable();
        this.name = ko.observable();
        this.username = ko.observable();
        this.roleId = ko.observable();
        this.roleName = ko.observable();
        this.locationId = ko.observable();
        this.locationName = ko.observable();
        this.email = ko.observable();
        this.active = ko.observable();
        this.cache = function () { };

        this.update(data);
    }

    ko.utils.extend(User.prototype, {
        update: function (data) {
            data = data || {};
            this.id(data.id);
            this.name(data.name);
            this.username(data.username);
            this.roleId(data.roleId);
            this.roleName(data.roleName);
            this.locationId(data.locationId);
            this.locationName(data.locationName);
            this.email(data.email);
            this.active(data.active);
            this.cache.lastestData = data;
        },
        revert: function () {
            this.update(this.cache.lastestData)
        },
        commit: function () {
            this.cache.lastestData = ko.toJS(this);
        }
    });

    function UsersViewModel() {
        var self = this;

        self.users = ko.observableArray([]);
        self.isEditMode = ko.observable(false);
        self.selectedObject = ko.observable();
        self.editingObject = ko.observable();
        self.selectedRow = null;
        self.activeUsers = ko.computed(function () {
            return ko.utils.arrayFilter(self.users(), function (user) { return !user._destroy; });
        });
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

        require(['viewModels/locations'], function (vm) {
            self.locations = vm.locations;
        });

        require(['viewModels/roles'], function (vm) {
            self.roles = vm.roles;
        });

        self.edit = function (data, event) {
            self.selectedObject(data);
            self.editingObject(new User(ko.toJS(data)));
            self.isEditMode(true);
            self.selectedRow = $(event.target.closest('tr'));
            self.selectedRow.addClass('highlight');
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

        self.cancel = function (data) {
            self.closeForm();
        };

        self.saveChanges = function (data) {
            var $form = $('#userEditForm');
            var fields = app.formToJSON($form);
            var args = { name: fields.name, username: fields.username, password: "", email: fields.email, role: parseInt(fields.role), location: parseInt(fields.location) };
            app.consumeAPI('users', 'modify', args).done(function (response) {
                alert('OK');
                var edited = ko.toJS(self.editingObject());
                self.selectedObject().update(edited);
                return response;
            }).fail(function (error) {
                app.showError(error);
                return error;
            }).always(function (response) {
                console.log(response);
                self.closeForm();
            });
        };

        self.delete = function (data) {
            var args = { id: self.editingObject().id() };
            app.consumeAPI('users', 'remove', args).done(function (response) {
                self.selectedObject().active(false);
            }).fail(function (error) {
                app.showError(error);
                return error;
            }).always(function (response) {
                console.log(response);
                self.closeForm();
            });
        }

        self.create = function (data) {
            var $form = $('#createUserForm');
            var fields = app.formToJSON($form);
            var obj = ko.toJS(self.editingObject());
            console.log(obj); return;
            var args = { name: obj.name, username: obj.username, password: obj.password, email: obj.email, role: parseInt(obj.role)};
            app.consumeAPI('users', 'add', args).done(function (response) {
                console.log(response);
            }).fail(function (error) {
                app.showError(error);
                return error;
            }).always(function (response) {
                console.log(response);
                $('#createUserModal').modal('hide');
            });
        }

        app.action('users', 'get').then(function (response) {
            var data = response.data;
            var mappedData = $.map(data, function (item) { return new User(item); });
            self.users(mappedData);
        });

        console.log('inside users vm');

    };

    return new UsersViewModel();
});