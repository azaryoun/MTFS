

var _ServicePath = "http://localhost:24713/api";

var varMainApplication =
    angular.module("mainApplication", ['ngRoute', 'ngAnimate', 'angularjs-datetime-picker']);

varMainApplication.directive('myPostRepeatDirective', function () {
    return function (scope, element, attrs) {
        if (scope.$last) {
            initApp.leftNav();
        }
    };
});


varMainApplication.directive('ngCompare', function () {
    return ({

        restrict: 'A',
        require: 'ngModel',

        link: function (scope, element, attributes, control) {

            validate = function (scope, element, attributes, control) {

                if ((scope[attributes.ngModel] == null) || (scope[attributes.ngModel] == "")) {

                    control.$setValidity("compare", true);

                }
                else {

                    if (scope[attributes.ngModel].toUpperCase() == scope[attributes.ngCompare].toUpperCase()) {

                        control.$setValidity("compare", true);
                    }
                    else {

                        control.$setValidity("compare", false);

                    }

                }

            };

            scope.$watch(attributes.ngModel, function () {

                validate(scope, element, attributes, control);

            });

            scope.$watch(attributes.ngCompare, function () {

                validate(scope, element, attributes, control);

            });

        },

    })

});


varMainApplication.factory('mainFactory', function ($http, $window, $rootScope) {

    function checkPageAccess(menuitemId) {

        var JWT_Token = 'Bearer ' + sessionStorage.getItem('JWT_Token');


        $http({
            url: _ServicePath + '/Account/CheckPageAccess',
            method: "POST",
            headers: { 'Content-Type': 'application/x-www-form-urlencoded', 'Authorization': JWT_Token },
            params: { "menuitemId": menuitemId },
        })
                   .then(
                       function (response) {
                           if (response.data.resultCode != "200")
                               Logout();
                           else {
                               $rootScope.pageTitle = response.data.pageTitle
                               return;
                           }


                       },
                       function (response) {
                           Logout();
                       }
                   );


    };

    function Logout() {


        sessionStorage.removeItem('JWT_Token');

        $window.location.href = '/pages/login.html';


    };


    var service = {
        checkPageAccess: checkPageAccess,
        Logout: Logout,
    };

    return service;

});


var varMainRouteConfig =
 function ($routeProvider) {
     $routeProvider.
         when('/', {
             templateUrl: '/Pages/home.html',
             controller: 'homeController',
             title: "Satras MTFS",
         }).
         when('/menus/:path',
             {
                 templateUrl: function (params) {
                     var res = "/Pages/" + params.path.replace(/ /g, "/") + ".html";
                     return res;
                 },
             }
         )
         .
     otherwise({ redirectTo: '/' })
     ;
 }

varMainApplication.config(varMainRouteConfig);
//jQuery.support.cors = true;