using System.Collections.Generic;
using HospitalBilling.DAL;
using HospitalBilling.Models;

namespace HospitalBilling.BLL
{
    public class CabinManager
    {
        private  readonly CabinGetway _cabinGetway=new CabinGetway();

        #region Check Cabin Info
        // check by name and floor
        internal bool IsNameExist(string name, int floorId)
        {
            return _cabinGetway.ChackCabinByNameAndFloorId(name, floorId);
        }
        // check by name
        internal bool IsNameExist(string name)
        {
            bool isNameExist = false;
            var aDiagnosis = _cabinGetway.GetCabinByName(name);
            if (aDiagnosis != null)
            {
                isNameExist = true;
            }
            return isNameExist;
        }
        #endregion

        #region Cabin Info Insert Update Delete
        internal int Save(Cabin aCabin)
        {
            return _cabinGetway.Save(aCabin);
        }
        internal int Update(Cabin aCabin)
        {
            return _cabinGetway.Update(aCabin);
        }
        internal int Delete(Cabin aCabin)
        {
            return _cabinGetway.Delete(aCabin);
        }
        #endregion

        #region Get Cabin Info
        internal List<Cabin> GetAllCabinList()
        {
            return _cabinGetway.GetAllCabinList();
        }
        // search
        internal List<Cabin> GetCabinByNameOrType(string searchInput)
        {
            return _cabinGetway.GetCabinByNameOrType(searchInput);
        }
        internal List<Cabin> GetCabinByRoomTypeId(int id)
        {
            return _cabinGetway.GetCabinByRoomTypeId(id);
        }
        internal List<Cabin> GetCabinByRoomTypeId()
        {
            return _cabinGetway.GetCabinByRoomTypeId();
        }
        internal Cabin GetCabinById(int id)
        {
            return _cabinGetway.GetCabinById(id);
        }
        internal Cabin GetCabinByName(string name)
        {
            return _cabinGetway.GetCabinByName(name);
        }
        #endregion

    }
}