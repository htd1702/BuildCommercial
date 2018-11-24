using Data.Infrastructure;
using Data.Repositories;
using Model.Model;
using System.Collections.Generic;

namespace Service
{
    public interface IPostService
    {
        Post Add(Post Post);

        void Update(Post Post);

        Post Delete(int id);

        IEnumerable<Post> GetAll();

        IEnumerable<Post> GetAll(string keyword);

        Post GetById(int id);

        List<string> ListNamePost(string keyword);

        void Save();
    }

    public class PostService : IPostService
    {
        private IPostRepository _postRepository;
        private IUnitOfWork _unitOfWork;

        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            this._postRepository = postRepository;
            this._unitOfWork = unitOfWork;
        }

        public Post Add(Post Post)
        {
            return _postRepository.Add(Post);
        }

        public Post Delete(int id)
        {
            return _postRepository.Delete(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll();
        }

        public IEnumerable<Post> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _postRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _postRepository.GetAll();
        }

        public Post GetById(int id)
        {
            return _postRepository.GetSingleById(id);
        }

        public List<string> ListNamePost(string keyword)
        {
            return _postRepository.ListNamePost(keyword);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Post Post)
        {
            _postRepository.Update(Post);
        }
    }
}