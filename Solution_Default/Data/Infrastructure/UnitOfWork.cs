namespace Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private DBContext dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public DBContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        //Khi thực thi xong sẽ lưu vào data
        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}