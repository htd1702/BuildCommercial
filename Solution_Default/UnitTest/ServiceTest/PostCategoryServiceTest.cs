using Data.Infrastructure;
using Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Model;
using Moq;
using Service;
using System.Collections.Generic;

namespace UnitTest.ServiceTest
{
    [TestClass]
    public class PostCategoryServiceTest
    {
        //Mock khởi tạo đối tượng giả
        private Mock<IPostCategoryRepository> _mockRepository;

        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IPostCategoryService _categoryService;
        private List<PostCategory> _listCategory;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IPostCategoryRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _categoryService = new PostCategoryService(_mockRepository.Object, _mockUnitOfWork.Object);
            _listCategory = new List<PostCategory>()
            {
                new PostCategory(){ID = 1, Name="DM1",Status = true },
                new PostCategory(){ID = 2, Name="DM2",Status = true },
                new PostCategory(){ID = 3, Name="DM3",Status = true },
            };
        }

        [TestMethod]
        public void PostCategory_Service_GetAll()
        {
            //setup method
            _mockRepository.Setup(x => x.GetAll(null)).Returns(_listCategory);

            //Call action
            var result = _categoryService.GetAll() as List<PostCategory>;

            //compare
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void PostCategory_ServiceCreate()
        {
            PostCategory category = new PostCategory();
            category.Name = "test";
            category.Status = true;
            category.Alias = "12";
            _mockRepository.Setup(m => m.Add(category)).Returns((PostCategory p) =>
            {
                p.ID = 1;
                return p;
            });

            var result = _categoryService.Add(category);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }
    }
}