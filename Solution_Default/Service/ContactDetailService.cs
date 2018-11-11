using Data.Infrastructure;
using Data.Repositories;
using Microsoft.ApplicationBlocks.Data;
using Model.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Service
{
    public interface IContactDetailService
    {
        ContactDetail Add(ContactDetail ContactDetail);

        void Update(ContactDetail ContactDetail);

        ContactDetail Delete(int id);

        IEnumerable<ContactDetail> GetAll();

        ContactDetail GetById(int id);

        ContactDetail GetDefaultContact();

        void Save();
    }

    public class ContactDetailService : IContactDetailService
    {
        private IContactDetailRepository _contactDetailRepository;
        private IUnitOfWork _unitOfWork;

        public ContactDetailService(IContactDetailRepository contactDetailRepository, IUnitOfWork unitOfWork)
        {
            this._contactDetailRepository = contactDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        public ContactDetail Add(ContactDetail ContactDetail)
        {
            return _contactDetailRepository.Add(ContactDetail);
        }

        public ContactDetail Delete(int id)
        {
            return _contactDetailRepository.Delete(id);
        }

        public IEnumerable<ContactDetail> GetAll()
        {
            return _contactDetailRepository.GetAll();
        }

        public ContactDetail GetById(int id)
        {
            return _contactDetailRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ContactDetail ContactDetail)
        {
            _contactDetailRepository.Update(ContactDetail);
        }

        public ContactDetail GetDefaultContact()
        {
            return _contactDetailRepository.GetSingleByCondition(x => x.Status);
        }
    }
}