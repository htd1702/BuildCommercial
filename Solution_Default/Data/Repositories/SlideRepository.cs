using Data.Infrastructure;
using Model.Model;

namespace Data.Repositories
{
    public interface ISlideRepository : IRepository<Slide>
    { }

    public class SlideRepository : RepositoryBase<Slide>, ISlideRepository
    {
        public SlideRepository(IDbFactory dbFactory) : base(dbFactory)
        { }
    }
}