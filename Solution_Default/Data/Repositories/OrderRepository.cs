using Data.Infrastructure;
using Model.Model;

namespace Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    { }

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        { }
    }
}