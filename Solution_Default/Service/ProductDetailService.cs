using Data.Infrastructure;
using Data.Repositories;
using Model.Model;
using System.Collections.Generic;
using System.Data;

namespace Service
{
    public interface IProductDetailService
    {
        ProductDetail Add(ProductDetail ProductDetail);

        IEnumerable<ProductCategory> GetCategories();

        IEnumerable<ProductCategory> GetProductCategories(int idCategory);

        int DeleteProductDetail(int id);

        ProductDetail GetById(int id);

        DataTable Get_ListProductBySizeColor(int id, int type);

        IEnumerable<ProductDetail> ListProductDetails(int id);

        void Save();
    }

    public class ProductDetailService : IProductDetailService
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

        public ProductDetail GetById(int id)
        {
            return _productDetailRepository.GetSingleById(id);
        }

        public int DeleteProductDetail(int id)
        {
            return _productDetailRepository.DeleteProductDetail(id);
        }

        public DataTable Get_ListProductBySizeColor(int id, int type)
        {
            return _productDetailRepository.Get_ListProductBySizeColor(id, type);
        }

        public IEnumerable<ProductDetail> ListProductDetails(int id)
        {
            return _productDetailRepository.ListProductDetails(id);
        }
    }
}