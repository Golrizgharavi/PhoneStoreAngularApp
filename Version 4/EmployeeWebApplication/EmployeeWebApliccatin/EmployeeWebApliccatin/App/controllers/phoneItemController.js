

//$scope.id = $routeParams.id;
'use strict'

app.controller('phoneItemController', function ($routeParams, productService) {


    var vm = this;
    var MemoryItems = document.getElementsByClassName("MemItemInn");
    var ColorItems = document.getElementsByClassName("ColorItemImg");
   //alert($routeParams.id);

    var promiseGet = productService.GetPhoneItembyID($routeParams.id);
    

    promiseGet.then(function (p1) {
    vm.phoneItem = p1.data

        vm.message = "Welcome to " + vm.phoneItem.Name +" Page";

    },

        function (errorP1) { alert('Failure Loading!' + errorP1) });

    vm.SetSelection = function (obj,index) {

        for (i = 0; i < obj.length; i++) {
            obj[i].className = obj[i].className.replace(" ItemSelected", "");
        }

        obj[index - 1].className += " ItemSelected";
    },
    vm.GetPhoneColor = function (Id, index) {
        
        vm.SetSelection(ColorItems, index);

        var GetPhoneColorResult = productService.GetPhoneFeatureInfo(Id);
        GetPhoneColorResult.then(function (p1) {

            vm.PhoneColor = p1.data;


        },

            function (errorP1) { alert('Failure Loading!' + errorP1) });

        },

        vm.GetPhoneMemory = function (Id, index) {

        vm.SetSelection(MemoryItems, index);

        var GetPhoneColorResult = productService.GetPhoneFeatureInfo(Id);
        GetPhoneColorResult.then(function (p1) {

            vm.PhoneColor = p1.data;


        },

            function (errorP1) { alert('Failure Loading!' + errorP1) });

    }

});


