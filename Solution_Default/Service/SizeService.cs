using Data;
using Data.Infrastructure;
using Data.Repositories;
using Model.Model;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public interface ISizeService
    {
        Size Add(Size Size);

        void Update(Size Size);

        Size Delete(int id);

        IEnumerable<Size> GetAll();

        IEnumerable<Size> GetAll(string keyword);

        Size GetById(int id);

        List<string> ListNameSize(string keyword);

        void Save();
    }

    public class SizeService : ISizeService
    {
        private ISizeRepository _sizeRepository;
        private IUnitOfWork _unitOfWork;
        private DBContext db = new DBContext();

        public SizeService(ISizeRepository sizeRepository, IUnitOfWork unitOfWork)
        {
            this._sizeRepository = sizeRepository;
            this._unitOfWork = unitOfWork;
        }

        public Size Add(Size Size)
        {
            return _sizeRepository.Add(Size);
        }

        public Size Delete(int id)
        {
            return _sizeRepository.Delete(id);
        }

        public IEnumerable<Size> GetAll()
        {
            return _sizeRepository.GetAll();
        }

        public IEnumerable<Size> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _sizeRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _sizeRepository.GetAll();
        }

        public Size GetById(int id)
        {
            return _sizeRepository.GetSingleById(id);
        }

        public List<string> ListNameSize(string keyword)
        {
            return db.Sizes.Where(p => p.Name.Contains(keyword) || p.Alias.Contains(keyword)).Select(x => x.Name).Take(8).ToList();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Size Size)
        {
            _sizeRepository.Update(Size);
        }
    }
}