define(['jquery', 'knockout', 'viewModels/roles', 'viewModels/title', 'bootstrap', 'switch'], function ($, ko, viewModel, title) {

    function Controller() {

        this.init = function () {

            var $containerElement = $('#rolesSection');

            setTimeout(function () {
                $('.make-switch').bootstrapSwitch('state', true);
            }, 100);

            $.when(viewModel.loading).then(function () {
                $(document).ready(function () {
                });

                ko.applyBindings(viewModel, $containerElement[0]);
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