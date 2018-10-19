using Data.Infrastructure;
using Model.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        string connectString { get; }

        IEnumerable<Product> ListProductByCategory(int id);
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        string IProductRepository.connectString { get => ConfigurationManager.ConnectionStrings["BuildingConnection"].ConnectionString; }

        public IEnumerable<Product> ListProductByCategory(int id)
        {
            return this.DbContext.Products.Where(p => p.CategoryID == id).ToList();
        }
    }
}