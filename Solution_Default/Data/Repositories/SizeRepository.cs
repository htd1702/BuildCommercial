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

        int CheckType(int id);
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

        public int CheckType(int id)
        {
            var type = this.DbContext.Sizes.FirstOrDefault(p => p.ID == id).Type;
            if (type == 1)
                return 1;
            else
                return 0;
        }
    }
}