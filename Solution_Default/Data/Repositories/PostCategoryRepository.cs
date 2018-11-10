using Data.Infrastructure;
using Model.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Data.Repositories
{
    public interface IPostCategoryRepository : IRepository<PostCategory>
    {
        IEnumerable<PostCategory> GetByAlias(string alias);

        IEnumerable<PostCategory> GetCategoryByTake(int take);

        IEnumerable<PostCategory> getCategoryByType(int type);

        string connectString { get; }
    }

    public class PostCategoryRepository : RepositoryBase<PostCategory>, IPostCategoryRepository
    {
        public PostCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        //Single
        public IEnumerable<PostCategory> GetByAlias(string alias)
        {
            return this.DbContext.PostCategorys.Where(x => x.Alias == alias);
        }

        public IEnumerable<PostCategory> GetCategoryByTake(int take)
        {
            return this.DbContext.PostCategorys.OrderBy(x => x.DisplayOrder).Take(take).ToList();
        }

        public IEnumerable<PostCategory> getCategoryByType(int type)
        {
            if (type == 1)
                return DbContext.PostCategorys.Where(c => c.ParentID == 0).ToList();
            else
                return DbContext.PostCategorys.Where(c => c.ParentID != 0).ToList();
        }

        string IPostCategoryRepository.connectString { get => ConfigurationManager.ConnectionStrings["BuildingConnection"].ConnectionString; }
    }
}