
var varloginController
       = function ($scope, $http, $window, $httpParamSerializerJQLike) {

           $scope.User = {
               username: "",
               password: "",
           };

           $scope.IsInvalid = 0;

           $scope.Login = function () {

               sessionStorage.removeItem('JWT_Token');

               $("div#myLoadingModal").modal({ backdrop: false, keyboard: false, });
               $http({
                   url: _ServicePath + '/account/login',
                   method: "POST",
                   headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                   data: $httpParamSerializerJQLike($scope.User) //use data for post,put and delete methods
                    })
                 .then(
                     function (response) {
                         $('div#myLoadingModal').modal('toggle');


                         if (response.data!=null &&  response.data.JWT != null) {
                             sessionStorage.setItem("JWT_Token", response.data.JWT);
                             $window.location.href = 'index.html';
                         }

                         else {
                             $scope.IsInvalid = 1;
                         }


                     },
                     function (response) {
                         sessionStorage.removeItem('JWT_Token');
                         $('div#myLoadingModal').modal('toggle');
                         $scope.IsInvalid = 1;
                     }
                 );
           };



       };

varMainApplication.controller("loginController", varloginController);
//jQuery.support.cors = true;