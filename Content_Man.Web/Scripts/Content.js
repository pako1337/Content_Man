var ContentModule = angular.module('Content', ['ui.bootstrap'])
    .config(function ($routeProvider) {
        $routeProvider
            .when('',                       { controller: ContentList, templateUrl: 'ContentList.html' })
            .when('/',                      { controller: ContentList, templateUrl: 'ContentList.html' })
            .when('/edit/:contentId/:lang', { controller: ContentEdit, templateUrl: 'ContentEdit.html' })
            .when('/add/:lang',             { controller: ContentAdd,  templateUrl: 'ContentEdit.html' })
            .otherwise({ redirectTo: '' });
    });

ContentModule.factory('contentProvider', function ($http, $q) {
    return {
        contentElements: [],
        getContentElements: function () {
            var that = this;
            var defered = $q.defer();

            if (that.contentElements.length > 0) {
                defered.resolve(that.contentElements);
            }
            else {
                $http.get('/api/ContentElement')
                    .success(function (data, status, headers, config) {
                        that.contentElements.length = 0;
                        data.map(function (e) { return new ContentElement(e) })
                            .forEach(function (e) { that.contentElements.push(e) });

                        defered.resolve(that.contentElements);
                    })
                    .error(function (data, status, headers, config) {
                        console.log("Failed to get ContentElements");
                        defered.reject("Failed to get ContentElements");
                    });
            }

            return defered.promise;
        },

        getcontentElement: function (elementId) {
            var that = this;
            var defered = $q.defer();

            var elements = that.contentElements.filter(
                function (e) { return e.contentElementId === elementId });

            if (elements.length > 0) {
                defered.resolve(elements[0]);
            }
            else {
                $http.get('api/ContentElement/' + elementId)
                     .success(function (data, status, headers, config) {
                         var contentElement = new ContentElement(data);
                         defered.resolve(contentElement);
                     })
                     .error(function (data, status, headers, config) {
                         console.log("Failed to get ContentElement: " + elementId);
                         defered.reject("Failed to get ContentElement: " + elementId);
                     });
            }

            return defered.promise;
        }
    };
});

function ContentList($scope, contentProvider) {
    $scope.contentElements = contentProvider.contentElements;
    $scope.selectedLanguage = "";
    $scope.availableLanguages = [];

    function prepareLanguageFiltering() {
        if ($scope.contentElements.length > 0) {
            $scope.contentElements
                  .map(function (element) { return element.languages; })
                  .reduce(function (previous, languages) { return previous.concat(languages) })
                  .forEach(function (language) {
                      if ($scope.availableLanguages.indexOf(language) == -1)
                          $scope.availableLanguages.push(language);
                  });
            $scope.selectedLanguage = $scope.availableLanguages[0];
        }
    }

    var resultPromise = contentProvider.getContentElements();
    resultPromise.then(prepareLanguageFiltering);
}

function ContentEdit($scope, $routeParams, $http, $location, contentProvider) {
    $scope.contentElement = undefined;
    
    var resultPromise = contentProvider.getcontentElement($routeParams.contentId);
    resultPromise.then(function (contentElement) {
        contentElement.changeLanguage($routeParams.lang);
        $scope.contentElement = contentElement;
    });

    $scope.save = function () {
        $http.put('api/ContentElement/' + $scope.contentElement.contentElementId, $scope.contentElement);
        $location.path("/");
    }
}

function ContentAdd($scope, $routeParams, $http, $location) {
    $scope.contentElement = new ContentElement({
        contentElementId: -1,
        defaultLanguage: $routeParams.lang,
        textContents: [{ Language: $routeParams.lang }]
    });

    $scope.save = function () {
        $http.post('api/ContentElement/', $scope.contentElement);
        $location.path("/");
    };
}

function ContentElement(contentElementDto) {
    this.contentElementId = contentElementDto.ContentElementId;
    this.defaultLanguage = contentElementDto.DefaultLanguage;
    this.textContents = contentElementDto.TextContents;
    this.contentType = 1;
    this.selectedValue;
    this.languages = this.textContents.map(function (val) { return val.Language; });

    this.changeLanguage = function (lang) {
        this.selectedValue = findValueWithLanguage(this.textContents, lang);
    };

    this.changeLanguage(this.defaultLanguage);

    function findValueWithLanguage(values, lang) {
        return values.filter(function (v) { return v.Language === lang })[0];
    }
}