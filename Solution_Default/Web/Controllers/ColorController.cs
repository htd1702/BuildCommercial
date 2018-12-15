using Service;
using System;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ColorController : Controller
    {
        private IColorService _colorService;

        //contructor
        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        // GET: Color
        [HttpGet]
        public JsonResult LoadListColor()
        {
            try
            {
                var model = _colorService.GetAll();
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}