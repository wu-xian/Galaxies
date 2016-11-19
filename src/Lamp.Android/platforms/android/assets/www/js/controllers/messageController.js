controllerModule.controller('messageController', function($scope, $state, $stateParams, $ionicLoading,
    HttpService, CommonService,WebService) {

    var ServiceUrl = HttpService.ServiceUrl;
    
    $scope.data={};
    $scope.pageIndex=0;
    $scope.roomList=[];
    // $scope.roomList.push({
    //     Name:"wuxian",
    //     LinkCount:2,
    //     CreateTime:'20161616',
    //     id:989
    // });

    // $scope.roomList.push({
    //     Name:"wuxian111",
    //     LinkCount:3,
    //     CreateTime:'201616112',
    //     id:777
    // });

    function init(){
        getRoomList();
    };
    init();

        function getRoomList() {
        HttpService.send(ServiceUrl + WebService.RoomGetList, {
                index: $scope.pageIndex
            },
            function(response) {
                $scope.roomList = response;
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
        init();
    }

    //go to article detail page
    $scope.gotoRoom = function(_id) {
        $state.go("room", {  id: _id } );
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