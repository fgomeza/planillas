define(['jquery', 'knockout', 'app/driver', 'viewModels/locations'], function ($, ko, app, locations) {
    function Employee(data) {
        var self = this;
        this.id = ko.observable(); 
        this.idCard = ko.observable();
        this.name = ko.observable();
        this.locationId = ko.observable();
        this.locationName = ko.observable();
        this.account = ko.observable();
        this.cms = ko.observable();
        this.cmsText = ko.observable();
        this.calls = ko.observable();
        this.active = ko.observable();
        this.salary = ko.observable();
        this.negativeAmount = ko.observable();

        this.salary.formatted = ko.computed(function () {
            return '₡ ' + self.salary();
        }, this);

        this.locationId.subscribe(function (newValue) {
            self.locationName(getNameFromId(locations.locations(), newValue));
        });

        this.update(data);
    }

    ko.utils.extend(Employee.prototype, {
        update: function (data) {
            data = data || {};
            this.id(data.id);
            this.idCard(data.idCard);
            this.name(data.name);
            this.locationId(data.location);
            this.account(data.account);
            this.cms(data.cms);
            this.cmsText(data.cmsText);
            this.calls(data.calls);
            this.active(data.active);
            this.salary(data.salary);
            this.negativeAmount(data.negativeAmount);
        }
    });

    var getNameFromId = function (list, id) {
        var item = ko.utils.arrayFirst(list, function (item) {
            return item.id() == id;
        });

        return item ? item.name() : '';
    };


    function EmployeesViewModel() {
        var self = this;

        self.employees = ko.observableArray();
        self.isEditMode = ko.observable(false);
        self.isCreateMode = ko.observable(false);
        self.isFormOpen = ko.observable(false);
        self.selectedObject = ko.observable();
        self.editingObject = ko.observable();
        self.selectedRow = null;
        self.locations = null;
        self.activeEmployees = ko.computed(function () {
            return ko.utils.arrayFilter(self.employees(), function (emp) { return !emp._destroy });
        });
        self.formTitle = ko.computed(function () {
            return self.isEditMode() ? 'Editar asociado' : 'Registrar nuevo asociado';
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
        
        self.openForm = function () {
            self.isFormOpen(true);
            $('body').animate({ scrollTop: $('#employeesEditSection').offset().top }, 'slow');
        }

        self.closeForm = function () {
            if (self.isEditMode()) self.selectedRow.removeClass('highlight');
            self.selectedRow = null;
            self.selectedObject(null);
            self.editingObject(null);
            self.isEditMode(false);
            self.isCreateMode(false);
            self.isFormOpen(false);
        }

        self.openCreateForm = function () {
            self.editingObject(new Employee());
            self.isEditMode(false);
            self.isCreateMode(true);
            self.openForm();
        }

        self.openEditForm = function (data, event) {
            self.selectedObject(data);
            self.editingObject(new Employee(ko.toJS(data)));
            self.selectedRow = $(event.target.closest('tr'));
            self.selectedRow.addClass('highlight');
            self.isCreateMode(false);
            self.isEditMode(true);
            self.openForm();
        };

        self.submitCreate = function () {
            var obj = ko.toJS(self.editingObject());
            var args, operation = 'add';
            if (obj.CMS) {
                operation += '/CMS';
                args = { idCard: obj.idCard, idCMS: obj.idCMS, name: obj.name, BCRAccount: obj.account };
            } else {
                operation += '/nonCMS';
                args = { idCard: obj.idCard, name: obj.name, BCRAccount: obj.account, salary: obj.salary };
            }

            app.consumeAPI('employees', operation, args).done(function (data) {
                self.employees.push(new Employee(data));
                self.editingObject();
                return data;
            }).fail(function (error) {
                app.showError(error);
                return error;
            }).always(function () {
                self.closeForm();
            });
        }

        self.submitChanges = function (data) {
            var obj = ko.toJS(self.editingObject);
            var args, operation = 'modify';
            if (obj.CMS) {
                operation += '/CMS';
                args = { id: obj.id, idCard: obj.idCard, idCMS: obj.idCMS, name: obj.name, location: obj.locationId, BCRAccount: obj.account };
            } else {
                operation += '/nonCMS';
                args = { id: obj.id, idCard: obj.idCard, name: obj.name, location: obj.locationId, BCRAccount: obj.account, salary: obj.salary };
            }
            
            app.consumeAPI('employees', operation, args).done(function (data) {
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
            app.consumeAPI('employees', 'remove', args).done(function (data) {
                self.employees.destroy(self.selectedObject());
                return data;
            }).fail(function (error) {
                app.showError(error);
                return error;
            }).always(function (response) {
                self.closeForm();
            });
        }

        self.loading = $.when(locations.loading).
            then(function () {
                self.locations = locations.locations;
                return app.consumeAPI('employees', 'get');
            }).done(function (data) {
                var mappedData = $.map(data, function (item) { return new Employee(item); });
                self.employees(mappedData);
            }).fail(function (error) {
                app.showError(error);
                return error;
            });
    };

    return new EmployeesViewModel();
});