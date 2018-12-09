using Data.Infrastructure;
using Microsoft.ApplicationBlocks.Data;
using Model.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        DataTable ListOrderDetail(string id);

        List<string> ListNameOrder(string keyword);
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

        public List<string> ListNameOrder(string keyword)
        {
            return this.DbContext.Orders.Where(p => p.CustomerName.Contains(keyword) || p.Email.Contains(keyword) || p.Phone.Contains(keyword)).Select(x => x.CustomerName).Take(8).ToList();
        }
    }
}