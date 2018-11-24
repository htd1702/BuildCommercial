using Common;
using Data.Infrastructure;
using Data.Repositories;
using Model.Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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

        IEnumerable<Product> GetLastest(int top);

        IEnumerable<Product> GetHotProduct(int top);

        DataTable ListProduct(string categories, string sortBy, string sortPrice, string sortColor, string parentID);

        DataTable ListProductByKeyword(string keyword);

        DataTable ListRelatedProduct(string id);

        IEnumerable<Product> ListProductByCategory(int id);

        List<string> ListNameProduct(string keyword);

        string GetCodeIndexProduct();

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

        public string GetCodeIndexProduct()
        {
            return _productRepository.GetCodeIndexProduct();
        }

        public IEnumerable<Product> GetLastest(int top)
        {
            return _productRepository.GetMulti(x => x.Status).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetHotProduct(int top)
        {
            return _productRepository.GetMulti(x => x.Status && x.HotFlag == true).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public List<Dictionary<string, object>> GetTableRows(DataTable dtData)
        {
            return _productRepository.GetTableRows(dtData);
        }

        public List<string> ListNameProduct(string keyword)
        {
            return _productRepository.ListNameProduct(keyword);
        }

        public DataTable ListProduct(string categories, string sortBy, string sortPrice, string sortColor, string parentID)
        {
            return _productRepository.ListProduct(categories, sortBy, sortPrice, sortColor, parentID);
        }

        public IEnumerable<Product> ListProductByCategory(int id)
        {
            return this._productRepository.ListProductByCategory(id);
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

        public DataTable ListProductByKeyword(string keyword)
        {
            return _productRepository.ListProductByKeyword(keyword);
        }

        public DataTable ListRelatedProduct(string id)
        {
            return _productRepository.ListRelatedProduct(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}