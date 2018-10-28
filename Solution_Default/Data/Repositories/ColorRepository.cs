using Data.Infrastructure;
using Model.Model;
using System.Configuration;

namespace Data.Repositories
{
    public interface IColorRepository : IRepository<Color>
    {
        string connectString { get; }
    }

    public class ColorRepository : RepositoryBase<Color>, IColorRepository
    {
        public ColorRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        string IColorRepository.connectString { get => ConfigurationManager.ConnectionStrings["BuildingConnection"].ConnectionString; }
    }
}