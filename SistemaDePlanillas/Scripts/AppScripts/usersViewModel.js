define(['jquery', 'knockout', 'app/testing'], function ($, ko, testingApp) {
    function User(data) {
        this.id = ko.observable(data.Id);
        this.name = ko.observable(data.Name);
        this.username = ko.observable(data.Username);
        this.password = ko.observable(data.Password);
        this.role = ko.observable(data.Role);
        this.location = ko.observable(data.Location);
        this.email = ko.observable(data.Email);
        this.active = ko.observable(data.Active);
    }

    function UsersViewModel () {
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

        self.edit = function (data) {
            console.log(data);
            self.isEditMode(true);
            self.editingObject(data);
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

        testingApp.action('users', 'get', function (data) {
            data = data.data;
            var mappedData = $.map(data, function (item) { return new User(item); });
            self.users(mappedData);
        });

    };

    return UsersViewModel;
});