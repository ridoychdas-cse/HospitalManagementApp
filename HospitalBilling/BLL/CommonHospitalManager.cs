using System.Collections.Generic;
using HospitalBilling.DAL;
using HospitalBilling.Models;

namespace HospitalBilling.BLL
{
    public class CommonHospitalManager
    {
        private readonly CommonHospitalGetway _commonHospitalGetway=new CommonHospitalGetway();

        internal List<Floor> GetAllFloors()
        {
            return _commonHospitalGetway.GetAllFloors();
        }

        internal List<RoomType> GetAllRoomTypes()
        {
            return _commonHospitalGetway.GetAllRoomTypes();
        }

        internal List<BloodGroup> GetallBloodGroups()
        {
            return _commonHospitalGetway.GetallBloodGroups();
        }

        internal List<Status> GetStatus()
        {
            return _commonHospitalGetway.GetStatus();
        }
    }
}