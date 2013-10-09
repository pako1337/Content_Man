var ContentModule = angular.module('Content', ['ui.bootstrap'])
    .config(function ($routeProvider) {
        $routeProvider
            .when('/', { controller: ContentList, templateUrl: 'ContentList.html' })
            .when('/edit/:contentId/', { controller: ContentEdit, templateUrl: 'ContentEdit.html' })
            .when('/edit/:contentId/:lang', { controller: ContentEdit, templateUrl: 'ContentEdit.html' })
            .otherwise({ redirectTo: '/' });
    });

ContentModule.factory('contentProvider', function ($http) {
    return {
        contentElements: [],
        getContentElements: function () {
            var that = this;
            $http.get('/api/ContentElement')
                .success(function (data, status, headers, config) {
                    that.contentElements.length = 0;
                    data.forEach(function (element) {
                        var ce = new ContentElement(element.Id, element.DefaultLanguage, element.Values);
                        that.contentElements.push(ce);
                    })
                })
                .error(function (data, status, headers, config) {
                    console.log("Failed to get ContentElements");
                });
        },

        getcontentElement: function (elementId, elementReady) {
            var that = this;
            $http.get('api/ContentElement/' + elementId)
                .success(function (data, status, headers, config) {
                    var ce = new ContentElement(data.Id, data.DefaultLanguage, data.Values);
                    elementReady(ce);
                })
                .error(function (data, status, headers, config) {
                    console.log("Failed to get ContentElement: " + elementId);
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
        for (var i = 0; i < contentProvider.contentElements.length; i++)
            if (contentProvider.contentElements[i].Id == $routeParams.contentId)
                return contentProvider.contentElements[i];

        contentProvider.getcontentElement($routeParams.contentId, function (contentElement) {
            $scope.contentElement = contentElement;
            if ($routeParams.lang)
                contentElement.changeLanguage($routeParams.lang);
        });
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

    this.changeLanguage = function (languageId) {
        this.SelectedValue = findValueWithLanguage(this.Values, languageId);
    };

    this.changeLanguage(language.LanguageId);

    function findValueWithLanguage(values, languageId) {
        for (var i = 0; i < values.length; i++)
            if (values[i].Language.LanguageId === languageId)
                return values[i];
    }
}