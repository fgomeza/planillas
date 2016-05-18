require([
    'bootstrap',
    'respond',
    'jasny',
    'simpleGrid',
    'knockstrap',
    'app/site'],
    function () {

    require(['app/sammy-config'], function (Router) {
        var router = new Router('#page', '#/navigation?page=welcome');
        router.run('#/');
    });

    require(['jquery', 'knockout', 'viewModels/title'], function ($, ko, vm) {
        vm.applicationName('Sistema de planillas');
        ko.applyBindings(vm, $('head')[0]);
        ko.applyBindings(vm, $('#sidebar')[0]);
        ko.applyBindings(vm, $('#topBanner')[0]);
    });

    require(['app/knockout-config'], function (koConfig) {
        koConfig.hiddenBindingHandler();
        koConfig.dateRangePicker();
        koConfig.bootstrapToggle();
        koConfig.bootstrapSelect();
    });
});
