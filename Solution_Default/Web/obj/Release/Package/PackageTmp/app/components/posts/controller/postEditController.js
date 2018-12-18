(function (app) {
    app.controller("postEditController", postEditController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state: trỏ đến page
    //$stateParams: set param
    postEditController.$inject = ["$scope", "apiService", "notificationService", "$state", "$stateParams", "commonService", "authData"];

    function postEditController($scope, apiService, notificationService, $state, $stateParams, commonService, authData) {
        $scope.moreImages = [];
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
        if ($scope.moreImages == "") {
            $("input[name=imageMore]").show();
        }
        //create funtion
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.EditPost = EditPost;
        $scope.ChooseImage = ChooseImage;
        $scope.ChooseImageMore = ChooseImageMore;
        $scope.RemoveImgMore = RemoveImgMore;
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
                $scope.moreImages = JSON.parse($scope.post.MoreImages);
            }, function (error) {
                notificationService.displayError("Lấy id thất bại!");
            });
        }
        //Edit
        function EditPost() {
            $scope.post.CreatedBy = authData.authenticationData.userName;
            $scope.post.UpdatedBy = $scope.post.CreatedBy;
            $scope.post.UpdatedDate = $scope.post.CreatedDate;
            $scope.post.MoreImages = JSON.stringify($scope.moreImages);
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
        //function upload multi img
        function ChooseImageMore() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (filtUrl) {
                $scope.$apply(function () {
                    $scope.post.ImageMore = filtUrl;
                    $scope.moreImages.push(filtUrl);
                    $.unique($scope.moreImages.sort()).sort();
                });
            }
            finder.popup();
        }
        //function remove img
        function RemoveImgMore(img, index) {
            var image = img;
            var i = index;
            $scope.moreImages = jQuery.grep($scope.moreImages, function (value) {
                return value != image;
            });
        }
        //call method load parent
        loadPostDetail();
        LoadParentCategory();
    }
})(angular.module('default.posts'));