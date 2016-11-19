serviceModule.factory('SqliteService', function($ionicLoading, $cordovaCamera, $cordovaFileTransfer) {
    var SqliteService = this;
    var db;

    SqliteService.query = function(query, dataParams, successCb, errorCb) {
        $ionicPlatform.ready(function() {
            $cordovaSQLite.execute(db, query, dataParams).then(function(res) {
                successCb(res);
            }, function(err) {
                errorCb(err);
            });
        }.bind(this));
    }

    SqliteService.open = function() {
        db = $cordovaSQLite.openDB({
            name: "lamp.db",
            location: 'default'
        });
    }

    return SqliteService;
})