define(['jquery', 'knockout', 'app/driver', 'viewModels/employees', 'viewModels/editables'], function ($, ko, app, employees, editables) {
    
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

        self.strings = {
            id: "Identificador",
            employee: "Asociado",
            detail: "Detalle",
            amount: "Monto",
            type: "Tipo",
            typeName: "Tipo",
            initialDate: "Fecha inicial",
            total: "Total",
            interestRate: "Tasa de interés",
            paymentsMade: "Pagos realizados",
            missingPayments: "Pagos faltantes",
            remainingAmount: "Monto pendiente",
            createFixedDebitLink: "Agregar un débito fijo",
            createPaymentsDebitLink: "Agregar un débito a pagos",
            pays: "Total de pagos",
            createAmortizationDebitLink: "Agregar un débito amortizado"
        };

        self.loading = null;
        self.employees = null;

        // Observables
        self.employeeId = ko.observable();
        self.debitTypeSelected = ko.observable();
        self.modalVisible = ko.observable(false);
        self.modalHeaderLabel = ko.observable();
        self.modalBodyTemplate = ko.observable("createFixedModalTemplate");
        self.modalBodyData = ko.observable(new editables.Debit());
        self.isEditMode = ko.observable(false);

        // Arrays
        self.fixedDebits = ko.observableArray();
        self.paymentsDebits = ko.observableArray();
        self.amortizationDebits = ko.observableArray();

        // Computed
        self.fixedDebitsVisible = ko.computed(function () {
            return self.debitTypeSelected() == "fixed";
        });
        self.paymentsDebitsVisible = ko.computed(function () {
            return self.debitTypeSelected() == "payments";
        });
        self.amortizationDebitsVisible = ko.computed(function () {
            return self.debitTypeSelected() == "amortization";
        });
        self.fixedDebitsTableVisible = ko.computed(function () {
            return self.fixedDebits().length > 0;
        });
        self.paymentsDebitsTableVisible = ko.computed(function () {
            return self.paymentsDebits().length > 0;
        });
        self.amortizationDebitsTableVisible = ko.computed(function () {
            return self.amortizationDebits().length > 0;
        });

        //functions
        self.loading = $.when(self.loading, employees.loading).
            then(function () {
                self.employees = employees.employees;
            });

        self.employeeId.subscribe(function (newValue) {
            if (newValue == null) return;

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

        self.debitTypeSelected.subscribe(function (newValue) {
            switch (newValue) {
                case "fixed":
                    self.modalBodyTemplate("createFixedModalTemplate");
                    self.modalHeaderLabel("Crear débito fijo");
                    break;
                case "payments":
                    self.modalBodyTemplate("createPaymentsModalTemplate");
                    self.modalHeaderLabel("Crear débito a pagos");
                    break;
                case "amortization":
                    self.modalBodyTemplate("createPaymentsModalTemplate");
                    self.modalHeaderLabel("Crear débito amortizado");
            }
        });

        self.submitAction = function () {
            if (self.debitTypeSelected() === "fixed") {
                submitCreateFixedDebit();
            } else if (self.debitTypeSelected() === "payments") {
                submitCreatePaymentsDebit();
            } else if (self.debitTypeSelected() === "amortization") {
                submitCreateAmortizationDebit();
            }
        }

        self.openModal = function () {
            self.modalBodyData(new editables.Debit());
            self.isEditMode(false);
            self.modalVisible(true);
        }

        function submitCreateFixedDebit () {
            var args = { employee: self.employeeId(), detail: self.modalBodyData().detail(), amount: self.modalBodyData().amount(), type: self.modalBodyData().type() };
            app.consumeAPI("debits", "add/Fixed", args).done(function (data) {
                self.fixedDebits.push(new editables.Debit(data));
                self.modalBodyData(new editables.Debit());
                return data;
            }).fail(function (error) {
                app.showError(error);
                return error;
            }).always(function () {
                closeModal();
            });
        }

        function submitCreatePaymentsDebit () {
            var args = { employee: self.employeeId(), initialDate: self.modalBodyData().initialDate(), detail: self.modalBodyData().detail(), total: self.modalBodyData().total(), interestRate: self.modalBodyData().interestRate(), pays: self.modalBodyData().missingPayments(), type: self.modalBodyData().type() };
            app.consumeAPI("debits", "add/Payment", args).done(function (data) {
                self.paymentsDebits.push(new editables.Debit(data));
                self.modalBodyData(new editables.Debit());
                return data;
            }).fail(function (error) {
                app.showError(error);
                return error;
            }).always(function () {
                closeModal();
            });
        }

        function submitCreateAmortizationDebit() {
            var args = { employee: self.employeeId(), initialDate: self.modalBodyData().initialDate(), detail: self.modalBodyData().detail(), total: self.modalBodyData().total(), interestRate: self.modalBodyData().interestRate(), pays: self.modalBodyData().missingPayments(), type: self.modalBodyData().type() };
            app.consumeAPI("debits", "add/Amortization", args).done(function (data) {
                self.amortizationDebits.push(new editables.Debit(data));
                self.modalBodyData(new editables.Debit());
                return data;
            }).fail(function (error) {
                app.showError(error);
                return error;
            }).always(function () {
                closeModal();
            });
        }

        function closeModal() {
            $('#modal').modal('hide');
        }

        function getDebits(operation, observable, args) {
            return app.consumeAPI('debits', operation, args).done(function (data) {
                var mappedData = $.map(data, function (item) { return new editables.Debit(item); });
                observable(mappedData);
                return data;
            }).fail(function (error) {
                app.showError(error);
                return error;
            });
        }

    };

    return new ViewModel();
});
