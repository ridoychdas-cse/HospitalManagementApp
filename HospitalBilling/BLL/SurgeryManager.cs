using System.Collections.Generic;
using HospitalBilling.DAL;
using HospitalBilling.Models;

namespace HospitalBilling.BLL
{
    public class SurgeryManager
    {
        private readonly SurgeryGetway _surgeryGetway=new SurgeryGetway();

        #region Surgery Info Check
        internal bool IsNameExist(string name)
        {
            bool isNameExist = false;
            var aSurgery = _surgeryGetway.GetSurgeryByName(name);
            if (aSurgery!=null)
            {
                isNameExist = true;
            }
            return isNameExist;
        }
        #endregion
        #region Surgery Info Save,Update,Delete
        internal int Save(Surgery aSurgery)
        {
            return _surgeryGetway.Save(aSurgery);
        }
        internal int Update(Surgery aSurgery)
        {
            return _surgeryGetway.Update(aSurgery);
        }
        internal int Delete(Surgery aSurgery)
        {
            return _surgeryGetway.Delete(aSurgery);
        }
        #endregion
        #region Surgery Info Get
        internal List<Surgery> GetAllSurgeryList()
        {
            return _surgeryGetway.GetAllSurgeryList();
        }
        internal List<Surgery> GetAllSurgeryList(int surgeryTypeId )
        {
            return _surgeryGetway.GetAllSurgeryList(surgeryTypeId);
        }
        internal Surgery GetSurgeryById(int id)
        {
            return _surgeryGetway.GetSurgeryById(id);
        }
        internal Surgery GetSurgeryByName(string name)
        {
            return _surgeryGetway.GetSurgeryByName(name);
        }
        internal List<Surgery> GetSurgeryByTypeId(int surgeryTypeId)
        {
            return _surgeryGetway.GetSurgeryByTypeId(surgeryTypeId);
        }
        #endregion
    }
}