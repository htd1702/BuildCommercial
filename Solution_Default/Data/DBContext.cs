using Microsoft.AspNet.Identity.EntityFramework;
using Model.Model;
using System.Collections.Generic;
using System.Data.Entity;

namespace Data
{
    public class DBContext : IdentityDbContext<ApplicationUser>
    {
        //Get connection string fix connect data
        public DBContext() : base("BuildingConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        //chạy khi khởi tạo entities
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId });
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId);
        }

        //Tạo mới identitydbcontext
        public static DBContext Create()
        {
            return new DBContext();
        }

        //Khai báo tất cả các table trong cấu hình confic
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> PostCategorys { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<ProductCategory> ProductCategorys { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<SupportOnline> SupportOnlines { get; set; }
        public DbSet<SystemConfig> SystemConfigs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<VisitorStatistic> VisitorStatistics { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<ContactDetail> ContactDetails { set; get; }
    }
}