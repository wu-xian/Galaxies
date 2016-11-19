// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
// 'starter.services' is found in services.js
// 'starter.controllers' is found in controllers.js
angular.module('starter', ['ionic', 'starter.controllers', 'starter.services', 'ngCordova'])

.run(function($ionicPlatform) {
    $ionicPlatform.ready(function() {
        // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
        // for form inputs)
        if (cordova.platformId === 'ios' && window.cordova && window.cordova.plugins && window.cordova.plugins.Keyboard) {
            cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
            cordova.plugins.Keyboard.disableScroll(true);
        }
        if (window.StatusBar) {
            // org.apache.cordova.statusbar required
            StatusBar.styleDefault();
        }
		var aa;
    });
})

.config(function($stateProvider, $urlRouterProvider, $ionicConfigProvider, $httpProvider) {

    $ionicConfigProvider.platform.android.tabs.style('standard');
    $ionicConfigProvider.platform.android.tabs.position('bottom');
    $ionicConfigProvider.platform.android.navBar.alignTitle('left');

    $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';

    // Ionic uses AngularUI Router which uses the concept of states
    // Learn more here: https://github.com/angular-ui/ui-router
    // Set up the various states which the app can be in.
    // Each state's controller can be found in controllers.js
    $stateProvider

    // setup an abstract state for the tabs directive
        .state('tab', {
        url: '/tab',
        abstract: true,
        templateUrl: 'templates/tabs.html'
    })

    // Each tab has its own nav history stack:


    .state('tab.list', {
        url: '/list',
        cache: true,
        views: {
            'tab-list': {
                templateUrl: 'templates/tab-list.html',
                controller: 'articleController'
            }
        }
    })

    .state('tab.detail', {
        url: '/list/:id/:title',
        cache: false,
        views: {
            'tab-list': {
                templateUrl: 'templates/tab-detail.html',
                controller: 'contentController'
            }
        }
    })

    .state('tab.new', {
        url: '/new',
        cache: true,
        views: {
            'tab-list': {
                templateUrl: 'templates/tab-new.html',
                controller: 'newArticleController'
            }
        }
    })

    .state('tab.message', {
        url: '/message',
        cache: false,
        views: {
            'tab-message': {
                templateUrl: 'templates/tab-message.html',
                controller: 'messageController'
            }
        }
    })

    .state('home', {
        url: '/home',
        cache: false,
        controller: 'homeController',
        templateUrl: 'templates/home.html'
    })
    .state('room', {
        url: '/room/:id',
        cache: false,
        templateUrl: 'templates/room.html',
        controller: 'roomController'
    });

    // if none of the above states are matched, use this as the fallback
    $urlRouterProvider.otherwise('/home');

});

var controllerModule = angular.module("starter.controllers", []);
var serviceModule = angular.module("starter.services", ['ngCordova']);