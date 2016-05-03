define(['viewModels/title'], function (title) {

    function Controller() {

        var $containerElement = $('#payrollSection');

        this.init = function () {
            $containerElement.parent().fadeIn();
            title.partialViewTitle('Planillas');
        }
    }

    return new Controller();
});
