using HospitalBilling.DAL;
using HospitalBilling.Models;
using System;

namespace HospitalBilling.BLL
{
    public class IndoorPatientManager
    {
        private readonly IndoorPatientGetway _indoorPatientGetway = new IndoorPatientGetway();

        internal bool IsPatientIdExist(string patientId)
        {
            bool isPatientIdExist = false;

            var aPatient = _indoorPatientGetway.GetPatientByPatientIdNamePhoneNo(patientId);

            if (aPatient != null)
            {
                isPatientIdExist = true;
            }
            return isPatientIdExist;
        }


        /// <summary>
        /// Save Indoor Patient in Databae 
        /// </summary>
        /// <param name="aIndoorPatient">Patient and Gurdian Information</param>
        /// <param name="aPatientBedInfo">Patient Bed Information</param>
        /// <returns></returns>
        internal int Save(IndoorPatient aIndoorPatient, PatientBedInfo aPatientBedInfo)
        {
            return _indoorPatientGetway.Save(aIndoorPatient, aPatientBedInfo);
        }

        internal int SavePatientBedInfo(int mstId, PatientBedInfo aPatientBedInfo)
        {
            return _indoorPatientGetway.SavePatientBedInfo(mstId, aPatientBedInfo);
        }

        internal IndoorPatient GetIndoorPatientByPatientId(string patientId)
        {
            return _indoorPatientGetway.GetIndoorPatientByPatientId(patientId);
        }
        internal IndoorPatient GetPatientByPatientIdNamePhoneNo(string seachInput)
        {
            return _indoorPatientGetway.GetPatientByPatientIdNamePhoneNo(seachInput);
        }


        /// <summary>
        /// Bedinfo Table
        /// </summary>
        /// <param name="patientTableId"></param>
        /// <returns></returns>
        internal PatientBedInfo GetBedInfoByPatientId(int patientTableId)
        {
            return _indoorPatientGetway.GetBedInfoByPatientId(patientTableId);
        }

       

        internal int BedRelese(int bedTransferId, int patientId, PatientBedInfo aPatientBedInfo, string roomType, int bedId)
        {
            return _indoorPatientGetway.BedRelese(bedTransferId, patientId, aPatientBedInfo, roomType, bedId);
        }

        internal int PatientRelese(int id, int status, DateTime releseDateTime)
        {
            return _indoorPatientGetway.PatientRelese(id, status, releseDateTime);
        }

        internal IndoorPatient GetPatientById(int id)
        {
            return _indoorPatientGetway.GetPatientById(id);
        }


        


        internal OutdoorPatient GetPatientByIdUsingReport(int id)
        {
            return _indoorPatientGetway.GetPatientByIdUsingReport(id);
        }

        
        // auto id
        internal string AutoId()
        {
            return _indoorPatientGetway.AutoId();
        }

        public int BedRelese(string bedType, int bedId)
        {
            return _indoorPatientGetway.BedRelese(bedType, bedId);
        }

        public IndoorPatient GetPatientByBillNo(string serchInput)
        {
            return _indoorPatientGetway.GetPatientByBillNo(serchInput);
        }
    }
}
 