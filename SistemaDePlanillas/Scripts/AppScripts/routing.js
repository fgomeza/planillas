define(['sammy'], function (Sammy) {
    function Router(contentSelector, defaultRoute) {
        var sammyApp = Sammy(contentSelector, function (context) {

            this.raise_errors = true;

            this.notFound = function () {
                console.log('not found');
                alert('not found');
            };

            this.swap = function (content, callback) {
                var context = this;
                context.$element().slideUp(function () {
                    context.$element().html(content);
                    context.$element().fadeIn(function () {
                        if (callback) {
                            callback.apply();
                        }
                    });
                });
            };

            this.get('#/navigation', function (context) {
                var url = '/PartialViews/' + context.params.page;
                console.log(url);
                $('html').trigger('click');
                context.load(url).swap();
            });

            this.get('#/', function (context) {
                context.redirect(defaultRoute);
            });

        });

        return sammyApp;

    }

    return Router;

});