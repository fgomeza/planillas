define(['jquery', 'knockout', 'app/driver', 'viewModels/employees'], function ($, ko, app, employees) {

    function ViewModel() {
        var vm = this;

        vm.strings = {
            id: "Idenficador",
            employee: "Asociado",
            detail: "Detalle",
            hours: "Horas extra"
        };

        vm.loading = null;
        vm.employees = null;

        vm.employeeId = ko.observable();
        vm.extras = ko.observableArray();

        vm.loading = $.when(vm.loading, employees.loading).
            then(function () {
                vm.employees = employees.employees;
            });

        vm.employeeId.subscribe(function (newValue) {
            if (newValue == null) return;

            if (app.urlParameterExists("employee")) {
                location.href = location.href.replace(/employee=[0-9]+/, "employee=" + newValue);
            } else {
                location.href += "&employee=" + newValue;
            }

            var args = { employeeId: newValue };
            vm.loading = $.when(
                vm.loading,
                getExtras('get/all', args, vm.extras));
        });

        function getExtras(operation, args, observable) {
            return app.consumeAPI('extras', operation, args).done(function (data) {
                var mappedData = $.map(data, function (item) { return new app.EditableObject(item); });
                observable(mappedData);
                return data;
            }).fail(function (error) {
                app.showError(error);
                return error;
            });
        }

    }

    return new ViewModel();
});