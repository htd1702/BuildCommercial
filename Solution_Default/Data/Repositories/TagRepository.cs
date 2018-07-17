using Data.Infrastructure;
using Model.Model;

namespace Data.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    { }

    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {
        public TagRepository(IDbFactory dbFactory) : base(dbFactory)
        { }
    }
}