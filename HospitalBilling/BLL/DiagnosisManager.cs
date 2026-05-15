using System.Collections.Generic;
using HospitalBilling.DAL;
using HospitalBilling.Models;
using System.Data;

namespace HospitalBilling.BLL
{
    public class DiagnosisManager
    {
        private readonly DiagnosisGetway _diagnosisGetway=new DiagnosisGetway();

        // check by name and type
        internal bool IsNameExist(string name, int typeId)
        {
            return _diagnosisGetway.ChackDiagnosisByNameAndType(name, typeId);
        }
        // check by name
        internal bool IsNameExist(string name)
        {
            bool isNameExist = false;
            var aDiagnosis = _diagnosisGetway.GetDiagnosesByName(name);
            if (aDiagnosis!=null)
            {
                isNameExist = true;
            }
            return isNameExist;
        }

        internal int Save(Diagnosis aDiagnosis)
        {
            return _diagnosisGetway.Save(aDiagnosis);
        }

        internal int Update(Diagnosis aDiagnosis)
        {
            return _diagnosisGetway.Update(aDiagnosis);
        }

        internal int Delete(int id)
        {
            return _diagnosisGetway.Delete(id);
        }

        internal List<Diagnosis> GetAllDiagnosesList()
        {
            return _diagnosisGetway.GetAllDiagnosesList();
        }

        // search
        internal List<Diagnosis> GetDiagnosesByNameOrType(string searchInput)
        {
            return _diagnosisGetway.GetDiagnosesByNameOrType(searchInput);
        }

        internal Diagnosis GetDiagnosesById(int id)
        {
            return _diagnosisGetway.GetDiagnosesById(id);
        }

        internal Diagnosis GetDiagnosesByName(string name)
        {
            return _diagnosisGetway.GetDiagnosesByName(name);
        }

        internal List<Diagnosis> GetDignosisByTypeId(int diagnosisTypeId)
        {
            return _diagnosisGetway.GetDignosisByTypeId(diagnosisTypeId);
        }



       public DataTable Input(string SerchName)
        {
            return _diagnosisGetway.Input(SerchName);
        }
    }
}