using Data.Infrastructure;
using Data.Repositories;
using Microsoft.ApplicationBlocks.Data;
using Model.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Service
{
    public interface IColorService
    {
        Color Add(Color Color);

        void Update(Color Color);

        Color Delete(int id);

        IEnumerable<Color> GetAll();

        Color GetById(int id);

        void Save();
    }

    public class ColorService : IColorService
    {
        private IColorRepository _colorRepository;
        private IUnitOfWork _unitOfWork;

        public ColorService(IColorRepository colorRepository, IUnitOfWork unitOfWork)
        {
            this._colorRepository = colorRepository;
            this._unitOfWork = unitOfWork;
        }

        public Color Add(Color Color)
        {
            return _colorRepository.Add(Color);
        }

        public Color Delete(int id)
        {
            return _colorRepository.Delete(id);
        }

        public IEnumerable<Color> GetAll()
        {
            return _colorRepository.GetAll();
        }

        public Color GetById(int id)
        {
            return _colorRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Color Color)
        {
            _colorRepository.Update(Color);
        }
    }
}