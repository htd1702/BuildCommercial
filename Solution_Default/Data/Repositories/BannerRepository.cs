using Data.Infrastructure;
using Model.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Data.Repositories
{
    public interface IBannerRepository : IRepository<Banner>
    {
        List<string> ListNameBanner(string keyword);

        IEnumerable<Banner> ListBannerByType(int type);
    }

    public class BannerRepository : RepositoryBase<Banner>, IBannerRepository
    {
        public BannerRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        private string connectString = ConfigurationManager.ConnectionStrings["BuildingConnection"].ConnectionString;

        public List<string> ListNameBanner(string keyword)
        {
            return this.DbContext.Banners.Where(p => p.Name.Contains(keyword)).Select(x => x.Name).Take(8).ToList();
        }

        public IEnumerable<Banner> ListBannerByType(int type)
        {
            if (type == 1)
            {
                return this.DbContext.Banners.Where(b => b.type == type).Take(3).OrderBy(b => b.CreatedDate).ToList();
            }
            else if (type == 2)
            {
                return this.DbContext.Banners.Where(b => b.type == type).Take(1).OrderBy(b => b.CreatedDate).ToList();
            }
            else
            {
                return this.DbContext.Banners.Where(b => b.type == type).Take(1).OrderBy(b => b.CreatedDate).ToList();
            }
        }
    }
}