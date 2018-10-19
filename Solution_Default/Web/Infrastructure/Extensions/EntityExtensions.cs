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
        public static void UpdatePost(this Post post, Post postVM)
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

        //Set value mmapp product
        public static void UpdateProduct(this Product product, ProductViewModel productVM)
        {
            product.ID = productVM.ID;
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
            product.CreatedDate = productVM.CreatedDate;
            product.CreatedBy = productVM.CreatedBy;
            product.UpdatedDate = productVM.UpdatedDate;
            product.UpdatedBy = productVM.UpdatedBy;
            product.MetaKeyword = productVM.MetaKeyword;
            product.MetaDescription = productVM.MetaDescription;
            product.Status = productVM.Status;
        }

        //Set value mmapp product
        public static void UpdateList(this List list, ListViewModel listVM)
        {
            list.ID = listVM.ID;
            list.Name = listVM.Name;
            list.Alias = listVM.Alias;
            list.Description = listVM.Description;
            list.CreatedDate = listVM.CreatedDate;
            list.CreatedBy = listVM.CreatedBy;
            list.UpdatedDate = listVM.UpdatedDate;
            list.UpdatedBy = listVM.UpdatedBy;
            list.Type = listVM.Type;
        }

        public static void UpdateProductDetail(this ProductDetail productDetail, ProductDetailViewModel productDetailVM, int type)
        {
            if (type == 2) {
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
    }
}