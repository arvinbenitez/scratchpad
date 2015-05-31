(function () {
    "use strict";

    var myAppModule = angular.module('myApp');

    myAppModule.factory('UserService', ['$http', function($http) {

            var getAllUsersImpl = function() {
                return $http.get('/accounts/user/list');
            };

            var getNewUserImpl = function() {
                return { "id": 0, "name": "New User" };
            };

            var getUserImpl = function(userId) {
                return $http.get('/accounts/user/get/1');
            };

            var updateUserImpl = function (currentUser) {
                return $http.post('/accounts/user/post',currentUser);
            };

            return {
                getAllUsers: getAllUsersImpl,
                getNewUser: getNewUserImpl,
                updateUser: updateUserImpl,
                getUser: getUserImpl
            };
        }
    ]);

})();