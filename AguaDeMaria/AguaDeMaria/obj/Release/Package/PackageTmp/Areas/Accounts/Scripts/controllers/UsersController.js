(function () {
    "use strict";

    var myAppModule = angular.module('myApp');
    myAppModule.controller('UsersController', ['$scope', '$state','$stateParams', 'UserService',
            function ($scope, $state, $stateParams, userService) {
                console.log('controller called: ' + $state.current.name);
                $scope.showList = ($state.current.name === 'users');

                if ($scope.showList) {
                    console.log('users loaded');
                    $scope.userList = userService.getAllUsers();
                } else {
                    if (typeof ($stateParams.userId) == 'undefined') {
                        $scope.mode = "New User";
                        $scope.currentUser = userService.getNewUser();
                    } else {
                        $scope.mode = "Edit User";
                        $scope.currentUser = userService.getUser($stateParams.userId);
                    }
                }
            }
    ]);

    myAppModule.controller('UserEditController', ['$scope', '$stateParams', 'UserService',
            function ($scope, $stateParams, userService) {
                if (typeof ($stateParams.userId) == 'undefined') {
                    $scope.mode = "New User";
                    $scope.currentUser = userService.getNewUser();
                } else {
                    $scope.mode = "Edit User";
                    $scope.currentUser = userService.getUser($stateParams.userId);
                }
            }
    ]);
})();