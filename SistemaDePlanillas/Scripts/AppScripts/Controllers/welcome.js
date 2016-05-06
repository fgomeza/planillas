define(['jquery', 'knockout', 'viewModels/contacts', 'viewModels/title'], function ($, ko, vm, title) {

    function Controller() {

        this.init = function () {

            var $containerElement = $('#welcomeSection');

            ko.applyBindings(vm, $containerElement[0]);

            $containerElement.parent().fadeIn();

            title.partialViewTitle('Pantalla de bienvenida');
        }
    }

    return new Controller();
});
