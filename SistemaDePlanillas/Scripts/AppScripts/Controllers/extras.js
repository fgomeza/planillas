define(['jquery', 'knockout', 'viewModels/extras', 'viewModels/title'], function ($, ko, viewModel, title) {
    
    function Controller() {
        
        this.init = function (params) {
            
            var $containerElement = $('#extrasSection');

            params = params || {};
            viewModel.employeeId(params.employee);

            $.when(viewModel.loading).then(function () {
                ko.applyBindings(viewModel, $containerElement[0]);
                $containerElement.parent().fadeIn();
            });

            title.partialViewTitle('Extras');
        }
    }

    return new Controller();
});
