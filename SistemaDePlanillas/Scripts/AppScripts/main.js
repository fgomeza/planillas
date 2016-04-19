require(['bootstrap', 'respond', 'jasny', 'app/site'], function () {
    require(['app/routing'], function (Router) {
        var router = new Router('#page', '#/navigation?page=dashboard');
        router.run('#/');
    });

    require(['jquery', 'knockout', 'app/titleViewModel'], function ($, ko, vm) {
        ko.applyBindings(vm, $('head')[0]);
    });
});
