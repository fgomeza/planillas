define(['jquery', 'knockout', 'viewModels/payroll', 'viewModels/title'], function ($, ko, vm, title) {

    function Controller() {

        /*
        function cb(start, end) {
            $('#reportrange span').html(start.format('MMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
        }
        */
        

        this.init = function () {

            var $containerElement = $('#payrollSection');

            $.when(vm.loading).then(function () {
                ko.applyBindings(vm, $containerElement[0]);
                $containerElement.parent().fadeIn();
            });

            title.partialViewTitle('Planillas');
        }
    }

    return new Controller();
});
