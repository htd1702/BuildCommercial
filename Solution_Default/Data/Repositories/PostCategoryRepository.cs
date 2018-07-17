using Data.Infrastructure;
using Model.Model;

namespace Data.Repositories
{
    public interface IPostCategoryRepository : IRepository<PostCategory>
    { }

    public class PostCategoryRepository : RepositoryBase<PostCategory>, IPostCategoryRepository
    {
        public PostCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        { }
    }
}