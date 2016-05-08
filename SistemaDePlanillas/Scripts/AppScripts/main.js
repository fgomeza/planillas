require(['bootstrap', 'respond', 'jasny', 'app/site'], function () {

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

    require(['knockout'], function (ko) {

        ko.bindingHandlers.hidden = {
            update: function (element, valueAccesor) {
                var value = ko.utils.unwrapObservable(valueAccesor());
                ko.bindingHandlers.visible.update(element, function () { return !value; });
            }
        };

        require(['toggle'], function () {
            //wrapper to an observable that requires accept/cancel
            ko.bindingHandlers.bootstrapToggle = {
                init: function (element, valueAccessor, allBindingsAccessor) {

                    var $element = $(element);

                    //initialize bootstrapToggle
                    $element.bootstrapToggle();

                    // setting initial value
                    //$element.bootstrapToggle('state', valueAccessor()());
                    $element.prop('checked', valueAccessor()()).change();

                    //handle the field changing
                    /*
                    $element.on('switchChange.bootstrapToggle', function (event, state) {
                        var observable = valueAccessor();
                        observable(state);
                    });
                    */
                    $element.change(function () {
                        var observable = valueAccessor();
                        observable(state);
                    });

                    // Adding component options
                    /*
                    var options = allBindingsAccessor().bootstrapToggleOptions || {};
                    for (var property in options) {
                        $element.bootstrapToggle(property, ko.utils.unwrapObservable(options[property]));
                    }
                    */

                    //handle disposal (if KO removes by the template binding)
                    ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                        $element.bootstrapToggle('destroy');
                    });

                },
                //update the control when the view model changes
                update: function (element, valueAccessor, allBindingsAccessor) {

                    var $element = $(element);
                    var value = ko.utils.unwrapObservable(valueAccessor());

                    // Adding component options
                    var options = allBindingsAccessor().bootstrapToggleOptions || {};
                    for (var property in options) {
                        $element.bootstrapToggle(property, ko.utils.unwrapObservable(options[property]));
                    }

                    $element.bootstrapToggle('state', value);
                }
            };
        });

        require(['app/daterangepicker-config', 'daterangepicker'], function (options) {
            ko.bindingHandlers.daterangepicker = {
                init: function (element, valueAccesor, allBindings, viewModel, bindingContext) {

                    var $element = $(element);

                    function cb (start, end, label) {
                        $element.find('span').html(start.format('MMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
                        console.log('New date range selected: ' + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD') + ' (predefined range: ' + label + ')');
                        valueAccesor().call(viewModel, start, end, label);
                    }

                    $(element).daterangepicker(options, cb);
                }

            }
        });

    });

});
