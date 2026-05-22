using System.Collections.Generic;
using HospitalBilling.DAL;
using HospitalBilling.Models;

namespace HospitalBilling.BLL
{
    public class PackageTypeManager
    {
        private readonly PackageTypeGetway _packageTypeGetway=new PackageTypeGetway();

        #region PackageType Info Check
        internal bool IsNameExist(string name)
        {
            bool isNameExist = false;
            var aPackageType = _packageTypeGetway.GetAllPackageTypesByName(name);
            if (aPackageType != null)
            {
                isNameExist = true;
            }
            return isNameExist;
        }
        #endregion
        #region PackageType Info Save,Update,Delete
        public int Save(PackageType aPackageType)
        {
            return _packageTypeGetway.Save(aPackageType);
        }
        public int Update(PackageType aPackageType)
        {
            return _packageTypeGetway.Update(aPackageType);
        }
        public int Delete(PackageType aPackageType)
        {
            return _packageTypeGetway.Delete(aPackageType);
        }
        #endregion
        #region PackageType Info Get
        public List<PackageType> GetAllPackageTypes()
        {
            return _packageTypeGetway.GetAllPackageTypes();
        }
        public PackageType GetAllPackageTypesById(int id)
        {
            return _packageTypeGetway.GetAllPackageTypesById(id);
        }
        public PackageType GetAllPackageTypesByName(string name)
        {
            return _packageTypeGetway.GetAllPackageTypesByName(name);
        }
        #endregion
    }
}