using HospitalBilling.BLL;
using HospitalBilling.Models;
using System;
using System.Data;
using System.Globalization;

namespace HospitalBilling.UI
{
    public partial class OutdoorPatientForm : System.Web.UI.Page
    {
        private readonly OutdoorPatientManager _outdoorPatientManager = new OutdoorPatientManager();
        private readonly HospitalDepartmentManager _hospitalDepartmentManager = new HospitalDepartmentManager();
        private readonly DoctorManager _doctorManager = new DoctorManager();
        private readonly ReferenceByManager _referenceByManager = new ReferenceByManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUserId"] != null && Session["LoginUserRole"] != null)
            {
                if (!IsPostBack)
                {
                    //patientIdTextBox.Text = _outdoorPatientManager.GetAutoPatientId();

                    entryDateTextBox.Text = DateTime.Now.ToString("dd-MM-yyyy");

                    GetAllDropdown();

                    patientIdTextBox.Text = _outdoorPatientManager.AutoId();
                }
            }
            else
            {
                Response.Redirect("../LoginForm.aspx");

            }

        }

        private void GetAllDropdown()
        {
            departmentDropDownList.DataSource = _hospitalDepartmentManager.GetAllDepartment();
            departmentDropDownList.DataTextField = "Name";
            departmentDropDownList.DataValueField = "Id";
            departmentDropDownList.DataBind();
            departmentDropDownList.Items.Insert(0, "");
            departmentDropDownList.SelectedIndex = -1;


            consultantDropDownList.DataSource = _doctorManager.GetAllDoctorList();
            consultantDropDownList.DataTextField = "Name";
            consultantDropDownList.DataValueField = "Id";
            consultantDropDownList.DataBind();
            consultantDropDownList.Items.Insert(0, "");
            consultantDropDownList.SelectedIndex = -1;

            referanceByDropDownList.DataSource = _referenceByManager.GetAllReferenceByList();
            referanceByDropDownList.DataTextField = "Name";
            referanceByDropDownList.DataValueField = "Id";
            referanceByDropDownList.DataBind();
            referanceByDropDownList.Items.Insert(0, "");
            referanceByDropDownList.SelectedIndex = -1;


            string query = "select DIVISION_CODE,DIVISION_NAME from DIVISION_CODE order by 1";
            DropDown.PopulationDropDownList(divisionDropdown, "DIVISION_CODE", query, "DIVISION_NAME", "DIVISION_CODE");
            divisionDropdown.SelectedIndex = -1;


      
        


        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (patientIdTextBox.Text == "")
                {
                    messageLabel.CssClass = "alert alert-warning";
                    messageLabel.Text = "<strong>Warning!</strong> Please Insert Patient Id Or Reset Form.";
                    resetButton.Focus();
                }
                else if (nameTextBox.Text == "")
                {
                    messageLabel.CssClass = "alert alert-warning";
                    messageLabel.Text = "<strong>Warning!</strong> Please Insert Patient Name.";
                    nameTextBox.Focus();
                }
                else if (entryDateTextBox.Text == "")
                {
                    messageLabel.CssClass = "alert alert-warning";
                    messageLabel.Text = "<strong>Warning!</strong> Please Select Date.";
                    entryDateTextBox.Focus();
                }
                else
                {
                    bool isPatientIdExist = _outdoorPatientManager.IsPatientIdExist(patientIdTextBox.Text);
                    if (isPatientIdExist)
                    {
                        messageLabel.CssClass = "alert alert-danger";
                        messageLabel.Text = "<strong>Warning!</strong> This Patient Id Alredy in Database.";
                    }
                    else
                    {
                        OutdoorPatient aOutdoorPatient = new OutdoorPatient();
                        aOutdoorPatient.PatientId = patientIdTextBox.Text;
                        aOutdoorPatient.Name = nameTextBox.Text;
                        aOutdoorPatient.PhoneNo = phoneNoTextBox.Text;

                        string gender;
                        if (maleRadioButton.Checked)
                        {
                            gender = "Male";
                        }
                        else if (femaleRadioButton.Checked)
                        {
                            gender = "Female";
                        }
                        else
                        {
                            gender = "Other";
                        }

                        aOutdoorPatient.Gender = gender;
                        aOutdoorPatient.Age = ageTextBox.Text;

                        string entrydate = entryDateTextBox.Text;
                        aOutdoorPatient.EntryDate = DateTime.ParseExact(entrydate, "dd-MM-yyyy",
                            CultureInfo.InvariantCulture);

                        if (departmentDropDownList.SelectedValue != "")
                        {
                            aOutdoorPatient.DepartmentId = Convert.ToInt32(departmentDropDownList.SelectedValue);
                        }
                        if (consultantDropDownList.SelectedValue != "")
                        {
                            aOutdoorPatient.DoctorId = Convert.ToInt32(consultantDropDownList.SelectedValue);
                        }
                        if (referanceByDropDownList.SelectedValue != "")
                        {
                            aOutdoorPatient.ReferenceById = Convert.ToInt32(referanceByDropDownList.SelectedValue);
                        }
                        if (divisionDropdown.SelectedValue != "")
                        {
                            aOutdoorPatient.DivisionId = Convert.ToInt32(divisionDropdown.SelectedValue);
                        }
                        if (DistrictsDropdown.SelectedValue != "")
                        {
                            aOutdoorPatient.DistrictId = Convert.ToInt32(DistrictsDropdown.SelectedValue);
                        }
                        if (ThanaDropdown.SelectedValue != "")
                        {
                            aOutdoorPatient.ThanaId = Convert.ToInt32(ThanaDropdown.SelectedValue);
                        }
                        int rowAffected = _outdoorPatientManager.Save(aOutdoorPatient);
                        if (rowAffected > 0)
                        {

                            Refress();
                            messageLabel.CssClass = "alert alert-success";
                            messageLabel.Text = "<strong>Success!</strong> Save Patient in Database.";


                        }
                        else
                        {
                            messageLabel.CssClass = "alert alert-danger";
                            messageLabel.Text = "<strong>Fail!</strong> Can'not Save Patient in Database.";
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n" + ex, true);
            }

        }

        protected void resetButton_Click(object sender, EventArgs e)
        {
            try
            {
                Refress();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n" + ex, true);
            }
        }

        private void Refress()
        {
            patientIdTextBox.Text = nameTextBox.Text = phoneNoTextBox.Text = ageTextBox.Text = "";
            maleRadioButton.Checked = true;

            GetAllDropdown();

            patientIdTextBox.Text = _outdoorPatientManager.AutoId();
        }

        protected void divisionDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            var divisionCode = divisionDropdown.SelectedValue;
            DistrictsDropdown.Items.Clear();
            string query1 = "select '' DISTRICT_CODE, '' District_Name union Select DISTRICT_CODE,District_Name from  DISTRICT_CODE  where DIVISION_CODE='"+divisionCode+"' Order by District_Name ASC";
            DropDown.PopulationDropDownList(DistrictsDropdown, "DISTRICT_CODE", query1, "District_Name", "DISTRICT_CODE");
            DistrictsDropdown.SelectedIndex = -1;
        }

        protected void DistrictsDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            var districtCode = DistrictsDropdown.SelectedValue;
            ThanaDropdown.Items.Clear();
            string query2 = "select '' THANA_CODE, '' THANA_NAME union Select THANA_CODE , THANA_NAME from THANA_CODE where DISTRICT_CODE='"+districtCode+"' Order by THANA_NAME ASC";
            DropDown.PopulationDropDownList(ThanaDropdown, "THANA_CODE", query2, "THANA_NAME", "THANA_CODE");
            ThanaDropdown.SelectedIndex = -1;
        }


    }
}