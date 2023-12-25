using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDAL
{
    public class EmpRepository : IEmpRepository
    {
        private readonly BlogDbContext _dbContext;

        public EmpRepository(BlogDbContext blogDbContext)
        {
            this._dbContext = blogDbContext;
        }

        public EmpInfo GetEmpInfoByEmialId(string EmailId)
        {
            return _dbContext.EmpInfos.FirstOrDefault(emp => emp.EmailId == EmailId);
        }

        public IEnumerable<EmpInfo> GetAllEmpInfos()
        {
            return _dbContext.EmpInfos.ToList();
        }

        public void AddEmpInfo(EmpInfo empInfo)
        {
            _dbContext.EmpInfos.Add(empInfo);
            _dbContext.SaveChanges();
        }

        public void UpdateEmpInfo(EmpInfo empInfo)
        {
            _dbContext.Entry(empInfo).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeleteEmpInfo(int empInfoId)
        {
            var empInfo = _dbContext.EmpInfos.Find(empInfoId);
            if (empInfo != null)
                _dbContext.EmpInfos.Remove(empInfo);
            _dbContext.SaveChanges();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public EmpInfo GetEmpInfoById(int EmpInfoId)
        {
            return _dbContext.EmpInfos.Find(EmpInfoId);

        }
    }
}