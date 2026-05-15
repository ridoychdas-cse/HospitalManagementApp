using HospitalBilling.DAL;
using HospitalBilling.Models;
using System.Collections.Generic;

namespace HospitalBilling.BLL
{
    public class OrganizationManager
    {
        private readonly OrganizationGetway _organizationGetway = new OrganizationGetway();

        public int Save(Organization organization)
        {
            return _organizationGetway.Save(organization);
        }

        public int Update(Organization organization, int id)
        {
            return _organizationGetway.Update(organization, id);
        }

        public int DeleteUpdate(int id)
        {
            return _organizationGetway.DeleteUpdate(id);
        }

        public Organization GetOrganizations()
        {
            return _organizationGetway.GetOrganizations();
        }

        public byte[] GetGlLogo(string Id)
        {
            return _organizationGetway.GetGlLogo(Id);
        }
        public List<Organization> GetOrganizationsList()
        {
            return _organizationGetway.GetOrganizationsList();
        }

        internal Organization GetOrganizationById(int id)
        {
            return _organizationGetway.GetOrganizationById(id);
        }

        public byte[] GetImageById(int id)
        {
           
             return _organizationGetway.GetImageById(id);
        }
    }
}