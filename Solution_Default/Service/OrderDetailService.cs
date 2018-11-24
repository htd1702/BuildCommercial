using Data.Infrastructure;
using Data.Repositories;
using Model.Model;
using System.Collections.Generic;

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
            return _orderDetailRepository.AddOrder(orderDetail);
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