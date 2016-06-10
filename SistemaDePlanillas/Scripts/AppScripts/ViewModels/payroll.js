define(['jquery', 'knockout', 'app/driver'], function ($, ko, app) {

    function Row(data) {
        this.amortizationDebits;
        this.calls = new Obj(data.calls);
        this.employee = data.employee;
        this.extras = new Obj(data.extras);
        this.fixedDebits;
        this.negativeAmount = data.negativeAmount;
        this.netSalary = data.netSalary;
        this.paymentDebits;
        this.penalties = new Obj(data.penalties);
        this.salary = data.salary;
        this.saving = {
            monthlyAmount: data.monthlyAmount,
            total: data.total
        }
    }

    function Obj(data) {
        this.count = data.count;
        this.total = data.total;
        this.list = data.list;
    }


    function ViewModel() {
        var self = this;

        self.loading = null;

        self.rows = ko.observableArray([]);
        self.payrollVisible = ko.computed(function () {
            return self.rows().length > 0;
        });

        self.submitCancel = function () {
            app.consumeAPI('Payroll', 'calculate/cancel').done(function (data) {
                self.rows([]);
            }).fail(function (error) {
                app.showError(error);
                return error;
            });
        }

        self.submitApprove = function () {
            app.consumeAPI('Payroll', 'calculate/setAsReady').done(function (data) {
                self.rows([]);
            }).fail(function (error) {
                app.showError(error);
                return error;
            });
        }

        self.submitRange = function (start, end, label) {
            self.rows([]);
            app.showLoading();
            var dateFormat = 'YYYY-MM-DD';
            var args = { initialDate: start.format(dateFormat), endDate: end.format(dateFormat) };
            app.consumeAPI('Payroll', 'calculate', args).done(function (data) {
                renderPayroll(data);
                return data;
            }).fail(function (error) {
                console.log('it failed!', error);
                app.showError(error);
            }).always(function (data) {
                app.hideLoading();
            });
        }

        self.loading = $.when(getAlreadyCalculated());

        function renderPayroll(data) {
            var mappedData = $.map(data.employees, function (item) { return new Row(item); });
            self.rows(mappedData);
        }

        function getAlreadyCalculated() {
            return app.consumeAPI('Payroll', 'get/current').done(function (data) {
                if (data) {
                    renderPayroll(data.payroll);
                }
                if (data.isPendingToApprove) {
                    //poner aprovar y reprobar  botones (aqui el de cancelar no se debe mostrar)
                } else {
                    //poner enviar para aprobación y cancelar
                }
            });
        }

    }

    return new ViewModel();
});