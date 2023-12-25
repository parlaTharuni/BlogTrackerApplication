using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Security.Principal;

namespace BlogDAL
{
    public enum UserRole
    {
        Admin,
        Employee

    }
    public class AuthService : IAuthService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IEmpRepository _empRepository;

        public AuthService(IAdminRepository adminRepository, IEmpRepository empRepository)
        {
            _adminRepository = adminRepository;
            _empRepository = empRepository;
        }

        public bool Authenticate(string email, string password, UserRole selectedRole)
        {
            switch (selectedRole)
            {
                case UserRole.Admin:
                    var admin = _adminRepository.GetAllAdminInfos().FirstOrDefault(a => a.EmailId == email && a.Password == password);
                    if (admin != null)
                    {
                        return true;
                    }
                    break;

                case UserRole.Employee:
                    var emp = _empRepository.GetAllEmpInfos().FirstOrDefault(e => e.EmailId == email && e.PassCode == password);
                    if (emp != null)
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }
    }
}