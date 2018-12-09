using Data.Infrastructure;
using Data.Repositories;
using Model.Model;
using System.Collections.Generic;

namespace Service
{
    public interface IBannerService
    {
        Banner Add(Banner Banner);

        void Update(Banner Banner);

        Banner Delete(int id);

        IEnumerable<Banner> GetAll();

        IEnumerable<Banner> GetAll(string keyword);

        Banner GetById(int id);

        List<string> ListNameBanner(string keyword);

        IEnumerable<Banner> ListBannerByType(int type);

        void Save();
    }

    public class BannerService : IBannerService
    {
        private IBannerRepository _bannerRepository;
        private IUnitOfWork _unitOfWork;

        public BannerService(IBannerRepository bannerRepository, IUnitOfWork unitOfWork)
        {
            this._bannerRepository = bannerRepository;
            this._unitOfWork = unitOfWork;
        }

        public Banner Add(Banner Banner)
        {
            return _bannerRepository.Add(Banner);
        }

        public Banner Delete(int id)
        {
            return _bannerRepository.Delete(id);
        }

        public IEnumerable<Banner> GetAll()
        {
            return _bannerRepository.GetAll();
        }

        public IEnumerable<Banner> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _bannerRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _bannerRepository.GetAll();
        }

        public Banner GetById(int id)
        {
            return _bannerRepository.GetSingleById(id);
        }

        public IEnumerable<Banner> ListBannerByType(int type)
        {
            return _bannerRepository.ListBannerByType(type);
        }

        public List<string> ListNameBanner(string keyword)
        {
            return _bannerRepository.ListNameBanner(keyword);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Banner Banner)
        {
            _bannerRepository.Update(Banner);
        }
    }
}