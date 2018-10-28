using Data.Infrastructure;
using Model.Model;
using System.Configuration;

namespace Data.Repositories
{
    public interface ISizeRepository : IRepository<Size>
    {
        string connectString { get; }
    }

    public class SizeRepository : RepositoryBase<Size>, ISizeRepository
    {
        public SizeRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        string ISizeRepository.connectString { get => ConfigurationManager.ConnectionStrings["BuildingConnection"].ConnectionString; }
    }
}