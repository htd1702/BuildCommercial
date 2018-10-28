using Data;
using Model.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Infrastructure.Core
{
    public class ShoppingCart
    {
        //public DBContext db = new DBContext();
        public List<Product> Items = new List<Product>();

        public List<ProductDetail> ItemDetails = new List<ProductDetail>();

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
                var Item = Items.Single(p => p.ID == ID);
                Item.Quantity++;
            }
            catch
            {
                using (var dbc = new DBContext())//giai~ phong dbc
                {
                    //find product
                    var Item = dbc.Products.Find(ID);
                    Item.Quantity = 1;
                    //add list cart product
                    Items.Add(Item);
                    //find productdetails
                    var ItemDetail = dbc.ProductDetails.FirstOrDefault(x => x.ProductID == ID && x.ColorID == colorID && x.SizeID == sizeID);
                    //add product detail
                    ItemDetails.Add(ItemDetail);
                }
            }
        }

        public void Remove(int ID)
        {
            var Item = Items.Single(p => p.ID == ID);
            Items.Remove(Item);
        }

        public void Update(int ID, int newQuantity)
        {
            var Item = Items.Single(p => p.ID == ID);
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

        public double getItemAmount(int Id)
        {
            var Item = Items.Single(p => p.ID == Id);
            return Item.Price * Item.Quantity * (1 - Item.PromotionPrice);
        }
    }
}