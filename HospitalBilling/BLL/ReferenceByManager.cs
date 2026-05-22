using System.Collections.Generic;
using HospitalBilling.DAL;
using HospitalBilling.Models;

namespace HospitalBilling.BLL
{
    public class ReferenceByManager
    {

        private readonly ReferenceByGetway _referenceByGetway = new ReferenceByGetway();

        #region ReferenceBy Information Check
        internal bool IsNameExist(string name)
        {
            bool isNameExist = false;
            var aReference = _referenceByGetway.GetReferanceByName(name);
            if (aReference != null)
            {
                isNameExist = true;
            }
            return isNameExist;
        }
        #endregion

        #region ReferenceBy Info Insert Update Delete
        internal int Save(ReferenceBy aSurgeryType)
        {
            return _referenceByGetway.Save(aSurgeryType);
        }
        internal int Update(ReferenceBy aSurgeryType)
        {
            return _referenceByGetway.Update(aSurgeryType);
        }
        internal int Delete(ReferenceBy aSurgeryType)
        {
            return _referenceByGetway.Delete(aSurgeryType);
        }
        #endregion

        #region ReferenceBy Info Get
        internal List<ReferenceBy> GetAllReferenceByList()
        {
            return _referenceByGetway.GetAllReferenceByList();
        }
        internal ReferenceBy GetReferenceById(int id)
        {
            return _referenceByGetway.GetReferenceById(id);
        }
        internal ReferenceBy GetReferanceByName(string name)
        {
            return _referenceByGetway.GetReferanceByName(name);
        }
        internal int SaveAndGetId(ReferenceBy referenceBy)
        {
            return _referenceByGetway.SaveAndGetId(referenceBy);
        }
        #endregion

    }
}