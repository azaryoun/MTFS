
var varnegotiationplanController = function ($scope, $http, mainFactory, $httpParamSerializerJQLike,$rootScope, $window) {
      
    //Check Access to the Page
    var menuitemID = 1; //Page Id Negotiation
    mainFactory.checkPageAccess(menuitemID);

    //Inititalizing scope (model) variables   
     
    $scope.records = null;
    $scope.filter = "";
    $scope.currentPage = 1;
    $scope.totalRecordCount = 0;
    $scope.fromRow = 1;
    $scope.toRow = 25;
    $scope.totalPagesCount = 1;
    $scope.record = null;
    $scope.showInsertMessage = 0;
    $scope.showUpdateMessage = 0;
    $scope.title = "";
    $scope.parentId = 0;
    $scope.parentTitle = ""; 
    $scope.showUp = $rootScope.isChild;
     
    if ($rootScope.isChild) {

        if ($rootScope.parentId != 'undefined') {
            $scope.parentId = $rootScope.parentId;
        }

        if ($rootScope.parentTitle != 'undefined') {
            $scope.parentTitle = $rootScope.parentTitle;
        }

    }

    if ($rootScope.isUp == 1 && $rootScope.back_LevelFirst != 0) {
        $scope.parentId = $rootScope.back_LevelFirst;
    }

    var JWT_Token = 'Bearer ' + sessionStorage.getItem('JWT_Token');

    //General Action Methods
    $scope.getRecords = function () {

        $scope.showInsertMessage = 0;
        $scope.showUpdateMessage = 0;

        $("div#myLoadingModal").modal({ backdrop: false, keyboard: false, });

        $http({
            url: _ServicePath + '/Negotiationplan/getNegotiationplansManagement',
            method: "GET",
            headers: { 'Content-Type': 'application/x-www-form-urlencoded', 'Authorization': JWT_Token },
            params: {
                "pageNo": $scope.currentPage,
                "filter": $scope.filter,
                "parentId": $scope.parentId,
                "parentTitle": $scope.parentTitle,
            },

        })
                .then(
                    function (response) {
                        $('div#myLoadingModal').modal('toggle');

                        $scope.title = response.data.title;
                        $scope.parent_ParentId = response.data.parent_ParentId;
                        $scope.records = response.data.getNegotiationplansDto;
                        $scope.currentPage = response.data.currentPage;
                        $scope.totalRecordCount = response.data.totalRecordCount;
                        $scope.totalPagesCount = Math.ceil($scope.totalRecordCount / response.data.recordCountPage);

                        if ($scope.records != null && $scope.records.length > 0) {
                            $scope.fromRow = (($scope.currentPage - 1) * response.data.recordCountPage) + 1;
                            $scope.toRow = $scope.records.length + $scope.fromRow - 1;
                        }
                        else {
                            $scope.fromRow = 0;
                            $scope.toRow = 0;
                        }

                    },
                    function (response) {
                        $('div#myLoadingModal').modal('toggle');
                        mainFactory.Logout();
                    }
                );

    };

    $scope.searchRecords = function () {


        $scope.currentPage = 1;
        $scope.getRecords();
    };

    $scope.getClass = function (index) {
        index++;
        if (index % 2 == 0)
            return "even";
        return "odd";

    }

    $scope.getFirstPage = function () {
        if ($scope.currentPage == 1)
            return;
        $scope.currentPage = 1;
        $scope.getRecords();
    }

    $scope.getPrevPage = function () {
        if ($scope.currentPage == 1)
            return;
        $scope.currentPage--;
        $scope.getRecords();
    }

    $scope.getNextPage = function () {
        if ($scope.currentPage == $scope.totalPagesCount)
            return;
        $scope.currentPage++;
        $scope.getRecords();
    }

    $scope.getLastPage = function () {
        if ($scope.currentPage == $scope.totalPagesCount)
            return;
        $scope.currentPage = $scope.totalPagesCount;
        $scope.getRecords();
    }

    $scope.getCurrentPage = function () {
        if ($scope.currentPage < 1)
            $scope.currentPage = 1;
        else if ($scope.currentPage > $scope.totalPagesCount)
            $scope.currentPage = $scope.totalPagesCount
        $scope.getRecords();
    }

    $scope.newRecord = function () {

        $scope.showInsertMessage = 0;
        $scope.showUpdateMessage = 0;

        $scope.mainFormNew.$setPristine();
        $scope.modalTitle = "Create Negotiation Plan"

        $("div#myLoadingModal").modal({ backdrop: false, keyboard: false, });
        $("body").css("padding-right", "0px");

        $http({
            url: _ServicePath + '/Negotiationplan/getNegotiationplanInitial',
            method: "GET",
            headers: { 'Content-Type': 'application/x-www-form-urlencoded', 'Authorization': JWT_Token },
            params: {
                "parentId": $scope.parentId,
            },
        })
                .then(
                    function (response) {
                        $('div#myLoadingModal').modal('toggle');
                        $scope.record = response.data;

                        $("div#divNew").modal({ backdrop: false, keyboard: false, });
                        $("body").css("padding-right", "0px");

                    },
                    function (response) {
                        $('div#myLoadingModal').modal('toggle');
                        mainFactory.Logout();
                    }
                );

    }

    $scope.editRecord = function (record) {

        $scope.showInsertMessage = 0;
        $scope.showUpdateMessage = 0;

        $scope.mainFormEdit.$setPristine();
        $scope.modalTitle = "Edit Negotiation Plan"

        $("div#myLoadingModal").modal({ backdrop: false, keyboard: false, });
        $("body").css("padding-right", "0px");


        $http({
            url: _ServicePath + '/Negotiationplan/getNegotiationplan',
            method: "GET",
            headers: { 'Content-Type': 'application/x-www-form-urlencoded', 'Authorization': JWT_Token },
            params: { "id": record.id },
        })
                .then(
                    function (response) {
                        $('div#myLoadingModal').modal('toggle');
                        $scope.record = response.data;


                        $("div#divEdit").modal({ backdrop: false, keyboard: false, });
                        $("body").css("padding-right", "0px");

                    },
                    function (response) {


                        $('div#myLoadingModal').modal('toggle');
                        mainFactory.Logout();
                    }
                );
    }

    //New Record Div Actions
    $scope.insertRecord = function () {


        $("div#myLoadingModal").modal({ backdrop: false, keyboard: false, });
        $("body").css("padding-right", "0px");

        $http({
            url: _ServicePath + '/Negotiationplan/insertNegotiationplan',
            method: "POST",
            headers: { 'Content-Type': 'application/x-www-form-urlencoded', 'Authorization': JWT_Token },
            data: $httpParamSerializerJQLike($scope.record),
        })
           .then(
               function (response) {
                   $('div#myLoadingModal').modal('toggle');
                   $("div#divNew").modal("toggle");
                   if (response.status == "201") {

                       $scope.filter = "";
                       $scope.currentPage = 1;
                       $scope.getRecords();
                       $scope.showInsertMessage = 1;

                   }
                   else {
                       mainFactory.Logout();
                   }

               },
               function (response) {
                   $('div#myLoadingModal').modal('toggle');
                   $("div#divNew").modal("toggle");
                   mainFactory.Logout();
               }
           );


    }
    //Edit Record Div Actions
    $scope.updateRecord = function () {


        $("div#myLoadingModal").modal({ backdrop: false, keyboard: false, });
        $("body").css("padding-right", "0px");

        $http({
            url: _ServicePath + '/Negotiationplan/updateNegotiationplan',
            method: "PUT",
            headers: { 'Content-Type': 'application/x-www-form-urlencoded', 'Authorization': JWT_Token },
            data: $httpParamSerializerJQLike($scope.record),
        })
           .then(
               function (response) {
                   $('div#myLoadingModal').modal('toggle');
                   $("div#divEdit").modal("toggle");
                   if (response.status == "200") {
                       $scope.getRecords();

                       $scope.showUpdateMessage = 1;

                   }

               },
               function (response) {
                   $('div#myLoadingModal').modal('toggle');
                   $("div#divEdit").modal("toggle");
                   mainFactory.Logout();
               }
           );

    }


    $scope.goUp = function () {

        $rootScope.isUp = 0;
        $rootScope.isChild = 0;
        $rootScope.parentId = 0;
        $rootScope.parentTitle = "";
        $rootScope.back_LevelFirst = 0;
        $rootScope.back_LevelSecond = 0;
        $window.location.href = "#/menus/" + "SalesMarketing/Negotiation/outboundNegotiation".replace(/\//g, " ");

    }

    //Initial Page Recrods
    $scope.getRecords();

    $scope.subAction1 = function (id, title, parent_ParentId) {

        $rootScope.isUp = 0;
        $rootScope.isChild = 1;
        $rootScope.parentId = id;
        $rootScope.back_LevelFirst = parent_ParentId;
        $rootScope.parentTitle = title;
        $window.location.href = "#/menus/" + "SalesMarketing/Negotiationplanroute/negotiationplanroute".replace(/\//g, " ");

    }
}
;

varMainApplication.controller("negotiationplanController", varnegotiationplanController);

