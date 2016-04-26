define(['jquery', 'knockout', 'viewModels/title', 'dropzone-amd-module'], function ($, ko, title, Dropzone) {

    function Controller() {

        this.init = function () {

            var $containerElement = $('#uploadSection');

            var uploader = new Dropzone('#uploadDemo', {
                paramName: "file"
            });

            $containerElement.parent().fadeIn();

            title.partialViewTitle('Subir archivo');
        }
    }

    return new Controller();
});
