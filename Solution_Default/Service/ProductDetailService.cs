using Data.Infrastructure;
using Data.Repositories;
using Model.Model;
using System.Collections.Generic;

namespace Service
{
    public interface IProductDetailService
    {
        ProductDetail Add(ProductDetail ProductDetail);

        void Update(ProductDetail ProductDetail);

        ProductDetail Delete(int id);

        IEnumerable<ProductDetail> GetAll();

        IEnumerable<ProductCategory> GetCategories();

        IEnumerable<ProductCategory> GetProductCategories(int idCategory);

        ProductDetail GetById(int id);

        void Save();
    }

    internal class ProductDetailService : IProductDetailService
    {
        private IProductDetailRepository _productDetailRepository;
        private IUnitOfWork _unitOfWork;

        public ProductDetailService(IProductDetailRepository productDetailRepository, IUnitOfWork unitOfWork)
        {
            this._productDetailRepository = productDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        public ProductDetail Add(ProductDetail ProductDetail)
        {
            return _productDetailRepository.Add(ProductDetail);
        }

        public ProductDetail Delete(int id)
        {
            return _productDetailRepository.Delete(id);
        }

        public IEnumerable<ProductDetail> GetAll()
        {
            return _productDetailRepository.GetAll();
        }

        public ProductDetail GetById(int id)
        {
            return _productDetailRepository.GetSingleById(id);
        }

        public IEnumerable<ProductCategory> GetCategories()
        {
            return _productDetailRepository.getCategories();
        }

        public IEnumerable<ProductCategory> GetProductCategories(int idCategory)
        {
            return _productDetailRepository.getProductCategories(idCategory);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductDetail ProductDetail)
        {
            _productDetailRepository.Update(ProductDetail);
        }
    }
}