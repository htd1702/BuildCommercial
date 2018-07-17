using Data.Infrastructure;
using Model.Model;

namespace Data.Repositories
{
    public interface IMenuRepository : IRepository<Menu>
    { }

    public class MenuRepository : RepositoryBase<Menu>, IMenuRepository
    {
        public MenuRepository(IDbFactory dbFactory) : base(dbFactory)
        { }
    }
}