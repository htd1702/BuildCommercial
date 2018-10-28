namespace Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        //Create data demo
        protected override void Seed(Data.DBContext context)
        {
            //  This method will be called after migrating to the latest version.
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DBContext()));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new DBContext()));

            var user = new ApplicationUser()
            {
                UserName = "hieunt",
                Email = "hieu.n2395@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "Ngô Trung Hiếu",
                PhoneNumber = "0938570330",
                Address = "59/15",
                LockoutEnabled = true,
            };
            //create new user
            manager.Create(user, "123456");

            //check role manager
            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }
            //find email
            var adminUser = manager.FindByEmail("hieu.n2395@gmail.com");

            manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }

        //create data demo productcategory
        private void CreateProductCategorySample(Data.DBContext context)
        {
            if (context.ProductCategorys.Count() == 0)
            {
                List<ProductCategory> productCategories = new List<ProductCategory>()
                {
                    new ProductCategory(){ Name = "Điện thoại", Status = true, Alias="dien-thoai",},
                    new ProductCategory(){ Name = "Đồng hồ", Status = true, Alias="dong-ho",},
                    new ProductCategory(){ Name = "Mỹ phẩm", Status = true, Alias="my-pham",},
                    new ProductCategory(){ Name = "Điện lạnh", Status = true, Alias="dien-lanh",},
                };
                context.ProductCategorys.AddRange(productCategories);
                context.SaveChanges();
            }
        }

        //create data demo product

        private void CreateProductSample(Data.DBContext context)
        {
            if (context.Products.Count() == 0)
            {
                List<Product> products = new List<Product>()
                {
                    new Product(){ Name = "iPhone", Status = true, Alias = "i-phone", CategoryID = 1, Price = 10000, },
                    new Product(){ Name = "SamSung", Status = true, Alias = "sam-sung", CategoryID = 2, Price = 20000, },
                    new Product(){ Name = "Oppo", Status = true, Alias = "o-ppo", CategoryID = 3, Price = 30000, },
                    new Product(){ Name = "Asus", Status = true, Alias = "a-sus", CategoryID = 4, Price = 40000, },
                };
                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }

    }
}
