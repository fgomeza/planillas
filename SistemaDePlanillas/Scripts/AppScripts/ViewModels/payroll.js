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

        self.rows = ko.observableArray([]);
        self.payrollVisible = ko.computed(function () {
            return self.rows().length > 0;
        });

        function renderPayroll(data) {
            var mappedData = $.map(data.employees, function (item) { return new Row(item); });
            self.rows(mappedData);
        }


        self.submitRange = function (start, end, label) {
            self.rows([]);
            app.showLoading();
            var dateFormat = 'YYYY-MM-DD';
            var args = { initialDate: start.format(dateFormat), endDate: end.format(dateFormat) };
            app.consumeAPI('Payroll', 'calculate', args).done(function (data) {
                data = data.data;
                renderPayroll(data);
                return data;
            }).fail(function (error) {
                console.log('it failed!', error);
                app.showError(error);
            }).always(function (data) {
                app.hideLoading();
            });
        }


    }

    return new ViewModel();
});