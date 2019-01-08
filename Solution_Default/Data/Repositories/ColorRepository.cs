using Data.Infrastructure;
using Model.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Data.Repositories
{
    public interface IColorRepository : IRepository<Color>
    {
        List<string> ListNameColor(string keyword);

        int CheckType(int id);
    }

    public class ColorRepository : RepositoryBase<Color>, IColorRepository
    {
        public ColorRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        private string connectString = ConfigurationManager.ConnectionStrings["BuildingConnection"].ConnectionString;

        public List<string> ListNameColor(string keyword)
        {
            return this.DbContext.Colors.Where(p => p.Name.Contains(keyword) || p.NameVN.Contains(keyword) || p.Alias.Contains(keyword)).Select(x => x.Name).Take(8).ToList();
        }

        public int CheckType(int id)
        {
            var type = this.DbContext.Colors.FirstOrDefault(p => p.ID == id).Type;
            if (type == 1)
                return 1;
            else
                return 0;
        }
    }
}