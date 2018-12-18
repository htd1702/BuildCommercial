(function (app) {
    app.controller("postAddController", postAddController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state : trỏ đến page
    postAddController.$inject = ["$scope", "apiService", "notificationService", "$state", "commonService", "authData"];

    function postAddController($scope, apiService, notificationService, $state, commonService, authData) {
        //load option parentID
        $scope.parentCategories = [];
        $scope.moreImages = [];
        //set value model
        $scope.post = {
            CreatedDate: new Date(),
            Status: true
        };
        if ($scope.moreImages == "") {
            $("input[name=imageMore]").show();
        }
        //setting ckeditor
        $scope.editorOptions = {
            lang: 'en',
            height: '120px'
        };
        //create function
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.AddPost = AddPost;
        $scope.ChooseImage = ChooseImage;
        $scope.ChooseImageMore = ChooseImageMore;
        $scope.RemoveImgMore = RemoveImgMore;
        //binding title seo by name
        function GetSeoTitle() {
            $scope.post.Alias = commonService.getSEOTitle($scope.post.Name);
        }
        //method load parentId
        function LoadParentCategory() {
            apiService.get("/api/postcategory/getallparents", null, function (result) {
                $scope.parentCategories = result.data;
                $scope.post.CategoryID = $scope.parentCategories[0].ID;
            }, function () {
                notificationService.displayError("Load thất bại!");
            });
        }
        //function add
        function AddPost() {
            if ($scope.post.ParentID == undefined)
                $scope.post.ParentID = 0;
            $scope.post.CreatedBy = authData.authenticationData.userName;
            $scope.post.UpdatedBy = $scope.post.CreatedBy;
            $scope.post.UpdatedDate = $scope.post.CreatedDate;
            $scope.post.MoreImages = JSON.stringify($scope.moreImages);
            apiService.post("/api/post/create", $scope.post, function (result) {
                notificationService.displaySuccess(result.data.Name + " thêm thành công!");
                $state.go("posts");
            }, function (error) {
                notificationService.displayError("Thêm mới thất bại!");
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
            $("input[name=imageMore]").hide();
            var finder = new CKFinder();
            finder.selectActionFunction = function (filtUrl) {
                $scope.$apply(function () {
                    $scope.post.ImageMore = filtUrl;
                    $scope.moreImages.push(filtUrl);
                    $.unique($scope.moreImages.sort()).sort();
                });
            };
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
        LoadParentCategory();
    }
})(angular.module('default.posts'));