define(['sammy', 'app/driver'], function (Sammy, app) {
    function Router(contentSelector, defaultRoute) {
        return Sammy(contentSelector, function (context) {

            var currentPage = '';

            this.raise_errors = true;

            this.notFound = function () {
                console.log('404');
            };

            this.swap = function (content, callback) {
                var context = this;
                context.$element().slideUp(function () {
                    context.$element().html(content);
                    if (callback) {
                        callback.apply();
                    }
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
                $('html').trigger('click');

                if (currentPage !== context.params.page)
                    currentPage = context.params.page;
                else return;

                var url = '/PartialViews/' + context.params.page;
                console.log('loading', url);
                
                var loading = app.startLoadingTimeout(1000);

                context.load(url, options(context)).swap(function () {
                    require(['controllers/' + context.params.page], function (controller) {
                        if (controller && controller.init) {
                            controller.init(context.params);
                        }
                        app.stopLoadingTimeout(loading);
                    }, function (error) {
                        console.error(error);
                        app.stopLoadingTimeout(loading);
                    });
                });
            });


            this.get('#/', function (context) {
                context.redirect(defaultRoute);
            });

            this._checkFormSubmission = function (form) {
                return false;
            }

        });

    }

    return Router;

});