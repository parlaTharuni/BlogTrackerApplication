using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDAL
{
   public interface IAdminRepository
    {
        AdminInfo GetAdminInfoByEmailId(string EmailId);
        IEnumerable<AdminInfo> GetAllAdminInfos();
        void AddAdminInfo(AdminInfo adminInfo);
        void UpdateAdminInfo(AdminInfo adminInfo);
        void DeleteAdminInfo(int adminInfoId);
        void SaveChanges();
    }
}
