angular.module('Content', [])
    .config(function ($routeProvider) {
        $routeProvider
            .when('/', { controller: ContentList, templateUrl: 'ContentList.html' })
            .when('/edit/:contentId', { controller: ContentEdit, templateUrl: 'ContentEdit.html' })
            .otherwise({ redirectTo: '/' });
    });

function ContentList($scope, $http) {
    $scope.contentElements = [];

    $http({ method: 'GET', url: '/api/ContentElement' })
        .success(function (data, status, headers, config) {
            $scope.contentElements.push(data);
        })
        .error(function (data, status, headers, config) {
            alert("Oh my! Something not nice has happened");
        });
}

function ContentEdit($scope, $routeParams) {
    $scope.contentElement = { contentId: $routeParams.contentId, Language: 'English', Category: 'Generic', Value: "Na pokładowym chronometrze zwanej przez załogę Latającą Holerą transgalaktycznej pirackiej łajby Małpilus dochodziła godzina dwudziesta pierwsza, gdy wydarzył się ten wypadek. Sam w sobie nie miał wielkiego znaczenia - ot, drobny błąd nawigatora, cała sprawa mogłaby spokojnie obyć się bez konsekwencji. Traf chciał jednak, iż autorzy uczonych ksiąg, które miały dopiero powstać, uznali tę właśnie chwilę za przełomową w dziejach Trzeciej Ery. Nam zaś wszakże nie wypada się spierać z historią. Zamilknijmy przeto i pozwólmy jej mówić własnymi słowy... " }
}