using Data.Infrastructure;
using Model.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Data.Repositories
{
    public interface IListRepository : IRepository<List>
    {
        IEnumerable<List> GetAllListByType(int type);

        string connectString { get; }
    }

    public class ListRepository : RepositoryBase<List>, IListRepository
    {
        public ListRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        string IListRepository.connectString { get => ConfigurationManager.ConnectionStrings["BuildingConnection"].ConnectionString; }

        public IEnumerable<List> GetAllListByType(int type)
        {
            return DbContext.List.Where(x => x.Type == type).ToList();
        }
    }
}