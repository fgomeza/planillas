define(['jquery', 'knockout', 'app/testing'], function ($, ko, jsonHandler) {
    function Employee(data) {
        this.id = ko.observable(data.id); 
        this.idCard = ko.observable(data.idCard);
        this.name = ko.observable(data.name);
        this.location = ko.observable(data.location);
        this.account = ko.observable(data.account);
        this.cms = ko.observable(data.cms);
        this.cmsText = ko.observable(data.cmsText);
        this.calls = ko.observable(data.calls);
        this.active = ko.observable(data.active);
        this.salary = ko.observable(data.salary);
    }

    function EmployeesViewModel() {
        var self = this;

        self.employees = ko.observableArray();
        self.isEditMode = ko.observable(false);
        self.editingObject = ko.observable();
        self.activeEmployees = ko.computed(function () {
            return ko.utils.arrayFilter(self.employees(), function (emp) { return !emp._destroy });
        });
        self.strings = {
            id: "identificador",
            idCard: "Cédula",
            name: "Nombre",
            location: "Sede",
            account: "Cuenta bancaria",
            cms: "cms",
            cmsText: "cmsText",
            calls: "Llamadas",
            active: "Activo",
            salary: "Salario",
            createEmployeeLink: 'Agregar un asociado',
            createEmployeeModalTitle: 'Creación de asociados'
        };

        self.edit = function (data) {
            console.log(data);
            self.isEditMode(true);
            self.editingObject(data);
            $('body').animate({ scrollTop: $('#employeesEditSection').offset().top }, 'slow');
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
            self.employees.destroy(data);
            self.cancel();
        }

        jsonHandler.action('employees', 'get', function (data) {
            data = data.data;
            var mappedData = $.map(data, function (item) { return new Employee(item); });
            self.employees(mappedData);
        });

    };

    return new EmployeesViewModel();
});