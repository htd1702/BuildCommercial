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
    public interface IProductRepository : IRepository<Product>
    {
        string GetCodeIndexProduct();

        DataTable ListProduct(string categories, string sortBy, string sortPrice, string sortColor, string parentID);

        IEnumerable<Product> ListProductByCategory(int id);

        DataTable ListProductByKeyword(string keyword);

        DataTable ListRelatedProduct(string id);

        List<string> ListNameProduct(string keyword);
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private string connectString = ConfigurationManager.ConnectionStrings["BuildingConnection"].ConnectionString;

        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Product> ListProductByCategory(int id)
        {
            return this.DbContext.Products.Where(p => p.CategoryID == id).ToList();
        }

        public string GetCodeIndexProduct()
        {
            string index = "";
            SqlParameter[] pram = new SqlParameter[10];
            pram[0] = new SqlParameter("@Index", SqlDbType.VarChar, 10);
            pram[0].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(connectString, CommandType.StoredProcedure, "dbo.GetIndex_Product", pram);
            return index = pram[0].Value.ToString();
        }

        public DataTable ListProduct(string categories, string sortBy, string sortPrice, string sortColor, string parentID)
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
            pram[4] = new SqlParameter("@ParentID", SqlDbType.VarChar, 10);
            pram[4].Value = parentID;
            return SqlHelper.ExecuteDataset(connectString, CommandType.StoredProcedure, "dbo.GetListProduct", pram).Tables[0];
        }

        public DataTable ListProductByKeyword(string keyword)
        {
            SqlParameter[] pram = new SqlParameter[10];
            pram[0] = new SqlParameter("@Alias", SqlDbType.VarChar, 50);
            pram[0].Value = keyword;
            return SqlHelper.ExecuteDataset(connectString, CommandType.StoredProcedure, "dbo.GetListProductByKeyword", pram).Tables[0];
        }

        public DataTable ListRelatedProduct(string id)
        {
            SqlParameter[] pram = new SqlParameter[5];
            pram[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            pram[0].Value = id;
            return SqlHelper.ExecuteDataset(connectString, CommandType.StoredProcedure, "dbo.GetListRelatedProduct", pram).Tables[0];
        }

        public List<string> ListNameProduct(string keyword)
        {
            return this.DbContext.Products.Where(p => p.Name.Contains(keyword) || p.Alias.Contains(keyword) || p.Code.Contains(keyword)).Select(x => x.Name).Take(8).ToList();
        }
    }
}