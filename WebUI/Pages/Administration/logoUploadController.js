

var varUploadLogoController = function ($scope, $http, $window, mainFactory) {

    var menuitemID = 4;
    $scope.message = "Upload Company Logo!!!";
    $scope.pageClass = 'page-send';
    mainFactory.checkPageAccess(menuitemID);

    $scope.add = function () {
        $("div#myLoadingModal").modal({ backdrop: false, keyboard: false, });

        var f = document.getElementById('file').files[0],
            r = new FileReader();
        r.onloadend = function (e) {


            var theLogo = e.target.result;
       
            var n = theLogo.indexOf(";base64,");
            var theMime = theLogo.substring(5,n);
            var theLogo = theLogo.substring(n + 8);
       
            var companyLogoDto = {};
            companyLogoDto.id = -1;
            companyLogoDto.logo = theLogo;
            companyLogoDto.mime = theMime;
         

            $http({
                url: '/Account/UpdateCompanyLogo',
                method: "POST",
                data: JSON.stringify(companyLogoDto),
                withCredentials: true,
                headers: { 'Content-Type': 'application/json' },
                dataType: 'json',
                transformRequest: angular.identity
            })
               .then(
                   function (response) {
                       $('div#myLoadingModal').modal('toggle');
                       alert("OK");
                   },
                   function (response) {
                       $('div#myLoadingModal').modal('toggle');
                       alert(response.data);
                   }
               );


        }
        r.readAsDataURL(f);
      
    }



};

varMainApplication.controller("logoUploadController", varUploadLogoController);
