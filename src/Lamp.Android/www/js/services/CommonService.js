serviceModule.factory('CommonService', function($ionicLoading, $cordovaCamera, $cordovaFileTransfer) {
    var CommonService = this;

    CommonService.showMessage = function(message) {
        $ionicLoading.show({
            template: message,
            noBackdrop: true,
            duration: 2000
        });
    }

    CommonService.takePhoto = function(_callback) {
        var options = {
            //这些参数可能要配合着使用，比如选择了sourcetype是0，destinationtype要相应的设置
            quality: 100, //相片质量0-100
            destinationType: Camera.DestinationType.FILE_URI, //返回类型：DATA_URL= 0，返回作为 base64 編碼字串。 FILE_URI=1，返回影像档的 URI。NATIVE_URI=2，返回图像本机URI (例如，資產庫)
            sourceType: Camera.PictureSourceType.CAMERA, //从哪里选择图片：PHOTOLIBRARY=0，相机拍照=1，SAVEDPHOTOALBUM=2。0和1其实都是本地图库
            allowEdit: false, //在选择之前允许修改截图
            encodingType: Camera.EncodingType.JPEG, //保存的图片格式： JPEG = 0, PNG = 1
            targetWidth: 200, //照片宽度
            targetHeight: 200, //照片高度
            mediaType: 0, //可选媒体类型：圖片=0，只允许选择图片將返回指定DestinationType的参数。 視頻格式=1，允许选择视频，最终返回 FILE_URI。ALLMEDIA= 2，允许所有媒体类型的选择。
            cameraDirection: 0, //枪后摄像头类型：Back= 0,Front-facing = 1
            popoverOptions: CameraPopoverOptions,
            saveToPhotoAlbum: true //保存进手机相册
        };

        $cordovaCamera.getPicture(options).then(function(imageData) {
                _callback(imageData);
            },
            function(err) {
                // error
                CommonService.showMessage("图片获取失败:" + err);
                //CommonJs.AlertPopup(err.message);
            });
    }

    CommonService.downloadFile = function(filePath) {
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

    CommonService.uploadFile = function(url, targetPath) {
        //target path may be local or url  
        var filename = targetPath.split("/").pop();
        var options = {
            fileKey: "file",
            fileName: filename,
            chunkedMode: false,
            mimeType: "image/jpg"
        };
        $cordovaFileTransfer.upload(url, targetPath, options)
            .then(function(result) {
                    console.log("SUCCESS: " + JSON.stringify(result.response));
                    alert("success");
                    alert(JSON.stringify(result.response));
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

    //表示全局唯一标识符 (GUID)。
    CommonService.Guid = function(g) {
            var arr = new Array(); //存放32位数值的数组
            if (typeof(g) == "string") { //如果构造函数的参数为字符串
                InitByString(arr, g);
            } else {
                InitByOther(arr);
            }
            //返回一个值，该值指示 Guid 的两个实例是否表示同一个值。
            this.Equals = function(o) {
                    if (o && o.IsGuid) {
                        return this.ToString() == o.ToString();
                    } else {
                        return false;
                    }
                }
                //Guid对象的标记
            this.IsGuid = function() {}
                //返回 Guid 类的此实例值的 String 表示形式。
            this.ToString = function(format) {
                    if (typeof(format) == "string") {
                        if (format == "N" || format == "D" || format == "B" || format == "P") {
                            return ToStringWithFormat(arr, format);
                        } else {
                            return ToStringWithFormat(arr, "D");
                        }
                    } else {
                        return ToStringWithFormat(arr, "D");
                    }
                }
                //由字符串加载
            function InitByString(arr, g) {
                g = g.replace(/\{|\(|\)|\}|-/g, "");
                g = g.toLowerCase();
                if (g.length != 32 || g.search(/[^0-9,a-f]/i) != -1) {
                    InitByOther(arr);
                } else {
                    for (var i = 0; i < g.length; i++) {
                        arr.push(g[i]);
                    }
                }
            }

            //由其他类型加载
            function InitByOther(arr) {
                var i = 32;
                while (i--) {
                    arr.push("0");
                }
            }
            /*
            根据所提供的格式说明符，返回此 Guid 实例值的 String 表示形式。
            N  32 位： xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
            D  由连字符分隔的 32 位数字 xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
            B  括在大括号中、由连字符分隔的 32 位数字：{xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}
            P  括在圆括号中、由连字符分隔的 32 位数字：(xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)
            */
            function ToStringWithFormat(arr, format) {
                switch (format) {
                    case "N":
                        return arr.toString().replace(/,/g, "");
                    case "D":
                        var str = arr.slice(0, 8) + "-" + arr.slice(8, 12) + "-" + arr.slice(12, 16) + "-" + arr.slice(16, 20) + "-" + arr.slice(20, 32);
                        str = str.replace(/,/g, "");
                        return str;
                    case "B":
                        var str = ToStringWithFormat(arr, "D");
                        str = "{" + str + "}";
                        return str;
                    case "P":
                        var str = ToStringWithFormat(arr, "D");
                        str = "(" + str + ")";
                        return str;
                    default:
                        return new Guid();
                }
            }
        }
        //Guid 类的默认实例，其值保证均为零。
    // Guid.Empty = new CommonService.Guid();
    // //初始化 Guid 类的一个新实例。
    // Guid.NewGuid = function() {
    //     var g = "";
    //     var i = 32;
    //     while (i--) {
    //         g += Math.floor(Math.random() * 16.0).toString(16);
    //     }
    //     return new Guid(g);
    // }

    return CommonService;
})