using System.Web.Mvc;
using Web.Infrastructure.Core;

namespace Web.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult ViewCart()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Add(int Id, int colorID, int sizeID)
        {
            ShoppingCart.Cart.Add(Id, colorID, sizeID);
            var response = new
            {
                Count = ShoppingCart.Cart.Count,
                Amount = ShoppingCart.Cart.Amount.ToString("#,###.#0"),
                Total = ShoppingCart.Cart.Total.ToString("#,###.#0"),
            };
            return Json(response);
        }

        [HttpPost]
        public JsonResult Remove(int Id)
        {
            ShoppingCart.Cart.Remove(Id);
            var response = new
            {
                Count = ShoppingCart.Cart.Count,
                Total = ShoppingCart.Cart.Total.ToString("#,###.#0"),
                //Amount = ShoppingCart.Cart.Amount.ToString("#,###.#0")
            };
            return Json(response);
        }

        [HttpPost]
        public JsonResult Update(int Id, int newQty)
        {
            ShoppingCart.Cart.Update(Id, newQty);
            var response = new
            {
                Count = ShoppingCart.Cart.Count,
                Total = ShoppingCart.Cart.Total.ToString("#,###.#0"),
                //Amount = ShoppingCart.Cart.Amount.ToString("#,###.#0"),
                //ItemAmount = ShoppingCart.Cart.getItemAmount(Id).ToString("c")
            };
            return Json(response);
        }

        public ActionResult Clear(int id)
        {
            //ShoppingCart.Cart.Clear(id);
            ShoppingCart.Cart.Items.Clear();
            return RedirectToAction("Index");
        }
    }
}