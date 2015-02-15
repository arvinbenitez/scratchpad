(function () {
    "use strict";

    var myAppModule = angular.module('myApp', ['ui.router']);
    myAppModule.config(function ($stateProvider, $urlRouterProvider) {
        // For any unmatched url, send to /users
        //$urlRouterProvider.otherwise("/users");

        $stateProvider
          .state('users', {
              url: "/users",
              templateUrl: "areas/accounts/scripts/views/users.html",
              controller: "UsersController"
          })
         .state('users_new', {
             url: "users/new",
             templateUrl: "areas/accounts/scripts/views/useredit.html",
             controller: "UsersController"
         })
        .state('users_edit', {
            url: "users/edit/:userId",
            templateUrl: "areas/accounts/scripts/views/useredit.html",
            controller: "UsersController"
        });
    });
})();