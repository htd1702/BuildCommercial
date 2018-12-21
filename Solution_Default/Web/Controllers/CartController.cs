using Service;
using System.Data;
using System.Web.Mvc;
using Web.Infrastructure.Core;

namespace Web.Controllers
{
    public class CartController : Controller
    {
        private IOrderService _orderService;

        public CartController(IOrderService orderService)
        {
            this._orderService = orderService;
        }

        // GET: Cart
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult ViewCart()
        {
            return View();
        }

        public ActionResult CheckOut()
        {
            return View();
        }

        public ActionResult ViewCartDetails()
        {
            return PartialView();
        }

        public ActionResult OrderSuccess(int id)
        {
            DataTable dt = new DataTable();
            dt = _orderService.ListOrderDetail(id.ToString());
            ViewBag.CustomerName = dt.Rows[0]["CustomerName"].ToString();
            ViewBag.Email = dt.Rows[0]["Email"].ToString();
            ViewBag.Phone = dt.Rows[0]["Phone"].ToString();
            ViewBag.Address = dt.Rows[0]["Address"].ToString();
            ViewBag.CustomerMessage = dt.Rows[0]["CustomerMessage"].ToString();
            ViewBag.PaymentMethod = dt.Rows[0]["PaymentMethod"].ToString();
            ViewBag.ID = dt.Rows[0]["ID"].ToString();
            ViewBag.Total = dt.Rows[0]["Total"].ToString();
            ViewBag.TotalVN = double.Parse(dt.Rows[0]["Total"].ToString()) * double.Parse(dt.Rows[0]["Scale"].ToString());
            ViewBag.ListOrders = dt.AsEnumerable();
            return View();
        }

        [HttpPost]
        public JsonResult Add(int Id, int colorID, int sizeID, string lang)
        {
            ShoppingCart.Cart.Add(Id, colorID, sizeID);
            if (lang == "vi")
            {
                var response = new
                {
                    Count = ShoppingCart.Cart.CountVN,
                    Amount = ShoppingCart.Cart.AmountVN.ToString("#,###.#0"),
                    Total = ShoppingCart.Cart.TotalVN.ToString("#,###.#0"),
                };
                return Json(response);
            }
            else if (lang == "fr")
            {
                var response = new
                {
                    Count = ShoppingCart.Cart.CountFr,
                    Amount = ShoppingCart.Cart.AmountFr.ToString("#,###.#0"),
                    Total = ShoppingCart.Cart.TotalFr.ToString("#,###.#0"),
                };
                return Json(response);
            }
            else
            {
                var response = new
                {
                    Count = ShoppingCart.Cart.Count,
                    Amount = ShoppingCart.Cart.Amount.ToString("#,##0.00"),
                    Total = ShoppingCart.Cart.Total.ToString("#,##0.00"),
                };
                return Json(response);
            }
        }

        [HttpPost]
        public JsonResult Remove(int Id, int colorID, int sizeID)
        {
            ShoppingCart.Cart.Remove(Id, colorID, sizeID);
            var response = new
            {
                Count = ShoppingCart.Cart.Count,
                Total = ShoppingCart.Cart.Total.ToString("#,###.#0"),
                //Amount = ShoppingCart.Cart.Amount.ToString("#,###.#0")
            };
            return Json(response);
        }

        [HttpPost]
        public JsonResult Update(int Id, int newQty, string lang, int colorID, int sizeID)
        {
            ShoppingCart.Cart.Update(Id, newQty, colorID, sizeID);
            if (lang == "vi")
            {
                var response = new
                {
                    Count = ShoppingCart.Cart.CountVN,
                    Amount = ShoppingCart.Cart.AmountVN.ToString("#,###.#0"),
                    Total = ShoppingCart.Cart.TotalVN.ToString("#,###.#0"),
                };
                return Json(response);
            }
            else if (lang == "fr")
            {
                var response = new
                {
                    Count = ShoppingCart.Cart.CountFr,
                    Amount = ShoppingCart.Cart.AmountFr.ToString("#,###.#0"),
                    Total = ShoppingCart.Cart.TotalFr.ToString("#,###.#0"),
                };
                return Json(response);
            }
            else
            {
                var response = new
                {
                    Count = ShoppingCart.Cart.Count,
                    Amount = ShoppingCart.Cart.Amount.ToString("#,##0.00"),
                    Total = ShoppingCart.Cart.Total.ToString("#,##0.00"),
                };
                return Json(response);
            }
        }

        public ActionResult Clear(int id)
        {
            //ShoppingCart.Cart.Clear(id);
            ShoppingCart.Cart.Items.Clear();
            return RedirectToAction("Index");
        }
    }
}