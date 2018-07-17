using Data.Infrastructure;
using Model.Model;

namespace Data.Repositories
{
    public interface IVisitorStatisticRepository : IRepository<VisitorStatistic>
    { }

    public class VisitorStatisticRepository : RepositoryBase<VisitorStatistic>, IVisitorStatisticRepository
    {
        public VisitorStatisticRepository(IDbFactory dbFactory) : base(dbFactory)
        { }
    }
}