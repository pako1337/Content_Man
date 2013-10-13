var ContentModule = angular.module('Content', ['ui.bootstrap'])
    .config(function ($routeProvider) {
        $routeProvider
            .when('/', { controller: ContentList, templateUrl: 'ContentList.html' })
            .when('/edit/:contentId/', { controller: ContentEdit, templateUrl: 'ContentEdit.html' })
            .when('/edit/:contentId/:lang', { controller: ContentEdit, templateUrl: 'ContentEdit.html' })
            .when('/add/:lang', { controller: ContentAdd, templateUrl: 'ContentEdit.html' })
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
                        var ce = new ContentElement(element.ContentElementId, element.DefaultLanguage, element.TextContents);
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
                    var ce = new ContentElement(data.Id, data.DefaultLanguage, data.TextContents);
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
    $scope.contentElement = undefined;

    (function () {
        for (var i = 0; i < contentProvider.contentElements.length; i++)
            if (contentProvider.contentElements[i].ContentElementId == $routeParams.contentId) {
                setContentElement(contentProvider.contentElements[i], $routeParams.lang);
                return;
            }

        contentProvider.getcontentElement($routeParams.contentId, function (contentElement) {
            setContentElement(contentElement, $routeParams.lang);
        });
    })();

    function setContentElement(ce, lang) {
        if (lang) ce.changeLanguage(lang);
        $scope.contentElement = ce;
    }
}

function ContentAdd($scope, $routeParams, $http) {
    var value = { Language: $routeParams.lang };
    $scope.contentElement = new ContentElement(-1, $routeParams.lang, [value]);

    $scope.save = function () {
        console.log("save");
        console.log($scope.contentElement);

        $http.post('api/ContentElement/', $scope.contentElement);
    };
}

function ContentElement(id, language, values) {
    this.ContentElementId = id;
    this.DefaultLanguage = language;
    this.Values = values;
    this.SelectedValue;

    this.Languages = (function (values) {
        var languages = [];
        for (var i = 0; i < values.length; i++)
            languages.push(values[i].Language);

        return languages;
    })(this.Values);

    this.changeLanguage = function (language) {
        this.SelectedValue = findValueWithLanguage(this.Values, language);
    };

    this.changeLanguage(language);

    function findValueWithLanguage(values, languageId) {
        for (var i = 0; i < values.length; i++)
            if (values[i].Language === languageId)
                return values[i];
    }
}