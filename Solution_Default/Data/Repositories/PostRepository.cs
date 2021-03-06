﻿using Data.Infrastructure;
using Model.Model;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        IEnumerable<Post> GetAllByTag(string tag, int pagIndex, int pageSize, out int totalRow);
    }

    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        //Lấy danh sách post by tag
        public IEnumerable<Post> GetAllByTag(string tag, int pagIndex, int pageSize, out int totalRow)
        {
            var query = from p in DbContext.Posts
                        join pt in DbContext.PostTags
                        on p.ID equals pt.PostID
                        where pt.TagID == tag && p.Status
                        orderby p.CreatedDate descending
                        select p;
            totalRow = query.Count();

            //page khi ban next
            query = query.Skip((pagIndex - 1) * pageSize).Take(pageSize);
            return query;
        }
    }
}