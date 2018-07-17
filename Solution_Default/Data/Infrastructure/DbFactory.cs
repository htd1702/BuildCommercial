namespace Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private DBContext dbContext;

        public DBContext Init()
        {
            //Phương thức init sẽ khởi tạo 1 đối tượng cho dbcontext
            return dbContext ?? (dbContext = new DBContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}