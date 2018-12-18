/// <reference path="../scripts/plugins/angular/angular.min.js" />

//config routing default
(function () {
    angular.module('default',
        [
            'default.products',
            'default.product_categories',
            'default.posts',
            'default.post_categories',
            'default.orders',
            'default.colors',
            'default.sizes',
            'default.banners',
            'default.common'
        ])
        .config(config)
        .config(configAuthentication)
        .config(translations);
    //inject config
    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    //function module config
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state("base", {
                url: "",
                templateUrl: "/app/components/home/views/Base.html",
                abstract: true
            })
            .state("login", {
                url: "/login",
                templateUrl: "/app/components/login/views/Login.html",
                controller: "loginController",
                authenticate: false
            })
            .state("home", {
                url: "/admin",
                parent: "base",
                templateUrl: "/app/components/home/views/homeView.html",
                controller: "homeController"
            });
        $urlRouterProvider.otherwise('/login');
    }
    //inject configAuthentication
    configAuthentication.$inject = ['$httpProvider'];
    //function module configAuthentication
    function configAuthentication($httpProvider) {
        $httpProvider.interceptors.push(function ($q, $location) {
            return {
                request: function (config) {
                    return config;
                },
                requestError: function (rejection) {
                    return $q.reject(rejection);
                },
                response: function (response) {
                    if (response.status == "401") {
                        $location.path('/login');
                    }
                    //the same response/modified/or a new one need to be returned.
                    return response;
                },
                responseError: function (rejection) {
                    if (rejection.status == "401") {
                        $location.path('/login');
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }
    //inject translations
    translations.$inject = ['$translateProvider'];
    //function module translations
    function translations($translateProvider) {
        $translateProvider
            .translations('en', {
                Product: 'Product',
                ProductDetail: 'Product Detail',
                Category: 'Category',
                Post: 'News Detail',
                PostCategory: 'News',
                Statistical: 'Statistical',
                Color: 'Color',
                Size: 'Size',
                Order: 'Order',
                Banner: 'Banner',
                OrderDetail: 'Order Detail',
                Name: 'Name',
                NameVN: 'Name VN',
                NameFr: 'Name Fr',
                Code: 'Code',
                Price: 'Price',
                PriceVN: 'Price VN',
                PriceFr: 'Price Fr',
                Type: 'Type',
                CreateDate: 'Create Date',
                CreateBy: 'Create By',
                Status: 'Status',
                UpdateBy: 'Update By',
                UpDateDate: 'UpDate Date',
                Handling: 'Handling',
                PromotionPrice: 'Promotion',
                Warranty: 'Warranty',
                View: 'View',
                Title: 'Title',
                TypeName: 'Type Name',
                CustomerName: 'Customer Name',
                Phone: 'Phone',
                Address: 'Address',
                OrderDate: 'OrderDate',
                Img: 'Img',
                MoreImg: 'More Img',
                Quantity: 'Quantity',
                Total: 'Total',
                Description: 'Description',
                Back: 'Back',
                Submit: 'Submit',
                Choose: 'Choose',
                Add: 'Add',
                Edit: 'Edit',
                Delete: 'Delete',
                ProductName: 'Product Name',
                CategoryName: 'Category Name',
                Scale: 'Scale',
                ColorList: 'Color List',
                SizeList: 'Size List',
                Active: 'Active',
                ShowHome: 'Show Home',
                Categories: 'Categories',
                Hot: 'Hot',
                en: 'English',
                vi: 'Tiếng Việt',
                fr: 'French'
            })
            .translations('vi', {
                Product: 'Quảng lý sản phẩm',
                ProductDetail: 'Quảng lý chi tiết sản phẩm',
                Category: 'Quảng lý danh Mục',
                Post: 'Quảng lý chi tiết tin',
                PostCategory: 'Quảng lý tin',
                Statistical: 'Thống kê',
                Color: 'Quảng lý màu sắc',
                Size: 'Quảng lý kích thước',
                Order: 'Quảng lý hóa đơn',
                Banner: 'Quảng lý quảng cáo',
                OrderDetail: 'Quảng lý chi tiết hóa đơn',
                Name: 'Tên',
                NameVN: 'Tên VN',
                NameFr: 'Tên Fr',
                Code: 'Mã',
                Price: 'Giá',
                PriceVN: 'Giá VN',
                PriceFr: 'Giá Fr',
                Type: 'Loại',
                CreateDate: 'Ngày tạo',
                CreateBy: 'Người tạo',
                Status: 'Tình trạng',
                UpdateBy: 'Người cập nhật',
                UpdateDate: 'Ngày cập nhật',
                Handling: 'Thao tác',
                PromotionPrice: 'Giá khuyến mãi',
                Warranty: 'Bảo hành',
                View: 'Lượt xem',
                Title: 'Tiêu đề',
                TypeName: 'Tên loại',
                CustomerName: 'Tên khách hàng',
                Phone: 'Số điện thoại',
                Address: 'Địa chỉ',
                OrderDate: 'Ngày đặt hàng',
                Img: 'Hình',
                MoreImg: 'Đa hình',
                Quantity: 'Số lượng',
                Total: 'Tổng tiền',
                Description: 'Mô tả',
                Back: 'Quay lại',
                Submit: 'Thêm',
                Choose: 'Chọn',
                Add: 'Thêm',
                Edit: 'Sửa',
                Delete: 'Xóa',
                ProductName: 'Sản phẩm',
                CategoryName: 'Danh mục',
                Scale: 'Tỷ lệ',
                ColorList: 'Màu',
                SizeList: 'Kích thước',
                Active: 'Kích hoạt',
                ShowHome: 'Hiển thị trang chủ',
                Hot: 'Hot',
                Categories: 'Danh mục',
                en: 'English',
                vi: 'Tiếng Việt',
                fr: 'French'
            })
            .translations('fr', {
                Product: 'Product',
                ProductDetail: 'Product Detail',
                Category: 'Category',
                Post: 'News Detail',
                PostCategory: 'News',
                Statistical: 'Statistical',
                Color: 'Color',
                Size: 'Size',
                Order: 'Order',
                Banner: 'Banner',
                OrderDetail: 'Order Detail',
                Name: 'Name',
                NameVN: 'Name VN',
                NameFr: 'Name Fr',
                Code: 'Code',
                Price: 'Price',
                PriceVN: 'Price VN',
                PriceFr: 'Price Fr',
                Type: 'Type',
                CreateDate: 'Create Date',
                CreateBy: 'Create By',
                Status: 'Status',
                UpdateBy: 'Update By',
                UpDateDate: 'UpDate Date',
                Handling: 'Handling',
                PromotionPrice: 'Promotion',
                Warranty: 'Warranty',
                View: 'View',
                Title: 'Title',
                TypeName: 'Type Name',
                CustomerName: 'Customer Name',
                Phone: 'Phone',
                Address: 'Address',
                OrderDate: 'OrderDate',
                Img: 'Img',
                MoreImg: 'More Img',
                Quantity: 'Quantity',
                Total: 'Total',
                Description: 'Description',
                Back: 'Back',
                Submit: 'Submit',
                Choose: 'Choose',
                Add: 'Add',
                Edit: 'Edit',
                Delete: 'Delete',
                ProductName: 'Product Name',
                CategoryName: 'Category Name',
                Scale: 'Scale',
                ColorList: 'Color List',
                SizeList: 'Size List',
                Active: 'Active',
                ShowHome: 'Show Home',
                Hot: 'Hot',
                Categories: 'Categories',
                en: 'English',
                vi: 'Tiếng Việt',
                fr: 'French'
            });
        $translateProvider.preferredLanguage('en');
    }
})();