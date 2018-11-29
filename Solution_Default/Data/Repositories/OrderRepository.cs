using Data.Infrastructure;
using Microsoft.ApplicationBlocks.Data;
using Model.Model;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        DataTable ListOrderDetail(string id);
    }

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        private string connectString = ConfigurationManager.ConnectionStrings["BuildingConnection"].ConnectionString;

        public DataTable ListOrderDetail(string id)
        {
            SqlParameter[] pram = new SqlParameter[5];
            pram[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            pram[0].Value = id;
            return SqlHelper.ExecuteDataset(connectString, CommandType.StoredProcedure, "dbo.GetListOrderDetail", pram).Tables[0];
        }
    }
}