using Data.Infrastructure;
using Data.Repositories;
using Microsoft.ApplicationBlocks.Data;
using Model.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Service
{
    public interface IOrderDetailService
    {
        int Add(OrderDetail orderDetail);

        void Update(OrderDetail orderDetail);

        OrderDetail Delete(int id);

        IEnumerable<OrderDetail> GetAll();

        OrderDetail GetById(int id);

        void Save();
    }

    public class OrderDetailService : IOrderDetailService
    {
        private IOrderDetailRepository _orderDetailRepository;
        private IUnitOfWork _unitOfWork;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IUnitOfWork unitOfWork)
        {
            this._orderDetailRepository = orderDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        public int Add(OrderDetail orderDetail)
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
            SqlHelper.ExecuteNonQuery(_orderDetailRepository.connectString, CommandType.StoredProcedure, "dbo.CreateOrderDetail", pram);
            return result = int.Parse(pram[4].Value.ToString());
        }

        public OrderDetail Delete(int id)
        {
            return _orderDetailRepository.Delete(id);
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return _orderDetailRepository.GetAll();
        }

        public OrderDetail GetById(int id)
        {
            return _orderDetailRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(OrderDetail orderDetail)
        {
            _orderDetailRepository.Update(orderDetail);
        }
    }
}