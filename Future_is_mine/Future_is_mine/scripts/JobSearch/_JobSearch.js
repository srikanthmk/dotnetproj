var app = angular.module('myApp', ['ngMaterial', 'ngMessages']);

app.controller('DisplayJobs', function ($scope, $http, $timeout) {
    alert("Hi")

    $scope.project = {
        description: 'Nuclear Missile Defense System',
        rate: 500,
        special: true
    };

    $scope.getJobDetails = function () {
       
        $http.get("/Home/DisplayJob/").then(function (response) {
            console.log(response.data)
            $scope.JobDetails = response.data;
            $timeout(function () {
                $("#joblist").DataTable();
            }, 2000);
        });

    }
    $scope.getJobDetails();

    $scope.onChnage = function (jobid, status) {
     
        $scope.savedata = { "jobid": jobid, "status": status };
        console.log($scope.savedata);


        $http({
            method: 'POST',
            url: '/Jobsearch/saveJob',
            params: $scope.savedata
            
        }).then(function (response) {
            console.log(response.data)
            if (response.data == "Login") {
                alert(response)
                window.location.href = "/Login/Index";
            }
        });

      



    };

});