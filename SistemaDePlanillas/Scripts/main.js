
require(['bootstrap', 'respond', 'jasny', 'app/site']);

require(['app/routing'], function (routing) {
    var app = new routing.config('@Url.Content("~/")', '#page', '#/Home/Dashboard');
    app.init();
});
