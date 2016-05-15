define(['jquery', 'knockout', 'viewModels/payroll', 'viewModels/title'], function ($, ko, vm, title) {

    function Controller() {

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
