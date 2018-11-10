(function (app) {
    app.controller("postEditController", postEditController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state: trỏ đến page
    //$stateParams: set param
    postEditController.$inject = ["$scope", "apiService", "notificationService", "$state", "$stateParams", "commonService", "authData"];

    function postEditController($scope, apiService, notificationService, $state, $stateParams, commonService, authData) {
        //set value model
        $scope.post = {
            CreatedDate: new Date(),
            Status: true
        };
        //setting ckeditor
        $scope.editorOptions = {
            lang: 'en',
            height: '120px'
        };
        //create funtion
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.EditPost = EditPost;
        $scope.ChooseImage = ChooseImage;
        //binding title seo bye name
        function GetSeoTitle() {
            $scope.post.Alias = commonService.getSEOTitle($scope.post.Name);
        }
        //method load parentId
        function LoadParentCategory() {
            apiService.get("/api/postcategory/getallparents", null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                notificationService.displayError("Load thất bại!");
            });
        }
        //load detail productcategory
        function loadPostDetail() {
            apiService.get("/api/post/getid/" + $stateParams.id, null, function (result) {
                $scope.post = result.data;
            }, function (error) {
                notificationService.displayError("Lấy id thất bại!");
            });
        }
        //Edit
        function EditPost() {
            $scope.post.CreatedBy = authData.authenticationData.userName;
            $scope.post.UpdatedBy = $scope.post.CreatedBy;
            $scope.post.UpdatedDate = $scope.post.CreatedDate;
            apiService.put("/api/post/update", $scope.post, function (result) {
                notificationService.displaySuccess(result.data.Name + " cập nhật thành công!");
                $state.go("posts");
            }, function (error) {
                notificationService.displayError("Cập nhật mới thất bại!");
            });
        }
        //funcion upload
        function ChooseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (filtUrl) {
                $scope.$apply(function () {
                    $scope.post.Image = filtUrl;
                });
            };
            finder.popup();
        }
        //call method load parent
        loadPostDetail();
        LoadParentCategory();
    }
})(angular.module('default.posts'));