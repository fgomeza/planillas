define(['sammy'], function (Sammy) {
    function Router(contentSelector, defaultRoute) {
        return Sammy(contentSelector, function (context) {

            this.raise_errors = true;

            this.notFound = function () {
                console.log('404');
            };

            this.swap = function (content, callback) {
                var context = this;
                context.$element().slideUp(function () {
                    context.$element().html(content);
                    context.$element().fadeIn(500, function () {
                        if (callback) {
                            callback.apply();
                        }
                    });
                });
            };
            
            var options = function (context) {
                return {
                    error: function (response) {
                        //$(context.app.element_selector).html(response.responseText);
                        document.write(response.responseText);
                        switch (response.status) {
                            case 404:
                                context.notFound();
                                break;
                            case 401:
                                //context.redirect('/');
                                setTimeout(location.reload.bind(location), 2000);
                                break;
                        }
                    }
                };
            };

            this.get('#/navigation', function (context) {
                var url = '/PartialViews/' + context.params.page;
                console.log('loading', url);
                $('html').trigger('click');
                context.load(url, options(context)).swap();
            });


            this.get('#/', function (context) {
                context.redirect(defaultRoute);
            });

        });

    }

    return Router;

});