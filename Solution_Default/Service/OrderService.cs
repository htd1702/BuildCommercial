using Data.Infrastructure;
using Data.Repositories;
using Model.Model;
using System.Collections.Generic;
using System.Data;

namespace Service
{
    public interface IOrderService
    {
        Order Add(Order Order);

        void Update(Order Order);

        Order Delete(int id);

        IEnumerable<Order> GetAll();

        IEnumerable<Order> GetAll(string keyword);

        Order GetById(int id);

        void Save();

        DataTable ListOrderDetail(string id);

        List<string> ListNameOrder(string keyword);
    }

    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            this._orderRepository = orderRepository;
            this._unitOfWork = unitOfWork;
        }

        public Order Add(Order Order)
        {
            return _orderRepository.Add(Order);
        }

        public Order Delete(int id)
        {
            return _orderRepository.Delete(id);
        }

        public IEnumerable<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public Order GetById(int id)
        {
            return _orderRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Order Order)
        {
            _orderRepository.Update(Order);
        }

        public DataTable ListOrderDetail(string id)
        {
            return _orderRepository.ListOrderDetail(id);
        }

        public List<string> ListNameOrder(string keyword)
        {
            return _orderRepository.ListNameOrder(keyword);
        }

        public IEnumerable<Order> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _orderRepository.GetMulti(p => p.CustomerName.Contains(keyword) || p.Email.Contains(keyword) || p.Phone.Contains(keyword));
            else
                return _orderRepository.GetAll();
        }
    }
}