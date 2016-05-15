define(['jquery', 'knockout', 'app/driver', 'viewModels/employees', 'simpleGrid'], function ($, ko, app, employees) {
    
    function EditableObject(data) {
        data = data || {};
        var self = this;
        $.each(data, function (property) {
            if ($.isArray(property))
                self[property] = ko.observableArray();
            else 
                self[property] = ko.observable();
        });
        this.update(data)
    }

    ko.utils.extend(EditableObject.prototype, {
        update: function (data) {
            var self = this;
            $.each(data, function (propertyName, value) {
                self[propertyName](value);
            });
        }
    });

    function ViewModel() {
        var self = this;

        self.loading = null;
        self.employees = null;

        self.employeeId = ko.observable();
        self.debitTypeSelected = ko.observable();
        self.fixedDebits = ko.observableArray();
        self.paymentsDebits = ko.observableArray();
        self.amortizationDebits = ko.observableArray();
        self.fixedDebitsVisible = ko.computed(function () {
            return self.debitTypeSelected() == "fixed" && self.fixedDebits().length > 0;
        });

        self.paymentsDebitsVisible = ko.computed(function () {
            return self.debitTypeSelected() == "payments" && self.paymentsDebits().length > 0;
        });

        self.amortizationDebitsVisible = ko.computed(function () {
            return self.debitTypeSelected() == "amortization" && self.amortizationDebits().length > 0;
        });

        self.loading = $.when(self.loading, employees.loading).
            then(function () {
                self.employees = employees.employees;
            });

        self.employeeId.subscribe(function (newValue) {
            if (!newValue) return;

            if (app.urlParameterExists("employee")) {
                location.href = location.href.replace(/employee=[0-9]+/, "employee=" + newValue);
            } else {
                location.href += "&employee=" + newValue;
            }

            var args = { employee: newValue };
            self.loading = $.when(
                self.loading,
                getDebits('get/AllFixed', self.fixedDebits, args),
                getDebits('get/AllPayment', self.paymentsDebits, args),
                getDebits('get/AllAmortization', self.amortizationDebits, args));
        });

        function getDebits(operation, observable, args) {
            return app.consumeAPI('debits', operation, args).done(function (data) {
                var mappedData = $.map(data, function (item) { return new EditableObject(item); });
                observable(mappedData);
                return data;
            }).fail(function (error) {
                app.showError(error);
                return error;
            });
        }

        self.fixedData = new ko.simpleGrid.viewModel({
            data: self.fixedDebits,
            columns: [
                { headerText: "id", rowText: "id", isVisible: true },
                { headerText: "employee", rowText: "employee", isVisible: true },
                { headerText: "detail", rowText: "detail", isVisible: true },
                { headerText: "amount", rowText: "amount", isVisible: true },
                { headerText: "type", rowText: "type", isVisible: true },
                { headerText: "typeName", rowText: "typeName", isVisible: true },
            ],
            pageSize: -1
        });

        self.paymentsData = new ko.simpleGrid.viewModel({
            data: self.paymentsDebits,
            columns: [
                { headerText: "id", rowText: "id", isVisible: true },
                { headerText: "employee", rowText: "employee", isVisible: true },
                { headerText: "detail", rowText: "detail", isVisible: true },
                { headerText: "initialDate", rowText: "initialDate", isVisible: true },
                { headerText: "total", rowText: "total", isVisible: true },
                { headerText: "interestRate", rowText: "interestRate", isVisible: true },
                { headerText: "paymentsMade", rowText: "paymentsMade", isVisible: true },
                { headerText: "missingPayments", rowText: "missingPayments", isVisible: true },
                { headerText: "remainingAmount", rowText: "remainingAmount", isVisible: true },
                { headerText: "type", rowText: "type", isVisible: true },
                { headerText: "typeName", rowText: "typeName", isVisible: true },
            ],
            pageSize: -1
        });
        self.amortizationData = new ko.simpleGrid.viewModel({
            data: self.amortizationDebits,
            columns: [
                { headerText: "id", rowText: "id", isVisible: true },
                { headerText: "employee", rowText: "employee", isVisible: true },
                { headerText: "detail", rowText: "detail", isVisible: true },
                { headerText: "initialDate", rowText: "initialDate", isVisible: true },
                { headerText: "total", rowText: "total", isVisible: true },
                { headerText: "interestRate", rowText: "interestRate", isVisible: true },
                { headerText: "paymentsMade", rowText: "paymentsMade", isVisible: true },
                { headerText: "missingPayments", rowText: "missingPayments", isVisible: true },
                { headerText: "remainingAmount", rowText: "remainingAmount", isVisible: true },
                { headerText: "type", rowText: "type", isVisible: true },
                { headerText: "typeName", rowText: "typeName", isVisible: true },
            ],
            pageSize: -1
        });
    };

    return new ViewModel();
});
