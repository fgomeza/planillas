define(['knockout'], function (ko) {

    var knockoutCustomConfiguration = {

        hiddenBindingHandler: function () {
            ko.bindingHandlers.hidden = {
                update: function (element, valueAccesor) {
                    var value = ko.utils.unwrapObservable(valueAccesor());
                    ko.bindingHandlers.visible.update(element, function () { return !value; });
                }
            };
        },

        bootstrapToggle: function () {
            require(['toggle'], function () {
                //wrapper to an observable that requires accept/cancel
                ko.bindingHandlers.bootstrapToggle = {
                    init: function (element, valueAccessor, allBindingsAccessor) {

                        var $element = $(element);

                        //initialize bootstrapToggle
                        $element.bootstrapToggle();

                        // setting initial value
                        $element.prop('checked', valueAccessor()()).change();

                        //handle the field changing
                        $element.change(function () {
                            var observable = valueAccessor();
                            var state = $element.prop('checked');
                            observable(state);
                        });


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
        },

        bootstrapSwitch: function () {
            require(['switch'], function () {
                //wrapper to an observable that requires accept/cancel
                ko.bindingHandlers.bootstrapSwitch = {
                    init: function (element, valueAccessor, allBindingsAccessor) {

                        var $element = $(element);

                        //initialize bootstrapSwitch
                        $element.bootstrapSwitch();

                        // setting initial value
                        $element.bootstrapSwitch('state', valueAccessor()());

                        //handle the field changing
                        $element.on('switchChange.bootstrapSwitch', function (event, state) {
                            var observable = valueAccessor();
                            observable(state);
                        });

                        // Adding component options
                        var options = allBindingsAccessor().bootstrapSwitchOptions || {};
                        for (var property in options) {
                            $element.bootstrapSwitch(property, ko.utils.unwrapObservable(options[property]));
                        }

                        //handle disposal (if KO removes by the template binding)
                        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                            $element.bootstrapSwitch('destroy');
                        });

                    },
                    //update the control when the view model changes
                    update: function (element, valueAccessor, allBindingsAccessor) {

                        var $element = $(element);
                        var value = ko.utils.unwrapObservable(valueAccessor());

                        // Adding component options
                        var options = allBindingsAccessor().bootstrapSwitchOptions || {};
                        for (var property in options) {
                            $element.bootstrapSwitch(property, ko.utils.unwrapObservable(options[property]));
                        }

                        $element.bootstrapSwitch('state', value);
                    }
                };
            });
        },

        dateRangePicker: function () {
            require(['app/daterangepicker-config', 'daterangepicker'], function (options) {
                ko.bindingHandlers.daterangepicker = {
                    init: function (element, valueAccesor, allBindings, viewModel, bindingContext) {

                        var $element = $(element);

                        function cb(start, end, label) {
                            $element.find('span').html(start.format('MMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
                            console.log('New date range selected: ' + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD') + ' (predefined range: ' + label + ')');
                            valueAccesor().call(viewModel, start, end, label);
                        }

                        $(element).daterangepicker(options, cb);
                    }

                }
            });
        },

        bootstrapSelect: function () {
            require(['bootstrap-select'], function () {
                ko.bindingHandlers.selectPicker = {
                    init: function (element, valueAccessor, allBindingsAccessor) {

                        var $element = $(element);
                        if ($element.is('select')) {
                            var selectPickerOptions = allBindingsAccessor().selectPickerOptions;
                            var options = selectPickerOptions.options;

                            ko.bindingHandlers.options.init(element, options, allBindingsAccessor);

                            $element.addClass('selectpicker').selectpicker();

                            $element.on("changed.bs.select", function () {
                                valueAccessor()(element.value);
                            });
                        }
                    },
                    update: function (element, valueAccessor, allBindingsAccessor) {

                        var $element = $(element);
                        if ($element.is('select')) {
                            var selectPickerOptions = allBindingsAccessor().selectPickerOptions;
                            var options = selectPickerOptions.options;

                            ko.bindingHandlers.options.update(element, options, allBindingsAccessor);

                            $element.selectpicker("refresh");
                            $element.selectpicker("val", valueAccessor()());
                        }
                    }
                    /*
                    update: function (element, valueAccessor, allBindingsAccessor) {
                        var $element = $(element);
                        if ($element.is('select')) {
                            var selectPickerOptions = allBindingsAccessor().selectPickerOptions;
                            if (typeof selectPickerOptions !== 'undefined' && selectPickerOptions !== null) {

                                var optionsArray = selectPickerOptions.optionsArray,
                                    isDisabled = selectPickerOptions.disabledCondition || false,
                                    resetOnDisabled = selectPickerOptions.resetOnDisabled || false;

                                if (ko.utils.unwrapObservable(optionsArray).length > 0) {
                                    // call the default Knockout options binding
                                    ko.bindingHandlers.options.update(element, optionsArray, allBindingsAccessor);
                                }

                                if (isDisabled && resetOnDisabled) {
                                    // the dropdown is disabled and we need to reset it to its first option
                                    $element.selectpicker('val', $element.children('option:first').val());
                                }

                                $element.prop('disabled', isDisabled);
                            }

                            if (ko.isObservable(valueAccessor())) {
                                if ($element.prop('multiple') && $.isArray(ko.utils.unwrapObservable(valueAccessor()))) {
                                    // in the case of a multiple select where the valueAccessor() is an observableArray, call the default Knockout selectedOptions binding
                                    ko.bindingHandlers.selectedOptions.update(element, valueAccessor);
                                } else {
                                    // call the default Knockout value binding
                                    ko.bindingHandlers.value.update(element, valueAccessor);
                                }
                            }

                            $element.selectpicker('refresh');
                        }
                    }
                    */
                }
            });
        }

    };

    return knockoutCustomConfiguration;

});
