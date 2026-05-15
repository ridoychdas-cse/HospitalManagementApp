using System.Collections.Generic;
using System.Data;
using HospitalBilling.DAL;
using HospitalBilling.Models;

namespace HospitalBilling.BLL
{
    public class PackageManager
    {
        private readonly PackageGetway _packageGetway=new PackageGetway();

        internal DataTable GetPackageDtlList(PackageDtl aPackageDtl, object p)
        {
            DataTable dt = (DataTable)p;
            dt.NewRow();
            dt.Rows.Add(aPackageDtl.ServiceType, aPackageDtl.ServiceId, aPackageDtl.ServiceName);
            return dt;
        }

        internal bool IsNameExist(string name)
        {
            bool isNameExist = false;
            var aPackage = _packageGetway.GetPackageMstsListByName(name);
            if (aPackage != null)
            {
                isNameExist = true;
            }
            return isNameExist;
        }

        internal int Save(PackageMst aPackageMst, DataTable packageDtl)
        {
            return _packageGetway.Save(aPackageMst, packageDtl);
        }

        internal List<PackageMst> GetPackageMstsList()
        {
            return _packageGetway.GetPackageMstsList();
        }

        internal int Update(int id, PackageMst aPackageMst, DataTable packageDtl)
        {
            return _packageGetway.Update(id, aPackageMst, packageDtl);
        }

        internal int Delete(int id)
        {
            return _packageGetway.Delete(id);
        }
        internal PackageMst GetPackageMstsListById(int id)
        {
            return _packageGetway.GetPackageMstsListById(id);
        }

        internal PackageMst GetPackageMstsListByName(string name)
        {
            return _packageGetway.GetPackageMstsListByName(name);
        }

        internal List<PackageDtl> GetAllPackageDtlsListByMstId(int mstId)
        {
            return _packageGetway.GetAllPackageDtlsListByMstId(mstId);
        }
    }
}