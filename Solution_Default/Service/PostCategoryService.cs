using Data.Infrastructure;
using Data.Repositories;
using Model.Model;
using System.Collections.Generic;
using System.Data;

namespace Service
{
    public interface IPostCategoryService
    {
        PostCategory Add(PostCategory PostCategory);

        void Update(PostCategory PostCategory);

        PostCategory Delete(int id);

        IEnumerable<PostCategory> GetAll();

        IEnumerable<PostCategory> GetAll(string keyword);

        IEnumerable<PostCategory> GetAllByParentId(int parentId);

        PostCategory GetById(int id);

        IEnumerable<PostCategory> GetCategoryByTake(int take);

        IEnumerable<PostCategory> GetCategoriyByType(int type);

        List<string> ListNamePostCategory(string keyword);

        DataTable GetPostCategoryByParent();

        void Save();
    }

    public class PostCategoryService : IPostCategoryService
    {
        private IPostCategoryRepository _postCategoryRepository;
        private IUnitOfWork _unitOfWork;

        public PostCategoryService(IPostCategoryRepository postCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._postCategoryRepository = postCategoryRepository;
            this._unitOfWork = unitOfWork;
        }

        public PostCategory Add(PostCategory PostCategory)
        {
            return _postCategoryRepository.Add(PostCategory);
        }

        public PostCategory Delete(int id)
        {
            return _postCategoryRepository.Delete(id);
        }

        public IEnumerable<PostCategory> GetAll()
        {
            return _postCategoryRepository.GetAll();
        }

        public IEnumerable<PostCategory> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _postCategoryRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _postCategoryRepository.GetAll();
        }

        public IEnumerable<PostCategory> GetAllByParentId(int parentId)
        {
            return _postCategoryRepository.GetMulti(x => x.Status && x.ParentID == parentId);
        }

        public PostCategory GetById(int id)
        {
            return _postCategoryRepository.GetSingleById(id);
        }

        public IEnumerable<PostCategory> GetCategoriyByType(int type)
        {
            return _postCategoryRepository.getCategoryByType(type);
        }

        public DataTable GetPostCategoryByParent()
        {
            return this.GetPostCategoryByParent();
        }

        public IEnumerable<PostCategory> GetCategoryByTake(int take)
        {
            return _postCategoryRepository.GetCategoryByTake(take);
        }

        public List<string> ListNamePostCategory(string keyword)
        {
            return ListNamePostCategory(keyword);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(PostCategory PostCategory)
        {
            _postCategoryRepository.Update(PostCategory);
        }
    }
}