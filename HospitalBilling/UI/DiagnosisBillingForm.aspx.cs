using HospitalBilling.BLL;
using HospitalBilling.BLL.MoneyReceives;
using HospitalBilling.Models;
using HospitalBilling.Models.MoneyReceives;
using HospitalBilling.Report;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using DiagnosisBillDtl = HospitalBilling.Models.DiagnosisBills.DiagnosisBillDtl;
using DiagnosisBillManager = HospitalBilling.BLL.DiagnosisBills.DiagnosisBillManager;
using DiagnosisBillMst = HospitalBilling.Models.DiagnosisBills.DiagnosisBillMst;

namespace HospitalBilling.UI
{
    public partial class DiagnosisBillingForm : System.Web.UI.Page
    {
        private readonly MoneyReceiveManager _moneyReceiveManager;
        private readonly DiagnosisBillManager _diagnosisBillManager;

        public DiagnosisBillingForm()
        {
            _diagnosisBillManager = new DiagnosisBillManager();
            _moneyReceiveManager = new MoneyReceiveManager();
            
        }
        private readonly DiagnosisManager _diagnosisManager = new DiagnosisManager();
        private readonly DiagnosisTypeManager _diagnosisTypeManager = new DiagnosisTypeManager();
        private readonly OutdoorPatientManager _outdoorPatientManager = new OutdoorPatientManager();
        private readonly IndoorPatientManager _indoorPatientManager = new IndoorPatientManager();

        private readonly HospitalDepartmentManager _hospitalDepartmentManager = new HospitalDepartmentManager();
        private readonly DoctorManager _doctorManager = new DoctorManager();
        private readonly ReferenceByManager _referenceByManager = new ReferenceByManager();

        // report
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUserId"] != null && Session["LoginUserRole"] != null)
            {
                if (!IsPostBack)
                {
                    PaymentSystemVisibleFalse();

                    RefressAssign();

                    entryDateTextBox.Text = DateTime.Now.ToString("dd-MM-yyyy");
                    deliveryDateTextBox.Text = DateTime.Now.ToString("dd-MM-yyyy");

                    diagnosisTypeDropDownList.DataSource = _diagnosisTypeManager.GetAllDiagnosisTypes();
                    diagnosisTypeDropDownList.DataTextField = "Name";
                    diagnosisTypeDropDownList.DataValueField = "Id";
                    diagnosisTypeDropDownList.DataBind();
                    diagnosisTypeDropDownList.Items.Insert(0, "");
                    diagnosisTypeDropDownList.SelectedIndex = -1;

                    BillNoTextBox.Text = _diagnosisBillManager.GetAutoBillNumber();


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

                    searchTextBox.Text = "OP-";

                    newEntryTextBox.Text = DateTime.Now.ToString("dd-MM-yyyy");
                }
            }
            else
            {
                Response.Redirect("../LoginForm.aspx");
            }
        }

        private void PaymentSystemVisibleFalse()
        {
            paidAmountLabel.Visible = false;
            dueAmountLabel.Visible = false;
            payAmountLabel.Visible = false;

            paidAmountTextBox.Visible = false;
            dueAmountTextBox.Visible = false;
            payAmountTextBox.Visible = false;
            payButton.Visible = false;

            BillNoTextBox.ReadOnly = false;
            discountByTotalTextBox.ReadOnly = false;
            vatTextBox.ReadOnly = false;

            BillNoTextBox.Text = _diagnosisBillManager.GetAutoBillNumber();
            dueAmountTextBox.Text = "0";
            payAmountTextBox.Text = "0";
        }

        private void PaymentSystemVisibleTrue()
        {
            paidAmountLabel.Visible = true;
            dueAmountLabel.Visible = true;
            payAmountLabel.Visible = true;

            paidAmountTextBox.Visible = true;
            dueAmountTextBox.Visible = true;
            payAmountTextBox.Visible = true;
            payButton.Visible = true;

            BillNoTextBox.ReadOnly = true;
            discountByTotalTextBox.ReadOnly = true;
            vatTextBox.ReadOnly = true;
        }

        protected void outdoorRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            idHiddenField.Value =
                nameTextBox.Text = patientIdTextBox.Text = phoneNoTextBox.Text = admitDateTextBox.Text = "";

            searchTextBox.Text = "OP-";

            //paidAmountLabel.Visible = true;
            //dueAmountLabel.Visible = true;

            //paidAmountTextBox.Visible = true;
            //dueAmountTextBox.Visible = true;

            payAmountLabel.Visible = false;
            payAmountTextBox.Visible = false;
            payButton.Visible = false;

            RadioButtonChengeRefress();
        }

        protected void indooorRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            idHiddenField.Value =
                nameTextBox.Text = patientIdTextBox.Text = phoneNoTextBox.Text = admitDateTextBox.Text = "";

            searchTextBox.Text = "IP-";



            paidAmountLabel.Visible = false;
            dueAmountLabel.Visible = false;

            paidAmountTextBox.Visible = false;
            dueAmountTextBox.Visible = false;

            payAmountLabel.Visible = true;
            payAmountTextBox.Visible = true;
            payButton.Visible = true;

            RadioButtonChengeRefress();
        }

        private void RadioButtonChengeRefress()
        {
            totalAmountTextBox.Text =
                discountByTotalTextBox.Text =
                    vatTextBox.Text =
                        payAbleAmountTextBox.Text =
                            paidAmountTextBox.Text = dueAmountTextBox.Text = payAmountTextBox.Text = "0";

            billGridView.DataSource = null;
            billGridView.DataBind();

            diagnosisBillGridView.DataSource = null;
            diagnosisBillGridView.DataBind();
        }


        protected void searchLinkButton_Click(object sender, EventArgs e)
        {
            if (searchTextBox.Text == "" || searchTextBox.Text == "OP-" || searchTextBox.Text == "IP-")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Pateint Id!!');", true);
                searchTextBox.Focus();
            }
            else
            {
                string patientType;
                if (outdoorRadioButton.Checked)
                {
                    string serchInput = searchTextBox.Text;
                    var patient = _outdoorPatientManager.GetPatientByPatientIdNamePhoneNo(serchInput);
                    if (patient != null)
                    {
                        idHiddenField.Value = patient.Id.ToString();
                        patientIdTextBox.Text = patient.PatientId;
                        nameTextBox.Text = patient.Name;
                        phoneNoTextBox.Text = patient.PhoneNo;
                        admitDateTextBox.Text = patient.EntryDate.ToString("dd-MM-yyyy");//dd/mm/yyyy
                        patientType = "OP";

                        var getBillList = _diagnosisBillManager.OpAllDiagnosisBillHistory(patient.Id, patientType);
                        if (getBillList != null)
                        {
                            billGridView.DataSource = getBillList;
                            billGridView.DataBind();
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Can not Fiend Pateint by This Patient Id!!');", true);
                        searchTextBox.Focus();
                    }

                }
                else
                {
                    string seachInput = searchTextBox.Text;
                    var patient = _indoorPatientManager.GetPatientByPatientIdNamePhoneNo(seachInput);
                    if (patient != null)
                    {
                        idHiddenField.Value = patient.Id.ToString();
                        patientIdTextBox.Text = patient.PatientId;
                        nameTextBox.Text = patient.Name;
                        phoneNoTextBox.Text = patient.PhoneNo;
                        admitDateTextBox.Text = patient.EntryDate.ToString("dd-MM-yyyy");//dd/mm/yyyy
                        patientType = "IP";

                        //var getBillList = _diagnosisBillManager.GetDueBillListByPatientId(patient.Id, patientType);
                        //if (getBillList != null)
                        //{
                        //    billGridView.DataSource = getBillList;
                        //    billGridView.DataBind();
                        //}
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Can not Fiend Pateint by This Patient Id!!');", true);
                        searchTextBox.Focus();
                    }
                }
            }

        }

        // ******************************************Stat Assign*****************************************************
        protected void diagnosisTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void diagnosisDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(diagnosisTypeDropDownList.SelectedValue))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select Valid Diagnosis !!');", true);
                diagnosisDropDownList.Focus();

            }
            else
            {
                int diagnosisId = Convert.ToInt32(diagnosisDropDownList.SelectedValue);
                var dignosis = _diagnosisManager.GetDiagnosesById(diagnosisId);
                if (dignosis != null)
                {
                    priceTextBox.Text = "";
                    priceTextBox.Text = dignosis.TotalFee.ToString();
                    totalPriceTextBox.Text = dignosis.TotalFee.ToString();
                }
            }
        }

        protected void discountByTestTextBox_TextChanged(object sender, EventArgs e)
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

        protected void assignButton_Click(object sender, EventArgs e)
        {
            if (idHiddenField.Value == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Search a Patient First Then Assign !!');", true);
                searchTextBox.Focus();
            }
            else if (BillNoTextBox.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Bill NO Is Empty. Please Use Auto Bill No !!');", true);
                BillNoTextBox.Text = _diagnosisBillManager.GetAutoBillNumber();
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
                aDiagnosisBillDtl.BillNo = BillNoTextBox.Text;
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


                if (aDiagnosisBillDtl.Price - aDiagnosisBillDtl.Discount == aDiagnosisBillDtl.PayableAmount)
                {
                    var selectedDiagnosisList = _diagnosisBillManager.GetAllSelectedDiagnosis(aDiagnosisBillDtl, ViewState["Diagnosis"]);
                    ViewState["Diagnosis"] = selectedDiagnosisList;
                    diagnosisBillGridView.DataSource = selectedDiagnosisList;
                    diagnosisBillGridView.DataBind();

                    // dignosis price
                    decimal price = Convert.ToDecimal(totalPriceTextBox.Text);

                    // billing price
                    decimal totalPrice = Convert.ToDecimal(totalAmountTextBox.Text);
                    totalPrice = totalPrice + price;
                    totalAmountTextBox.Text = totalPrice.ToString();

                    payAbleAmountTextBox.Text = totalPrice.ToString();

                    decimal payAbleAmount = Convert.ToDecimal(payAbleAmountTextBox.Text);
                    decimal due = payAbleAmount;
                    dueAmountTextBox.Text = due.ToString();
                    RefressDiagnosis();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Totol Price and Discount Price Not Match!!');", true);
                    discountByTestTextBox.Focus();
                }

            }

        }

        private void RefressAssign()
        {
            ViewState["Diagnosis"] = null;
            var diagnosisBillDttlList = new List<Models.DiagnosisBills.DiagnosisBillDtl>();
            ViewState["Diagnosis"] = diagnosisBillDttlList;
        }


        protected void EditLinkButton_Click(object sender, EventArgs e)
        {

            var dl = (LinkButton)sender;
            var gvr = (GridViewRow)dl.NamingContainer;
            var diagnosisList = (List<Models.DiagnosisBills.DiagnosisBillDtl>)ViewState["Diagnosis"];

            var rowItem = diagnosisList.ElementAt(gvr.RowIndex);

            decimal price = Convert.ToDecimal(rowItem.PayableAmount);
            decimal totalPrice = Convert.ToDecimal(totalAmountTextBox.Text);
            decimal duiAmount = Convert.ToDecimal(dueAmountTextBox.Text);
            totalPrice = totalPrice - price;
            duiAmount = duiAmount - price;
            totalAmountTextBox.Text = totalPrice.ToString();
            payAbleAmountTextBox.Text = totalPrice.ToString();
            dueAmountTextBox.Text = duiAmount.ToString();

            diagnosisList.RemoveAt(gvr.RowIndex);
            diagnosisBillGridView.DataSource = diagnosisList;
            ViewState["Diagnosis"] = diagnosisList;
            diagnosisBillGridView.DataBind();
        }


        // *****************************************End Assign ************************************************
        protected void saveAssignButton_Click(object sender, EventArgs e)
        {
            if (idHiddenField.Value == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Search a Patient First Then Pay !!');", true);
                searchTextBox.Focus();
            }
            else if (BillNoTextBox.Text == "")
            {

                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Bill No Is Empty. Please Insert Bill No !!');", true);
                BillNoTextBox.Focus();
            }
            else
            {

                // OP = Outdoor Patient and IP = Indoor Patient
                string patientType = outdoorRadioButton.Checked ? "OP" : "IP";

                var diagnosisMst = new DiagnosisBillMst();
                diagnosisMst.PatientId = Convert.ToInt32(idHiddenField.Value);
                diagnosisMst.PatientType = patientType;
                diagnosisMst.BillNo = BillNoTextBox.Text;
                diagnosisMst.SpecialDiscount = Convert.ToDecimal(discountByTotalTextBox.Text);
                diagnosisMst.Vat = Convert.ToDecimal(vatTextBox.Text);
                //diagnosisMst.TotalPayableAmount=
                diagnosisMst.EntryDate = DateTime.Now;

                // Diagnosis Bill Dtl
                string billNo = BillNoTextBox.Text;
                bool isBillNoExist = _diagnosisBillManager.BillNoIsExist(billNo);
                if (isBillNoExist)
                {
                    billNo = _diagnosisBillManager.GetAutoBillNumber();
                    var diagnosisDtlList1 = (List<DiagnosisBillDtl>)ViewState["Diagnosis"];

                    var newDiagnosisBillList = diagnosisDtlList1.Select(diagnosisBillDtl => new DiagnosisBillDtl
                    {
                        BillNo = billNo,
                        DiagnosisTypeId = diagnosisBillDtl.DiagnosisTypeId,
                        DiagnosisTypeName = diagnosisBillDtl.DiagnosisTypeName,
                        DiagnosisId = diagnosisBillDtl.DiagnosisId,
                        DiagnosisName = diagnosisBillDtl.DiagnosisName,
                        Price = diagnosisBillDtl.Price
                    }).ToList();
                    ViewState["Diagnosis"] = newDiagnosisBillList;
                }
                var diagnosisDtlList = (List<DiagnosisBillDtl>)ViewState["Diagnosis"];

                // Payable Amount Calclutor
                decimal totalAmount = diagnosisDtlList.Sum(diagnosisBillDtl => diagnosisBillDtl.Price);
                totalAmount = totalAmount - diagnosisMst.SpecialDiscount;
                diagnosisMst.TotalPayableAmount = totalAmount + (totalAmount * diagnosisMst.Vat / 100);

                // Save DiagnosisMst and Details
                int rowAffected = _diagnosisBillManager.Save(diagnosisMst, diagnosisDtlList);
                if (rowAffected > 0)
                {
                    diagnosisBillMstIdHiddenField.Value = rowAffected.ToString();
                    if (patientType == "IP")
                    {
                        var billNoforReport = BillNoTextBox.Text;
                        var patientId = Convert.ToInt32(idHiddenField.Value);

                    }

                    RefressSave();
                }

            }
        }

        protected void payButton_Click(object sender, EventArgs e)
        {
            if (idHiddenField.Value == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Search a Patient First Then Pay !!');", true);
                searchTextBox.Focus();
            }
            else if (diagnosisBillMstIdHiddenField.Value == "" || diagnosisBillMstIdHiddenField.Value == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select A Bill First !!');", true);
            }
            else
            {
                // OP = Outdoor Patient and IP = Indoor Patient
                string patientType = outdoorRadioButton.Checked ? "OP" : "IP";

                int pay = Convert.ToInt32(payAmountTextBox.Text);
                if (pay > 0)
                {
                    if (patientType == "OP") // Outdoor patient payment
                    {
                        var opMoneyReceiveMst = new OpMoneyReceiveMst()
                        {
                            PatientId = Convert.ToInt32(idHiddenField.Value),
                            PatientType = patientType,
                            DiagnosisBillMstId = Convert.ToInt32(diagnosisBillMstIdHiddenField.Value),  ///////
                            PayAmount = pay
                        };

                        var opMoneyReceiveDtl = new OpMoneyReceiveDtl()
                        {
                            PayMethode = "Cash",
                            BankName = "",
                            ChequeNo = "",
                            ChequeDate = null,
                            EntryDate = DateTime.Now
                        };

                        int rowAffected = _moneyReceiveManager.OutdoorPatientMoneyReceiveSave(opMoneyReceiveMst,
                             opMoneyReceiveDtl);
                        if (rowAffected > 0)
                        {
                            var billNo = BillNoTextBox.Text;

                        }
                    }
                    else
                    {
                        var ipMoneyReceiveMst = new IpMoneyReceiveMst()
                        {
                            PatientId = Convert.ToInt32(idHiddenField.Value),
                            PatientType = patientType,
                            PaymentType = "Diagnosis",
                            PayAmount = pay,
                            AdvanceAmount = 0,
                            SpecialDiscount = 0,
                            IPVat = 0
                        };

                        var opMoneyReceiveDtl = new OpMoneyReceiveDtl()
                        {
                            PayMethode = "Cash",
                            BankName = "",
                            ChequeNo = "",
                            ChequeDate = null,
                            EntryDate = DateTime.Now
                        };

                        int rowAffected = _moneyReceiveManager.IpMoneyReceiveFormDiagnosisSave(ipMoneyReceiveMst,
                             opMoneyReceiveDtl);
                        if (rowAffected > 0)
                        {


                        }
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('You Enter 0 Tk In Pay Textbox !!');", true);
                    payAmountTextBox.Focus();

                }
            }

        }

        private void RefressSave()
        {
            RefressAssign();
            idHiddenField.Value =
                patientIdTextBox.Text =
                    nameTextBox.Text = phoneNoTextBox.Text = admitDateTextBox.Text = priceTextBox.Text = "";

            totalAmountTextBox.Text =
                discountByTotalTextBox.Text =
                    vatTextBox.Text =
                        payAbleAmountTextBox.Text =
                            paidAmountTextBox.Text = dueAmountTextBox.Text = payAmountTextBox.Text = "0";

            entryDateTextBox.Text = DateTime.Now.ToString("dd-MM-yyyy");

            diagnosisTypeDropDownList.DataSource = _diagnosisTypeManager.GetAllDiagnosisTypes();
            diagnosisTypeDropDownList.DataTextField = "Name";
            diagnosisTypeDropDownList.DataValueField = "Id";
            diagnosisTypeDropDownList.DataBind();
            diagnosisTypeDropDownList.Items.Insert(0, "");
            diagnosisTypeDropDownList.SelectedIndex = -1;

            billGridView.DataSource = null;
            billGridView.DataBind();

            diagnosisBillGridView.DataSource = null;
            diagnosisBillGridView.DataBind();
        }

        private void RefressUpdate()
        {
            idHiddenField.Value =
                patientIdTextBox.Text =
                    nameTextBox.Text = phoneNoTextBox.Text = admitDateTextBox.Text = priceTextBox.Text = "";

            totalAmountTextBox.Text =
                discountByTotalTextBox.Text =
                    vatTextBox.Text =
                        payAbleAmountTextBox.Text =
                            paidAmountTextBox.Text = dueAmountTextBox.Text = payAmountTextBox.Text = "0";
            BillNoTextBox.Text = _diagnosisBillManager.GetAutoBillNumber();

            diagnosisBillGridView.DataSource = null;
            diagnosisBillGridView.DataBind();

            billGridView.DataSource = null;
            billGridView.DataBind();
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

        protected void discountByTotalTextBox_TextChanged(object sender, EventArgs e)
        {
            decimal discount;
            decimal totalFee = Convert.ToDecimal(totalAmountTextBox.Text);

            if (discountByTestTextBox.Text != "")
            {
                discount = Convert.ToDecimal(discountByTotalTextBox.Text);
            }
            else
            {
                discount = 0;
            }

            decimal payAbleFee = totalFee - discount;
            if (payAbleFee > 0)
            {
                payAbleAmountTextBox.Text = payAbleFee.ToString();

                decimal payAbleAmount = Convert.ToDecimal(payAbleAmountTextBox.Text);
                decimal due = payAbleAmount;
                dueAmountTextBox.Text = due.ToString();
            }
        }

        protected void vatTextBox_TextChanged(object sender, EventArgs e)
        {
            decimal vat;
            if (vatTextBox.Text != "")
            {
                vat = Convert.ToDecimal(vatTextBox.Text);
            }
            else
            {
                vat = 0;
            }
            decimal payableAmount = Convert.ToDecimal(payAbleAmountTextBox.Text);
            vat = (payableAmount * vat) / 100;

            decimal totalAmount = payableAmount + vat;
            payAbleAmountTextBox.Text = totalAmount.ToString();
            dueAmountTextBox.Text = totalAmount.ToString();

        }

        protected void billSearchLinkButton_Click(object sender, EventArgs e)
        {
            //if (BillNoTextBox.Text == "")
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Bill  No !!');", true);
            //    BillNoTextBox.Focus();

            //}
            //else
            //{
            //    string billNo = BillNoTextBox.Text;

            //    

            //    }
            //    else
            //    {
            //        ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Can not Fiend This Bill No in Database !!');", true);
            //        BillNoTextBox.Focus();
            //    }
            //}

        }


        // create outdoor Patient
        protected void createLinkButton_Click(object sender, EventArgs e)
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
                OutdoorPatient aOutdoorPatient = new OutdoorPatient();
                aOutdoorPatient.PatientId = newPatientIdTextBox.Text;
                aOutdoorPatient.Name = newPatientNameTextBox.Text;
                aOutdoorPatient.PhoneNo = newPhoneNoTextBox.Text;

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
                aOutdoorPatient.Age = newAgeTextBox.Text;

                string entryDate = newEntryTextBox.Text;
                aOutdoorPatient.EntryDate = DateTime.ParseExact(entryDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                aOutdoorPatient.DepartmentId = Convert.ToInt32(newDepartmentDropDownList.SelectedValue);
                aOutdoorPatient.DoctorId = Convert.ToInt32(newConsultantDropDownList.SelectedValue);
                aOutdoorPatient.ReferenceById = Convert.ToInt32(newReferanceByDropDownList.SelectedValue);

                int rowAffected = _outdoorPatientManager.Save(aOutdoorPatient);
                if (rowAffected > 0)
                {
                    Refress();
                    outdoorPatientPopup.Hide();
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(System.Web.UI.Page), "Script", "HideModalDiv();", true);
                    //messageLabel.CssClass = "alert alert-success";
                    //messageLabel.Text = "<strong>Success!</strong> Save Patient in Database.";
                }
                else
                {
                    messageLabel.CssClass = "alert alert-warning";
                    messageLabel.Text = "<strong>Fail!</strong> Can'not Save Patient in Database.";
                }
            }

        }

        private void Refress()
        {
            patientIdTextBox.Text = nameTextBox.Text = phoneNoTextBox.Text = newAgeTextBox.Text = "";
            maleRadioButton.Checked = true;

            newDepartmentDropDownList.DataSource = _hospitalDepartmentManager.GetAllDepartment();
            newDepartmentDropDownList.DataTextField = "Name";
            newDepartmentDropDownList.DataValueField = "Id";
            newDepartmentDropDownList.DataBind();
            newDepartmentDropDownList.Items.Insert(0, "");
            newDepartmentDropDownList.SelectedIndex = -1;

            newConsultantDropDownList.SelectedIndex = -1;

            newReferanceByDropDownList.DataSource = _referenceByManager.GetAllReferenceByList();
            newReferanceByDropDownList.DataTextField = "Name";
            newReferanceByDropDownList.DataValueField = "Id";
            newReferanceByDropDownList.DataBind();
            newReferanceByDropDownList.Items.Insert(0, "Select Reference");
            newReferanceByDropDownList.SelectedIndex = -1;
        }

        protected void billGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id = ((Label)billGridView.SelectedRow.FindControl("idLabel")).Text;

            var diagnosisBillMst = _diagnosisBillManager.GetDiagnosisBillMstById(Convert.ToInt32(id));
            if (diagnosisBillMst != null)
            {
                diagnosisBillMstIdHiddenField.Value = diagnosisBillMst.Id.ToString();
                BillNoTextBox.Text = diagnosisBillMst.BillNo;
                totalAmountTextBox.Text = diagnosisBillMst.TotalPayableAmount.ToString();
                discountByTotalTextBox.Text = diagnosisBillMst.SpecialDiscount.ToString();
                vatTextBox.Text = diagnosisBillMst.Vat.ToString();
                payAbleAmountTextBox.Text = diagnosisBillMst.TotalPayableAmount.ToString();
                var dto = _diagnosisBillManager.OutdoorPatientDiagnosisBillHistory(diagnosisBillMst.BillNo);
                dueAmountTextBox.Text = dto.DueAmount.ToString();

                PaymentSystemVisibleTrue();

            }

            //var billInfo = _diagnosisBillManager.GetBillInformationById(Convert.ToInt32(id));
            //if (billInfo != null)
            //{
            //    BillNoTextBox.Text = billInfo.BillingId;
            //    totalAmountTextBox.Text = billInfo.TotalNetPrice.ToString();
            //    discountByTotalTextBox.Text = billInfo.Discount.ToString();
            //    vatTextBox.Text = billInfo.Vat.ToString();
            //    payAbleAmountTextBox.Text = billInfo.TotalPayableAmount.ToString();
            //    paidAmountTextBox.Text = billInfo.TotalPayAmount.ToString();
            //    decimal dueAmount = billInfo.TotalPayableAmount - billInfo.TotalPayAmount;
            //    dueAmountTextBox.Text = dueAmount.ToString();
            //}
        }

        protected void createLinkButton_Click1(object sender, EventArgs e)
        {
            string patientId = _outdoorPatientManager.GetAutoPatientId();

            newPatientIdTextBox.Text = patientId;
        }



       


    }
}