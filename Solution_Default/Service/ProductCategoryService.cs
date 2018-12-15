using Data.Infrastructure;
using Data.Repositories;
using Model.Model;
using System.Collections.Generic;
using System.Data;

namespace Service
{
    public interface IProductCategoryService
    {
        ProductCategory Add(ProductCategory ProductCategory);

        void Update(ProductCategory ProductCategory);

        ProductCategory Delete(int id);

        IEnumerable<ProductCategory> GetAll();

        IEnumerable<ProductCategory> GetAll(string keyword);

        IEnumerable<ProductCategory> GetAllByParentId(int parentId);

        ProductCategory GetById(int id);

        IEnumerable<ProductCategory> GetCategoryByTake(int take);

        IEnumerable<ProductCategory> GetCategoriyByType(int type);

        List<string> ListNameCategory(string keyword);

        DataTable GetCategoryByParent();

        IEnumerable<ProductCategory> GetCategoryShowHome(int take);

        int CheckExistsProductCategory(int id, int type);

        void Save();
    }

    public class ProductCategoryService : IProductCategoryService
    {
        private IProductCategoryRepository _productCategoryRepository;
        private IUnitOfWork _unitOfWork;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._productCategoryRepository = productCategoryRepository;
            this._unitOfWork = unitOfWork;
        }

        public ProductCategory Add(ProductCategory ProductCategory)
        {
            return _productCategoryRepository.Add(ProductCategory);
        }

        public int CheckExistsProductCategory(int id, int type)
        {
            return _productCategoryRepository.CheckExistsProductCategory(id, type);
        }

        public ProductCategory Delete(int id)
        {
            return _productCategoryRepository.Delete(id);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return _productCategoryRepository.GetAll();
        }

        public IEnumerable<ProductCategory> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _productCategoryRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _productCategoryRepository.GetAll();
        }

        public IEnumerable<ProductCategory> GetAllByParentId(int parentId)
        {
            return _productCategoryRepository.GetMulti(x => x.Status && x.ParentID == parentId);
        }

        public ProductCategory GetById(int id)
        {
            return _productCategoryRepository.GetSingleById(id);
        }

        public IEnumerable<ProductCategory> GetCategoriyByType(int type)
        {
            return _productCategoryRepository.GetCategoryByType(type);
        }

        public DataTable GetCategoryByParent()
        {
            return _productCategoryRepository.GetCategoryByParent();
        }

        public IEnumerable<ProductCategory> GetCategoryByTake(int take)
        {
            return _productCategoryRepository.GetCategoryByTake(take);
        }

        public IEnumerable<ProductCategory> GetCategoryShowHome(int take)
        {
            return _productCategoryRepository.GetCategoryShowHome(take);
        }

        public List<string> ListNameCategory(string keyword)
        {
            return _productCategoryRepository.ListNameCategory(keyword);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductCategory ProductCategory)
        {
            _productCategoryRepository.Update(ProductCategory);
        }
    }
}