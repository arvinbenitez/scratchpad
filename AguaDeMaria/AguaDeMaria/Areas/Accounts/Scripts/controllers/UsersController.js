(function () {
    "use strict";

    var myAppModule = angular.module('myApp');

    myAppModule.factory('fieldState', function() {
        return { showList: true, errorMessage: "" }
    });

    myAppModule.controller('UsersController', ['$scope', '$state', '$stateParams', 'UserService', 'fieldState',
            function ($scope, $state, $stateParams, userService, fieldState) {

                $scope.fieldState = fieldState;
                $scope.fieldState.showList = true;

                userService.getAllUsers().then(function(response) {
                    $scope.userList = response.data;
                });
            }
    ]);

    myAppModule.controller('UserEditController', ['$scope', '$state', '$stateParams', 'UserService', 'fieldState',
            function ($scope, $state, $stateParams, userService,fieldState) {
                $scope.fieldState = fieldState;
                $scope.fieldState.showList = false;

                if (typeof ($stateParams.userId) == 'undefined') {
                    $scope.mode = "New User";
                    $scope.currentUser = userService.getNewUser();
                } else {
                    $scope.mode = "Edit User";
                    userService.getUser($stateParams.userId).then(function(response) {
                        $scope.currentUser = response.data;
                    });
                }

                $scope.submit = function () {
                    userService.updateUser($scope.currentUser).then(function (response) {
                        if (response.data.Success === true) {
                            $scope.fieldState.showList = true;
                            $state.go('users');
                        } else {
                            $scope.fieldState.errorMessage = response.data.Message;
                        }
                    });
                };

                $scope.cancel = function() {
                    $scope.fieldState.showList = true;
                    $state.go('users');
                };
            }
    ]);
})();