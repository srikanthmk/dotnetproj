var app = angular.module("myModule", ['ngRoute','ngMessages']);
 


app.controller("RegistrationCont", function ($scope, $rootScope, $http) {
    console.log("in controller");

    $rootScope.regdata = [];

    $scope.countrys = ["India","New Zealand","Denmark"];
    $scope.classification = ["Web Developement","Web Designing","full stack","mean stack"];
    
    $scope.saveCustomer = function (reg) {
        $rootScope.regdata = reg;
        console.log($rootScope.regdata);
        $scope.isViewLoading = true;
       $http({
            method: 'POST',
            url: '/Employee/InsertEmployee',
            data: $rootScope.regdata
        }).then(function mySuccess(data, status, headers, config) {
            if (data.success === true) {
                $scope.message = 'Form data Saved!';
                $scope.result = "color-green";
                $scope.custModel = {};
                console.log(data);
            }
            else {
                $scope.message = 'Form data not Saved!';
                $scope.result = "color-red";
            }
        }).error(function myError(data, status, headers, config) {
            $scope.message = 'Unexpected Error while saving data!!' + data.errors;
            $scope.result = "color-red";
            console.log($scope.message);
        });
        $scope.isViewLoading = false;
    };




});


app.directive('usernameAvailable', function ($timeout, $q, $http) {
    return {
        restrict: 'AE',
        require: 'ngModel',
        link: function (scope, elm, attr, model) {
            model.$asyncValidators.usernameExists = function () {
                console.log(model.$viewValue);

                return $http({
                    method: 'POST',
                    url: '/Registration/CheckEmail',
                    data: { "EmployeeEmail": model.$viewValue }
                }).then(function mySuccess(res, status, headers, config) {
                    $timeout(function () {

                        //  model.$setValidity('usernameExists', !!res.data);
                        model.$setValidity('usernameExists', res.data);
                    }, 1000);
                    console.log(res.data);
                });



            };
        }
    }
});
app.directive('ngCheckemail', ['$http', function (async) {
    var message;
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, ctrl) {

            elem.on('change', function (evt) {


                scope.$apply(function () {
                    var val = elem.val();
                    var req = {
                        "EmployeeEmail": val
                    }

                    var ajaxConfiguration = {
                        method: 'POST',
                        url: '/Registration/CheckEmail',
                        data: req
                    };
                    async(ajaxConfiguration)
                        .success(function (data, status, headers, config) {
                            ctrl.$setValidity('unique', data.result);
                            if (data == "success") {
                                message = "Email Already Exit"
                            }
                            else {

                            }
                            alert(message)
                        });
                });
            });
        }
    }
}]);

app.controller("employeeLogin", function ($scope, $http) {

    $scope.message;
    $scope.checkLoginDetails = function (formdata) {
        alert("HI")
        var email = formdata.email;
        var password = formdata.password;
        console.log(email);
        $http({
            method: 'POST',
            url: '/Employee/CheckCredentials',
            params: {
                email, password
            },
        }).then(function mySuccess(response) {
            console.log(response.data);
            if (response.data == "True") {
                //window.location.href = "/Jobsearch/jobs";
            }
            else {
                $scope.message = 'Invalid Credentials!';
            }

        }, function myError(response) {
            $scope.myWelcome = response.statusText;
        });

    }
});


