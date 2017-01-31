

var varindexController =
function ($scope, $http, $window, $rootScope, mainFactory) {



    $scope.userInfo = null;
    $scope.menutitlesDto = null;
    $rootScope.pageTitle = "Satras MTFS";


    $window.onload = function () {

        var JWT_Token = 'Bearer ' + sessionStorage.getItem('JWT_Token');

        $("div#myLoadingModal").modal({ backdrop: false, keyboard: false, });

        $http({
            url: _ServicePath + '/account/getUserInfo',
            method: "GET",
            headers: { 'Content-Type': 'application/x-www-form-urlencoded', 'Authorization': JWT_Token },
        })
                .then(
                    function (response) {
                        $('div#myLoadingModal').modal('toggle');
                        if (response.data.resultCode == "200") {
                            $scope.menutitlesDto = response.data.menutitlesDto;
                            $scope.userInfo = response.data.userInfoDto;
                        }
                        else
                            mainFactory.Logout();

                    },
                    function (response) {
                        $('div#myLoadingModal').modal('toggle');
                        mainFactory.Logout();
                    }
                );

    };

    $scope.Logout = function () {

        $.SmartMessageBox({
            "title": "<i class='fa fa-sign-out' style='color:green'></i> Quit System ",
            "content": "Are you sure to log out?",
            "buttons": "[No][Yes]"
        }, function (ButtonPress, name) {
            if (ButtonPress !== "Yes")
                return;

            $("div#myLoadingModal").modal({ backdrop: false, keyboard: false, });


            mainFactory.Logout();
            return;
        });

    };

    $scope.getHref = function (item) {
        var strHref = "";
        if (item.href == null)
            strHref = "javascript:void(0);"
        else
            strHref = "#/menus/" + item.href.replace(/\//g, " ");


        return strHref;
    };

    $scope.getMenuTitleClass = function (item) {
        if (item.displayOrder == "1")
            return "active";
        return "";
    };


    $scope.resetSetting = function () {

        $rootScope.isUp = 0;
        $rootScope.isChild = 0;
        $rootScope.parentId = 0;
        $rootScope.parentTitle = "";
        $rootScope.back_LevelFirst = 0;
        $rootScope.back_LevelSecond = 0;
    }
};
varMainApplication.controller("indexController", varindexController);





