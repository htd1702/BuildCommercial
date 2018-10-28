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

        Size GetById(int id);

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

        public Size GetById(int id)
        {
            return _sizeRepository.GetSingleById(id);
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