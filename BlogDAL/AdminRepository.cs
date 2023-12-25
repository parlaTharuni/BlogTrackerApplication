using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDAL
{
    public class AdminRepository : IAdminRepository
    {
        private readonly BlogDbContext _dbContext;

        public AdminRepository(BlogDbContext blogDbContext)
        {
            _dbContext = new BlogDbContext();
        }

        public AdminInfo GetAdminInfoByEmailId(string EmailId)
        {
            return _dbContext.AdminInfos.FirstOrDefault(admin => admin.EmailId == EmailId);
        }

        public IEnumerable<AdminInfo> GetAllAdminInfos()
        {
            return _dbContext.AdminInfos.ToList();
        }

        public void AddAdminInfo(AdminInfo adminInfo)
        {
            _dbContext.AdminInfos.Add(adminInfo);
        }

        public void UpdateAdminInfo(AdminInfo adminInfo)
        {
            _dbContext.Entry(adminInfo).State = EntityState.Modified;
        }

        public void DeleteAdminInfo(int adminInfoId)
        {
            var adminInfo = _dbContext.AdminInfos.Find(adminInfoId);
            if (adminInfo != null)
                _dbContext.AdminInfos.Remove(adminInfo);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }

}

