define(['jquery', 'knockout', 'app/testing'], function ($, ko, testingApp) {
    function User(data) {
        this.id = ko.observable(data.Id);
        this.name = ko.observable(data.Name);
        this.username = ko.observable(data.Username);
        this.password = ko.observable(data.Password);
        this.role = ko.observable(data.Role);
        this.location = ko.observable(data.Location);
        this.email = ko.observable(data.Email);
        this.session = ko.observable(data.Session);
    }

    function UsersViewModel () {
        var self = this;
        
        self.users = ko.observableArray();
        self.labels = {
            id: 'Id',
            name: 'Nombre',
            username: 'Usuario',
            password: 'Contraseña',
            role: 'Rol',
            location: 'Sede',
            email: 'Correo electrónico',
            session: 'Sesión'
        };

        testingApp.action('users', 'get', function (data) {
            data = data.data;
            var mappedData = $.map(data, function (item) { return new User(item); });
            self.users(mappedData);
        });

    };

    return UsersViewModel;
});