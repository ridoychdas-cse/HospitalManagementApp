using HospitalBilling.DAL;
using HospitalBilling.Models;
using System.Collections.Generic;

namespace HospitalBilling.BLL
{
    public class UserRoleManager
    {
        private readonly UserRoleGetway _userRoleGetway = new UserRoleGetway();

        internal List<UserRole> GetAllUserRole()
        {
            return _userRoleGetway.GetAllUserList();
        }

        internal int Save(UserRole role)
        {
            return _userRoleGetway.Save(role);
        }

        internal int Update(UserRole role, int id)
        {
            return _userRoleGetway.Update(role, id);
        }

        internal int Delete(int id)
        {
            return _userRoleGetway.Delete(id);
        }

        internal UserRole GetUserRoleById(int id)
        {
            return _userRoleGetway.GetUserRoleById(id);
        }
    }
}