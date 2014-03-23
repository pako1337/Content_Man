var ContentModule = angular.module('Content', ['ui.bootstrap'])
    .config(function ($routeProvider) {
        $routeProvider
            .when('',                       { controller: ContentList, templateUrl: 'ContentList.html' })
            .when('/',                      { controller: ContentList, templateUrl: 'ContentList.html' })
            .when('/edit/:contentId/:lang', { controller: ContentEdit, templateUrl: 'ContentEdit.html' })
            .when('/add/:lang',             { controller: ContentAdd,  templateUrl: 'ContentEdit.html' })
            .otherwise({ redirectTo: '' });
    });

ContentModule.factory('contentProvider', function ($http) {
    return {
        contentElements: [],
        getContentElements: function () {
            var that = this;
            $http.get('/api/ContentElement')
                 .success(function (data, status, headers, config) {
                     that.contentElements.length = 0;
                     data.map(function (e) { return new ContentElement(e) })
                         .forEach(function (e) { that.contentElements.push(e) });
                 })
                 .error(function (data, status, headers, config) {
                     console.log("Failed to get ContentElements");
                 });
        },

        getcontentElement: function (elementId, elementReady) {
            $http.get('api/ContentElement/' + elementId)
                 .success(function (data, status, headers, config) {
                     elementReady(new ContentElement(data));
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

function ContentEdit($scope, $routeParams, $http, $location, contentProvider) {
    $scope.contentElement = undefined;

    (function () {
        var elements = contentProvider.contentElements.filter(
            function (e) { return e.ContentElementId === $routeParams.contentId });
        if (elements.length > 0)
            setContentElement(elements[0], $routeParams.lang);
        else
            contentProvider.getcontentElement(
                $routeParams.contentId,
                function (e) { setContentElement(e, $routeParams.lang) });
    })();

    function setContentElement(contentElement, lang) {
        contentElement.changeLanguage(lang);
        $scope.contentElement = contentElement;
    }

    $scope.save = function () {
        $http.put('api/ContentElement/' + $scope.contentElement.ContentElementId, $scope.contentElement);
        $location.path("/");
    }
}

function ContentAdd($scope, $routeParams, $http, $location) {
    $scope.contentElement = new ContentElement({
        ContentElementId: -1,
        DefaultLanguage: $routeParams.lang,
        TextContents: [{ Language: $routeParams.lang }]
    });

    $scope.save = function () {
        $http.post('api/ContentElement/', $scope.contentElement);
        $location.path("/");
    };
}

function ContentElement(contentElementDto) {
    this.ContentElementId = contentElementDto.ContentElementId;
    this.DefaultLanguage = contentElementDto.DefaultLanguage;
    this.TextContents = contentElementDto.TextContents;
    this.ContentType = 1;
    this.SelectedValue;
    this.Languages = this.TextContents.map(function (val) { return val.Language; });

    this.changeLanguage = function (lang) {
        this.SelectedValue = findValueWithLanguage(this.TextContents, lang);
    };

    this.changeLanguage(this.DefaultLanguage);

    function findValueWithLanguage(values, lang) {
        return values.filter(function (v) { return v.Language === lang })[0];
    }
}