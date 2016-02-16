function AngularPagination(jsonItems, displeyNumber) {
    //$scope.Items = [
    //    ApplicationUser = {
    //        Roles: [{
    //            UserId: null,
    //            RoleId: null,
    //            Role: {
    //                Id: null,
    //                Name: null
    //            }
    //        }],
    //        Nombre: null,
    //        Email: null,
    //        Id: null,
    //        UserName: null
    //    }];

    var _tempIt = {
        page: 0,
        item: null,
        constt: function (p, i) {
            this.page = p,
            this.item = i
        }
    }
    Array.prototype.PushLR = function (direction, obj) {
        switch (direction) {
            case 'left':
                for (var i = this.length - 1; i > 0; i--) {
                    this[i] = this[i - 1];
                }
                this[0] = obj;
                break;
            case 'right': for (var i = 0; i < this.length; i++) {
                this[i] = this[i + 1];
            }
                this[this.length - 1] = obj;
                break;
            default:
        }
    }

    var ArraySimpleObjects = new Array();
    var app = angular.module('listApp', []);

    app.controller('listController', function ($scope, $http, $filter) {
        $scope.items = [];
        $scope.ranges = [];
        $scope.rangeItems = [];
        $scope.pagesIndexed = [];
        $scope.strCadena = "";
        var _tempItems = $.parseJSON(jsonItems);
        var _tempRange = new Array();
        var _tempItem = {
            index: 0,
            items: [],
            constr: function (idx, itms) {
                this.index = idx,
                this.items = itms
            }
        }

        debugger;
        var _tempindex = 0;
        var cont = 0;
        for (var i = 0; i <= _tempItems.length; i++) {
            _tempRange.push(new _tempIt.constt(0, _tempItems[i]));
            cont++;
            if (cont == displeyNumber) {
                cont = 0;
                _tempindex++;
                _tempRange.forEach(function (val) {
                    val.page = _tempindex;
                });
                $scope.ranges.push(new _tempItem.constr(_tempindex, _tempRange));
                _tempRange = new Array();
            }
        }

        $scope.rangeItems = $scope.ranges[0].items

        if ($scope.ranges.length > 5) {
            for (var i = 0; i < 5; i++) {
                $scope.pagesIndexed.push($scope.ranges[i].index);
            }
        }
        else {
            for (var i = 0; i < $scope.ranges.length; i++) {
                $scope.pagesIndexed.push($scope.ranges[i].index);
            }
        }

        $scope.items = _tempItems;


        $scope.loadItems = function (index) {
            $scope.rangeItems = $scope.ranges[index - 1].items;
        }

        $scope.confirmDelete=function(url,id){
            var modalContent = $("#idModal .modal-content");
            var modal = $("#idModal");
            $http.get(url + '/' + id).success(function (response) {                
                modalContent.replaceWith('<div class="modal-content">' + response + '</div>');
                modal.modal('show');
            });
        }

        $scope.first = function (index) {
            for (var i = 0; i < $scope.pagesIndexed.length; i++) {
                $scope.pagesIndexed[i] = $scope.ranges[i].index;
            }
            $scope.rangeItems = $scope.ranges[0].items;
        }

        $scope.last = function (index) {
            for (var i = 0; i < $scope.pagesIndexed.length; i++) {
                $scope.pagesIndexed[i] = $scope.ranges[($scope.ranges.length - $scope.pagesIndexed.length) + i].index;
            }
            $scope.rangeItems = $scope.ranges[index - 1].items;
        }

        $scope.prev = function (index) {
            if (!($scope.pagesIndexed[0] <= 1)) {
                $scope.pagesIndexed.PushLR('left', ($scope.pagesIndexed[0] - 1));
            }
            $scope.rangeItems = $scope.ranges[index - 2].items
        }

        $scope.next = function (index) {
            if (!($scope.pagesIndexed[$scope.pagesIndexed.length - 1] >= $scope.ranges.length)) {
                $scope.pagesIndexed.PushLR('right', ($scope.pagesIndexed[$scope.pagesIndexed.length - 1] + 1));
            }
            $scope.rangeItems = $scope.ranges[index].items
        }

        $scope.serch = function (str) {
            $scope.rangeItems = new Array();
            var temp = $filter("filter")($scope.items, str);
            for (var i = 0; i < temp.length; i++) {
                $scope.rangeItems.push(new _tempIt.constt(0, temp[i]));
            }
        }
    });
}