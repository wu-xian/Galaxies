controllerModule.controller('articleController', function($scope, $state, $stateParams, $ionicLoading,
    HttpService, CommonService) {

    var ServiceUrl = HttpService.ServiceUrl;
    $scope.articleList = [];

    $scope.init = function() {
        $scope.isShowInfinite = true;
        $scope.listBottomShow = false;
        $scope.pageIndex = 0;
        getArticleList();
    }
    $scope.init();


    function getArticleList() {
        HttpService.send(ServiceUrl + '/article/gettitlelist', {
                index: $scope.pageIndex
            },
            function(response) {
                $scope.articleList = response;
                $scope.pageIndex++;
                //CommonService.showMessage("刷新成功");
            },
            function(err) {
                CommonService.showMessage("刷新失败，请检查你的网络连接:" + err);
            },
            function() {
                $scope.$broadcast('scroll.refreshComplete');
            });
    }

    $scope.refresh = function() {
        $scope.init();
    }

    //go to article detail page
    $scope.gotoDetail = function(_id, _title) {
        //var _model=$scope.articleList[_id];
        $state.go("tab.detail", { title: _title, id: _id } );
    }

    //*add new page ======================================

    //go to add new article page
    $scope.newModel = {};

    $scope.goNew = function() {
        $state.go("tab.new");
    }

    $scope.createNew = function() {
        $ionicLoading.show({
            template: 'loading...123'
        });
        HttpService.send('/article/add', {
                json: JSON.stringify($scope.newModel)
            },
            function(response) {
                alert("seccess" + response);
                $ionicLoading.hide();
            },
            function(err) {
                alert("err" + err);
                $ionicLoading.hide();
            })
    }

    $scope.loadMore = function() {
        $scope.pageIndex++;
        HttpService.send(ServiceUrl + '/article/gettitlelist', {
                index: $scope.pageIndex
            },
            function(response) {
                //$scope.articleList = response;
                if (response[0]) {
                    //$scope.articleList.push(response);

                    angular.forEach(response, function(data, index) {
                        $scope.articleList.push(data);
                    });

                } else {
                    CommonService.showMessage("没有更多啦~~~");
                    $scope.listBottomShow = true;
                    $scope.isShowInfinite = false;
                }
                //CommonService.showMessage("加载成功");
            },
            function(err) {
                CommonService.showMessage("加载失败，请检查你的网络连接:" + err);
            },
            function() {
                $scope.$broadcast("scroll.infiniteScrollComplete");
            });
    }
});