(function () {
    function EmployeesViewModel() {
        var self = this;
        self.employees = ko.observable();


        $.post("/api/action/employees/get", null, self.employees);
    }
})();
