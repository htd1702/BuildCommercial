using Model.Model;
using Web.Models;

namespace Web.Infrastructure.Extensions
{
    //Tạo ra 1 phương thức mỡ rộng cho 1 đối tượng
    public static class EntityExtensions
    {
        //Set value mapp postCategory
        public static void UpdatePostCategory(this PostCategory postCategory, PostCategoryViewModel postCategoryVM)
        {
            postCategory.ID = postCategoryVM.ID;
            postCategory.Name = postCategoryVM.Name;
            postCategory.Alias = postCategoryVM.Alias;
            postCategory.Description = postCategoryVM.Description;
            postCategory.ParentID = postCategoryVM.ParentID;
            postCategory.DisplayOrder = postCategoryVM.DisplayOrder;
            postCategory.Image = postCategoryVM.Image;
            postCategory.HomeFlag = postCategoryVM.HomeFlag;
            postCategory.CreatedBy = postCategoryVM.CreatedBy;
            postCategory.CreatedDate = postCategoryVM.CreatedDate;
            postCategory.UpdatedBy = postCategoryVM.UpdatedBy;
            postCategory.UpdatedDate = postCategoryVM.UpdatedDate;
            postCategory.MetaKeyword = postCategoryVM.MetaKeyword;
            postCategory.MetaDescription = postCategoryVM.MetaDescription;
            postCategory.Status = postCategoryVM.Status;
        }

        //Set value mapp post
        public static void UpdatePost(this Post post, PostViewModel postVM)
        {
            post.ID = postVM.ID;
            post.Name = postVM.Name;
            post.Alias = postVM.Alias;
            post.Description = postVM.Description;
            post.CategoryID = postVM.CategoryID;
            post.Image = postVM.Image;
            post.Content = postVM.Content;
            post.HomeFlag = postVM.HomeFlag;
            post.HotFlag = postVM.HotFlag;
            post.ViewCount = postVM.ViewCount;
            post.CreatedBy = postVM.CreatedBy;
            post.CreatedDate = postVM.CreatedDate;
            post.UpdatedBy = postVM.UpdatedBy;
            post.UpdatedDate = postVM.UpdatedDate;
            post.MetaKeyword = postVM.MetaKeyword;
            post.MetaDescription = postVM.MetaDescription;
            post.Status = postVM.Status;
        }

        //Set value mapp productCategory
        public static void UpdateProductCategory(this ProductCategory productCategory, ProductCategoryViewModel productCategoryVM)
        {
            productCategory.ID = productCategoryVM.ID;
            productCategory.Name = productCategoryVM.Name;
            productCategory.Alias = productCategoryVM.Alias;
            productCategory.Description = productCategoryVM.Description;
            productCategory.ParentID = productCategoryVM.ParentID;
            productCategory.DisplayOrder = productCategoryVM.DisplayOrder;
            productCategory.Image = productCategoryVM.Image;
            productCategory.HomeFlag = productCategoryVM.HomeFlag;
            productCategory.CreatedBy = productCategoryVM.CreatedBy;
            productCategory.CreatedDate = productCategoryVM.CreatedDate;
            productCategory.UpdatedBy = productCategoryVM.UpdatedBy;
            productCategory.UpdatedDate = productCategoryVM.UpdatedDate;
            productCategory.MetaKeyword = productCategoryVM.MetaKeyword;
            productCategory.MetaDescription = productCategoryVM.MetaDescription;
            productCategory.Status = productCategoryVM.Status;
        }

        //Set value mapp product
        public static void UpdateProduct(this Product product, ProductViewModel productVM, int type)
        {
            if (type == 1)
            {
                product.CreatedDate = productVM.CreatedDate;
                product.CreatedBy = productVM.CreatedBy;
            }
            else
            {
                product.ID = productVM.ID;
            }
            product.Code = productVM.Code;
            product.Name = productVM.Name;
            product.Alias = productVM.Alias;
            product.CategoryID = productVM.CategoryID;
            product.Image = productVM.Image;
            product.MoreImages = productVM.MoreImages;
            product.Price = productVM.Price;
            product.PromotionPrice = productVM.PromotionPrice;
            product.Quantity = productVM.Quantity;
            product.Warranty = productVM.Warranty;
            product.Description = productVM.Description;
            product.Content = productVM.Content;
            product.HomeFlag = productVM.HomeFlag;
            product.HotFlag = productVM.HotFlag;
            product.ViewCount = productVM.ViewCount;
            product.Tags = productVM.Tags;
            product.UpdatedDate = productVM.UpdatedDate;
            product.UpdatedBy = productVM.UpdatedBy;
            product.MetaKeyword = productVM.MetaKeyword;
            product.MetaDescription = productVM.MetaDescription;
            product.Status = productVM.Status;
        }

        //Set value mapp product detail
        public static void UpdateProductDetail(this ProductDetail productDetail, ProductDetailViewModel productDetailVM, int type)
        {
            if (type == 2)
            {
                productDetail.ID = productDetailVM.ID;
            }
            productDetail.ProductID = productDetailVM.ProductID;
            productDetail.ColorID = productDetailVM.ColorID;
            productDetail.SizeID = productDetailVM.SizeID;
            productDetail.Quantity = productDetailVM.Quantity;
            productDetail.Type = productDetailVM.Type;
            //productDetail.Description = productDetailVM.Description;
            productDetail.CreatedDate = productDetailVM.CreatedDate;
            productDetail.CreatedBy = productDetailVM.CreatedBy;
            productDetail.UpdatedDate = productDetailVM.UpdatedDate;
            productDetail.UpdatedBy = productDetailVM.UpdatedBy;
            //productDetail.MoreImages = productDetailVM.MoreImages;
        }

        //Set value mmapp product
        public static void UpdateColor(this Color color, ColorViewModel colorVM)
        {
            color.ID = colorVM.ID;
            color.Name = colorVM.Name;
            color.NameVN = colorVM.NameVN;
            color.Alias = colorVM.Alias;
            color.Description = colorVM.Description;
            color.CreatedDate = colorVM.CreatedDate;
            color.CreatedBy = colorVM.CreatedBy;
            color.UpdatedDate = colorVM.UpdatedDate;
            color.UpdatedBy = colorVM.UpdatedBy;
        }

        //Set value mmapp product
        public static void UpdateSize(this Size size, SizeViewModel sizeVM)
        {
            size.ID = sizeVM.ID;
            size.Name = sizeVM.Name;
            size.NameVN = sizeVM.NameVN;
            size.Alias = sizeVM.Alias;
            size.Description = sizeVM.Description;
            size.CreatedDate = sizeVM.CreatedDate;
            size.CreatedBy = sizeVM.CreatedBy;
            size.UpdatedDate = sizeVM.UpdatedDate;
            size.UpdatedBy = sizeVM.UpdatedBy;
        }

        //Set value mmapp order
        public static void UpdateOrder(this Order order, OrderViewModel orderVM)
        {
            order.ID = orderVM.ID;
            order.CustomerName = orderVM.CustomerName;
            order.Address = orderVM.Address;
            order.Email = orderVM.Email;
            order.Phone = orderVM.Phone;
            order.Total = orderVM.Total;
            order.CustomerMessage = orderVM.CustomerMessage;
            order.PaymentMethod = orderVM.PaymentMethod;
            order.OrderDate = orderVM.OrderDate;
            order.PaymentStatus = orderVM.PaymentStatus;
            order.Status = orderVM.Status;
        }

        //Set value mmapp order detail
        public static void UpdateOrderDetail(this OrderDetail orderDetail, OrderDetailViewModel orderDetailVM)
        {
            orderDetail.OrderID = orderDetailVM.OrderID;
            orderDetail.ProductID = orderDetailVM.ProductID;
            orderDetail.Quantitty = orderDetailVM.Quantitty;
            orderDetail.UnitPrice = orderDetailVM.UnitPrice;
        }
    }
}