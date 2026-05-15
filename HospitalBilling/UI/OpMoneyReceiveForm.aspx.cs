using HospitalBilling.BLL;
using HospitalBilling.Models;
using HospitalBilling.Report.Models;
using HospitalBilling.Report.OpReport;
using HospitalBilling.ViewModels;
using HospitalManager.Library.Billings.OpBilling;
using HospitalModels.Library.Billings;
using HospitalModels.Library.Billings.Op;
using HospitalModels.Library.CommonClass;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

namespace HospitalBilling.UI
{
    public partial class OpMoneyReceiveForm : System.Web.UI.Page
    {
        private readonly OutdoorPatientManager _outdoorPatientManager;
        private readonly OpPaymentHisotyManager _opPaymentHistoryManager;

        private readonly OpMoneyReceiveManager _moneyReceiveManager;

        private readonly OpDiagnosisBillManager _diagnosisBillManager;
    

        public OpMoneyReceiveForm()
        {
            _outdoorPatientManager = new OutdoorPatientManager();
            _opPaymentHistoryManager = new OpPaymentHisotyManager();
            _moneyReceiveManager = new OpMoneyReceiveManager();

            _diagnosisBillManager = new OpDiagnosisBillManager();


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


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUserId"] != null && Session["LoginUserRole"] != null)
            {
                if (!IsPostBack)
                {
                    if (!IsPostBack)
                    {
                        GetDropdownAndGridView();
                        //discountLabel.Visible = false;
                       // specialDiscountTextBox.Visible = false;
                    }
                }
            }
            else
            {
                Response.Redirect("../LoginForm.aspx");
            }

        }

        private void GetDropdownAndGridView()
        {
            paymentTypeDiv.Visible = false;

            paymentTypeDropDownList.DataSource = GetPaymentTypes();
            paymentTypeDropDownList.DataTextField = "Name";
            paymentTypeDropDownList.DataValueField = "Id";
            paymentTypeDropDownList.DataBind();


            billinfoGridView.DataSource = new List<MrParticular>() { new MrParticular() };
            billinfoGridView.DataBind();

            paymentHistoryGridView.DataSource = new List<IpPaymentHistory>() { new IpPaymentHistory() };
            paymentHistoryGridView.DataBind();
        }

        private void Refress()
        {
            patientIdTextBox.ReadOnly = false;

            GetDropdownAndGridView();
            idHiddenField.Value =
                patientIdTextBox.Text =
                    patientNameTextBox.Text =
                        phoneNoTextBox.Text =
                            totalBillTextBox.Text =
                                totalPaidTextBox.Text =
                                    totalDueTextBox.Text =
                                        specialDiscountTextBox.Text =
                                            payAmountTextBox.Text =
                                                bankNameTextBox.Text = chequeTextBox.Text = chequeDateTextBox.Text = "";

            billListGridView.DataSource = null;
            billListGridView.DataBind();

            GetDropdownAndGridView();
        }

        protected void searchLinkButton_Click(object sender, EventArgs e)
        {
            string searchInput = patientIdTextBox.Text;
            if (searchInput == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Pateint Id/ Name/ Phone No!!');", true);
                patientIdTextBox.Focus();
            }
            else
            {
                var patient = _outdoorPatientManager.GetPatientByPatientIdNamePhoneNo(searchInput);
                if (patient == null)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Can not Find Patient by This Pateint Id/ Name/ Phone No!!');", true);
                    patientIdTextBox.Focus();
                }
                else
                {
                    patientIdTextBox.ReadOnly = true;

                    idHiddenField.Value = patient.Id.ToString();
                    patientIdTextBox.Text = patient.PatientId;
                    patientNameTextBox.Text = patient.Name;
                    phoneNoTextBox.Text = patient.PhoneNo;

                    // bill List gridview
                    var opDiagnosisMstList = _diagnosisBillManager.GetDiagnosisBillMstsByPatientIdAndType(patient.Id,
                        "OP");

                    if (opDiagnosisMstList.Count > 0)
                    {
                        var viewModelList = (from diagnosisBillMst in opDiagnosisMstList
                                             let paymentHistory = _opPaymentHistoryManager.GetAllPaymentHistoryByDiagnosisBillMstId(diagnosisBillMst.Id)
                                             let totalPay = paymentHistory.Sum(c => c.PayAmount)
                                             let totalDue = diagnosisBillMst.TotalPayableAmount - totalPay
                                             select new OpDiagnsoisBillListViewModel()
                                             {
                                                 Id = diagnosisBillMst.Id,
                                                 BillNo = diagnosisBillMst.BillNo,
                                                 SpecialDiscount = diagnosisBillMst.SpecialDiscount,
                                                 TotalPayableAmount = diagnosisBillMst.TotalPayableAmount,
                                                 Vat = diagnosisBillMst.Vat,
                                                 TotalPayAmount = totalPay,
                                                 TotalDueAmount = totalDue,
                                                 EntryDate = diagnosisBillMst.EntryDate
                                             }).ToList();

                        billListGridView.DataSource = viewModelList;
                        billListGridView.DataBind();
                    }

                }
            }
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


        // money Receive
        protected void payButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (idHiddenField.Value == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Search Pateint first by Id/ Name/ Phone No!!');", true);
                    patientIdTextBox.ReadOnly = false;
                    patientIdTextBox.Focus();
                }
                else if (payAmountTextBox.Text == "" && specialDiscountTextBox.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Pay some Amount or Give Discount!!');", true);
                    payAmountTextBox.Focus();
                }
                else if (paymentTypeDropDownList.SelectedValue == "" || paymentTypeDropDownList.SelectedValue == "0")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select Payment Methode');", true);

                }
                else
                {


                  

                    var moneyReceiveMst = new OpMoneyReceiveMst();
                    moneyReceiveMst.PatientId = Convert.ToInt32(idHiddenField.Value);
                    moneyReceiveMst.PatientType = "OP";
                    moneyReceiveMst.DiagnosisBillMstId = Convert.ToInt32(diagnosisBillMstIdHiddenField.Value);
                    moneyReceiveMst.PayAmount = payAmountTextBox.Text == "" ? 0 : Convert.ToDecimal(payAmountTextBox.Text);
                    moneyReceiveMst.AdvanceAmount = 0;
                    moneyReceiveMst.SpecialDiscount =
                        specialDiscountTextBox.Text == "" ? 0 : Convert.ToDecimal(specialDiscountTextBox.Text);
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

                            int rowAffected = _moneyReceiveManager.MoneyReceiveDtlSaveByCash(moneyReceiveDtl);
                            if (rowAffected > 0)
                            {

                                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Payment Successfull');", true);
                                patientIdTextBox.ReadOnly = false;
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Payment Success but MoneyReceive Dtl Save Fail');", true);

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
                            int rowAffected = _moneyReceiveManager.MoneyReceiveDtlSaveByBank(moneyReceiveDtl);
                            if (rowAffected > 0)
                            {

                                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Payment Successfull');", true);
                                patientIdTextBox.ReadOnly = false;

                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Payment Success but MoneyReceive Dtl Save Fail');", true);
                                patientIdTextBox.ReadOnly = false;
                            }

                        }
                        Report();
                        Refress();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Payment Fail');", true);

                    }

                }
            }
            catch
            {

            }
        }





        protected void billListGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idLeable = ((Label)billListGridView.SelectedRow.FindControl("idLabel")).Text;
            int id = Convert.ToInt32(idLeable);

            var diagnosisBillMst = _diagnosisBillManager.GetDiagnosisBillMstsById(id);
            if (diagnosisBillMst != null)
            {
                ViewState["BillNo"] = diagnosisBillMst.BillNo;

                diagnosisBillMstIdHiddenField.Value = id.ToString();

                var paymentHistory =
                    _opPaymentHistoryManager.GetAllPaymentHistoryByDiagnosisBillMstId(diagnosisBillMst.Id);

                totalBillTextBox.Text = diagnosisBillMst.TotalPayableAmount.ToString();
                //totalDiscountTextBox.Text = diagnosisBillMst.SpecialDiscount.ToString();

                var totalPaid = paymentHistory.Sum(c => c.PayAmount);
                totalPaidTextBox.Text = totalPaid.ToString();

                var SpecialDiscount = paymentHistory.Sum(c => c.SpecialDiscount);
                totalDiscountTextBox.Text = SpecialDiscount.ToString();

                totalDueTextBox.Text = (diagnosisBillMst.TotalPayableAmount - totalPaid - SpecialDiscount).ToString();

                var list = new List<MrParticular>()
                {
                    new MrParticular()
                    {
                        Particular = diagnosisBillMst.BillNo,
                        Amount =  diagnosisBillMst.TotalPayableAmount
                    }
                };


               // billinfoGridView.DataSource = list;

                billinfoGridView.DataSource = _opPaymentHistoryManager.GetAllDiagonisHistoryByDiagnosisBillMstId(diagnosisBillMst.Id); ;
                billinfoGridView.DataBind();


                
                try
                {
                    if (paymentHistory.Count > 0)
                    {
                        // payment history
                        var listPaymentHistory = new List<IpPaymentHistory>();
                        foreach (var opMoneyReceiveMst in paymentHistory)
                        {
                            var detail = _opPaymentHistoryManager.GetOpMoneyReceiveDtlsByMoneyReceiveMstId(opMoneyReceiveMst.Id);

                            var opPaymentHistory = new IpPaymentHistory()
                            {
                                PayMethode = detail.PayMethode,
                                Amount = opMoneyReceiveMst.PayAmount,
                                PayDate = detail.EntryDate.ToString("dd-MM-yyyy"),
                            };
                            listPaymentHistory.Add(opPaymentHistory);

                        }

                        ViewState["PaymentHistory"] = listPaymentHistory;
                        paymentHistoryGridView.DataSource = listPaymentHistory;
                        paymentHistoryGridView.DataBind();
                    }
                }
                catch
                {

                }

            }
        }

        protected void billReportButton_Click(object sender, EventArgs e)
        {
            if (idHiddenField.Value == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Find Paient First');", true);
            }
            else if (ViewState["BillNo"] == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select Bill No');", true);
            }
            else
            {
                var outdoorPatient = new OutdoorPatient
                {
                    PatientId = patientIdTextBox.Text,
                    Name = patientNameTextBox.Text,
                    PhoneNo = phoneNoTextBox.Text
                };

                var billInfo = new OpBillInfoReportModel()
                {
                    BillNo = ViewState["BillNo"].ToString(),
                    TotalPayableAmount = totalBillTextBox.Text,
                    TotalPay = totalPaidTextBox.Text,
                    TotalDue = totalDueTextBox.Text,
                   
                };

                var reportModel = new OpPaymentHistoryReceiveReportModel
                {
                    OutdoorPatient = outdoorPatient,
                    OpBillInfoReport = billInfo,

                };

                if (ViewState["PaymentHistory"] != null)
                {
                    reportModel.PaymentHistories = (List<IpPaymentHistory>)ViewState["PaymentHistory"];
                }
                //var filePath = Server.MapPath("~/image/logo2.png");
                //var fileBytes = File.ReadAllBytes(filePath);

                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition",
                    "attachment; filename=MoneyReceive History " + reportModel.OutdoorPatient.PatientId + ".pdf");
                var document = new Document(PageSize.A4, 40f, 30f, 30f, 50f);
                var writer = PdfWriter.GetInstance(document, Response.OutputStream);
                document.Open();

                var assignReport = new OpPaymentHistoryReport();
                assignReport.GetReport(document, writer, reportModel);
               // assignReport.GetReport(document, writer, reportModel, fileBytes);

                document.Close();
                Response.Flush();
                Response.End();
               
            }
        }

        protected void reloadButton_Click(object sender, EventArgs e)
        {
            var pageName = System.IO.Path.GetFileName(Request.Url.ToString());
            Response.Redirect(pageName);
        }

        private void Report()
        {


            var idLeable = ((Label)billListGridView.SelectedRow.FindControl("idLabel")).Text;
            int id = Convert.ToInt32(idLeable);
            var billList = _opPaymentHistoryManager.GetAllDiagonisHistoryByDiagnosisBillMstId(id); 

            var patient = new OutdoorPatient()
            {
                PatientId = patientIdTextBox.Text,
                Name = patientNameTextBox.Text,
                PhoneNo = phoneNoTextBox.Text
            };
            const string patientType = "Outdoor Patient";

            // Bill
            var totalBill = Convert.ToDecimal(totalBillTextBox.Text);
            var priviousPaid = Convert.ToDecimal(totalPaidTextBox.Text);

            decimal nowPay = 0;
            if (payAmountTextBox.Text != "")
            {
                nowPay = Convert.ToDecimal(payAmountTextBox.Text);

            }
            decimal specialDiscount = 0;
            if (specialDiscountTextBox.Text != "")
            {
                specialDiscount = Convert.ToDecimal(specialDiscountTextBox.Text);

            }

            var due = totalBill - (priviousPaid + nowPay) - specialDiscount;

            var opBillInfo = new OpBillInfoReportModel()
            {
                BillNo = ViewState["BillNo"].ToString(),
                TotalPayableAmount = totalBillTextBox.Text,
                TotalPay = totalPaidTextBox.Text,
                NowPay = payAmountTextBox.Text,
                SpecialDiscount = specialDiscount.ToString(),
                TotalDue = due.ToString()
            };
            var reportModel = new MoneyReceiveReportModel
            {
                Patient = patient,
                OpBillInfoReportModel = opBillInfo
            };

            //var filePath = Server.MapPath("~/image/logo2.png");
            //var fileBytes = File.ReadAllBytes(filePath);

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition",
                "attachment; filename=MoneyReceive History " + reportModel.Patient.PatientId + ".pdf");
            var document = new Document(PageSize.A4, 40f, 30f, 30f, 50f);
            var writer = PdfWriter.GetInstance(document, Response.OutputStream);
            document.Open();

            var assignReport = new OpMoneyReceiveReport();
            assignReport.GetReport(document, writer, reportModel, patientType,billList);
            //assignReport.GetReport(document, writer, reportModel, patientType,fileBytes);

            document.Close();
            Response.Flush();
            Response.End();
            ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Payment Success');", true);
            Refress();
        }

        protected void patientIdTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable PatientId = _moneyReceiveManager.Input(patientIdTextBox.Text);
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