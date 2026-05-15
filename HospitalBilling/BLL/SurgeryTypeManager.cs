using System.Collections.Generic;
using HospitalBilling.DAL;
using HospitalBilling.Models;

namespace HospitalBilling.BLL
{
    public class SurgeryTypeManager
    {
        private readonly SurgeryTypeGetway _surgeryTypeGetway=new SurgeryTypeGetway();

        internal bool IsNameExist(string name)
        {
            bool isNameExist = false;
            var aSurgeryType = _surgeryTypeGetway.GetSurgeryTypesByName(name);
            if (aSurgeryType!=null)
            {
                isNameExist = true;
            }
            return isNameExist;
        }

        internal int Save(SurgeryType aSurgeryType)
        {
            return _surgeryTypeGetway.Save(aSurgeryType);
        }

        internal int Update(SurgeryType aSurgeryType)
        {
            return _surgeryTypeGetway.Update(aSurgeryType);
        }

        internal int Delete(int id)
        {
            return _surgeryTypeGetway.Delete(id);
        }

        internal List<SurgeryType> GetAllSurgeryTypesList()
        {
            return _surgeryTypeGetway.GetAllSurgeryTypesList();
        }

        internal SurgeryType GetSurgeryTypesById(int id)
        {
            return _surgeryTypeGetway.GetSurgeryTypesById(id);
        }

        internal SurgeryType GetSurgeryTypesByName(string name)
        {
            return _surgeryTypeGetway.GetSurgeryTypesByName(name);
        }
    }
}