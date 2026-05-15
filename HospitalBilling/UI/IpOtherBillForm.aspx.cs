using HospitalBilling.BLL;
using HospitalManager.Library.Billings;
using HospitalManager.Library.SetupForm;
using System;
using HospitalModels.Library.Billings;

namespace HospitalBilling.UI
{
    public partial class IpOtherBillForm : System.Web.UI.Page
    {
        private readonly IndoorPatientManager _indoorPatientManager;
        private readonly IpOtherBillManager _ipOtherBillManager;
        private readonly OtherBillManager _otherBillManager;

        public IpOtherBillForm()
        {
            _indoorPatientManager = new IndoorPatientManager();
            _ipOtherBillManager = new IpOtherBillManager();
            _otherBillManager = new OtherBillManager();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Refress();
            }
        }

        private void Refress()
        {
            patientIdTextBox.ReadOnly = false;
            otherBillTypeDropDownList.DataSource = _otherBillManager.GetAllOtherBills();
            otherBillTypeDropDownList.DataTextField = "Name";
            otherBillTypeDropDownList.DataValueField = "Id";
            otherBillTypeDropDownList.DataBind();
            otherBillTypeDropDownList.Items.Insert(0, "");
            otherBillTypeDropDownList.SelectedIndex = -1;

            idHiddenField.Value = patientIdTextBox.Text = patientNameTextBox.Text = phoneNoTextBox.Text = priceTextBox.Text = "";

        }

        private void AsignRefresh()
        {
            
            otherBillTypeDropDownList.DataSource = _otherBillManager.GetAllOtherBills();
            otherBillTypeDropDownList.DataTextField = "Name";
            otherBillTypeDropDownList.DataValueField = "Id";
            otherBillTypeDropDownList.DataBind();
            otherBillTypeDropDownList.Items.Insert(0, "");
            otherBillTypeDropDownList.SelectedIndex = -1;
            priceTextBox.Text = "";
           

        }

        protected void searchLinkButton_Click(object sender, EventArgs e)
        {
            string searchInput = patientIdTextBox.Text;
            if (searchInput == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale",
                    "alert('Please Insert Pateint Id/ Name/ Phone No!!');", true);
                patientIdTextBox.Focus();
            }
            else
            {
                var patient = _indoorPatientManager.GetPatientByPatientIdNamePhoneNo(searchInput);
                if (patient == null)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale",
                        "alert('Can not Find Patient by This Pateint Id/ Name/ Phone No!!');", true);
                    patientIdTextBox.Focus();
                }
                else
                {
                    patientIdTextBox.ReadOnly = true;

                    idHiddenField.Value = patient.Id.ToString();
                    patientIdTextBox.Text = patient.PatientId;
                    patientNameTextBox.Text = patient.Name;
                    phoneNoTextBox.Text = patient.PhoneNo;
                }
            }
        }

        protected void assignButton_Click(object sender, EventArgs e)
        {
            if (idHiddenField.Value == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Search Pateint first by Id/ Name/ Phone No!!');", true);
                patientIdTextBox.ReadOnly = false;
                patientIdTextBox.Focus();
            }
            else if (String.IsNullOrEmpty(otherBillTypeDropDownList.SelectedValue))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select Bill Type!!');", true);
                otherBillTypeDropDownList.Focus();
            }
            else if (priceTextBox.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Price');", true);

            }
            else
            {
                var otherBill = new OtherBill()
                {
                    PatientId = Convert.ToInt32(idHiddenField.Value),
                    PatientType = "IP",
                    OtherBillId = Convert.ToInt32(otherBillTypeDropDownList.SelectedValue),
                    Price = Convert.ToDecimal(priceTextBox.Text),
                    EntryDate = DateTime.Now
                };

                int rowAffected = _ipOtherBillManager.Save(otherBill);
                if (rowAffected > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Successfully Save Bill in Database!!');", true);

                    AsignRefresh();
                }
            }
        }

        protected void reloadButton_Click(object sender, EventArgs e)
        {
            var pageName = System.IO.Path.GetFileName(Request.Url.ToString());
            Response.Redirect(pageName);
        }
    }
}