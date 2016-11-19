serviceModule.factory('FileService', function($ionicLoading, $cordovaCamera, $cordovaFileTransfer) {
    var FileService = this;

    FileService.downloadFile = function(filePath) {
        var filename = url.split("/").pop();
        alert(filename);
        var targetPath = cordova.file.externalRootDirectory + filename;
        var trustHosts = true
        var options = {};
        //url提交的服务器地址 targetPath提交图片的本地地址  
        $cordovaFileTransfer.download(filePath, targetPath, options, trustHosts)
            .then(function(result) {
                // Success!  
                alert(JSON.stringify(result)); //把对象转化成字符串  
            }, function(error) {
                // Error  
                alert(JSON.stringify(error));
            }, function(progress) {
                $timeout(function() {
                    //$scope.downloadProgress = (progress.loaded / progress.total) * 100;
                })
            });
    }

    FileService.uploadFile = function(url, targetPath, _callback) {
        //target path may be local or url  
        var filename = targetPath.split("/").pop();
        var _options = {
            fileKey: "file",
            fileName: filename,
            chunkedMode: false,
            mimeType: "image/jpg"
        };
        $cordovaFileTransfer.upload(url, targetPath, _options)
            .then(function(result) {
                    console.log("SUCCESS: " + JSON.stringify(result.response));
                    alert("success");
                    alert(JSON.stringify(result.response));
                    _callback(result);
                },
                function(err) {
                    console.log("ERROR: " + JSON.stringify(err));
                    alert(JSON.stringify(err));
                },
                function(progress) {
                    // constant progress updates $timeout(function () { $scope.downloadProgress = (progress.loaded / progress.total) * 100; }) }); }});
                }
            )
    }

    return FileService;
})