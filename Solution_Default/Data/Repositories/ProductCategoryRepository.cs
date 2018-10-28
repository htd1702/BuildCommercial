﻿using Data.Infrastructure;
using Model.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Data.Repositories
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        IEnumerable<ProductCategory> GetByAlias(string alias);

        IEnumerable<ProductCategory> GetCategoryByTake(int take);

        IEnumerable<ProductCategory> getCategoryByType(int type);

        string connectString { get; }
    }

    public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(IDbFactory dbFactory) : base(dbFactory)//Name base da nhận 1 đối tượng truyền vào
        {
        }

        //Single
        public IEnumerable<ProductCategory> GetByAlias(string alias)
        {
            return this.DbContext.ProductCategorys.Where(x => x.Alias == alias);
        }

        public IEnumerable<ProductCategory> GetCategoryByTake(int take)
        {
            return this.DbContext.ProductCategorys.OrderBy(x => x.DisplayOrder).Take(take).ToList();
        }

        public IEnumerable<ProductCategory> getCategoryByType(int type)
        {
            if (type == 1)
                return DbContext.ProductCategorys.Where(c => c.ParentID == 0).ToList();
            else
                return DbContext.ProductCategorys.Where(c => c.ParentID != 0).ToList();
        }

        string IProductCategoryRepository.connectString { get => ConfigurationManager.ConnectionStrings["BuildingConnection"].ConnectionString; }
    }
}