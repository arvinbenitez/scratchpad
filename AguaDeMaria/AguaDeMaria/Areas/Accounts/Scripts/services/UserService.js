(function () {
    "use strict";

    var myAppModule = angular.module('myApp');

    myAppModule.factory('UserService', ['$http', function($http) {

            var getAllUsersImpl = function() {
                return $http.get('/accounts/user/list');
            };

            var getNewUserImpl = function() {
                return { "Id": 0, "Email": "", "UserName": "New User", "FirstName": "", "LastName": "" };
            };

            var getUserImpl = function(userId) {
                return $http.get('/accounts/user/get/' + userId);
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