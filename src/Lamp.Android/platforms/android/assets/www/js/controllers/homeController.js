controllerModule.controller('homeController', function($scope, $state, $stateParams, $ionicLoading,
    HttpService, CommonService, WebService, SqliteService) {

    var ServiceUrl = HttpService.ServiceUrl;

    $scope.data = {};
    $scope.data.username='admin';
    $scope.data.password="admin";

    function init() {
        //socketInit();
    };
    init();

    $scope.login = function() {
        var loginModel = {
            userName: $scope.data.username,
            password: $scope.data.password,
            remember: $scope.data.remember,
            auto: $scope.data.auto
        };
        HttpService.send(ServiceUrl + WebService.AccountLogin, loginModel, function(response) {
            //alert(JSON.stringify(response));
            if (response.result == "Success") {
                alert(response.result);
                $scope.goList();
            } else {
                alert(JSON.stringify(loginModel));
                alert("user name or password wrong");
            }
        })
    }

    $scope.goList = function() {
        $state.go('tab.list');
    }

    function copyDatabaseFile(dbName) {

        var sourceFileName = cordova.file.applicationDirectory + 'www/' + dbName;
        var targetDirName = cordova.file.dataDirectory;

        return Promise.all([
            new Promise(function(resolve, reject) {
                resolveLocalFileSystemURL(sourceFileName, resolve, reject);
            }),
            new Promise(function(resolve, reject) {
                resolveLocalFileSystemURL(targetDirName, resolve, reject);
            })
        ]).then(function(files) {
            var sourceFile = files[0];
            var targetDir = files[1];
            return new Promise(function(resolve, reject) {
                targetDir.getFile(dbName, {}, resolve, reject);
            }).then(function() {
                console.log("file already copied");
            }).catch(function() {
                console.log("file doesn't exist, copying it");
                return new Promise(function(resolve, reject) {
                    sourceFile.copyTo(targetDir, dbName, resolve, reject);
                }).then(function() {
                    console.log("database file copied");
                });
            });
        });
    }
});