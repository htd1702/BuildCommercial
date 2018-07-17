using Data.Infrastructure;
using Model.Model;

namespace Data.Repositories
{
    public interface IMenuGroupRepository : IRepository<MenuGroup>
    { }

    public class MenuGroupRepository : RepositoryBase<MenuGroup>, IMenuGroupRepository
    {
        public MenuGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        { }
    }
}