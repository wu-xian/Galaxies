controllerModule.controller('roomController', function($scope, $state, $stateParams, $ionicLoading,$ionicScrollDelegate,
    HttpService, CommonService,WebService) {

    var ServiceUrl = HttpService.ServiceUrl;
    var _socket=new WebSocket(HttpService.SocketUrl+$stateParams.id);
    $scope.msgList=[];

    function init(){
        initSocket();
    }
    init();

    function initSocket(){
        _socket.onopen=function(){
            alert("opened");
        }

        _socket.onclose=function(){
            alert("closed");
        }
        _socket.onmessage=function(event){
            $scope.msgList.push(event.data);
            $scope.$apply();
            $ionicScrollDelegate.scrollBottm();
        }
    }

    $scope.sendMsg=function(){
        _socket.send($scope.msgData);
    }
});