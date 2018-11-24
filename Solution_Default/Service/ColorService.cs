using Data;
using Data.Infrastructure;
using Data.Repositories;
using Model.Model;
using System.Collections.Generic;
using System.Linq;

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
        private DBContext db = new DBContext();

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
            return db.Colors.Where(p => p.Name.Contains(keyword) || p.Alias.Contains(keyword)).Select(x => x.Name).Take(8).ToList();
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