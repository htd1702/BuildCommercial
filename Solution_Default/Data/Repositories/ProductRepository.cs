using Data.Infrastructure;
using Model.Model;
using System.Configuration;

namespace Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        string connectString { get; }
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        string IProductRepository.connectString { get => ConfigurationManager.ConnectionStrings["BuildingConnection"].ConnectionString; }
    }
}