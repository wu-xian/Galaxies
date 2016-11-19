controllerModule.controller('newArticleController', function($scope, $state, $stateParams, $ionicLoading, $ionicActionSheet, $cordovaImagePicker,
    HttpService, CommonService, FileService) {
    //*add new page ======================================

    var fileList = [];

    //go to add new article page
    $scope.newModel = {};

    $scope.openCamera = function() {
        var fileParth = CommonService.takePhoto(function(imageData) {
            var imgElement = "<img src='" + imageData + "' style='width:50%;height:100%;'>";
            document.getElementById("imgContainer").innerHTML += imgElement;
            fileList.push(imageData);
        });
    }

    $scope.addNew = function() {
        $ionicLoading.show({
            template: '内容提交中'
        });
        HttpService.send(HttpService.ServiceUrl + '/article/addcontent', {
                json: JSON.stringify($scope.newModel)
            },
            function(response) {
                var guid = response.id;
                angular.forEach(fileList, function(data, index) {
                    HttpService.uploadFile(HttpService.ServiceUrl + '/article/savefile?guid=' + guid + '&index=' + index,
                        data, {},
                        function(fileResponse) {})
                });
                $ionicLoading.hide();
                $state.go("tab.list");
            },
            function(err) {
                alert("err" + err);
                $ionicLoading.hide();
            })

        //}
    }

    $scope.actionSheet = function() {
        if (!checkImgCountIsOK()) {
            CommonService.showMessage("已经超过最大数量");
            return;
        }

        var hideSheet = $ionicActionSheet.show({
            buttons: [
                { text: '图片库' },
                { text: '拍照' }
            ],
            titleText: '上传图片',
            cancelText: '取消',
            cancel: function() {
                // add cancel code..
            },
            buttonClicked: function(index) {
                switch (index) {
                    case 0:
                        {
                            $scope.pickImage();
                            break;
                        }
                    case 1:
                        {
                            $scope.openCamera();
                            break;
                        }

                }
                return true;
            }
        });

        $scope.pickImage = function() {



            var options = {
                maximumImagesCount: 4 - fileList.length,
                width: 800,
                height: 800,
                quality: 80
            };

            $cordovaImagePicker.getPictures(options)
                .then(function(results) {
                    angular.forEach(results, function(data, index) {
                        var imgElement = "<img src='" + data + "' style='width:50%;height:100%;'>";
                        document.getElementById("imgContainer").innerHTML += imgElement;
                        fileList.push(data);
                    })

                }, function(error) {
                    // error getting photos  
                });

        }

        function checkImgCountIsOK() {
            if (fileList.length < 4) {
                return true;
            }
            return false;
        }

        // $timeout(function() {
        //     hideSheet();
        // }, 2000);

    };

});