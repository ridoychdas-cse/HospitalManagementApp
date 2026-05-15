using System.Collections.Generic;
using HospitalBilling.DAL;
using HospitalBilling.Models;

namespace HospitalBilling.BLL
{
    public class WardManager
    {
        private readonly WardGetway _wardGetway=new WardGetway();


        #region Ward Information Check
        // check by name and floor
        internal bool IsNameExist(string name, int floorId)
        {
            return _wardGetway.ChackWardByNameAndFloorId(name, floorId);
        }
        // check by name
        internal bool IsNameExist(string name)
        {
            bool isNameExist = false;
            var aWard = _wardGetway.GetWardByName(name);
            if (aWard!=null)
            {
                isNameExist = true;
            }
            return isNameExist;
        }
        #endregion

        #region Ward Information Insert Update Delete
        internal int Save(Ward aWard)
        {
            return _wardGetway.Save(aWard);
        }
        internal int Update(Ward aWard)
        {
            return _wardGetway.Update(aWard);
        }
        internal int Delete(Ward aWard)
        {
            return _wardGetway.Delete(aWard);
        }
        #endregion

        #region Ward Info Get
        internal List<Ward> GetAllWardList()
        {
            return _wardGetway.GetAllWardList();
        }
        // for search
        internal List<Ward> GetAllWardList(string name)
        {
            return _wardGetway.GetAllWardList(name);
        }
        internal Ward GetWardById(int id)
        {
            return _wardGetway.GetWardById(id); 
        }
        internal Ward GetWardByName(string name)
        {
            return _wardGetway.GetWardByName(name);
        }
        #endregion
    }
}