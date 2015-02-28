(function () {
    "use strict";

    var myAppModule = angular.module('myApp');

    myAppModule.factory('UserService', ['$http', function ($resource) {

        var getAllUsersImpl = function () {
            return [{ "id": 1, "name": "Arvin" }, { "id": 2, "name": "Vince" }, { "id": 3, "name": "Lyn" }];
        };

        var getNewUserImpl = function () {
            return { "id": 0, "name": "New User" };
        };

        var getUserImpl = function (userId) {
            return { "id": userId, "name": "User " + userId};
        };


        return {
            getAllUsers: getAllUsersImpl,
            getNewUser: getNewUserImpl,
            getUser: getUserImpl
        };
        }
    ]);

})();