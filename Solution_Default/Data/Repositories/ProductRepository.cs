using Data.Infrastructure;
using Model.Model;

namespace Data.Repositories
{
    public interface IProductRepository
    { }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}