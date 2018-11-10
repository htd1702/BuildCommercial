﻿using AutoMapper;
using Model.Model;
using Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class ProductCategoryController : Controller
    {
        private IProductCategoryService _productCategoryService;
        private IProductService _productService;

        //contructor
        public ProductCategoryController(IProductCategoryService productCategoryService, IProductService productService)
        {
            _productCategoryService = productCategoryService;
            _productService = productService;
        }

        // GET: ProductCategory
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetCategoryByTake(int take)
        {
            try
            {
                var list = _productCategoryService.GetCategoryByTake(take);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public JsonResult GetCategoryByType(int type)
        {
            try
            {
                var list = _productCategoryService.GetCategoriyByType(type);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult _viewParent()
        {
            ViewBag.ListCategory = _productCategoryService.GetCategoryByTake(3);
            return PartialView();
        }
    }
}