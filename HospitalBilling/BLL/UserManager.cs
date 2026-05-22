using HospitalBilling.DAL;
using HospitalBilling.Models;
using System.Collections.Generic;

namespace HospitalBilling.BLL
{
    public class UserManager
    {
        private readonly UserGetway _userGetway = new UserGetway();


        #region User Info Check
        internal bool IsNameExist(string userName)
        {
            bool isNameExist = false;
            var user = _userGetway.GetUserByUserName(userName);
            if (user != null)
            {
                isNameExist = true;
            }
            return isNameExist;
        }
        #endregion

        #region User Info Save,Update,Delete
        internal int Save(User aUser)
        {
            return _userGetway.Save(aUser);
        }
        internal int Update(User aUser)
        {
            return _userGetway.Update(aUser);
        }
        public int Delete(User aUser)
        {
            return _userGetway.Delete(aUser);
        }
        #endregion
        #region User Info Get
        // get all user
        public List<User> GetAllUserList()
        {
            return _userGetway.GetAllUserList();
        }

        public List<User> GetUserListBySearchInput(string searchInput)
        {
            return _userGetway.GetUserListBySearchInput(searchInput);
        }

        // get user by id
        public User GetUserById(int id)
        {
            return _userGetway.GetUserById(id);
        }

        // for searching
        public User GetUserByUserNameOrPhoneNo(string userName, string phoneNo)
        {
            return _userGetway.GetUserByUserNameOrPhoneNo(userName, phoneNo);
        }

        // for login
        public User GetUserByUserNameAndPassword(string userName, string password)
        {
            return _userGetway.GetUserByUserNameAndPassword(userName, password);
        }

        // unique check for UserName
        public User GetUserByUserName(string userName)
        {
            return _userGetway.GetUserByUserName(userName);
        }
#endregion
    }
}