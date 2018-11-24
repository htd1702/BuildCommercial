using Data.Infrastructure;
using Data.Repositories;
using Model.Model;
using System.Collections.Generic;

namespace Service
{
    public interface IColorService
    {
        Color Add(Color Color);

        void Update(Color Color);

        Color Delete(int id);

        IEnumerable<Color> GetAll();

        IEnumerable<Color> GetAll(string keyword);

        Color GetById(int id);

        List<string> ListNameColor(string keyword);

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

        public IEnumerable<Color> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _colorRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _colorRepository.GetAll();
        }

        public Color GetById(int id)
        {
            return _colorRepository.GetSingleById(id);
        }

        public List<string> ListNameColor(string keyword)
        {
            return _colorRepository.ListNameColor(keyword);
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