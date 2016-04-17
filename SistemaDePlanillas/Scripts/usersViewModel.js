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

        testingApp.action('users', 'get', function (data) {
            console.log(data);
            data = data.data;
            var mappedUsers = $.map(data, function (item) { return new User(item); });
            self.users(mappedUsers);
        });

    };

    return new UsersViewModel;
});