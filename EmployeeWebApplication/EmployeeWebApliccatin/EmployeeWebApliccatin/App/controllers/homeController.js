'use strict'
/*
app.controller('homeController', function($scope) {

    
    //$scope.message = "Welcome";
})
*/

app.controller('homeController', function (productService) {

    var vm = this;
    vm.message = "Golriz Test";
    var promiseGet = productService.getProductCount();

    promiseGet.then(function (p1) { vm.productCount = p1.data; },
        function (errorP1) {
            alert('FAILURE LOADING Products', errorP1)
        });



});
