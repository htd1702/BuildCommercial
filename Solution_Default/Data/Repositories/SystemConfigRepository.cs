using Data.Infrastructure;
using Model.Model;

namespace Data.Repositories
{
    public interface ISystemConfigRepository : IRepository<SystemConfig>
    { }

    public class SystemConfigRepository : RepositoryBase<SystemConfig>, ISystemConfigRepository
    {
        public SystemConfigRepository(IDbFactory dbFactory) : base(dbFactory)
        { }
    }
}