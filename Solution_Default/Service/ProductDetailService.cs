using Data.Infrastructure;
using Data.Repositories;
using Microsoft.ApplicationBlocks.Data;
using Model.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Service
{
    public interface IProductDetailService
    {
        ProductDetail Add(ProductDetail ProductDetail);

        IEnumerable<ProductCategory> GetCategories();

        IEnumerable<ProductCategory> GetProductCategories(int idCategory);

        int DeleteProductDetail(int id);

        DataTable Get_ListProductBySizeColor(int id, int type);

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

        public int DeleteProductDetail(int id)
        {
            int result = 0;
            SqlParameter[] pram = new SqlParameter[5];
            pram[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            pram[0].Value = id;
            pram[1] = new SqlParameter("@Result", SqlDbType.Int, 4);
            pram[1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(_productDetailRepository.connectString, CommandType.StoredProcedure, "dbo.DeleteProductDetailByProduct", pram);
            return result = int.Parse(pram[1].Value.ToString());
        }

        public DataTable Get_ListProductBySizeColor(int id, int type)
        {
            SqlParameter[] pram = new SqlParameter[5];
            pram[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            pram[0].Value = id;
            pram[1] = new SqlParameter("@Type", SqlDbType.Int, 4);
            pram[1].Value = type;
            return SqlHelper.ExecuteDataset(_productDetailRepository.connectString, CommandType.StoredProcedure, "dbo.Get_ListProductBySizeColor", pram).Tables[0];
        }
    }
}