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

        IEnumerable<Product> ListProductDiscount();

        IEnumerable<Product> ListProductByCategory(int id);

        IEnumerable<Product> GetLastest(int top);

        IEnumerable<Product> GetListProductByTag(string tagId, int page, int pagesize, out int totalRow);

        List<Dictionary<string, object>> GetTableRows(DataTable dtData);

        List<string> ListNameProduct(string keyword);

        Product GetById(int id);

        DataTable ListProduct(string colorID, string fromPrice, string toPrice, string categoryID);

        DataTable LoadListProductByCategory(string categories);

        DataTable ListProductByKeyword(string keyword);

        DataTable ListRelatedProduct(string id);

        DataTable ListCartProduct(string id, string colorID, string sizeID);

        DataTable ListStoreOverview(int type, int categoryType);

        DataTable ListProductByCategoryType(int type, int categoryType);

        string GetCodeIndexProduct();

        int GetViewProduct(int id);

        //IEnumerable<Tag> GetListTagByProductId(int id);

        Tag GetTag(string tagId);

        void IncreaseView(int id);

        int CheckInventoryProduct(int colorID, int sizeID);

        int InventoryByProductDetails(int colorID, int sizeID);

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
            return _productRepository.Add(Product);
            //var product = _productRepository.Add(Product);
            //_unitOfWork.Commit();
            //if (!string.IsNullOrEmpty(Product.Tags))
            //{
            //    string[] tags = Product.Tags.Split(',');
            //    for (var i = 0; i < tags.Length; i++)
            //    {
            //        var tagID = StringHelper.ToUnsignString(tags[i]);
            //        if (_tagRepository.Count(x => x.ID == tagID) == 0)
            //        {
            //            Tag tag = new Tag();
            //            tag.ID = tagID;
            //            tag.Name = tags[i];
            //            tag.Type = CommonConstants.ProductTag;
            //            _tagRepository.Add(tag);
            //        }
            //        ProductTag productTag = new ProductTag();
            //        productTag.ProductID = Product.ID;
            //        productTag.TagID = tagID;
            //        _productTagRepository.Add(productTag);
            //    }
            //    _unitOfWork.Commit();
            //}
            //return product;
        }

        public void Update(Product Product)
        {
            _productRepository.Update(Product);
            //_productRepository.Update(Product);
            //if (!string.IsNullOrEmpty(Product.Tags))
            //{
            //    string[] tags = Product.Tags.Split(',');
            //    for (var i = 0; i < tags.Length; i++)
            //    {
            //        var tagID = StringHelper.ToUnsignString(tags[i]);
            //        if (_tagRepository.Count(x => x.ID == tagID) == 0)
            //        {
            //            Tag tag = new Tag();
            //            tag.ID = tagID;
            //            tag.Name = tags[i];
            //            tag.Type = CommonConstants.ProductTag;
            //            _tagRepository.Add(tag);
            //        }
            //        _productTagRepository.DeleteMulti(x => x.ProductID == Product.ID);
            //        ProductTag productTag = new ProductTag();
            //        productTag.ProductID = Product.ID;
            //        productTag.TagID = tagID;
            //        _productTagRepository.Add(productTag);
            //    }
            //    _unitOfWork.Commit();
            //}
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

        public List<Dictionary<string, object>> GetTableRows(DataTable dtData)
        {
            return _productRepository.GetTableRows(dtData);
        }

        public List<string> ListNameProduct(string keyword)
        {
            return _productRepository.ListNameProduct(keyword);
        }

        public DataTable ListProduct(string colorID, string fromPrice, string toPrice, string categoryID)
        {
            return _productRepository.ListProduct(colorID, fromPrice, toPrice, categoryID);
        }

        public IEnumerable<Product> ListProductByCategory(int id)
        {
            return this._productRepository.ListProductByCategory(id);
        }

        public DataTable ListProductByKeyword(string keyword)
        {
            return _productRepository.ListProductByKeyword(keyword);
        }

        public DataTable ListRelatedProduct(string id)
        {
            return _productRepository.ListRelatedProduct(id);
        }

        public IEnumerable<Product> ListProductDiscount()
        {
            return _productRepository.ListProductDiscount();
        }

        public DataTable ListCartProduct(string id, string colorID, string sizeID)
        {
            return _productRepository.ListCartProduct(id, colorID, sizeID);
        }

        public DataTable ListStoreOverview(int type, int categoryType)
        {
            return _productRepository.ListStoreOverview(type, categoryType);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public int GetViewProduct(int id)
        {
            return _productRepository.GetViewProduct(id);
        }

        //public IEnumerable<Tag> GetListTagByProductId(int id)
        //{
        //    return _productTagRepository.GetMulti(x => x.ProductID == id, new string[] { "Tag" }).Select(y => y.Tag);
        //}

        public void IncreaseView(int id)
        {
            var product = _productRepository.GetSingleById(id);
            if (product.ViewCount.HasValue)
                product.ViewCount += 1;
            else
                product.ViewCount = 1;
        }

        public IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, out int totalRow)
        {
            var model = _productRepository.GetListProductByTag(tagId, page, pageSize, out totalRow);
            return model;
        }

        public Tag GetTag(string tagId)
        {
            return _tagRepository.GetSingleByCondition(x => x.ID == tagId);
        }

        public int CheckInventoryProduct(int colorID, int sizeID)
        {
            return _productRepository.CheckInventoryProduct(colorID, sizeID);
        }

        public int InventoryByProductDetails(int colorID, int sizeID)
        {
            return _productRepository.InventoryByProductDetails(colorID, sizeID);
        }

        public DataTable ListProductByCategoryType(int type, int categoryType)
        {
            return _productRepository.ListProductByCategoryType(type, categoryType);
        }

        public DataTable LoadListProductByCategory(string categories)
        {
            return _productRepository.LoadListProductByCategory(categories);
        }
    }
}