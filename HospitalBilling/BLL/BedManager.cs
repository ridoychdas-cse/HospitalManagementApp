using System.Collections.Generic;
using HospitalBilling.DAL;
using HospitalBilling.Models;

namespace HospitalBilling.BLL
{
    public class BedManager
    {
        private readonly BedGetway _bedGetway=new BedGetway();


        #region Bed Information Check
        // check by name and ward
        internal bool IsNameExist(string name, int wardId)
        {
            return _bedGetway.ChackBadByNameAndWardId(name, wardId);
        }
        // check by name
        internal bool IsNameExist(string name)
        {
            bool isNameExist = false;
            var aDiagnosis = _bedGetway.GetBedByName(name);
            if (aDiagnosis != null)
            {
                isNameExist = true;
            }
            return isNameExist;
        }
        #endregion

        #region Bed Info Insert Update Delete
        internal int Save(Bed aBed)
        {
            return _bedGetway.Save(aBed);
        }
        internal int Update(Bed aBed)
        {
            return _bedGetway.Update(aBed);
        }
        internal int Delete(Bed aBed)
        {
            return _bedGetway.Delete(aBed);
        }
        #endregion

        #region Bed Info Get
        internal List<Bed> GetAllBedList()
        {
            return _bedGetway.GetAllBedList();
        }
        // search 
        internal List<Bed> GetbedByNameOrWardNo(string searchInput)
        {
            return _bedGetway.GetbedByNameOrWardNo(searchInput);
        }
        internal List<Bed> GetBedByWardId(int id)
        {
            return _bedGetway.GetBedByWardId(id);
        }
        internal List<Bed> GetBedByWardId()
        {
            return _bedGetway.GetBedByWardId();
        }
        internal Bed GetBedById(int id)
        {
            return _bedGetway.GetBedById(id);
        }
        internal Bed GetBedByName(string name)
        {
            return _bedGetway.GetBedByName(name);
        }
        #endregion
    }
}