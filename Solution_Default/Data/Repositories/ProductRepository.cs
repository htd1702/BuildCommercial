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

        DataTable ListProduct(string colorID, string fromPrice, string toPrice, string categoryID);

        DataTable ListStoreOverview(int type, int categoryType);

        DataTable LoadListProductByCategory(string categories);

        DataTable ListProductByKeyword(string keyword);

        DataTable ListRelatedProduct(string id);

        DataTable ListCartProduct(string id, string colorID, string sizeID);

        DataTable ListProductByCategoryType(int type, int categoryType);

        DataTable ReportProduct(string fromDate, string toDate);

        List<string> ListNameProduct(string keyword);

        IEnumerable<Product> ListProductByCategory(int id);

        IEnumerable<Product> ListProductDiscount();

        IEnumerable<Product> ListHotProduct();

        IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, out int totalRow);

        int GetViewProduct(int id);

        int CheckInventoryProduct(int colorID, int sizeID);

        int InventoryByProductDetails(int colorID, int sizeID);
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

        public DataTable ListProduct(string colorID, string fromPrice, string toPrice, string categoryID)
        {
            SqlParameter[] pram = new SqlParameter[10];
            pram[0] = new SqlParameter("@ColorID", SqlDbType.VarChar, 10);
            pram[0].Value = colorID;
            pram[1] = new SqlParameter("@FromPrice", SqlDbType.VarChar, 10);
            pram[1].Value = fromPrice;
            pram[2] = new SqlParameter("@ToPrice", SqlDbType.VarChar, 10);
            pram[2].Value = toPrice;
            pram[3] = new SqlParameter("@Categories", SqlDbType.VarChar, 10);
            pram[3].Value = categoryID;
            return SqlHelper.ExecuteDataset(connectString, CommandType.StoredProcedure, "dbo.GetListProduct", pram).Tables[0];
        }

        public DataTable LoadListProductByCategory(string categories)
        {
            SqlParameter[] pram = new SqlParameter[10];
            pram[0] = new SqlParameter("@Categories", SqlDbType.VarChar, 10);
            pram[0].Value = categories;
            return SqlHelper.ExecuteDataset(connectString, CommandType.StoredProcedure, "dbo.GetListProductByCategory", pram).Tables[0];
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

        public DataTable ReportProduct(string fromDate, string toDate)
        {
            SqlParameter[] pram = new SqlParameter[5];
            pram[0] = new SqlParameter("@FromDate", SqlDbType.VarChar, 10);
            pram[0].Value = fromDate;
            pram[1] = new SqlParameter("@ToDate", SqlDbType.VarChar, 10);
            pram[1].Value = toDate;
            return SqlHelper.ExecuteDataset(connectString, CommandType.StoredProcedure, "dbo.ReportProduct", pram).Tables[0];
        }

        public List<string> ListNameProduct(string keyword)
        {
            return this.DbContext.Products.Where(p => p.Name.Contains(keyword) || p.NameVN.Contains(keyword) || p.NameFr.Contains(keyword) || p.Alias.Contains(keyword) || p.Code.Contains(keyword)).Select(x => x.Name).Take(8).ToList();
        }

        public IEnumerable<Product> ListProductDiscount()
        {
            return this.DbContext.Products.Where(p => p.Status == true && p.PromotionPrice > 0).OrderByDescending(p => p.PromotionPrice).ToList();
        }

        public DataTable ListCartProduct(string id, string colorID, string sizeID)
        {
            SqlParameter[] pram = new SqlParameter[5];
            pram[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            pram[0].Value = id;
            pram[1] = new SqlParameter("@ColorID", SqlDbType.Int, 4);
            pram[1].Value = colorID;
            pram[2] = new SqlParameter("@SizeID", SqlDbType.Int, 4);
            pram[2].Value = sizeID;
            return SqlHelper.ExecuteDataset(connectString, CommandType.StoredProcedure, "dbo.GetListCartProduct", pram).Tables[0];
        }

        public DataTable ListStoreOverview(int type, int categoryType)
        {
            SqlParameter[] pram = new SqlParameter[5];
            pram[0] = new SqlParameter("@Type", SqlDbType.Int, 4);
            pram[0].Value = type;
            pram[1] = new SqlParameter("@CategoryType", SqlDbType.Int, 4);
            pram[1].Value = categoryType;
            return SqlHelper.ExecuteDataset(connectString, CommandType.StoredProcedure, "dbo.ListStoreOverview", pram).Tables[0];
        }

        public int GetViewProduct(int id)
        {
            var list = this.DbContext.Products.Find(id);
            return int.Parse(list.ViewCount.ToString());
        }

        public IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, out int totalRow)
        {
            var query = from p in DbContext.Products
                        join pt in DbContext.ProductTags
                        on p.ID equals pt.ProductID
                        where pt.TagID == tagId
                        select p;
            totalRow = query.Count();

            return query.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public int CheckInventoryProduct(int colorID, int sizeID)
        {
            int inventory = this.DbContext.ProductDetails.FirstOrDefault(p => p.ColorID == colorID && p.SizeID == sizeID).Inventory;
            if (inventory > 0)
                return 1;
            else
                return 0;
        }

        public int InventoryByProductDetails(int colorID, int sizeID)
        {
            return this.DbContext.ProductDetails.FirstOrDefault(p => p.ColorID == colorID && p.SizeID == sizeID).Inventory;
        }

        public DataTable ListProductByCategoryType(int type, int categoryType)
        {
            SqlParameter[] pram = new SqlParameter[5];
            pram[0] = new SqlParameter("@Type", SqlDbType.Int, 4);
            pram[0].Value = type;
            pram[1] = new SqlParameter("@CategoryType", SqlDbType.Int, 4);
            pram[1].Value = categoryType;
            return SqlHelper.ExecuteDataset(connectString, CommandType.StoredProcedure, "dbo.ListProductByCategoryType", pram).Tables[0];
        }

        public IEnumerable<Product> ListHotProduct()
        {
            return this.DbContext.Products.Where(p => p.Status == true && p.HotFlag == true).ToList();
        }
    }
}