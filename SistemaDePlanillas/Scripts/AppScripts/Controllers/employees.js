define(['jquery', 'knockout', 'viewModels/employees', 'viewModels/title'], function ($, ko, viewModel, title) {

    function Controller() {

        this.init = function () {

            var $containerElement = $('#employeesSection');

            $.when(viewModel.loading).then(function () {
                ko.applyBindings(viewModel, $containerElement[0]);
                $containerElement.parent().fadeIn();
            });

            title.partialViewTitle('Administración de asociados');
        }
    }

    return new Controller();
});
