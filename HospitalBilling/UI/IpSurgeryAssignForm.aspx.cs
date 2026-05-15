using HospitalBilling.BLL;
using HospitalBilling.BLL.SurgerysBills;
using HospitalBilling.Model.SurgeryBills;
using HospitalBilling.Models;
using HospitalBilling.Report.Billing;
using HospitalBilling.Report.Models;
using HospitalBilling.Report.OpReport;
using HospitalManager.Library.Billings;
using HospitalManager.Library.IpAssignDiagnosis;
using HospitalModels.Library.Billings;
using HospitalModels.Library.CommonClass;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using DiagnosisBillDtl = HospitalModels.Library.Billings.DiagnosisBillDtl;
using DiagnosisBillMst = HospitalModels.Library.Billings.DiagnosisBillMst;

namespace HospitalBilling.UI
{
    public partial class IpSurgeryAssignForm : System.Web.UI.Page
    {
        private readonly IndoorPatientManager _indoorPatientManager;

        private readonly DiagnosisTypeManager _diagnosisTypeManager;
        private readonly DiagnosisManager _diagnosisManager;

        private readonly IpAssignDiagnosisManager _assignDiagnosisManager;
        private readonly IpAssignSurgeryManager _assignsurgeryManager = new IpAssignSurgeryManager();

        private readonly IpMoneyReceiveManager _moneyReceiveManager;

        private readonly ReferenceByManager _referenceByManager;

        private readonly IpDiagnosisAssignReport _report;
        private readonly SurgeryTypeManager _surgeryTypeManager = new SurgeryTypeManager();
        private readonly SurgeryManager _surgeryManager = new SurgeryManager();

        public IpSurgeryAssignForm()
        {
            _indoorPatientManager = new IndoorPatientManager();
            _diagnosisTypeManager = new DiagnosisTypeManager();
            _diagnosisManager = new DiagnosisManager();

            _assignDiagnosisManager = new IpAssignDiagnosisManager();

            _moneyReceiveManager = new IpMoneyReceiveManager();

            _referenceByManager = new ReferenceByManager();

            _report = new IpDiagnosisAssignReport();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefressAssign();
                LoadDropdown();
            }
        }

        // for viewState
        private void RefressAssign()
        {
            ViewState["DiagnosisBillNo"] = null;
            ViewState["Surgery"] = null;
            var surgeryBillDttlList = new List<SurgeryBillDtl>();
            ViewState["Surgery"] = surgeryBillDttlList;
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
            surgeryTypeDropDownList.DataSource = _surgeryTypeManager.GetAllSurgeryTypesList();
            surgeryTypeDropDownList.DataTextField = "Name";
            surgeryTypeDropDownList.DataValueField = "Id";
            surgeryTypeDropDownList.DataBind();
            surgeryTypeDropDownList.Items.Insert(0, "");
            surgeryTypeDropDownList.SelectedIndex = -1;



            paymentTypeDiv.Visible = false;

            paymentTypeDropDownList.DataSource = GetPaymentTypes();
            paymentTypeDropDownList.DataTextField = "Name";
            paymentTypeDropDownList.DataValueField = "Id";
            paymentTypeDropDownList.DataBind();


            referenceDropDownList.DataSource = _referenceByManager.GetAllReferenceByList();
            referenceDropDownList.DataTextField = "Name";
            referenceDropDownList.DataValueField = "Id";
            referenceDropDownList.DataBind();
            referenceDropDownList.Items.Insert(0, "");
            referenceDropDownList.SelectedIndex = -1;
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
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n" + ex, true);
            }

        }

        protected void surgeryTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(surgeryTypeDropDownList.SelectedValue))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select Valid Surgery Type !!');", true);
                    surgeryTypeDropDownList.Focus();

                }
                else
                {
                    int surgeryTypeId = Convert.ToInt32(surgeryTypeDropDownList.SelectedValue);
                    List<Surgery> surgeryList = _surgeryManager.GetAllSurgeryList(surgeryTypeId);
                    if (surgeryList != null)
                    {
                        surgeryDropDownList.DataSource = surgeryList;
                        surgeryDropDownList.DataTextField = "Name";
                        surgeryDropDownList.DataValueField = "Id";
                        surgeryDropDownList.DataBind();
                        surgeryDropDownList.Items.Insert(0, "");
                        surgeryDropDownList.SelectedIndex = -1;
                    }
                }
            }

            catch { 
            
            }
        }

        protected void surgeryDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(surgeryTypeDropDownList.SelectedValue))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select Valid Surgery !!');", true);
                surgeryTypeDropDownList.Focus();

            }
            else
            {
                int surgeryId = Convert.ToInt32(surgeryDropDownList.SelectedValue);
               // var surgerys = _diagnosisManager.GetDiagnosesById(surgeryId);
                var surgerys = _surgeryManager.GetSurgeryById(surgeryId);
                if (surgerys != null)
                {
                    priceTextBox.Text = discountByTestTextBox.Text = "";
                    priceTextBox.Text = surgerys.TotalFee.ToString();
                    discountByTestTextBox.Text = "0";
                    totalPriceTextBox.Text = surgerys.TotalFee.ToString();
                }
            }
        }

        protected void discountByTestTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(surgeryTypeDropDownList.SelectedValue))
                {
                    discountByTestTextBox.Text = "0";
                    surgeryTypeDropDownList.Focus();

                }
                else if (String.IsNullOrEmpty(surgeryDropDownList.SelectedValue))
                {
                    discountByTestTextBox.Text = "0";
                    surgeryDropDownList.Focus();
                }
                else
                {
                    decimal discount;

                    decimal price;
                    if (priceTextBox.Text != "")
                    {
                        price = Convert.ToDecimal(priceTextBox.Text);
                    }
                    else
                    {
                        priceTextBox.Text = "0";
                        price = 0;
                    }

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
            catch
            { 
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
                else if (String.IsNullOrEmpty(surgeryTypeDropDownList.SelectedValue))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select Surgery Type !!');", true);
                    surgeryTypeDropDownList.Focus();
                }
                else if (String.IsNullOrEmpty(surgeryDropDownList.SelectedValue))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select Surgery !!');", true);
                    surgeryDropDownList.Focus();
                }
                else if (surgeryDateTextBox.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select Delivery Date !!');", true);
                    surgeryDateTextBox.Focus();
                }
                else
                {

                    var aSurgeryBillDtl = new SurgeryBillDtl();
                    aSurgeryBillDtl.SurgeryBillMstId = 0;
                    aSurgeryBillDtl.SurgeryTypeId = Convert.ToInt32(surgeryTypeDropDownList.SelectedValue);
                    aSurgeryBillDtl.SurgeryTypeName = surgeryTypeDropDownList.SelectedItem.Text;
                    aSurgeryBillDtl.SurgeryId = Convert.ToInt32(surgeryDropDownList.SelectedValue);
                    aSurgeryBillDtl.SurgeryName = surgeryDropDownList.SelectedItem.Text;
                    aSurgeryBillDtl.Price = Convert.ToDecimal(priceTextBox.Text);

                    if (!String.IsNullOrEmpty(referenceDropDownList.SelectedValue))
                    {
                        aSurgeryBillDtl.Reference = Convert.ToInt32(referenceDropDownList.SelectedValue);
                    }

                    if (discountByTestTextBox.Text != "")
                    {
                        aSurgeryBillDtl.Discount = Convert.ToDecimal(discountByTestTextBox.Text);
                    }
                    else
                    {
                        aSurgeryBillDtl.Discount = 0;
                    }
                    aSurgeryBillDtl.PayableAmount = aSurgeryBillDtl.Price - aSurgeryBillDtl.Discount;
                    string deliverDate = surgeryDateTextBox.Text;
                    aSurgeryBillDtl.DeliveryDate = DateTime.ParseExact(deliverDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                    if (aSurgeryBillDtl.PayableAmount < aSurgeryBillDtl.Discount)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Payable amount less than 0!!');", true);
                        discountByTestTextBox.Focus();
                    }
                    else
                    {
                        var surgeryBillDtlList = (List<SurgeryBillDtl>)ViewState["Surgery"];
                        bool checkDuplicate = surgeryBillDtlList.Any(surgeryBillDtl => surgeryBillDtl.SurgeryId == aSurgeryBillDtl.SurgeryId);

                        if (checkDuplicate)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Already Select This Surgery!!');", true);

                        }
                        else
                        {
                            var selectedSurgeryList = _assignsurgeryManager.GetAllSelectedSurgery(aSurgeryBillDtl, ViewState["Surgery"]);
                            ViewState["Surgery"] = selectedSurgeryList;
                            surgeryBillGridView.DataSource = selectedSurgeryList;
                            surgeryBillGridView.DataBind();

                            CalclutionRigntsidePaymentSystem(selectedSurgeryList);

                            RefressSurgery();
                        }
                    }
                }
            }
             catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n" + ex, true);
            }

            
        }


        private void CalclutionRigntsidePaymentSystem(List<SurgeryBillDtl> selectedSurgeryList)
        {
            // for billing ***************************************************************
            if (selectedSurgeryList.Count > 0)
            {
                // total net Price
                decimal netTotalPrice = selectedSurgeryList.Sum(surgeryBillDtl => surgeryBillDtl.PayableAmount);


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

        private void RefressSurgery()
        {
            priceTextBox.Text = discountByTestTextBox.Text = totalPriceTextBox.Text = "0";

            int surgeryTypeId = Convert.ToInt32(surgeryTypeDropDownList.SelectedValue);
            var surgeryList = _surgeryManager.GetSurgeryByTypeId(surgeryTypeId);
             
            if (surgeryList != null)
            {
                surgeryDropDownList.DataSource = surgeryList;
                surgeryDropDownList.DataTextField = "Name";
                surgeryDropDownList.DataValueField = "Id";
                surgeryDropDownList.DataBind();
                surgeryDropDownList.Items.Insert(0, "");
                surgeryDropDownList.SelectedIndex = -1;
            }
        }

        protected void EditLinkButton_Click(object sender, EventArgs e)
        {
            var dl = (LinkButton)sender;
            var gvr = (GridViewRow)dl.NamingContainer;
            var surgeryList = (List<SurgeryBillDtl>)ViewState["Surgery"];

            //var rowItem = diagnosisList.ElementAt(gvr.RowIndex);

            surgeryList.RemoveAt(gvr.RowIndex);
            surgeryBillGridView.DataSource = surgeryList;
            ViewState["Surgery"] = surgeryList;
            surgeryBillGridView.DataBind();

            CalclutionRigntsidePaymentSystem(surgeryList);
        }


        protected void discountByTotalTextBox_TextChanged(object sender, EventArgs e)
        {
            var surgeryList = (List<SurgeryBillDtl>)ViewState["Surgery"];
            CalclutionRigntsidePaymentSystem(surgeryList);
        }

        protected void vatTextBox_TextChanged(object sender, EventArgs e)
        {
            var surgeryList = (List<SurgeryBillDtl>)ViewState["Surgery"];
            CalclutionRigntsidePaymentSystem(surgeryList);
        }

        protected void saveAssignButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (idHiddenField.Value == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Search a Patient First Then Pay !!');", true);
                    patientIdTextBox.Focus();
                }
                else
                {
                    var surgeryDtlList = (List<SurgeryBillDtl>)ViewState["Surgery"];

                    // for sefe calclution
                    CalclutionRigntsidePaymentSystem(surgeryDtlList);

                    var surgeryMst = new SurgeryBillMst();
                    surgeryMst.PatientId = idHiddenField.Value;
                    surgeryMst.PatientType = "IP";
                    surgeryMst.BillNo = _assignsurgeryManager.GetAutoBillNumber();
                    surgeryMst.SpecialDiscount = Convert.ToDecimal(discountByTotalTextBox.Text);
                    surgeryMst.Vat = Convert.ToDecimal(vatTextBox.Text);
                    surgeryMst.TotalPayableAmount = Convert.ToDecimal(payAbleAmountTextBox.Text);
                    surgeryMst.EntryDate = DateTime.Now;

                    bool isBillNoExist = _assignsurgeryManager.BillNoUniqueCheck(surgeryMst.BillNo);
                    if (isBillNoExist)
                    {
                        surgeryMst.BillNo = _assignsurgeryManager.GetAutoBillNumber();
                    }
                    else
                    {
                        ViewState["DiagnosisBillNo"] = surgeryMst.BillNo;
                        // Save DiagnosisMst and Details
                        int rowAffected = _assignsurgeryManager.Save(surgeryMst, surgeryDtlList);
                        if (rowAffected > 0)
                        {

                            if (payTextBox.Text != "" || payTextBox.Text != "0")
                            {
                                // Payment System
                                var moneyReceiveMst = new HospitalModels.Library.Billings.IpMoneyReceiveMst
                                {
                                    PatientId = Convert.ToInt32(idHiddenField.Value),
                                    PatientType = "IP",
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
                                        var moneyReceiveDtl = new IpMoneyReceiveDtl()
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
                                        var moneyReceiveDtl = new IpMoneyReceiveDtl()
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

                            var patientId = patientIdTextBox.Text;

                            //// report
                            GetReport();

                            RefressSave();

                            ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Diagnosis Assign Successfull');", true);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "ale",
                                                "alert('Surgery Assign Fail');", true);
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


            surgeryTypeDropDownList.DataSource = _surgeryManager.GetAllSurgeryList();
           
            surgeryTypeDropDownList.DataTextField = "Name";
            surgeryTypeDropDownList.DataValueField = "Id";
            surgeryTypeDropDownList.DataBind();
            surgeryTypeDropDownList.Items.Insert(0, "");
            surgeryTypeDropDownList.SelectedIndex = -1;


            surgeryBillGridView.DataSource = null;
            surgeryBillGridView.DataBind();
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

        protected void resetButton_Click(object sender, EventArgs e)
        {
            var pageName = System.IO.Path.GetFileName(Request.Url.ToString());
            Response.Redirect(pageName);
        }

        private void GetReport()
        {
            var surgeryDtlList = (List<SurgeryBillDtl>)ViewState["Surgery"];

            // total net Price
            decimal netTotalPrice = surgeryDtlList.Sum(surgeryBillDtl => surgeryBillDtl.PayableAmount);

            // consultent fee

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
                SpecialDiscount = totalDiscount.ToString("N2"),
                Vat = vat.ToString() + "%",
                TotalPayableAmount = totalPayableAmount.ToString("N2"),
                NowReceive = payAmount.ToString("N2"),
                TotalDue = totalDue.ToString("N2")
            };




            var outdoorPatient = new OutdoorPatient();
            outdoorPatient.Id = Convert.ToInt32(idHiddenField.Value);
            outdoorPatient.PatientId = patientIdTextBox.Text;
            outdoorPatient.Name = patientNameTextBox.Text;
            outdoorPatient.PhoneNo = phoneNoTextBox.Text;
            var outdoorPatientInfo = _indoorPatientManager.GetPatientByIdUsingReport(Convert.ToInt32(idHiddenField.Value));
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
                SurgeryBillDtls = surgeryDtlList,
                OpDiagnosisMoneyReportModel = diagnosisAssignModeyReport
            };

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition",
                "attachment; filename=Surgery " + reportModel.OutdoorPatient.PatientId + ".pdf");
            var document = new Document(PageSize.A4, 40f, 30f, 30f, 50f);
            var writer = PdfWriter.GetInstance(document, Response.OutputStream);
            document.Open();

            var assignReport = new Report.IpReport.IpDiagnosisAssignReport();
            assignReport.GetSurgeryReport(document, writer, reportModel);

            document.Close();
            Response.Flush();
            Response.End();
        }
        private void GetReport1()
        {

            var surgeryDtlList = (List<SurgeryBillDtl>)ViewState["Surgery"];

            // total net Price
            decimal netTotalPrice = surgeryDtlList.Sum(surgeryBillDtl => surgeryBillDtl.PayableAmount);

            // consultent fee

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
                TotalNetPrice = netTotalPrice.ToString("N2"),
                SpecialDiscount = totalDiscount.ToString("N2"),
                Vat = vat.ToString() + "%",
                TotalPayableAmount = totalPayableAmount.ToString("N2"),
                NowReceive = payAmount.ToString("N2"),
                TotalDue = totalDue.ToString("N2")
            };

            var outdoorPatient = new OutdoorPatient()
            {
                Id = Convert.ToInt32(idHiddenField.Value),
                PatientId = patientIdTextBox.Text,
                Name = patientNameTextBox.Text,
                PhoneNo = phoneNoTextBox.Text,
            };

            var reportModel = new OpDiagnosisAssignReportModel
            {
                OutdoorPatient = outdoorPatient,
                SurgeryBillDtls = surgeryDtlList,
                OpDiagnosisMoneyReportModel = diagnosisAssignModeyReport
            };

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition",
                "attachment; filename=Surgery " + reportModel.OutdoorPatient.PatientId + ".pdf");
            var document = new Document(PageSize.A4, 40f, 30f, 30f, 50f);
            var writer = PdfWriter.GetInstance(document, Response.OutputStream);
            document.Open();

            var assignReport = new Report.IpReport.IpDiagnosisAssignReport();
            assignReport.GetSurgeryReport(document, writer, reportModel);

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

                var surgeryList = (List<SurgeryBillDtl>)ViewState["Surgery"];
                CalclutionRigntsidePaymentSystem(surgeryList);
            }
            catch
            {

            }


        }

        protected void patientIdTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtEducation = IpAssignDiagnosisManager.Input(patientIdTextBox.Text);
                if (dtEducation != null)
                {
                    patientIdTextBox.Text = dtEducation.Rows[0]["PatientId"].ToString();
                }
            }
            catch
            {

            }
        }

    }
}