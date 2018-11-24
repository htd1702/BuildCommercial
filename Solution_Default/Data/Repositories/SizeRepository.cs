using Data.Infrastructure;
using Model.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Data.Repositories
{
    public interface ISizeRepository : IRepository<Size>
    {
        List<string> ListNameSize(string keyword);
    }

    public class SizeRepository : RepositoryBase<Size>, ISizeRepository
    {
        public SizeRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        private string connectString = ConfigurationManager.ConnectionStrings["BuildingConnection"].ConnectionString;

        public List<string> ListNameSize(string keyword)
        {
            return this.DbContext.Sizes.Where(p => p.Name.Contains(keyword) || p.Alias.Contains(keyword)).Select(x => x.Name).Take(8).ToList();
        }
    }
}