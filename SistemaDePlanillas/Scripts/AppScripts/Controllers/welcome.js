define(['jquery', 'viewModels/title'], function ($, title) {

    function Controller() {

        this.init = function () {

            var $containerElement = $('#welcomeSection');

            $containerElement.parent().fadeIn();

            title.partialViewTitle('Pantalla de bienvenida');
        }
    }

    return new Controller();
});
