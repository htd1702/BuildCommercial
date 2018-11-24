using Data.Infrastructure;
using Microsoft.ApplicationBlocks.Data;
using Model.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace Data.Repositories
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        IEnumerable<ProductCategory> GetByAlias(string alias);

        IEnumerable<ProductCategory> GetCategoryByTake(int take);

        IEnumerable<ProductCategory> GetCategoryByType(int type);

        List<string> ListNameCategory(string keyword);

        DataTable GetCategoryByParent();
    }

    public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(IDbFactory dbFactory) : base(dbFactory)//Name base da nhận 1 đối tượng truyền vào
        {
        }

        private string connectString = ConfigurationManager.ConnectionStrings["BuildingConnection"].ConnectionString;

        public IEnumerable<ProductCategory> GetByAlias(string alias)
        {
            return this.DbContext.ProductCategorys.Where(x => x.Alias == alias);
        }

        public IEnumerable<ProductCategory> GetCategoryByTake(int take)
        {
            return this.DbContext.ProductCategorys.OrderBy(x => x.DisplayOrder).Take(take).ToList();
        }

        public IEnumerable<ProductCategory> GetCategoryByType(int type)
        {
            if (type == 1)
                return DbContext.ProductCategorys.Where(c => c.ParentID == 0).ToList();
            else if (type == 2)
                return DbContext.ProductCategorys.Where(c => c.ParentID != 0).ToList();
            else
                return DbContext.ProductCategorys.OrderByDescending(c => c.Name).ToList();
        }

        public List<string> ListNameCategory(string keyword)
        {
            return this.DbContext.ProductCategorys.Where(p => p.Name.Contains(keyword) || p.Alias.Contains(keyword)).Select(x => x.Name).Take(8).ToList();
        }

        public DataTable GetCategoryByParent()
        {
            return SqlHelper.ExecuteDataset(connectString, CommandType.StoredProcedure, "dbo.GetCategoryByParent").Tables[0];
        }
    }
}