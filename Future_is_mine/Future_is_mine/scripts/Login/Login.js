var app = angular.module("myApp", ['ngMaterial', 'ngMessages']);

app.controller("Employee", function ($scope, $http) {
    
    $scope.message;
    $scope.checkLoginDetails = function (formdata) {
        alert("HI")
        var email = formdata.email;
        var password = formdata.password;
        console.log(email);
        $http({
            method: 'GET',
            url: '/Employee/CheckCredentials',
            params: {
             email,password
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