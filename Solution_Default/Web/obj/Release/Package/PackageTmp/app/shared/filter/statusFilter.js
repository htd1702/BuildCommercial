(function (app) {
    app.filter("statusFilter", statusFilter);
    app.filter("orderStatusFilter", orderStatusFilter);
    function statusFilter() {
        return function (input) {
            if (input == true)
                return "Active";
            else
                return "Lock";
        }
    }
    function orderStatusFilter() {
        return function (input) {
            if (input == true)
                return "Paid";
            else
                return "Unpaid";
        }
    }
})(angular.module("default.common"));