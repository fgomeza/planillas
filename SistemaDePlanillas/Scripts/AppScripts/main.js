require(['bootstrap', 'respond', 'jasny', 'app/site'], function () {

    require(['app/routing'], function (Router) {
        var router = new Router('#page', '#/navigation?page=dashboard');
        router.run('#/');
    });

    require(['jquery', 'knockout', 'viewModels/title'], function ($, ko, vm) {
        vm.applicationName("Sistema de planillas");
        ko.applyBindings(vm, $('head')[0]);
        ko.applyBindings(vm, $('#sidebar')[0]);
        ko.applyBindings(vm, $('#topBanner')[0]);
    });

    require(['knockout'], function (ko) {
        require(['toggle'], function () {
            //wrapper to an observable that requires accept/cancel
            ko.bindingHandlers.bootstrapToggle = {
                init: function (element, valueAccessor, allBindingsAccessor) {

                    var elem = $(element);

                    //initialize bootstrapToggle
                    elem.bootstrapToggle();

                    // setting initial value
                    $(element).bootstrapToggle('state', valueAccessor()());

                    //handle the field changing
                    $(element).on('switchChange.bootstrapToggle', function (event, state) {
                        var observable = valueAccessor();
                        observable(state);
                    });

                    // Adding component options
                    var options = allBindingsAccessor().bootstrapToggleOptions || {};
                    for (var property in options) {
                        $(element).bootstrapToggle(property, ko.utils.unwrapObservable(options[property]));
                    }

                    //handle disposal (if KO removes by the template binding)
                    ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                        $(element).bootstrapToggle('destroy');
                    });

                },
                //update the control when the view model changes
                update: function (element, valueAccessor, allBindingsAccessor) {
                    var value = ko.utils.unwrapObservable(valueAccessor());

                    // Adding component options
                    var options = allBindingsAccessor().bootstrapToggleOptions || {};
                    for (var property in options) {
                        $(element).bootstrapToggle(property, ko.utils.unwrapObservable(options[property]));
                    }

                    $(element).bootstrapToggle('state', value);
                }
            };
        });

        ko.bindingHandlers.hidden = {
            update: function (element, valueAccesor) {
                var value = ko.utils.unwrapObservable(valueAccesor());
                ko.bindingHandlers.visible.update(element, function () { return !value; });
            }
        };

    });

});
