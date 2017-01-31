
var varCarrierController = function ($scope, $http, mainFactory, $httpParamSerializerJQLike) {


    //Check Access to the Page
    var menuitemID =10; //Page ID in DataBase
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
    $scope.showDeleteMessage = 0;

    var JWT_Token = 'Bearer ' + sessionStorage.getItem('JWT_Token');

    //General Action Methods
    $scope.getRecords = function () {


        $scope.showInsertMessage = 0;
        $scope.showUpdateMessage = 0;
        $scope.showDeleteMessage = 0;

        $("div#myLoadingModal").modal({ backdrop: false, keyboard: false, });

        $http({
            url: _ServicePath + '/Carrier/getCarriersManagement',
            method: "GET",
            headers: { 'Content-Type': 'application/x-www-form-urlencoded', 'Authorization': JWT_Token },
            params: {
                "pageNo": $scope.currentPage,
                "filter": $scope.filter,
            },

        })
                .then(
                    function (response) {
                        $('div#myLoadingModal').modal('toggle');

                        $scope.records = response.data.getCarriersDto;
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
        $scope.showDeleteMessage = 0;

        $scope.mainFormNew.$setPristine();
        $scope.modalTitle = "Create Carrier"

        $("div#myLoadingModal").modal({ backdrop: false, keyboard: false, });
        $("body").css("padding-right", "0px");

        $http({
            url: _ServicePath + '/Carrier/getCarrierInitial',
            method: "GET",
            headers: { 'Content-Type': 'application/x-www-form-urlencoded', 'Authorization': JWT_Token },
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
        $scope.showDeleteMessage = 0;

        $scope.mainFormEdit.$setPristine();
        $scope.modalTitle = "Edit Carrier"

        $("div#myLoadingModal").modal({ backdrop: false, keyboard: false, });
        $("body").css("padding-right", "0px");


        $http({
            url: _ServicePath + '/Carrier/getCarrier',
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
            url: _ServicePath + '/Carrier/insertCarrier',
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
            url: _ServicePath + '/Carrier/updateCarrier',
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

    $scope.deleteRecord = function () {

        $.SmartMessageBox({
            "title": "<i class='fa fa-sign-out' style='color:red'></i> Delete the current record ",
            "content": "Are you sure to delete the current record?",
            "buttons": "[No][Yes]"
        }, function (ButtonPress, name) {
            if (ButtonPress != "Yes")
                return;


            $("div#myLoadingModal").modal({ backdrop: false, keyboard: false, });
            $("body").css("padding-right", "0px");

            $http({
                url: _ServicePath + '/Carrier/deleteCarrier',
                method: "Delete",
                headers: { 'Content-Type': 'application/x-www-form-urlencoded', 'Authorization': JWT_Token },
                params: { "id": $scope.record.id },
            })
               .then(
                   function (response) {
                       $('div#myLoadingModal').modal('toggle');
                       $("div#divEdit").modal("toggle");
                       if (response.status == "200") {
                           $scope.filter = "";
                           $scope.currentPage = 1;
                           $scope.getRecords();

                           $scope.showDeleteMessage = 1;

                       }

                   },
                   function (response) {
                       $('div#myLoadingModal').modal('toggle');
                       $("div#divEdit").modal("toggle");
                       mainFactory.Logout();
                   }
               );
        });

    }


    //Initial Page Recrods
    $scope.getRecords();

}
;

varMainApplication.controller("carrierController", varCarrierController);

