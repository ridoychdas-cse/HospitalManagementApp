using System.Collections.Generic;
using HospitalBilling.DAL;
using HospitalBilling.Models;

namespace HospitalBilling.BLL
{
    public class IndoorPatientBillManager
    {
        private readonly IndoorPatientBillGetway _indoorPatientBillGetway=new IndoorPatientBillGetway();


        // save in BedBillMr
        public int SaveBedBillMr(BedBill aBedBill)
        {
            return _indoorPatientBillGetway.SaveBedBillMr(aBedBill);
        }


        // update in bedbillMr
        public int UpdatebedBillMr(int id, BedBill aBedBill)
        {
            return _indoorPatientBillGetway.UpdatebedBillMr(id, aBedBill);
        }

        public decimal GetTotalBedBill(int id)
        {
            return _indoorPatientBillGetway.GetTotalBedBill(id);

        }


        public BedBill GetTotalPaymentHistory(int id)
        {
            return _indoorPatientBillGetway.GetTotalPaymentHistory(id);
        }

        public List<IndoorPatientBedBill> GetAllBedBillByPatientId(int id)
        {
            return _indoorPatientBillGetway.GetAllBedBillByPatientId(id);
        }

        public BedBill GetBedBillMrHistory(int id)
        {
            return _indoorPatientBillGetway.GetBedBillMrHistory(id);
        }
    }
}