using Data.Infrastructure;
using Microsoft.ApplicationBlocks.Data;
using Model.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Data.Repositories
{
    public interface IProductDetailRepository : IRepository<ProductDetail>
    {
        IEnumerable<ProductCategory> getCategories();

        IEnumerable<ProductCategory> getProductCategories(int idCategory);

        int DeleteProductDetail(int id);

        DataTable Get_ListProductBySizeColor(int id, int type);

        IEnumerable<ProductDetail> ListProductDetails(int id);
    }

    public class ProductDetailRepository : RepositoryBase<ProductDetail>, IProductDetailRepository
    {
        public ProductDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        private string connectString = ConfigurationManager.ConnectionStrings["BuildingConnection"].ConnectionString;

        public IEnumerable<ProductCategory> getCategories()
        {
            return DbContext.ProductCategorys.Where(c => c.ParentID == 0).ToList();
        }

        public IEnumerable<ProductCategory> getProductCategories(int idCategory)
        {
            return DbContext.ProductCategorys.Where(c => c.ParentID == idCategory).ToList();
        }

        public int DeleteProductDetail(int id)
        {
            int result = 0;
            SqlParameter[] pram = new SqlParameter[5];
            pram[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            pram[0].Value = id;
            pram[1] = new SqlParameter("@Result", SqlDbType.Int, 4);
            pram[1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(connectString, CommandType.StoredProcedure, "dbo.DeleteProductDetailByProduct", pram);
            return result = int.Parse(pram[1].Value.ToString());
        }

        public DataTable Get_ListProductBySizeColor(int id, int type)
        {
            SqlParameter[] pram = new SqlParameter[5];
            pram[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            pram[0].Value = id;
            pram[1] = new SqlParameter("@Type", SqlDbType.Int, 4);
            pram[1].Value = type;
            return SqlHelper.ExecuteDataset(connectString, CommandType.StoredProcedure, "dbo.Get_ListProductBySizeColor", pram).Tables[0];
        }

        public IEnumerable<ProductDetail> ListProductDetails(int id)
        {
            return this.DbContext.ProductDetails.Where(d => d.ProductID == id).ToList();
        }
    }
}