using Data.Infrastructure;
using Model.Model;

namespace Data.Repositories
{
    public interface ISupportOnlineRepository : IRepository<SupportOnline>
    { }

    public class SupportOnlineRepository : RepositoryBase<SupportOnline>, ISupportOnlineRepository
    {
        public SupportOnlineRepository(IDbFactory dbFactory) : base(dbFactory)
        { }
    }
}