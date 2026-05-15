using System.Collections.Generic;
using HospitalBilling.DAL;
using HospitalBilling.Models;

namespace HospitalBilling.BLL
{
    public class PackageTypeManager
    {
        private readonly PackageTypeGetway _packageTypeGetway=new PackageTypeGetway();

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

        public int Save(PackageType aPackageType)
        {
            return _packageTypeGetway.Save(aPackageType);
        }

        public int Update(int id, PackageType aPackageType)
        {
            return _packageTypeGetway.Update(id, aPackageType);
        }

        public int Delete(int id)
        {
            return _packageTypeGetway.Delete(id);
        }

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
    }
}