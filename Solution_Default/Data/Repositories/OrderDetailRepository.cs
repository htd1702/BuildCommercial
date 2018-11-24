using Data.Infrastructure;
using Microsoft.ApplicationBlocks.Data;
using Model.Model;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Data.Repositories
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        int AddOrder(OrderDetail orderDetail);
    }

    public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        private string connectString = ConfigurationManager.ConnectionStrings["BuildingConnection"].ConnectionString;

        public int AddOrder(OrderDetail orderDetail)
        {
            //return _orderDetailRepository.Add(OrderDetail);
            int result = 0;
            SqlParameter[] pram = new SqlParameter[10];
            pram[0] = new SqlParameter("@ProductID", SqlDbType.Int, 4);
            pram[0].Value = orderDetail.ProductID;
            pram[1] = new SqlParameter("@OrderID", SqlDbType.Int, 4);
            pram[1].Value = orderDetail.OrderID;
            pram[2] = new SqlParameter("@Quantity", SqlDbType.Int, 4);
            pram[2].Value = orderDetail.Quantitty;
            pram[3] = new SqlParameter("@UnitPrice", SqlDbType.Float, 4);
            pram[3].Value = orderDetail.UnitPrice;
            pram[4] = new SqlParameter("@Result", SqlDbType.Int, 4);
            pram[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(connectString, CommandType.StoredProcedure, "dbo.CreateOrderDetail", pram);
            return result = int.Parse(pram[4].Value.ToString());
        }
    }
}