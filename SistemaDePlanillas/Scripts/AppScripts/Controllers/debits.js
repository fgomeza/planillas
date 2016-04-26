define(['jquery', 'knockout', 'viewModels/debits', 'viewModels/title'], function ($, ko, viewModel, title) {

    function Controller() {

        this.init = function () {

            var $containerElement = $('#debitsSection');

            $.when(viewModel.loading).then(function () {
                ko.applyBindings(viewModel, $containerElement[0]);
                $containerElement.parent().fadeIn();
            });

            title.partialViewTitle('Débitos');
        }
    }

    return new Controller();
});
