using Data.Infrastructure;
using Microsoft.ApplicationBlocks.Data;
using Model.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace Data.Repositories
{
    public interface IPostCategoryRepository : IRepository<PostCategory>
    {
        IEnumerable<PostCategory> GetByAlias(string alias);

        IEnumerable<PostCategory> GetCategoryByTake(int take);

        IEnumerable<PostCategory> getCategoryByType(int type);

        DataTable GetPostCategoryByParent();

        List<string> ListNamePostCategory(string keyword);

        int CheckExistsPostCategory(int id);
    }

    public class PostCategoryRepository : RepositoryBase<PostCategory>, IPostCategoryRepository
    {
        private string connectString = ConfigurationManager.ConnectionStrings["BuildingConnection"].ConnectionString;

        public PostCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public int CheckExistsPostCategory(int id)
        {
            var post = this.DbContext.Posts.Where(p => p.CategoryID == id).ToList();
            if (post.Count > 0)
                return 1;
            else
                return 0;
        }

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

        public DataTable GetPostCategoryByParent()
        {
            return SqlHelper.ExecuteDataset(connectString, CommandType.StoredProcedure, "dbo.GetPostCategoryByParent").Tables[0];
        }

        public List<string> ListNamePostCategory(string keyword)
        {
            return this.DbContext.PostCategorys.Where(p => p.Name.Contains(keyword) || p.Alias.Contains(keyword)).Select(x => x.Name).Take(8).ToList();
        }
    }
}