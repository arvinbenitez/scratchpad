var DateFunctions = function() {

    var UtctoLocalDateDateImpl = function(utcDateString) {
        var d = new Date(utcDateString + " UTC");
        return d;
    };

    return ({
        UtctoLocalDate: UtctoLocalDateDateImpl
    });
}();