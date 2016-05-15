define(['viewModels/title'], function (title) {

    function Controller() {

        var $containerElement = $('#dashboardSection');

        this.init = function () {
            $containerElement.parent().fadeIn();
            title.partialViewTitle('Dashboard');
        }
    }

    return new Controller();
});
