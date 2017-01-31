
var varspController = function ($scope, $http, mainFactory, $window) {

  
    //Check Access to the Page
    var menuitemID = 4; //Page ID in DataBase
    mainFactory.checkPageAccess(menuitemID);

    //Inititalizing scope (model) variables
    $scope.records = null;
    $scope.filter = null;
    $scope.currentPage = 1;
    $scope.totalRecordCount = 0;
    $scope.fromRow = 1;
    $scope.toRow = 25;
    $scope.totalPagesCount = 1;
    $scope.record = null;
    $scope.showInsertMessage = 0;
    $scope.showUpdateMessage = 0;
    $scope.showDeleteMessage = 0;
   
    //General Action Methods
    $scope.getRecords = function () {

        $scope.showInsertMessage = 0;
        $scope.showUpdateMessage = 0;
        $scope.showDeleteMessage = 0;

        $("div#myLoadingModal").modal({ backdrop: false, keyboard: false, });

        $http({
            url: '/SP/getChargesManagement',
            method: "GET",
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            dataType: 'json',
            params: {
                "pageNo": $scope.currentPage,
                "filter": $scope.filter,
            },
        })
                .then(
                    function (response) {
                        $('div#myLoadingModal').modal('toggle');

                        $scope.records = response.data.charges;
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
                        $window.location.href = '/pages/login.html';
                    }
                );

    };

    $scope.searchRecords = function () {
      
        if ($scope.filter == "")
            $scope.filter = null;
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
      
        $scope.modalTitle = "ثبت جابجایی شارژ مربوط به SP"
      

        $("div#myLoadingModal").modal({ backdrop: false, keyboard: false, });
        $("body").css("padding-right", "0px");

        $http({
            url: '/SP/getChargeInitial',
            method: "GET",
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            dataType: 'json',
            params: null,
        })
                .then(
                    function (response) {
                        $('div#myLoadingModal').modal('toggle');

                        $scope.record = response.data;
                        
                        $("div#divNew").modal({ backdrop: false, keyboard: false, });

                        TempPCale(jQuery);
                       
                        $("body").css("padding-right", "0px");
                  
                    },
                    function (response) {
                        $('div#myLoadingModal').modal('toggle');
                        $window.location.href = '/pages/login.html';
                    }
                );

    }

    $scope.editRecord = function (record) {


        $scope.showInsertMessage = 0;
        $scope.showUpdateMessage = 0;
        $scope.showDeleteMessage = 0;

        $scope.mainFormEdit.$setPristine();
        $scope.modalTitle = "ویرایش جابجایی شارژ مربوط به SP"



        $("div#myLoadingModal").modal({ backdrop: false, keyboard: false, });
        $("body").css("padding-right", "0px");

    

        $http({
            url: '/SP/getCharge',
            method: "GET",
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            dataType: 'json',
            params: {"id":record.id},
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
                        $window.location.href = '/pages/login.html';
                    }
                );
    }

    //New Record Div Actions
    $scope.insertRecord = function () {
   
    
        $("div#myLoadingModal").modal({ backdrop: false, keyboard: false, });
        $("body").css("padding-right", "0px");
      
        $http({
            url: '/SP/insertCharge',
            method: "POST",
            headers: { 'Content-Type': 'application/json' },
            dataType: 'json',
            transformRequest: true,
            data: JSON.stringify($scope.record),
        })
           .then(
               function (response) {
                   $('div#myLoadingModal').modal('toggle');
                  if (response.data.resultCode == "200")
                   {
                       $("div#divNew").modal("toggle");
                   
                       $scope.filter = null;
                       $scope.currentPage = 1;
                       $scope.getRecords();
                       $scope.showInsertMessage = 1;
                    
                   }
                   else
                   {
                       $("div#divNew").modal("toggle");
                       $window.location.href = '/pages/login.html';
                   }
                  
               },
               function (response) {
                   $('div#myLoadingModal').modal('toggle');
                   $("div#divNew").modal("toggle");
                   $window.location.href = '/pages/login.html';
               }
           );


    }
    //Edit Record Div Actions
    $scope.updateRecord = function () {

     
         $("div#myLoadingModal").modal({ backdrop: false, keyboard: false, });
         $("body").css("padding-right", "0px");
        
         $http({
             url: '/SP/updateCharge',
             method: "PUT",
             headers: { 'Content-Type': 'application/json' },
             dataType: 'json',
             transformRequest: true,
             data: JSON.stringify($scope.record),
         })
            .then(
                function (response) {
                    $('div#myLoadingModal').modal('toggle');
                    $("div#divEdit").modal("toggle");
                    if (response.data.resultCode == "200") {
                        $scope.getRecords();

                        $scope.showUpdateMessage = 1;
                    
                    }

                },
                function (response) {
                    $('div#myLoadingModal').modal('toggle');
                    $("div#divEdit").modal("toggle");
                    $window.location.href = '/pages/login.html';
                }
            );

    }
   
    $scope.deleteRecord = function () {
        return;

    };

     
    //Initial Page Recrods
    $scope.getRecords();

}
;

varMainApplication.controller("spController", varspController);

