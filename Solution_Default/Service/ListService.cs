using Data.Infrastructure;
using Data.Repositories;
using Microsoft.ApplicationBlocks.Data;
using Model.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Service
{
    public interface IListService
    {
        List Add(List List);

        void Update(List List);

        List Delete(int id);

        IEnumerable<List> GetAll();

        IEnumerable<List> GetAll(string keyword);

        IEnumerable<List> GetAllListByType(int type);

        DataTable GetListAll(int type);

        List GetById(int id);

        void Save();
    }

    public class ListService : IListService
    {
        private IListRepository _listRepository;
        private IUnitOfWork _unitOfWork;

        public ListService(IListRepository listRepository, IUnitOfWork unitOfWork)
        {
            this._listRepository = listRepository;
            this._unitOfWork = unitOfWork;
        }

        public List Add(List List)
        {
            return _listRepository.Add(List);
        }

        public List Delete(int id)
        {
            return _listRepository.Delete(id);
        }

        public IEnumerable<List> GetAll()
        {
            return _listRepository.GetAll();
        }

        public IEnumerable<List> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _listRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _listRepository.GetAll();
        }

        public IEnumerable<List> GetAllListByType(int type)
        {
            return _listRepository.GetAllListByType(type);
        }

        public List GetById(int id)
        {
            return _listRepository.GetSingleById(id);
        }

        public DataTable GetListAll(int type)
        {
            SqlParameter[] pram = new SqlParameter[10];
            pram[0] = new SqlParameter("@Type", SqlDbType.Int, 4);
            pram[0].Value = type;
            return SqlHelper.ExecuteDataset(_listRepository.connectString, CommandType.StoredProcedure, "dbo.GetListAllByType", pram).Tables[0];
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(List List)
        {
            _listRepository.Update(List);
        }
    }
}