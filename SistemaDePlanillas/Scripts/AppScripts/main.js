require(['bootstrap', 'respond', 'jasny', 'switch', 'app/site'], function () {

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
        //wrapper to an observable that requires accept/cancel
        ko.protectedObservable = function (initialValue) {
            //private variables
            var _actualValue = ko.observable(initialValue),
                _tempValue = initialValue;

            //computed observable that we will return
            var result = ko.computed({
                //always return the actual value
                read: function () {
                    return _actualValue();
                },
                //stored in a temporary spot until commit
                write: function (newValue) {
                    _tempValue = newValue;
                }
            }).extend({ notify: "always" });

            //if different, commit temp value
            result.commit = function () {
                if (_tempValue !== _actualValue()) {
                    _actualValue(_tempValue);
                }
            };

            //force subscribers to take original
            result.reset = function () {
                _actualValue.valueHasMutated();
                _tempValue = _actualValue();   //reset temp value
            };

            return result;
        };
    });
});
