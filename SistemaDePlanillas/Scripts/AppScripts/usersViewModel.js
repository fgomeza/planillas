define(['jquery', 'knockout', 'app/testing'], function ($, ko, testingApp) {
    function User(data) {
        this.id = ko.observable(data.id);
        this.name = ko.observable(data.name);
        this.username = ko.observable(data.username);
        this.role = ko.observable(data.role);
        this.location = ko.observable(data.location);
        this.email = ko.observable(data.email);
        this.active = ko.observable(data.active);
    }

    function UsersViewModel() {
        var self = this;

        self.users = ko.observableArray([]);
        self.isEditMode = ko.observable(false);
        self.editingObject = ko.observable();
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

        self.edit = function (data, event) {
            console.log(data);
            self.isEditMode(true);
            self.editingObject(data);
            //$(event.target.closest('tr')).addClass('highlight');
            $('body').animate({ scrollTop: $('#usersEditSection').offset().top }, 'slow');
        };

        self.cancel = function (data) {
            self.isEditMode(false);
            self.editingObject(null);
            $('body').animate({ scrollTop: 0 }, 'slow');
        };

        self.save = function (data) {
            self.cancel();
        };

        self.delete = function (data) {
            self.users.destroy(data);
            self.cancel();
        }

        self.create = function (data) {
            var form = testingApp.formToArray('#createUserForm');
            //args = form.name 
            testingApp.action('users', 'add', form, function (response) {
                console.log(response);
            });
        }

        testingApp.action('users', 'get', function (response) {
            response = response.data;
            var mappedData = $.map(response, function (item) { return new User(item); });
            self.users(mappedData);
        });

    };

    return UsersViewModel;
});