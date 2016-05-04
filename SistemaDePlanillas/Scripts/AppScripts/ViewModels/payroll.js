define(['jquery', 'knockout', 'app/driver'], function ($, ko, app) {

    function ViewModel() {
        var self = this;

        self.submitRange = function (start, end, label) {
            var dateFormat = 'YYYY-MM-DD';
            var args = { initialDate: start.format(dateFormat), endDate: end.format(dateFormat) };
            app.consumeAPI('Payroll', 'calculate', args).done(function (data) {
                console.log('it worked!', data);
                return data;
            }).fail(function (error) {
                console.log('it failed!', error);
                app.showError(error);
            }).always(function () {
                console.log('something happened...');
            });
        }
    }

    return new ViewModel();
});