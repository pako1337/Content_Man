var ContentModule = angular.module('Content', ['ui.bootstrap'])
    .config(function ($routeProvider) {
        $routeProvider
            .when('/', { controller: ContentList, templateUrl: 'ContentList.html' })
            .when('/edit/:contentId', { controller: ContentEdit, templateUrl: 'ContentEdit.html' })
            .otherwise({ redirectTo: '/' });
    });

ContentModule.factory('contentProvider', function ($http) {
    return {
        contentElements: [],
        getContentElements: function () {
            var that = this;
            $http({ method: 'GET', url: '/api/ContentElement' })
                .success(function (data, status, headers, config) {
                    that.contentElements.length = 0;
                    data.forEach(function (element) {
                        var ce = new ContentElement(element.Id, element.DefaultLanguage, element.Values);
                        that.contentElements.push(ce);
                    })
                })
                .error(function (data, status, headers, config) {
                    alert("Oh my! We have a sittuation here!");
                });
        }
    };
});

function ContentList($scope, contentProvider) {
    $scope.contentElements = contentProvider.contentElements;

    if (contentProvider.contentElements.length == 0)
        contentProvider.getContentElements();
}

function ContentEdit($scope, $routeParams, contentProvider) {
    $scope.contentElement = (function () {
        for (var i = 0; i < contentProvider.contentElements.length; i++) {
            if (contentProvider.contentElements[i].Id == $routeParams.contentId)
                return contentProvider.contentElements[i];
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