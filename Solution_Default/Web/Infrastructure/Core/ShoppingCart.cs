using Data;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Web.Infrastructure.Core
{
    public class InfoCart
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameVN { get; set; }
        public string NameFr { get; set; }
        public double Price { get; set; }
        public double PriceVN { get; set; }
        public double PriceFr { get; set; }
        public string Image { get; set; }
        public int PromotionPrice { get; set; }
        public int Quantity { get; set; }
        public int Warranty { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public bool HomeFlag { get; set; }
        public bool HotFlag { get; set; }
        public int ViewCount { get; set; }
        public string Tags { get; set; }
        public bool Status { get; set; }
        public int ColorID { get; set; }
        public string ColorName { get; set; }
        public int SizeID { get; set; }
        public string SizeName { get; set; }
    }

    public class ShoppingCart
    {
        public List<InfoCart> Items = new List<InfoCart>();

        //public List<Product> Items = new List<Product>();

        //public List<ProductDetail> ItemDetails = new List<ProductDetail>();

        public static ShoppingCart Cart
        {
            get
            {
                var cart = HttpContext.Current.Session["ShoppingCart"] as ShoppingCart;
                if (cart == null)
                {
                    cart = new ShoppingCart();
                    HttpContext.Current.Session["ShoppingCart"] = cart;
                }
                return cart;
            }
        }

        public void Add(int ID, int colorID, int sizeID)
        {
            try
            {
                var Item = Items.Single(p => p.ID == ID && p.ColorID == colorID && p.SizeID == sizeID);
                Item.Quantity++;
            }
            catch
            {
                using (var dbc = new DBContext())
                {
                    var Item = ListCartProduct(ID.ToString(), colorID.ToString(), sizeID.ToString());
                    if (Item.Rows.Count > 0)
                    {
                        InfoCart info = new InfoCart();
                        foreach (var row in Item.AsEnumerable())
                        {
                            foreach (var prop in info.GetType().GetProperties())
                            {
                                try
                                {
                                    PropertyInfo propertyInfo = info.GetType().GetProperty(prop.Name);
                                    propertyInfo.SetValue(info, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                        }
                        info.Quantity = 1;
                        Items.Add(info);
                    }

                    ////find product
                    //var Item = dbc.Products.Find(ID);
                    //Item.Quantity = 1;
                    ////add list cart product
                    //Items.Add(Item);
                    ////find productdetails
                    //var ItemDetail = dbc.ProductDetails.FirstOrDefault(x => x.ProductID == ID && x.ColorID == colorID && x.SizeID == sizeID);
                    ////add product detail
                    //ItemDetails.Add(ItemDetail);
                }
            }
        }

        public void Remove(int ID, int colorID, int sizeID)
        {
            var Item = Items.Single(p => p.ID == ID && p.ColorID == colorID && p.SizeID == sizeID);
            Items.Remove(Item);
        }

        public void Update(int ID, int newQuantity, int colorID, int sizeID)
        {
            var Item = Items.Single(p => p.ID == ID && p.ColorID == colorID && p.SizeID == sizeID);
            Item.Quantity = newQuantity;
        }

        public void Clear(int ID)
        {
            Items.Clear();
        }

        public double Amount
        {
            get
            {
                var amount = Items.Sum(p => ((p.Price * p.Quantity) - ((p.Price * p.Quantity) * p.PromotionPrice)) / 100);
                return amount;
            }
        }

        public double Total
        {
            get
            {
                var total = Items.Sum(p => (p.Price * p.Quantity) - (((p.Price * p.PromotionPrice) / 100) * p.Quantity));
                return total;
            }
        }

        public int Count
        {
            get
            {
                var count = Items.Sum(p => p.Quantity);
                return count;
            }
        }

        public double AmountVN
        {
            get
            {
                var amount = Items.Sum(p => ((p.PriceVN * p.Quantity) - ((p.PriceVN * p.Quantity) * p.PromotionPrice)) / 100);
                return amount;
            }
        }

        public double TotalVN
        {
            get
            {
                var total = Items.Sum(p => (p.PriceVN * p.Quantity) - (((p.PriceVN * p.PromotionPrice) / 100) * p.Quantity));
                return total;
            }
        }

        public int CountVN
        {
            get
            {
                var count = Items.Sum(p => p.Quantity);
                return count;
            }
        }

        public double AmountFr
        {
            get
            {
                var amount = Items.Sum(p => ((p.PriceFr * p.Quantity) - ((p.PriceFr * p.Quantity) * p.PromotionPrice)) / 100);
                return amount;
            }
        }

        public double TotalFr
        {
            get
            {
                var total = Items.Sum(p => (p.PriceFr * p.Quantity) - (((p.PriceFr * p.PromotionPrice) / 100) * p.Quantity));
                return total;
            }
        }

        public int CountFr
        {
            get
            {
                var count = Items.Sum(p => p.Quantity);
                return count;
            }
        }

        public double getItemAmount(int Id)
        {
            var Item = Items.Single(p => p.ID == Id);
            return Item.Price * Item.Quantity * (1 - Item.PromotionPrice);
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
            return SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["BuildingConnection"].ConnectionString, CommandType.StoredProcedure, "dbo.GetListCartProduct", pram).Tables[0];
        }
    }
}