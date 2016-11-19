controllerModule.controller('contentController', function($scope, $state, $stateParams, $ionicLoading,
    HttpService, FileService, CommonService) {

    $scope.detailmodel = {};
    $scope.detailmodel.title = $stateParams.title;


    $scope.init = function() {
        var articleId = $stateParams.id;
        var serviceUrl = HttpService.ServiceUrl;
        var requestFile = serviceUrl + '/download';

        $ionicLoading.show({
            template: "内容载入中"
        });
        HttpService.send(serviceUrl + "/article/getarticlecontent", {
            articleId: articleId
        }, function(response) {
            //设置文章内容
            $scope.detailmodel = {
                content: response.content,
                title: response.title,
                files: response.files
            };
            var imgContainer = document.getElementById("imgContainer");
            if ($scope.detailmodel.files[0].name) {
                angular.forEach($scope.detailmodel.files, function(data, index) {
                    //请求文章中的文件
                    HttpService.downloadFile(requestFile + "/" + articleId + "/" + data.number,
                        data.name, {},
                        function(targetPath, response) {
                            //alert(targetPath+":"+data.number);
                            imgContainer.innerHTML += addImage(targetPath);
                        });
                });
            }
        }, function(err) {
            alert(err);
        }, function() {
            $ionicLoading.hide();
        });
    }
    $scope.init();

    function addImage(targetPath) {
        return '<div class="item item-image"><img src = "' + targetPath + '"></div>';
    }
})