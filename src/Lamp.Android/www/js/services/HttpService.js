serviceModule.factory('HttpService', function($http, $cordovaFileTransfer) {
    var HttpService = this;
    //var ApiUrl = "http://10.202.101.45:88";
    var ApiUrl = "http://192.168.0.88:88";
    var SocketUrl = "ws://192.168.0.88:88/ws/233";
    HttpService.ServiceUrl = ApiUrl;
    HttpService.SocketUrl = SocketUrl;

    HttpService.get = function(action, param, successCallback, errorCallback, finallyCallback) {
        $http.post(ApiUrl + action, param)
            .success(function(response) {
                if (successCallback) {
                    successCallback(response);
                }
            })
            .error(function(errMsg) {
                if (errorCallback) {
                    errorCallback(errMsg);
                }
            }).finally(function() {
                if (finallyCallback) {
                    finallyCallback();
                }
            })
    }

    function ObjecttoParams(obj) {
        var p = [];
        for (var key in obj) {
            p.push(key + '=' + encodeURIComponent(obj[key]));
        }
        return p.join('&');
    };

    HttpService.send = function(action, params, successCallback, errorCallback, finallyCallback) {

        var req = {
            method: 'POST',
            url: action,
            //data: ObjecttoParams(params),
            params: params,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        };

        $http(req)
            .success(function(response, status, headers, config) {
                if (successCallback) {
                    successCallback(response, status, headers, config);
                }
            })
            .error(function(errMsg) {
                if (errorCallback) {
                    errorCallback(errMsg);
                }
            })
            .finally(function() {
                if (finallyCallback) {
                    finallyCallback();
                }
            })
    }

    HttpService.downloadFile = function(action, saveName, params, successCallback, errorCallback, finallyCallback) {
        var filename = saveName;
        var req = {
            method: 'POST',
            url: action,
            data: ObjecttoParams(params),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        };
        //alert(filename);
        var targetPath = cordova.file.externalRootDirectory + filename;
        var trustHosts = true
        var options = {};
        //url提交的服务器地址 targetPath提交图片的本地地址  
        $cordovaFileTransfer.download(action, targetPath, req, trustHosts)
            .then(function(result) {
                // Success!  
                //alert(JSON.stringify(result)); //把对象转化成字符串  
                successCallback(targetPath, result);
            }, function(error) {
                // Error  
                //alert(JSON.stringify(error));
                if (errorCallback) {
                    errorCallback(error);
                }
            }, function(progress) {
                //$timeout(function() {
                //$scope.downloadProgress = (progress.loaded / progress.total) * 100;
                //})
            });
    }

    HttpService.uploadFile = function(action, targetPath, params, successCallback, errorCallback, finallyCallback) {
        //target path may be local or url  
        var filename = targetPath.split("/").pop();
        var req = {
            method: 'POST',
            url: action,
            data: ObjecttoParams(params),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            fileKey: "file",
            fileName: filename,
            chunkedMode: false,
            mimeType: "image/jpg"
        };
        var _options = {
            fileKey: "file",
            fileName: filename,
            chunkedMode: false,
            mimeType: "image/jpg"
        };
        $cordovaFileTransfer.upload(action, targetPath, _options)
            .then(function(result) {
                    console.log("SUCCESS: " + JSON.stringify(result.response));
                    //alert("success");
                    //alert(JSON.stringify(result.response));
                    successCallback(result);
                },
                function(err) {
                    console.log("ERROR: " + JSON.stringify(err));
                    //alert(JSON.stringify(err));
                    if (errorCallback) {
                        errorCallback(err);
                    }
                },
                function(progress) {
                    // constant progress updates $timeout(function () { $scope.downloadProgress = (progress.loaded / progress.total) * 100; }) }); }});
                    //finallyCallback();
                }
            )
    }

    HttpService.uploadFiles = function(action, targetPath, params, successCallback, errorCallback, finallyCallback) {

        var filePaths = [
            'files:///file1',
            'files:///file2'
        ];

        uploadFiles('http://server.dev/upload', filePaths, function(filePath) {
            return {
                fileKey: 'file',
                params: {
                    filePath: filePath
                },
                chunkedMode: false
            };
        });

        //target path may be local or url  
        var filename = targetPath.split("/").pop();
        var req = {
            method: 'POST',
            url: action,
            data: ObjecttoParams(params),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            fileKey: "file",
            fileName: filename,
            chunkedMode: false,
            mimeType: "image/jpg"
        };
        $cordovaFileTransfer.upload(action, targetPath, _options)
            .then(function(result) {
                    console.log("SUCCESS: " + JSON.stringify(result.response));
                    //alert("success");
                    //alert(JSON.stringify(result.response));
                    successCallback(result);
                },
                function(err) {
                    console.log("ERROR: " + JSON.stringify(err));
                    //alert(JSON.stringify(err));
                    if (errorCallback) {
                        errorCallback(err);
                    }
                },
                function(progress) {
                    // constant progress updates $timeout(function () { $scope.downloadProgress = (progress.loaded / progress.total) * 100; }) }); }});
                    //finallyCallback();
                }
            )
    }
    return HttpService;
})