define(['jquery', 'knockout'], function ($, ko) {

    function Editables() {
        this.Debit = function (data) {
            this.id = ko.observable();
            this.detail = ko.observable();
            this.employee = ko.observable();
            this.initialDate = ko.observable();
            this.interestRate = ko.observable();
            this.missingPayments = ko.observable();
            this.paymentsMade = ko.observable();
            this.remainingAmount = ko.observable();
            this.amount = ko.observable();
            this.total = ko.observable();
            this.type = ko.observable();
            this.typeName = ko.observable();
            this.update = update;
            this.update(data);
        }

        function update(data) {
            data = data || {};
            var self = this;
            $.each(data, function (propertyName, value) {
                self[propertyName](value);
            });
        }
    }

    return new Editables();

});