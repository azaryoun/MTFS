
var varchangePasswordController = function ($scope, $rootScope, $http, mainFactory, $window, $httpParamSerializerJQLike) {


    //Inititalizing scope (model) variables
    $rootScope.pageTitle = "Change password";

    $scope.showOkMessage = 0;
    $scope.showNokMessage = 0;

    $scope.modalTitle = $rootScope.pageTitle;

    $scope.userChangePass = null;

    //Action Mathods
    $scope.changePassword = function () {

       
        var JWT_Token = 'Bearer ' + sessionStorage.getItem('JWT_Token');


        $scope.showOkMessage = 0;
        $scope.showNokMessage = 0;

        $scope.userChangePass.newPassword = $scope.newPassword;

        $("div#myLoadingModal").modal({ backdrop: false, keyboard: false, });
        $("body").css("padding-right", "0px");



        $http({
            url: _ServicePath + '/Account/ChangePassword',
            method: "PUT",
            headers: { 'Content-Type': 'application/x-www-form-urlencoded', 'Authorization': JWT_Token },
            data: $httpParamSerializerJQLike($scope.userChangePass)

        })
           .then(
               function (response) {
                   $('div#myLoadingModal').modal('toggle');
                   $("div#divEdit").modal("toggle");
                   if (response.status == "200") {
                       $scope.userChangePass = null;
                       $scope.newPassword = "";
                       $scope.confirmPassword = "";
                       $scope.showOkMessage = 1;
                       $scope.mainForm.$setPristine();

                   }

                   else {
                       $scope.showNokMessage = 1;
                       $scope.userChangePass.currentPassword = "";
                       $scope.mainForm.$setPristine();

                   }

               },
               function (response) {
                   $('div#myLoadingModal').modal('toggle');
                   mainFactory.Logout();
               }
           );

    }

}
;

varMainApplication.controller("changePasswordController", varchangePasswordController);

