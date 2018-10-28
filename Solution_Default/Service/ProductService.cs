using Common;
using Data.Infrastructure;
using Data.Repositories;
using Microsoft.ApplicationBlocks.Data;
using Model.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Service
{
    public interface IProductService
    {
        Product Add(Product Product);

        void Update(Product Product);

        Product Delete(int id);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAll(string keyword);

        IEnumerable<Product> GetAllByParentId(int parentId);

        List<Dictionary<string, object>> GetTableRows(DataTable dtData);

        Product GetById(int id);

        DataTable ListProduct(string categories, string sortBy, string sortPrice, string sortColor);

        IEnumerable<Product> ListProductByCategory(int id);

        void Save();
    }

    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private ITagRepository _tagRepository;
        private IProductTagRepository _productTagRepository;
        private IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IProductTagRepository productTagRepository, ITagRepository tagRepository, IUnitOfWork unitOfWork)
        {
            this._productRepository = productRepository;
            this._productTagRepository = productTagRepository;
            this._tagRepository = tagRepository;
            this._unitOfWork = unitOfWork;
        }

        public Product Add(Product Product)
        {
            var product = _productRepository.Add(Product);
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(Product.Tags))
            {
                string[] tags = Product.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagID = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagID) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagID;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }
                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = Product.ID;
                    productTag.TagID = tagID;
                    _productTagRepository.Add(productTag);
                }
                _unitOfWork.Commit();
            }
            return product;
        }

        public Product Delete(int id)
        {
            return _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _productRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetAllByParentId(int parentId)
        {
            return _productRepository.GetMulti(x => x.Status && x.CategoryID == parentId);
        }

        public Product GetById(int id)
        {
            return _productRepository.GetSingleById(id);
        }

        public List<Dictionary<string, object>> GetTableRows(DataTable dtData)
        {
            return _productRepository.GetTableRows(dtData);
        }

        public DataTable ListProduct(string categories, string sortBy, string sortPrice, string sortColor)
        {
            SqlParameter[] pram = new SqlParameter[10];
            pram[0] = new SqlParameter("@Categories", SqlDbType.VarChar, 10);
            pram[0].Value = categories;
            pram[1] = new SqlParameter("@SortBy", SqlDbType.VarChar, 10);
            pram[1].Value = sortBy;
            pram[2] = new SqlParameter("@SortPrice", SqlDbType.VarChar, 10);
            pram[2].Value = sortPrice;
            pram[3] = new SqlParameter("@SortColor", SqlDbType.VarChar, 10);
            pram[3].Value = sortColor;
            return SqlHelper.ExecuteDataset(_productRepository.connectString, CommandType.StoredProcedure, "dbo.GetListProduct", pram).Tables[0];
        }

        public IEnumerable<Product> ListProductByCategory(int id)
        {
            return this._productRepository.ListProductByCategory(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Product Product)
        {
            _productRepository.Update(Product);
            if (!string.IsNullOrEmpty(Product.Tags))
            {
                string[] tags = Product.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagID = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagID) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagID;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }
                    _productTagRepository.DeleteMulti(x => x.ProductID == Product.ID);
                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = Product.ID;
                    productTag.TagID = tagID;
                    _productTagRepository.Add(productTag);
                }
                _unitOfWork.Commit();
            }
        }
    }
}