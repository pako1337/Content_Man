var LanguageModule = angular.module('Language', ['ui.bootstrap'])
    .config(function ($routeProvider) {
        $routeProvider
            .when("", { controller: LanguageList, templateUrl: "LanguageList.html" })
            .otherwise({ redirectTo: "" });
    });

function LanguageList($scope, $http) {
    $http.get('api/Language')
         .success(function (data, status, headers, config) {
             $scope.languages = data;
         })
         .error(function (data, status, headers, config) {
             console.log("Failed to get languages");
         });
}