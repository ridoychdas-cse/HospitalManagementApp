using System.Collections.Generic;
using HospitalBilling.DAL;
using HospitalBilling.Models;
using System.Data;

namespace HospitalBilling.BLL
{
    public class DiagnosisTypeManager
    {
        private readonly DiagnosisTypeGetway _diagnosisTypeGetway=new DiagnosisTypeGetway();

        #region DiagnosisType Check
        internal bool IsNameExist(string name)
        {
            bool isNameExist = false;
            var aDiagnosisType = _diagnosisTypeGetway.GetDiagnosisTypeByName(name);
            if (aDiagnosisType!=null)
            {
                isNameExist = true;
            }
            return isNameExist;
        }
        #endregion

        #region Diagnosistype Setup Save, Update, Delete
        internal int Save(DiagnosisType aDiagnosisType)
        {
            return _diagnosisTypeGetway.Save(aDiagnosisType);
        }
        internal int Update(DiagnosisType aDiagnosisType)
        {
            return _diagnosisTypeGetway.Update(aDiagnosisType);
        }
        internal int Delete(DiagnosisType aDiagnosisType)
        {
            return _diagnosisTypeGetway.Delete(aDiagnosisType);
        }
        #endregion


        #region DiagnosisType Info Get
        internal List<DiagnosisType> GetAllDiagnosisTypes()
        {
            return _diagnosisTypeGetway.GetAllDiagnosisTypes();
        }
        // search
        internal List<DiagnosisType> GetDiagnosisTypeByNameOrShortName(string searchInput)
        {
            return _diagnosisTypeGetway.GetDiagnosisTypeByNameOrShortName(searchInput);
        }
        internal DiagnosisType GetDiagnosisTypeById(int id)
        {
            return _diagnosisTypeGetway.GetDiagnosisTypeById(id);
        }
        internal DiagnosisType GetDiagnosisTypeByName(string name)
        {
            return _diagnosisTypeGetway.GetDiagnosisTypeByName(name);
        }
        public DataTable GetAllUOM()
        {
            return _diagnosisTypeGetway.GetAllUOM();
        }
        #endregion
    }
}