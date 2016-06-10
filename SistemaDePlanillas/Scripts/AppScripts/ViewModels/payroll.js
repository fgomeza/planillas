define(['jquery', 'knockout', 'app/driver', 'moment'], function ($, ko, app, moment) {

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

    $datePicker = $("#reportrange");

    function ViewModel() {
        var self = this;

        self.loading = null;

        self.rows = ko.observableArray([]);
        self.isPendingToApprove = ko.observable();

        self.payrollVisible = ko.computed(function () {
            return self.rows().length > 0;
        });

        self.submitCancel = function () {
            app.consumeAPI('Payroll', 'calculate/cancel').done(function (data) {
                clearTable();
            }).fail(function (error) {
                app.showError(error);
                return error;
            });
        }
        
        self.submitSetAsReady = function () {
            var timeout = app.startLoadingTimeout(200);
            app.consumeAPI('Payroll', 'calculate/setAsReady').done(function (data) {
                self.isPendingToApprove(true);
            }).fail(function (error) {
                app.showError(error);
                return error;
            }).always(function () {
                app.stopLoadingTimeout(timeout);
            });
        }

        self.submitReprove = function () {
            app.consumeAPI('Payroll', 'reprove').done(function (data) {
                clearTable();
                self.isPendingToApprove(false);
            }).fail(function (error) {
                app.showError(error);
                return error;
            });
        }

        self.submitApprove = function () {
            app.consumeAPI('Payroll', 'aprove').done(function (data) {
                clearTable();
                self.isPendingToApprove(false);
            }).fail(function (error) {
                app.showError(error);
                return error;
            });
        }

        self.submitRange = function (start, end, label) {
            var savedRows = self.rows();
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
                self.rows(savedRows);
            }).always(function (data) {
                app.hideLoading();
            });
        }

        self.loading = $.when(getAlreadyCalculated());

        function renderPayroll(data) {
            var mappedData = $.map(data.employees, function (item) { return new Row(item); });
            self.rows(mappedData);

            var start = moment(data.initialDate);
            var end = moment(data.endDate);
            $datePicker.find('span').html(start.format('MMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
        }

        function getAlreadyCalculated() {
            return app.consumeAPI('Payroll', 'get/current').done(function (data) {
                if (data) {
                    renderPayroll(data.payroll);
                    if (typeof data.isPendingToApprove === "boolean") {
                        self.isPendingToApprove(data.isPendingToApprove);
                    }
                }
            });
        }

        function clearTable() {
            self.rows([]);
            $datePicker.find('span').html('');
        }

    }

    return new ViewModel();
});