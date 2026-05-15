using System.Collections.Generic;
using HospitalBilling.DAL;
using HospitalBilling.Models;

namespace HospitalBilling.BLL
{
    public class HospitalDepartmentManager
    {
        private readonly HospitalDepartmentGetway _departmentGetway=new HospitalDepartmentGetway();

        #region HospitalDepartment Info Insert Update Delete
        internal int Save(HospitalDepartment objHospitalDepartment)
        {
            return _departmentGetway.Save(objHospitalDepartment);
        }

        internal int Update(HospitalDepartment objHospitalDepartment)
        {
            return _departmentGetway.Update(objHospitalDepartment);
        }

        internal int Delete(HospitalDepartment objHospitalDepartment)
        {
            return _departmentGetway.Delete(objHospitalDepartment);
        }

        #endregion

        #region Check HospitalDepartment Info
        internal bool IsNameExist(string name)
        {
            bool isNameExist = false;
            var aDepartment = _departmentGetway.GetDepartmentByName(name);
            if (aDepartment != null)
            {
                isNameExist = true;
            }
            return isNameExist;
        }

        internal bool IsShortNameExist(string shortName)
        {
            bool isNameExist = false;
            var aDepartment = _departmentGetway.GetDepartmentByShortName(shortName);
            if (aDepartment != null)
            {
                isNameExist = true;
            }
            return isNameExist;
        }
        #endregion

        #region HospitalDepartment Info Get 
        internal List<HospitalDepartment> GetAllDepartment()
        {
            return _departmentGetway.GetAllDepartment();
        }
        internal HospitalDepartment GetDepartmentByName(string name)
        {
            return _departmentGetway.GetDepartmentByName(name);
        }
        internal HospitalDepartment GetDepartmentByShortName(string shortName)
        {
            return _departmentGetway.GetDepartmentByShortName(shortName);
        }
        internal HospitalDepartment GetDepartmentById(int id)
        {
            return _departmentGetway.GetDepartmentById(id);
        }
        // search
        internal List<HospitalDepartment> GetDepartmentByNameOrShortName(string searchInput)
        {
            return _departmentGetway.GetDepartmentByNameOrShortName(searchInput);
        }
        #endregion
    }
}