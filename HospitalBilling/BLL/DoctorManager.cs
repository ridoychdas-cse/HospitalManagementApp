using System.Collections.Generic;
using HospitalBilling.DAL;
using HospitalBilling.Models;

namespace HospitalBilling.BLL
{
    public class DoctorManager
    {

        private readonly DoctorGetway _doctorGetway = new DoctorGetway();

        internal bool IsNameExist(string name)
        {
            bool isNameExist = false;
            var aDoctor = _doctorGetway.GetDoctorByName(name);
            if (aDoctor != null)
            {
                isNameExist = true;
            }
            return isNameExist;
        }

        internal int Save(Doctor aDoctor)
        {
            return _doctorGetway.Save(aDoctor);
        }

        internal int Update(Doctor aDoctor)
        {
            return _doctorGetway.Update(aDoctor);
        }

        internal int Delete(int id)
        {
            return _doctorGetway.Delete(id);
        }

        internal List<Doctor> GetAllDoctorList()
        {
            return _doctorGetway.GetAllDoctorList();
        }

        internal Doctor GetDoctorId(int id)
        {
            return _doctorGetway.GetDoctorById(id);
        }

        internal Doctor GetDoctorName(string name)
        {
            return _doctorGetway.GetDoctorByName(name);
        }
    }
}