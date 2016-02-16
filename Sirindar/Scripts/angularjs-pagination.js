function jsPageing(jsonObject, jsonUrl, numberOfItemsToShow, numberOfPagesToShow) {

    var app = angular.module('listApp', []);

    app.controller('listController', function ($scope, $http, $filter) {

        //Inicialilzacion Start
        Validations();
        Initializer(jsonObject);
        startPagination();
        //Inicialilzacion End

        function Initializer(_jsonObject) {
            $scope.tempItems = [];
            $scope.pages = [];
            $scope.tempPages = [];
            $scope.currentPage = 1;
            $scope.items = $.parseJSON(_jsonObject);
            var pages = Math.round(($scope.items.length / numberOfItemsToShow) + .4999);
            for (var i = 0; i < pages; i++) {
                $scope.pages.push(i + 1);
            }
            $scope.notMore = $scope.items.length < $scope.count;
            $scope.count = $scope.items.length;
            $scope.isHide = numberOfItemsToShow >= $scope.items.length;
        }

        function Validations() {
            var e;
            if ($.parseJSON(jsonObject).length == 0)
                $scope.message = "No hay ningun registro aun en la base de datos";
            else if (numberOfPagesToShow > 17)
                e = "The max value for 'numberOfPagesToShow' is 17";
            else if (numberOfPagesToShow % 2 != 1)
                e = "The 'numberOfPagesToShow' is not a impart number";
            else if (numberOfItemsToShow <= 1)
                e = "The min value for 'numberOfItemsToShow' is 2";
            else if (numberOfPagesToShow < 3)
                e = "The min value'numberOfPagesToShow' is 3";
            if (e != null)
                throw e;
        }

        function setTempItems(start, end) {
            if (!($scope.items.length < numberOfItemsToShow)) {
                if ($scope.items.length < end && start != 0) {
                    end = start + (numberOfItemsToShow - (end - $scope.items.length));
                }
            } else {
                start = 0;
                end = $scope.items.length;
            }
            for (var i = start; i < end; i++) {
                $scope.tempItems.push($scope.items[i]);
            }
        }

        function startPagination() {
            setTempItems(0, numberOfItemsToShow);
            setTempPages(1);
        }

        function setTempPages(page) {
            var pivot = Math.round(numberOfPagesToShow / 2);
            var endPage = $scope.pages[$scope.pages.length - 1];
            var first = 0;
            var last = 0;

            if (endPage < numberOfPagesToShow) {
                first = 1;
                last = endPage;
            }
            else if (page <= pivot) {
                first = 1;
                last = numberOfPagesToShow;
            } else if (page > pivot && page <= (endPage - pivot)) {
                first = (page - pivot) + 1;
                last = (page + pivot) - 1;
            } else if (page >= (endPage - pivot)) {
                first = (endPage - numberOfPagesToShow) + 1;
                last = endPage;
            }
            $scope.tempPages = [];
            for (first; first <= last; first++) {
                $scope.tempPages.push(first);
            }
        }

        function showContentInModal(content) {
            $("#idModal .modal-content").replaceWith('<div class="modal-content">' + content + '</div>');
            $("#idModal").modal('show');
        }

        $scope.changePage = function (p) {
            if (p <= 0 || p > $scope.pages.length)
                return;
            $scope.tempItems = [];
            setTempItems(((numberOfItemsToShow * p) - numberOfItemsToShow), (numberOfItemsToShow * p));
            setTempPages(p);

            $scope.currentPage = p;
        }        

        //Reinicialilzacion Start
        $scope.sp = function (start, count) {
            $http.get(jsonUrl + '?start=' + start + '&' + 'count=' + count).success(function (jsonObject) {
                Initializer(jsonObject);
                startPagination();
                $scope.n = ($scope.notMore) ? 0 : $scope.n;
            });
        }
        //Reinicialilzacion End

        $scope.serch = function (str) {
            $scope.tempItems = [];
            if (str == "") {
                Initializer(jsonObject);
                startPagination();
                return;
            }
            else
                $scope.isHide = true;
            var _ti = $filter("filter")($scope.items, str);
            var l = (_ti.length > 16) ? 16 : _ti.length;
            for (var i = 0; i < l; i++) {
                $scope.tempItems.push(_ti[i]);
            }
        }

        $scope.createItem = function (url) {
            $http.get(url).success(function (r) {
                showContentInModal(r);
            });
        }

        $scope.editItem = function (url, id) {
            $http.get(url + '/' + id).success(function (r) {
                showContentInModal(r);
            });
        }

        $scope.deleteConfirm = function (url, id) {
            $http.get(url + '/' + id).success(function (r) {
                showContentInModal(r)
            });
        }


    });
}