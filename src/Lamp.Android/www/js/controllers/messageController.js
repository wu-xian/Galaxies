controllerModule.controller('messageController', function($scope, $state, $stateParams, $ionicLoading,
    HttpService, CommonService) {

    var ServiceUrl = HttpService.ServiceUrl;
    var _socket=new WebSocket(HttpService.SocketUrl);
    
    $scope.data={};

    function init(){
        socketInit();
    };
    init();

    $scope.send=function(){
        alert("##:"+$scope.data.message);
        _socket.send($scope.data.message);
    }

    function socketInit(){

        _socket.onopen=function(){
            alert("im opened");
        }
        _socket.onclose=function(){
            alert("im closed");
        }

        _socket.onmessage=function(event){
            alert("on message#:"+event.data);
        }
    }
});