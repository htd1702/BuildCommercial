using Data.Infrastructure;
using Model.Model;

namespace Data.Repositories
{
    //Tất cả con của Interface đều faj~ kế thừa từ IRepository
    public interface IFooterRepository : IRepository<Footer>
    {
    }

    public class FooterRepository : RepositoryBase<Footer>, IFooterRepository
    {
        public FooterRepository(IDbFactory dbFactory) : base(dbFactory)
        { }
    }
}