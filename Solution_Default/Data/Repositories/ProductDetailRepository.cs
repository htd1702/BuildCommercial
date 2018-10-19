using Data.Infrastructure;
using Model.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Data.Repositories
{
    public interface IProductDetailRepository : IRepository<ProductDetail>
    {
        IEnumerable<ProductCategory> getCategories();

        IEnumerable<ProductCategory> getProductCategories(int idCategory);

        string connectString { get; }
    }

    public class ProductDetailRepository : RepositoryBase<ProductDetail>, IProductDetailRepository
    {
        public ProductDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        string IProductDetailRepository.connectString { get => ConfigurationManager.ConnectionStrings["BuildingConnection"].ConnectionString; }

        public IEnumerable<ProductCategory> getCategories()
        {
            return DbContext.ProductCategorys.Where(c => c.ParentID == 0).ToList();
        }

        public IEnumerable<ProductCategory> getProductCategories(int idCategory)
        {
            return DbContext.ProductCategorys.Where(c => c.ParentID == idCategory).ToList();
        }
    }
}