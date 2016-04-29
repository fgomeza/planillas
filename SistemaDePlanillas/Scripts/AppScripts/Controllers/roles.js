define(['jquery', 'knockout', 'viewModels/roles', 'viewModels/title', 'bootstrap', 'toggle'], function ($, ko, viewModel, title) {

    function Controller() {

        this.init = function () {

            var $containerElement = $('#rolesSection');

            $.when(viewModel.loading).then(function () {
                ko.applyBindings(viewModel, $containerElement[0]);

                $('.make-switch').bootstrapToggle({
                });

                $containerElement.parent().fadeIn();
            });

            title.partialViewTitle('Administración de roles');
        }
    }

    return new Controller();
});



/*
$('.alert-status').bootstrapSwitch('state', true);


$('.alert-status').on('switchChange.bootstrapSwitch', function (event, state) {

    alert($(this).data('checkbox'));
    //alert(event);
   // alert(state);
});
*/