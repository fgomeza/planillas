(function (factory) {
    // Support module loading scenarios
    if (typeof define === 'function' && define.amd) {
        // AMD Anonymous Module
        define(['sammy'], factory);
    } else {
        // No module loader (plain <script> tag) - put directly in global namespace
        window.routing = factory(Sammy);
    }
})(function (Sammy) {

    return {
        config: function (appRoot, contentSelector, defaultRoute) {

            function getUrlFromHash(hash) {
                var url = hash.replace('#/', '');
                if (url === appRoot)
                    url = defaultRoute;
                return url;
            }

            var sammyApp = Sammy(contentSelector);

            sammyApp.get(/\#\/(.*)/, function (context) {
                var url = getUrlFromHash(context.path);
                context.load(url).swap();
            });

            return {
                init: function () { sammyApp.run('/#/'); }
            }
        }
    }
});
