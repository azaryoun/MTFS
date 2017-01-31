



angular.module('KLibrary', []);

angular.module('KLibrary').directive('persianKalender', function () {

    return ({

        scope: {
            kalndername: '=',
            value: '=',
            showtime: '=',
        },
        restrict: 'A',
        replace: true,

        template:
             "<div class='input-group'>" +
             "<div class='input-group-addon'  data-MdDateTimePicker='true' data-targetselector='#{{kalndername}}' data-trigger='click' data-enabletimepicker='{{showtime}}'>" +
              "<span class='glyphicon glyphicon-calendar'></span>" +
              "</div>" +
             "<input type='text' id='{{kalndername}}' data-ng-model='value' readonly='readonly' style='text-align:center;background-color:transparent' class='form-control'  />" +
             "</div>"


            ,

        controller: function ($scope) {
            alert(1);
            $scope.showtime = "false";
            //   $scope.kalndername = "kalndername";

        },

    });

});