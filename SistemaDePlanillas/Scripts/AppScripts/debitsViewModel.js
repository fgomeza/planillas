define(['jquery', 'knockout', 'app/testing'], function ($, ko, testingApp) {
    function User(data) {
        console.log(data);
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
        self.labels = {
            id: 'Id',
            name: 'Nombre',
            username: 'Usuario',
            password: 'Contraseña',
            role: 'Rol',
            location: 'Sede',
            email: 'Correo electrónico',
            active: 'Activo'
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
            $.when($.get('Modals/Template'), $.get('Modals/CreateUser')).done(function (template, createUser) {
                var elem = $('#createUserModal');
                elem.html(template[0]);
                elem.find('.modal-body').html(createUser[0]);
                elem.find('.modal').modal();
            });
        }

        testingApp.action('users', 'get', function (data) {
            data = data.data;
            var mappedData = $.map(data, function (item) { return new User(item); });
            self.users(mappedData);
        });

    };

    return UsersViewModel;
});