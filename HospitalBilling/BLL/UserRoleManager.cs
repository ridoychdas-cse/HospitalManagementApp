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

        #region User Role Save Update Delete
        internal int Save(UserRole role)
        {
            return _userRoleGetway.Save(role);
        }
        internal int Update(UserRole role)
        {
            return _userRoleGetway.Update(role);
        }
        internal int Delete(UserRole role)
        {
            return _userRoleGetway.Delete(role);
        }
        #endregion
        #region User Role Info Get
        internal UserRole GetUserRoleById(int id)
        {
            return _userRoleGetway.GetUserRoleById(id);
        }
        #endregion
    }
}