serviceModule.factory('WebService', function($http, $cordovaFileTransfer) {
    var WebService = this;

    WebService.AccountLogin="/account/login";

    return WebService;
})