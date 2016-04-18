require(['bootstrap', 'respond', 'jasny', 'app/site'], function () {
    require(['app/routing'], function (routing) {
        var app = new routing.config('/', '#page', '/Home/Dashboard');
        app.init();
    });

    require(['jquery', 'knockout', 'app/titleViewModel'], function ($, ko, vm) {
        ko.applyBindings(vm, $('head')[0]);
    });
});
