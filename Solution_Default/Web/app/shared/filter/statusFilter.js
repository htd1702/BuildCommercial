(function (app) {
    app.filter("statusFilter", statusFilter);
    function statusFilter() {
        return function (input) {
            if (input == true)
                return "Active";
            else
                return "Lock";
        }
    }
})(angular.module("default.common"));