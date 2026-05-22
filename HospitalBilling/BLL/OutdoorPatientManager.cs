using HospitalBilling.DAL;
using HospitalBilling.Models;
using System.Data;

namespace HospitalBilling.BLL
{
    public class OutdoorPatientManager
    {
        private readonly OutdoorPatientGetway _outdoorPatientGetway=new OutdoorPatientGetway();

        #region Outdoor Patient Info Check
        internal bool IsPatientIdExist(string patientId)
        {
            bool isPatientIdExist = false;

            var aPatient = _outdoorPatientGetway.GetPatientByPatientId(patientId);

            if (aPatient!=null)
            {
                isPatientIdExist = true;
            }
            return isPatientIdExist;
        }
        #endregion

        #region Outdoor Patient Info Save,Update,Delete
        internal int Save(OutdoorPatient aOutdoorPatient)
        {
            return _outdoorPatientGetway.Save(aOutdoorPatient);
        }
        internal int Update(OutdoorPatient aOutdoorPatient)
        {
            return _outdoorPatientGetway.Update(aOutdoorPatient);
        }
        internal int Delete(OutdoorPatient aOutdoorPatient)
        {
            return _outdoorPatientGetway.Delete(aOutdoorPatient);
        }
        internal int UpdateDiagnosisBillResult(int Id, string ResultValue)
        {
            return _outdoorPatientGetway.UpdateDiagnosisBillResult(Id, ResultValue);
        }
        #endregion

        #region Outdoor Patient Info Get
        internal OutdoorPatient GetPatientByPatientId(string patientId)
        {
            return _outdoorPatientGetway.GetPatientByPatientId(patientId);
        }
        // search
        internal OutdoorPatient GetPatientByPatientIdNamePhoneNo(string serchInput)
        {
            return _outdoorPatientGetway.GetPatientByPatientIdNamePhoneNo(serchInput);
        }
        // search
        public OutdoorPatient GetPatientByBillNo(string serchInput)
        {
            return _outdoorPatientGetway.GetPatientByBillNo(serchInput);
        }
        internal string GetAutoPatientId()
        {
            return _outdoorPatientGetway.GetAutoPatientId();
        }
        internal OutdoorPatient GetPatientById(int id)
        {
            return _outdoorPatientGetway.GetPatientById(id);
        }
        internal OutdoorPatient GetPatientByIdUsingReport(int id)
        {
            return _outdoorPatientGetway.GetPatientByIdUsingReport(id);
        }
        // Get Auto Patient Id
        internal string AutoId()
        {
            return _outdoorPatientGetway.AutoId();
        }
        internal DataTable GetDiagnosis(string mstId)
        {
            return _outdoorPatientGetway.GetDiagnosis(mstId);
        }
        #endregion
    }
}