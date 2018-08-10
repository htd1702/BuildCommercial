using Data.Infrastructure;
using Model.Model;

namespace Data.Repositories
{
    //Tất cả con của Interface đều faj~ kế thừa từ IRepository
    public interface IErrorRepository : IRepository<Error>
    {
    }

    public class ErrorRepository : RepositoryBase<Error>, IErrorRepository
    {
        public ErrorRepository(IDbFactory dbFactory) : base(dbFactory)
        { }
    }
}