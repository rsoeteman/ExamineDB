angular.module("umbraco")
    .controller("ExamineDB.Backoffice.ExamineDBDashboardController",
    function ($scope, $http, notificationsService) {
        $scope.nodeId = 123;

        $scope.reindexAll = function () {
            {
                $http.get("backoffice/ExamineDB/ExamineDBTesterApi/ReIndexAll").then(function (res) {
                    notificationsService.success('Done', 'All items are re-indexed');
                    notificationsService.showNotification();
                });
            };
        };

        $scope.reindex = function () {
            {
                $http.get("backoffice/ExamineDB/ExamineDBTesterApi/ReIndex?nodeId="+ $scope.nodeId).then(function (res) {
                    notificationsService.success('Done', 'NodeId '+$scope.nodeId + ' is re-indexed');
                    notificationsService.showNotification();
                });
            };
        };

        $scope.delete = function () {
            {
                $http.get("backoffice/ExamineDB/ExamineDBTesterApi/Delete?nodeId=" + $scope.nodeId).then(function (res) {
                    notificationsService.success('Done', 'NodeId ' + $scope.nodeId + ' is deleted');
                    notificationsService.showNotification();
                });
            };
        };
    });

