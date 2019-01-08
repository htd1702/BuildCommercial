using Data.Infrastructure;
using Data.Repositories;
using Model.Model;
using System.Collections.Generic;

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

        int CheckType(int id);

        void Save();
    }

    public class SizeService : ISizeService
    {
        private ISizeRepository _sizeRepository;
        private IUnitOfWork _unitOfWork;

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

        public int CheckType(int id)
        {
            return _sizeRepository.CheckType(id);
        }

        public List<string> ListNameSize(string keyword)
        {
            return ListNameSize(keyword);
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