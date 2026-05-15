using HospitalBilling.BLL;
using HospitalBilling.Models;
using HospitalBilling.Report.Models;
using HospitalBilling.Report.OpReport;
using HospitalManager.Library.Billings.OpBilling;
using HospitalManager.Library.IpAssignDiagnosis;
using HospitalModels.Library.Billings.Op;
using HospitalModels.Library.CommonClass;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using DiagnosisBillDtl = HospitalModels.Library.Billings.DiagnosisBillDtl;
using DiagnosisBillMst = HospitalModels.Library.Billings.DiagnosisBillMst;

namespace HospitalBilling.UI
{
    public partial class OpDiagnosisAssignForm : System.Web.UI.Page
    {
        private readonly OutdoorPatientManager _outdoorPatientManager;

        private readonly DiagnosisTypeManager _diagnosisTypeManager;
        private readonly DiagnosisManager _diagnosisManager;

        private readonly IpAssignDiagnosisManager _assignDiagnosisManager;

        private readonly OpMoneyReceiveManager _moneyReceiveManager;

        private readonly HospitalDepartmentManager _hospitalDepartmentManager;

        private readonly ReferenceByManager _referenceByManager;
        private readonly DoctorManager _doctorManager;


        public OpDiagnosisAssignForm()
        {
            _outdoorPatientManager = new OutdoorPatientManager();
            _diagnosisTypeManager = new DiagnosisTypeManager();
            _diagnosisManager = new DiagnosisManager();

            _assignDiagnosisManager = new IpAssignDiagnosisManager();

            _moneyReceiveManager = new OpMoneyReceiveManager();

            _hospitalDepartmentManager = new HospitalDepartmentManager();
            _referenceByManager = new ReferenceByManager();
            _doctorManager = new DoctorManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUserId"] != null && Session["LoginUserRole"] != null)
            {
                if (!IsPostBack)
                {
                    RefressAssign();
                    LoadDropdown();

                    deliveryDateTextBox.Text = DateTime.Now.ToString("dd-MM-yyyy");

                    newEntryTextBox.Text = DateTime.Now.ToString("dd-MM-yyyy");

                    newPatientIdTextBox.Text = _outdoorPatientManager.AutoId();

                    Refress();
                }
            }
            else
            {
                Response.Redirect("../LoginForm.aspx");
            }

        }

        // for viewState
        private void RefressAssign()
        {
            ViewState["Diagnosis"] = null;
            var diagnosisBillDttlList = new List<DiagnosisBillDtl>();
            ViewState["Diagnosis"] = diagnosisBillDttlList;
        }

        public List<PaymentType> GetPaymentTypes()
        {
            var paymentTypeList = new List<PaymentType>()
            {
                //new PaymentType(){Id = 0, Name = ""},
                new PaymentType(){Id = 1, Name = "Cash"},
                new PaymentType(){Id = 2, Name = "Bank"}
            };
            return paymentTypeList;
        }

        private void LoadDropdown()
        {
            diagnosisTypeDropDownList.DataSource = _diagnosisTypeManager.GetAllDiagnosisTypes();
            diagnosisTypeDropDownList.DataTextField = "Name";
            diagnosisTypeDropDownList.DataValueField = "Id";
            diagnosisTypeDropDownList.DataBind();
            diagnosisTypeDropDownList.Items.Insert(0, "");
            diagnosisTypeDropDownList.SelectedIndex = -1;



            paymentTypeDiv.Visible = false;

            paymentTypeDropDownList.DataSource = GetPaymentTypes();
            paymentTypeDropDownList.DataTextField = "Name";
            paymentTypeDropDownList.DataValueField = "Id";
            paymentTypeDropDownList.DataBind();



            newDepartmentDropDownList.DataSource = _hospitalDepartmentManager.GetAllDepartment();
            newDepartmentDropDownList.DataTextField = "Name";
            newDepartmentDropDownList.DataValueField = "Id";
            newDepartmentDropDownList.DataBind();
            newDepartmentDropDownList.Items.Insert(0, "");
            newDepartmentDropDownList.SelectedIndex = -1;


            newConsultantDropDownList.DataSource = _doctorManager.GetAllDoctorList();
            newConsultantDropDownList.DataTextField = "Name";
            newConsultantDropDownList.DataValueField = "Id";
            newConsultantDropDownList.DataBind();
            newConsultantDropDownList.Items.Insert(0, "");
            newConsultantDropDownList.SelectedIndex = -1;

            newReferanceByDropDownList.DataSource = _referenceByManager.GetAllReferenceByList();
            newReferanceByDropDownList.DataTextField = "Name";
            newReferanceByDropDownList.DataValueField = "Id";
            newReferanceByDropDownList.DataBind();
            newReferanceByDropDownList.Items.Insert(0, "");
            newReferanceByDropDownList.SelectedIndex = -1;
        }

        protected void searchLinkButton_Click(object sender, EventArgs e)
        {
            try
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
                    var patient = _outdoorPatientManager.GetPatientByPatientIdNamePhoneNo(searchInput);
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

                        ViewState["ReferenceById"] = patient.ReferenceById;
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n" + ex, true);
            }


        }

        protected void diagnosisTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(diagnosisTypeDropDownList.SelectedValue))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select Valid Diagnosis Type !!');", true);
                    diagnosisTypeDropDownList.Focus();

                }
                else
                {
                    int diagnosisTypeId = Convert.ToInt32(diagnosisTypeDropDownList.SelectedValue);
                    var dignosisList = _diagnosisManager.GetDignosisByTypeId(diagnosisTypeId);
                    if (dignosisList != null)
                    {
                        diagnosisDropDownList.DataSource = dignosisList;
                        diagnosisDropDownList.DataTextField = "Name";
                        diagnosisDropDownList.DataValueField = "Id";
                        diagnosisDropDownList.DataBind();
                        diagnosisDropDownList.Items.Insert(0, "");
                        diagnosisDropDownList.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n" + ex, true);
            }
            
        }

        public List<Diagnosis> GetDiagnosisByType(string name)
        {
            int diagnosisTypeId = Convert.ToInt32(diagnosisTypeDropDownList.SelectedValue);
            var diagnosis = _diagnosisManager.GetAllDiagnosesList()
                            .Where(c => c.Name.ToLower().Contains(name.ToLower()) && c.DiagnosisTypeId == diagnosisTypeId)
                            .ToList();
            return diagnosis;
        }

        protected void diagnosisDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(diagnosisTypeDropDownList.SelectedValue))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select Valid Diagnosis !!');", true);
                    diagnosisTypeDropDownList.Focus();

                }
                else
                {
                    int diagnosisId = Convert.ToInt32(diagnosisDropDownList.SelectedValue);
                    var dignosis = _diagnosisManager.GetDiagnosesById(diagnosisId);
                    if (dignosis != null)
                    {
                        priceTextBox.Text = discountByTestTextBox.Text = "";
                        priceTextBox.Text = dignosis.TotalFee.ToString();
                        discountByTestTextBox.Text = "0";
                        totalPriceTextBox.Text = dignosis.TotalFee.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n" + ex, true);
            }
            
        }

        protected void discountByTestTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(diagnosisTypeDropDownList.SelectedValue))
                {
                    discountByTestTextBox.Text = "0";
                    diagnosisTypeDropDownList.Focus();

                }
                else if (String.IsNullOrEmpty(diagnosisDropDownList.SelectedValue))
                {
                    discountByTestTextBox.Text = "0";
                    diagnosisDropDownList.Focus();
                }
                else
                {
                    decimal discount;
                    decimal price = Convert.ToDecimal(priceTextBox.Text);
                    if (discountByTestTextBox.Text != "")
                    {
                        discount = Convert.ToDecimal(discountByTestTextBox.Text);
                    }
                    else
                    {
                        discount = 0;
                    }
                    decimal totalPrice = price - discount;

                    totalPriceTextBox.Text = totalPrice.ToString();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n" + ex, true);
            }
            
        }

        protected void assignButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (idHiddenField.Value == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Search a Patient First Then Assign !!');", true);
                    patientIdTextBox.Focus();
                }
                else if (String.IsNullOrEmpty(diagnosisTypeDropDownList.SelectedValue))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select Diagnosis Type !!');", true);
                    diagnosisTypeDropDownList.Focus();
                }
                else if (String.IsNullOrEmpty(diagnosisDropDownList.SelectedValue))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select Diagnosis !!');", true);
                    diagnosisDropDownList.Focus();
                }
                else if (deliveryDateTextBox.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select Delivery Date !!');", true);
                    deliveryDateTextBox.Focus();
                }
                else
                {

                    var aDiagnosisBillDtl = new DiagnosisBillDtl();
                    aDiagnosisBillDtl.DiagnosisBillMstId = 0;
                    aDiagnosisBillDtl.DiagnosisTypeId = Convert.ToInt32(diagnosisTypeDropDownList.SelectedValue);
                    aDiagnosisBillDtl.DiagnosisTypeName = diagnosisTypeDropDownList.SelectedItem.Text;
                    aDiagnosisBillDtl.DiagnosisId = Convert.ToInt32(diagnosisDropDownList.SelectedValue);
                    aDiagnosisBillDtl.DiagnosisName = diagnosisDropDownList.SelectedItem.Text;
                    aDiagnosisBillDtl.Price = Convert.ToDecimal(priceTextBox.Text);

                    if (discountByTestTextBox.Text != "")
                    {
                        aDiagnosisBillDtl.Discount = Convert.ToDecimal(discountByTestTextBox.Text);
                    }
                    else
                    {
                        aDiagnosisBillDtl.Discount = 0;
                    }
                    aDiagnosisBillDtl.PayableAmount = aDiagnosisBillDtl.Price - aDiagnosisBillDtl.Discount;
                    string deliverDate = deliveryDateTextBox.Text;
                    aDiagnosisBillDtl.DeliveryDate = DateTime.ParseExact(deliverDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                    if (aDiagnosisBillDtl.PayableAmount < aDiagnosisBillDtl.Discount)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Payable amount less than 0!!');", true);
                        discountByTestTextBox.Focus();
                    }
                    else
                    {
                        var diagnosisBillDtlList = (List<DiagnosisBillDtl>)ViewState["Diagnosis"];
                        bool checkDuplicate = diagnosisBillDtlList.Any(diagnosisBillDtl => diagnosisBillDtl.DiagnosisId == aDiagnosisBillDtl.DiagnosisId);

                        if (checkDuplicate)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Already Select This Diagnosis!!');", true);

                        }
                        else
                        {
                            var selectedDiagnosisList = _assignDiagnosisManager.GetAllSelectedDiagnosis(aDiagnosisBillDtl, ViewState["Diagnosis"]);
                            ViewState["Diagnosis"] = selectedDiagnosisList;
                            diagnosisBillGridView.DataSource = selectedDiagnosisList;
                            diagnosisBillGridView.DataBind();

                            CalclutionRigntsidePaymentSystem(selectedDiagnosisList);

                            RefressDiagnosis();
                        }
                    }
                }
            }
             catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n" + ex, true);
            }
           
        }


        private void CalclutionRigntsidePaymentSystem(List<DiagnosisBillDtl> selectedDiagnosisList)
        {
            try
            {
                if (selectedDiagnosisList.Count > 0)
                {
                    // total net Price
                    decimal netTotalPrice = selectedDiagnosisList.Sum(diagnosisBillDtl => diagnosisBillDtl.PayableAmount);

                    // consultent fee
                    decimal consultentFee = 0;
                    if (consultantFeeTextBox.Text != "" || consultantFeeTextBox.Text != "0")
                    {
                        consultentFee = Convert.ToDecimal(consultantFeeTextBox.Text);
                    }


                    // discount
                    decimal totalDiscount = 0;
                    if (discountByTotalTextBox.Text == "" || discountByTotalTextBox.Text == "0")
                    {
                        totalDiscount = 0;
                    }
                    else
                    {
                        totalDiscount = Convert.ToDecimal(discountByTotalTextBox.Text);
                    }

                    // vat
                    decimal vat = 0;
                    if (vatTextBox.Text == "" || vatTextBox.Text == "0")
                    {
                        vat = 0;
                    }
                    else
                    {
                        vat = Convert.ToDecimal(vatTextBox.Text);
                    }



                    // payable amount
                    decimal totalPayableAmount = (netTotalPrice - totalDiscount) +
                                                 ((netTotalPrice - totalDiscount) * vat / 100);

                    totalPayableAmount += consultentFee;

                    totalAmountTextBox.Text = netTotalPrice.ToString();
                    discountByTotalTextBox.Text = totalDiscount.ToString();
                    vatTextBox.Text = vat.ToString();
                    payAbleAmountTextBox.Text = totalPayableAmount.ToString();

                    // ***********************************************************************************
                }
                else
                {
                    totalAmountTextBox.Text = "0";
                    discountByTotalTextBox.Text = "0";
                    vatTextBox.Text = "0";
                    payAbleAmountTextBox.Text = "0";
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n" + ex, true);
            }

            // for billing ***************************************************************
           
        }

        private void RefressDiagnosis()
        {
            priceTextBox.Text = discountByTestTextBox.Text = totalPriceTextBox.Text = "0";

            int diagnosisTypeId = Convert.ToInt32(diagnosisTypeDropDownList.SelectedValue);
            var dignosisList = _diagnosisManager.GetDignosisByTypeId(diagnosisTypeId);
            if (dignosisList != null)
            {
                diagnosisDropDownList.DataSource = dignosisList;
                diagnosisDropDownList.DataTextField = "Name";
                diagnosisDropDownList.DataValueField = "Id";
                diagnosisDropDownList.DataBind();
                diagnosisDropDownList.Items.Insert(0, "");
                diagnosisDropDownList.SelectedIndex = -1;
            }
        }

        protected void EditLinkButton_Click(object sender, EventArgs e)
        {
            var dl = (LinkButton)sender;
            var gvr = (GridViewRow)dl.NamingContainer;
            var diagnosisList = (List<DiagnosisBillDtl>)ViewState["Diagnosis"];

            //var rowItem = diagnosisList.ElementAt(gvr.RowIndex);

            diagnosisList.RemoveAt(gvr.RowIndex);
            diagnosisBillGridView.DataSource = diagnosisList;
            ViewState["Diagnosis"] = diagnosisList;
            diagnosisBillGridView.DataBind();

            CalclutionRigntsidePaymentSystem(diagnosisList);
        }


        protected void discountByTotalTextBox_TextChanged(object sender, EventArgs e)
        {
            var diagnosisList = (List<DiagnosisBillDtl>)ViewState["Diagnosis"];
            CalclutionRigntsidePaymentSystem(diagnosisList);
        }

        protected void vatTextBox_TextChanged(object sender, EventArgs e)
        {
            var diagnosisList = (List<DiagnosisBillDtl>)ViewState["Diagnosis"];
            CalclutionRigntsidePaymentSystem(diagnosisList);
        }

        protected void saveAssignButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["LoginUserId"] == null)
                {
                    Response.Redirect("../LoginForm.aspx");
                }
                if (idHiddenField.Value == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Search a Patient First Then Pay !!');", true);
                    patientIdTextBox.Focus();
                }
                else
                {
                    var diagnosisDtlList = (List<DiagnosisBillDtl>)ViewState["Diagnosis"];

                    // for sefe calclution
                    CalclutionRigntsidePaymentSystem(diagnosisDtlList);

                    var diagnosisMst = new DiagnosisBillMst();
                    diagnosisMst.PatientId = idHiddenField.Value;
                    diagnosisMst.PatientType = "OP";
                    diagnosisMst.BillNo = _assignDiagnosisManager.GetAutoBillNumber();
                    diagnosisMst.SpecialDiscount = Convert.ToDecimal(discountByTotalTextBox.Text);
                    diagnosisMst.Vat = Convert.ToDecimal(vatTextBox.Text);
                    diagnosisMst.TotalPayableAmount = Convert.ToDecimal(payAbleAmountTextBox.Text);
                    diagnosisMst.EntryDate = DateTime.Now;



                    bool isBillNoExist = _assignDiagnosisManager.BillNoUniqueCheck(diagnosisMst.BillNo);
                    if (isBillNoExist)
                    {
                        diagnosisMst.BillNo = _assignDiagnosisManager.GetAutoBillNumber();
                    }
                    else
                    {
                        ViewState["DiagnosisBillNo"] = diagnosisMst.BillNo;
                        // Save DiagnosisMst and Details
                        int rowAffected = _assignDiagnosisManager.Save(diagnosisMst, diagnosisDtlList);
                        if (rowAffected > 0)
                        {

                            if (payTextBox.Text != "" || payTextBox.Text != "0")
                            {
                                // Payment System
                                var moneyReceiveMst = new OpMoneyReceiveMst()
                                {
                                    PatientId = Convert.ToInt32(idHiddenField.Value),
                                    PatientType = "OP",
                                    DiagnosisBillMstId = rowAffected,
                                    PayAmount = payTextBox.Text == "" ? 0 : Convert.ToDecimal(payTextBox.Text),
                                    AdvanceAmount = 0,
                                    SpecialDiscount = 0
                                };

                                int moneyReceiveId = _moneyReceiveManager.MoneyReceiveMstSave(moneyReceiveMst);
                                if (moneyReceiveId > 0)
                                {

                                    var paymentMethode = Convert.ToInt32(paymentTypeDropDownList.SelectedValue);

                                    if (paymentMethode == 1)
                                    {
                                        var moneyReceiveDtl = new OpMoneyReceiveDtl()
                                        {
                                            MoneyReceiveMstId = moneyReceiveId,
                                            PayMethode = paymentTypeDropDownList.SelectedItem.Text,
                                            EntryDate = DateTime.Now,
                                            MoneyReceiveBy = Session["LoginUserId"].ToString()
                                        };

                                        rowAffected = _moneyReceiveManager.MoneyReceiveDtlSaveByCash(moneyReceiveDtl);
                                        if (rowAffected > 0)
                                        {

                                            ClientScript.RegisterStartupScript(this.GetType(), "ale",
                                                "alert('Payment Successfull');", true);
                                            patientIdTextBox.ReadOnly = false;
                                        }
                                        else
                                        {
                                            ClientScript.RegisterStartupScript(this.GetType(), "ale",
                                                "alert('Payment Success but MoneyReceive Dtl Save Fail');", true);

                                            patientIdTextBox.ReadOnly = false;
                                        }
                                    }
                                    else if (paymentMethode == 2)
                                    {
                                        var moneyReceiveDtl = new OpMoneyReceiveDtl()
                                        {
                                            MoneyReceiveMstId = moneyReceiveId,
                                            PayMethode = paymentTypeDropDownList.SelectedItem.Text,
                                            BankName = bankNameTextBox.Text,
                                            ChequeNo = chequeTextBox.Text,
                                            ChequerDate = chequeDateTextBox.Text,
                                            EntryDate = DateTime.Now,
                                            MoneyReceiveBy = Session["LoginUserId"].ToString()
                                        };
                                        rowAffected = _moneyReceiveManager.MoneyReceiveDtlSaveByBank(moneyReceiveDtl);
                                        if (rowAffected > 0)
                                        {
                                            ClientScript.RegisterStartupScript(this.GetType(), "ale",
                                                "alert('Payment Successfull');", true);
                                            patientIdTextBox.ReadOnly = false;

                                        }
                                        else
                                        {
                                            ClientScript.RegisterStartupScript(this.GetType(), "ale",
                                                "alert('Payment Success but MoneyReceive Dtl Save Fail');", true);
                                            patientIdTextBox.ReadOnly = false;
                                        }

                                    }
                                }
                            }
                            GetReport();
                            RefressSave();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n" + ex, true);
            }
            
        }

        private void RefressSave()
        {


            RefressAssign();
            idHiddenField.Value =
                patientIdTextBox.Text =
                    patientNameTextBox.Text = phoneNoTextBox.Text = "";

            priceTextBox.Text = discountByTestTextBox.Text = totalPriceTextBox.Text = "0";

            totalAmountTextBox.Text =
                discountByTotalTextBox.Text = vatTextBox.Text = payAbleAmountTextBox.Text = payTextBox.Text = "0";


            diagnosisTypeDropDownList.DataSource = _diagnosisTypeManager.GetAllDiagnosisTypes();
            diagnosisTypeDropDownList.DataTextField = "Name";
            diagnosisTypeDropDownList.DataValueField = "Id";
            diagnosisTypeDropDownList.DataBind();
            diagnosisTypeDropDownList.Items.Insert(0, "");
            diagnosisTypeDropDownList.SelectedIndex = -1;


            diagnosisBillGridView.DataSource = null;
            diagnosisBillGridView.DataBind();
        }

        protected void paymentTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(paymentTypeDropDownList.SelectedValue);
            if (id == 1)
            {
                paymentTypeDiv.Visible = false;
            }
            else
            {
                paymentTypeDiv.Visible = true;
            }
        }




        protected void newPatientSaveButton_Click(object sender, EventArgs e)
        {
            if (newPatientIdTextBox.Text == "")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Insert Patient Id.";
            }
            else if (newPatientNameTextBox.Text == "")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Insert Patient Name.";
            }
            else
            {
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

                var aOutdoorPatient = new OutdoorPatient
                {
                    PatientId = _outdoorPatientManager.AutoId(),
                    Name = newPatientNameTextBox.Text,
                    PhoneNo = newPhoneNoTextBox.Text,
                    Gender = gender,
                    Age = newAgeTextBox.Text
                };

                string entryDate = newEntryTextBox.Text;
                aOutdoorPatient.EntryDate = DateTime.ParseExact(entryDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                if (newDepartmentDropDownList.SelectedValue != "")
                {
                    aOutdoorPatient.DepartmentId = Convert.ToInt32(newDepartmentDropDownList.SelectedValue);
                }
                if (newConsultantDropDownList.SelectedValue != "")
                {
                    aOutdoorPatient.DoctorId = Convert.ToInt32(newConsultantDropDownList.SelectedValue);
                }
                if (newReferanceByDropDownList.SelectedValue != "")
                {
                    aOutdoorPatient.ReferenceById = Convert.ToInt32(newReferanceByDropDownList.SelectedValue);
                }

                int rowAffected = _outdoorPatientManager.Save(aOutdoorPatient);
                if (rowAffected > 0)
                {
                    ViewState["NewPatientId"] = aOutdoorPatient.PatientId;
                    newPatientIdTextBox.Text = _outdoorPatientManager.AutoId();
                    Refress();
                    //outdoorPatientPopup.Hide();
                    //System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(System.Web.UI.Page), "Script", "HideModalDiv();", true);

                    messageLabel.CssClass = "alert alert-success";
                    messageLabel.Text = "<strong>Success!</strong> Save Patient in Database.";
                }
                else
                {
                    messageLabel.CssClass = "alert alert-warning";
                    messageLabel.Text = "<strong>Fail!</strong> Can'not Save Patient in Database.";
                }
            }
        }

        protected void closeLinkButton_Click(object sender, EventArgs e)
        {
            outdoorPatientPopup.Hide();
            patientIdTextBox.Text = ViewState["NewPatientId"].ToString();
        }

        private void Refress()
        {
            newDepartmentDropDownList.DataSource = _hospitalDepartmentManager.GetAllDepartment();
            newDepartmentDropDownList.DataTextField = "Name";
            newDepartmentDropDownList.DataValueField = "Id";
            newDepartmentDropDownList.DataBind();
            newDepartmentDropDownList.Items.Insert(0, "");
            newDepartmentDropDownList.SelectedIndex = -1;


            newConsultantDropDownList.DataSource = _doctorManager.GetAllDoctorList();
            newConsultantDropDownList.DataTextField = "Name";
            newConsultantDropDownList.DataValueField = "Id";
            newConsultantDropDownList.DataBind();
            newConsultantDropDownList.Items.Insert(0, "");
            newConsultantDropDownList.SelectedIndex = -1;

            newReferanceByDropDownList.DataSource = _referenceByManager.GetAllReferenceByList();
            newReferanceByDropDownList.DataTextField = "Name";
            newReferanceByDropDownList.DataValueField = "Id";
            newReferanceByDropDownList.DataBind();
            newReferanceByDropDownList.Items.Insert(0, "");
            newReferanceByDropDownList.SelectedIndex = -1;

        }

        protected void resetButton_Click(object sender, EventArgs e)
        {
            var pageName = System.IO.Path.GetFileName(Request.Url.ToString());
            Response.Redirect(pageName);
        }

        private void GetReport()
        {
            var diagnosisDtlList = (List<DiagnosisBillDtl>)ViewState["Diagnosis"];

            // total net Price
            decimal netTotalPrice = diagnosisDtlList.Sum(diagnosisBillDtl => diagnosisBillDtl.PayableAmount);

            // consultent fee
            decimal consultentFee = 0;
            if (consultantFeeTextBox.Text != "" || consultantFeeTextBox.Text != "0")
            {
                consultentFee = Convert.ToDecimal(consultantFeeTextBox.Text);
            }


            // discount
            decimal totalDiscount = 0;
            if (discountByTotalTextBox.Text == "" || discountByTotalTextBox.Text == "0")
            {
                totalDiscount = 0;
            }
            else
            {
                totalDiscount = Convert.ToDecimal(discountByTotalTextBox.Text);
            }

            // vat
            decimal vat = 0;
            if (vatTextBox.Text == "" || vatTextBox.Text == "0")
            {
                vat = 0;
            }
            else
            {
                vat = Convert.ToDecimal(vatTextBox.Text);
            }


            // payable amount
            decimal totalPayableAmount = (netTotalPrice - totalDiscount) +
                                         ((netTotalPrice - totalDiscount) * vat / 100);

            totalPayableAmount += consultentFee;

            // pay
            decimal payAmount = 0;
            if (payTextBox.Text == "" || payTextBox.Text == "0")
            {
                payAmount = 0;
            }
            else
            {
                payAmount = Convert.ToDecimal(payTextBox.Text);
            }

            // due
            var totalDue = totalPayableAmount - payAmount;

            var diagnosisAssignModeyReport = new OpDiagnosisMoneyReportModel()
            {
                BillNo = ViewState["DiagnosisBillNo"].ToString(),
                TotalNetPrice = netTotalPrice.ToString("N2"),
                ConsultantFee = consultentFee.ToString("N2"),
                SpecialDiscount = totalDiscount.ToString("N2"),
                Vat = vat.ToString() + "%",
                TotalPayableAmount = totalPayableAmount.ToString("N2"),
                NowReceive = payAmount.ToString("N2"),
                TotalDue = totalDue.ToString("N2")
            };



            //var outdoorPatient = new OutdoorPatient()
            //{

            //    Id = Convert.ToInt32(idHiddenField.Value),
            //    PatientId = patientIdTextBox.Text,
            //    Name = patientNameTextBox.Text,
            //    PhoneNo = phoneNoTextBox.Text,
            //};
            var outdoorPatient = new OutdoorPatient();
            outdoorPatient.Id = Convert.ToInt32(idHiddenField.Value);
            outdoorPatient.PatientId = patientIdTextBox.Text;
            outdoorPatient.Name = patientNameTextBox.Text;
            outdoorPatient.PhoneNo = phoneNoTextBox.Text;
            var outdoorPatientInfo = _outdoorPatientManager.GetPatientByIdUsingReport(outdoorPatient.Id);
            outdoorPatient.Age = outdoorPatientInfo.Age;
            outdoorPatient.Gender = outdoorPatientInfo.Gender;
            outdoorPatient.DoctioName = outdoorPatientInfo.DoctioName;

            if (ViewState["ReferenceById"] != null)
            {
                var refId = Convert.ToInt32(ViewState["ReferenceById"]);
                var referenceNamager = new ReferenceByManager();
                var refBy = referenceNamager.GetReferenceById(refId);

                if (refBy != null)
                    outdoorPatient.ReferenceByName = refBy.Name;
            }

            var reportModel = new OpDiagnosisAssignReportModel
            {
                OutdoorPatient = outdoorPatient,
                DiagnosisBillDtls = diagnosisDtlList,
                OpDiagnosisMoneyReportModel = diagnosisAssignModeyReport
            };

            //var filePath=Server.MapPath("~/image/logo2.png");
            //var fileBytes = File.ReadAllBytes(filePath);

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition",
                "attachment; filename=Diagnosis " + reportModel.OutdoorPatient.PatientId + ".pdf");
            var document = new Document(PageSize.A4, 40f, 30f, 30f, 50f);
            var writer = PdfWriter.GetInstance(document, Response.OutputStream);
            document.Open();

            var assignReport = new OpDiagnosisAssignReport();
            //assignReport.GetReport(document, writer, reportModel, fileBytes);
            assignReport.GetReport(document, writer, reportModel);

            document.Close();
            Response.Flush();
            Response.End();
        }

        protected void discountByParcentTotalTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal Amount = Convert.ToDecimal(totalAmountTextBox.Text);

                discountByTotalTextBox.Text = (Convert.ToDecimal(Amount) * (Convert.ToDecimal(discountByParcentTotalTextBox.Text) / 100)).ToString();
                
                var diagnosisList = (List<DiagnosisBillDtl>)ViewState["Diagnosis"];
                CalclutionRigntsidePaymentSystem(diagnosisList);
            }
            catch
            {

            }


        }

        protected void patientIdTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable PatientId = _diagnosisManager.Input(patientIdTextBox.Text);
                if (PatientId != null)
                {
                    patientIdTextBox.Text = PatientId.Rows[0]["PatientId"].ToString();
                }
            }
            catch
            {

            }
        }

     
    }
}