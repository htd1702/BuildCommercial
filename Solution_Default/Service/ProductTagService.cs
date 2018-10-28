using Data.Infrastructure;
using Data.Repositories;
using Model.Model;
using System;
using System.Collections.Generic;

namespace Service
{
    public interface IProductTagService
    {
        ProductTag Add(ProductTag ProductTag);

        void Update(ProductTag ProductTag);

        ProductTag Delete(int id);

        IEnumerable<ProductTag> GetAll();

        IEnumerable<ProductTag> GetAllByParentId(int parentId);

        ProductTag GetById(int id);

        void Save();
    }

    public class ProductTagService : IProductTagService
    {
        private IProductTagRepository _productTagRepository;
        private IUnitOfWork _unitOfWork;

        public ProductTagService(IProductTagRepository productRepository, IUnitOfWork unitOfWork)
        {
            this._productTagRepository = productRepository;
            this._unitOfWork = unitOfWork;
        }

        public ProductTag Add(ProductTag ProductTag)
        {
            return _productTagRepository.Add(ProductTag);
        }

        public ProductTag Delete(int id)
        {
            return _productTagRepository.Delete(id);
        }

        public IEnumerable<ProductTag> GetAll()
        {
            return _productTagRepository.GetAll();
        }

        public IEnumerable<ProductTag> GetAllByParentId(int parentId)
        {
            throw new NotImplementedException();
        }

        public ProductTag GetById(int id)
        {
            return _productTagRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductTag ProductTag)
        {
            _productTagRepository.Update(ProductTag);
        }
    }
}