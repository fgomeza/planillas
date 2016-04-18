(function (factory) {
    // Support module loading scenarios
    if (typeof define === 'function' && define.amd) {
        // AMD Anonymous Module
        define(['sammy'], factory);
    } else {
        // No module loader (plain <script> tag) - put directly in global namespace
        window.routing = factory(Sammy);
    }
})(function (Sammy, Presenter) {

    return {
        config: function (appRoot, contentSelector, defaultRoute) {

            function getUrlFromHash(hash) {
                var url = hash.replace('#/', '');
                if (url === appRoot)
                    url = defaultRoute;
                return url;
            }

            var sammyApp = Sammy(contentSelector);
/*
            sammyApp.get(/\#\/(.*)/, function (context) {
                var url = getUrlFromHash(context.path);
                context.load(url).swap();
            });
*/
            sammyApp.swap = function (content, callback) {
                var context = this;
                context.$element().fadeOut(300, function () {
                    context.$element().html(content);
                    context.$element().fadeIn(300, function () {
                        if (callback) {
                            callback.apply();
                        }
                    });
                });
            };

            sammyApp.get('#/navigation', function (context) {
                //var url = getUrlFromHash(context.path);
                var url = '/PartialViews/' + context.params.page;
                console.log(url);
                $('html').trigger('click');
                context.load(url).swap();
            });

            return {
                init: function () { sammyApp.run('/#/navigation?page=Dashboard'); }
            }
        }
    }
});
