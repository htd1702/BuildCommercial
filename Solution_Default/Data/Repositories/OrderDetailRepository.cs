using Data.Infrastructure;
using Model.Model;
using System.Configuration;

namespace Data.Repositories
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        string connectString { get; }
    }

    public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        string IOrderDetailRepository.connectString { get => ConfigurationManager.ConnectionStrings["BuildingConnection"].ConnectionString; }
    }
}