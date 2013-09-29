angular.module('Content', ['ui.bootstrap'])
    .config(function ($routeProvider) {
        $routeProvider
            .when('/', { controller: ContentList, templateUrl: 'ContentList.html' })
            .when('/edit/:contentId', { controller: ContentEdit, templateUrl: 'ContentEdit.html' })
            .otherwise({ redirectTo: '/' });
    });

var contentElements = [];

function ContentList($scope, $http) {
    $scope.contentElements = contentElements;

    if ($scope.contentElements.length == 0) {
        $http({ method: 'GET', url: '/api/ContentElement' })
            .success(function (data, status, headers, config) {
                data.forEach(function (element) {
                    var ce = new ContentElement(element.Id, element.DefaultLanguage, element.Values);
                    contentElements.push(ce);
                })

                $scope.contentElements = contentElements;
            })
            .error(function (data, status, headers, config) {
                alert("Oh my! Something not nice has happened");
            });
    }
}

function ContentEdit($scope, $routeParams) {
    $scope.contentElement = (function () {
        for (var i = 0; i < contentElements.length; i++) {
            if (contentElements[i].Id == $routeParams.contentId)
                return contentElements[i];
        }
    })();
}

function ContentElement(id, language, values) {
    this.Id = id;
    this.Values = values;
    this.SelectedValue;
    this.Languages = (function (values) {
        var languages = [];
        for (var i = 0; i < values.length; i++)
            languages.push(values[i].Language);

        return languages;
    })(this.Values);

    this.changeLanguage = function (isoCode) {
        this.SelectedValue = findValueWithLanguage(this.Values, isoCode);
    };

    this.changeLanguage(language.IsoCode);

    function findValueWithLanguage(values, isoCode) {
        for (var i = 0; i < values.length; i++)
            if (values[i].Language.IsoCode === isoCode)
                return values[i];
    }
}