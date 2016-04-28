define(['jquery', 'knockout', 'viewModels/users', 'viewModels/title'], function ($, ko, viewModel, title) {

    function Controller() {

        this.init = function () {

            var $containerElement = $('#usersSection');

            $.when(viewModel.loading).then(function () {
                ko.applyBindings(viewModel, $containerElement[0]);
                $containerElement.parent().fadeIn();
            });

            title.partialViewTitle('Administración de usuarios');
        }
    }

    return new Controller();
});
