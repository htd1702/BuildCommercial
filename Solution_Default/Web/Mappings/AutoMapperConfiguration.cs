using AutoMapper;
using Model.Model;
using Web.Models;

namespace Web.Mappings
{
    public class AutoMapperConfiguration
    {
        //
        public static void Configure()
        {
            Mapper.CreateMap<Post, PostViewModel>();
            Mapper.CreateMap<PostCategory, PostCategoryViewModel>();
            Mapper.CreateMap<Tag, TagViewModel>();
            Mapper.CreateMap<ProductCategory, ProductCategoryViewModel>();
            Mapper.CreateMap<Product, ProductViewModel>();
            Mapper.CreateMap<ProductTag, ProductTagViewModel>();
            Mapper.CreateMap<Color, ColorViewModel>();
            Mapper.CreateMap<Size, SizeViewModel>();
            Mapper.CreateMap<Order, OrderViewModel>();
            Mapper.CreateMap<OrderDetail, OrderDetailViewModel>();
            Mapper.CreateMap<ProductDetail, ProductDetailViewModel>();
            Mapper.CreateMap<ContactDetail, ContactDetailViewModel>();
            Mapper.CreateMap<Banner, BannerViewModel>();
        }
    }
}