

var app = angular.module('myApp', ['ngRoute'])

    .config(['$routeProvider', function ($routeProvider) {

 
        $routeProvider
            .when('/', {
                templateUrl:'views/home.html',
                controller:'homeController'

            })

            .when('/products', {
                templateUrl: 'views/products.html',
                controller: 'productsController'

            })

            .when('/phone', {
                templateUrl: 'views/phone.html',
                controller: 'phoneController'

            })

            .when('/contact', {
                templateUrl: 'views/contact.html',
                controller: 'contactController'

            })
            .when('/createEmployee', {
                templateUrl: 'views/createEmployee.html',
                controller: 'createEmployeeController'

            })
            .when('/employees', {
                templateUrl: 'views/employees.html',
                controller: 'employeesController'

            })
            .when('/updateEmployee', {
                templateUrl: 'views/updateEmployee.html',
                controller: 'updateEmployeeController'

            })

            .when('/phone/:id', {
                templateUrl: 'views/phoneItem.html',
                controller: 'phoneItemController'
            })

            .when('/test2', {
                templateUrl: 'views/Test2.html',
                controller: 'TestCtrl'

            })
            

    }])

    .controller('mainController', function ($scope) {
      
        $scope.message = "main content";
    })