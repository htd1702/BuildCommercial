using Data.Infrastructure;
using Model.Model;
using System.Configuration;

namespace Data.Repositories
{
    public interface IContactDetailRepository : IRepository<ContactDetail>
    {
        string connectString { get; }
    }

    public class ContactDetailRepository : RepositoryBase<ContactDetail>, IContactDetailRepository
    {
        public ContactDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        string IContactDetailRepository.connectString { get => ConfigurationManager.ConnectionStrings["BuildingConnection"].ConnectionString; }
    }
}