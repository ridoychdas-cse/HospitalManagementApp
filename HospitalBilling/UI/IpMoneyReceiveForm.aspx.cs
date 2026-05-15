using HospitalBilling.BLL;
using HospitalBilling.BLL.SurgerysBills;
using HospitalBilling.Report.Billing;
using HospitalBilling.Report.Models;
using HospitalManager.Library.Billings;
using HospitalModels.Library.Billings;
using HospitalModels.Library.CommonClass;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HospitalBilling.UI
{
    public partial class IpMoneyReceiveForm : System.Web.UI.Page
    {
        private readonly IndoorPatientManager _indoorPatientManager;
        private readonly IpDiagnosisBillManager _ipDiagnosisBillManager;
        private readonly IpBedBillManager _ipBedBillManager;
        private readonly IpPaymentHistoryManager _ipPaymentHistoryManager;
        private readonly IpMoneyReceiveManager _moneyReceiveManager;
        private readonly IpOtherBillManager _ipOtherBillManager;

        private readonly IpMoneyReceiveReport _ipMoneyReceiveReport;

        IpAssignSurgeryManager _ipAssignSurgeryManager = new IpAssignSurgeryManager();
        public IpMoneyReceiveForm()
        {
            _indoorPatientManager = new IndoorPatientManager();
            _ipDiagnosisBillManager = new IpDiagnosisBillManager();
            _ipBedBillManager = new IpBedBillManager();
            _ipPaymentHistoryManager = new IpPaymentHistoryManager();
            _moneyReceiveManager = new IpMoneyReceiveManager();
            _ipOtherBillManager = new IpOtherBillManager();

            _ipMoneyReceiveReport = new IpMoneyReceiveReport();
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
            if (!IsPostBack)
            {
                GetDropdownAndGridView();
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
        }

        protected void searchLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                string searchInput = patientIdTextBox.Text;
                if (searchInput == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Pateint Id/ Name/ Phone No!!');", true);
                    patientIdTextBox.Focus();
                }
                else
                {
                    var patient = _indoorPatientManager.GetPatientByPatientIdNamePhoneNo(searchInput);
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



                        // Calclution bill
                        var billList = new List<MrParticular>();
                        decimal totalBill = 0;

                        // diagnosisBill
                        var ipDiagnosisBill =
                            _ipDiagnosisBillManager.GetTotalDiagnosisBillByPatientIdAndPatientType(patient.Id, "IP");
                        if (ipDiagnosisBill != null)
                        {
                            ipDiagnosisBill.Serial = 1;
                            billList.Add(ipDiagnosisBill);
                            totalBill += ipDiagnosisBill.Amount;
                        }


                        // bedbill
                        var ipBedBill = _ipBedBillManager.GetTotalBedBill(patient.Id);
                        if (ipBedBill > 0)
                        {
                            var mrParticular = new MrParticular()
                            {
                                Particular = "Bed Bill",
                                Amount = ipBedBill,
                                Serial = 2
                            };
                            billList.Add(mrParticular);
                            totalBill += ipBedBill;
                        }

                        //
                        // Surgery Bill
                        var ipSurgeryBill = _ipAssignSurgeryManager.GetTotalSurgeryBill(patient.Id);

                        if (ipSurgeryBill != null)
                        {
                            ipSurgeryBill.Serial = 3;
                            billList.Add(ipSurgeryBill);
                            totalBill += ipSurgeryBill.Amount;
                        }

                        // Other Bill
                        var otherBillList = _ipOtherBillManager.GetAllOtherBillsByPatientIdAndType(patient.Id, "IP");
                        var totalOtherBill = _ipOtherBillManager.GetTotalOtherBill(patient.Id, "IP");

                        if (totalOtherBill > 0)
                        {
                            int serial = 4;

                            var demo = otherBillList.GroupBy(c => c.OtherBillId)
                                        .Select(c => new OtherBill()
                                        {
                                            OtherBillName = c.First().OtherBillName,
                                            Price = c.Sum(p => p.Price)
                                        });


                            billList.AddRange(demo.Select(otherBill => new MrParticular()
                            {
                                Particular = otherBill.OtherBillName,
                                Amount = otherBill.Price,
                                Serial = serial++
                            }));
                            totalBill += totalOtherBill;
                        }


                        // bill info gridView
                        billinfoGridView.DataSource = billList;
                        billinfoGridView.DataBind();


                        // totalDiscount
                        var totalDiscount = _ipPaymentHistoryManager.TotalSpecialDiscountByPatientIdAndType(patient.Id, "IP");

                        // totalPaid
                        var totalPaid = _ipPaymentHistoryManager.TotalPaymentByPatientIdandPatientType(patient.Id, "IP");

                        // totalDue
                        var totalDue = (totalBill - totalDiscount) - totalPaid;

                        // Texbox value
                        totalBillTextBox.Text = totalBill.ToString();
                        totalDiscountTextBox.Text = totalDiscount.ToString();
                        totalPaidTextBox.Text = totalPaid.ToString();
                        totalDueTextBox.Text = totalDue.ToString();






                        // Get Payment history
                        var historyList = new List<IpPaymentHistory>();
                        var paymentMstList = _ipPaymentHistoryManager.GetAllIpMoneyReceiveMstsByPatientIdAndType(
                            patient.Id, "IP");

                        if (paymentMstList != null)
                        {
                            foreach (var ipMoneyReceiveMst in paymentMstList)
                            {
                                var paymentDtl = _ipPaymentHistoryManager.GetIpMoneyReceiveDtlsByMoneyReceiveMstId(ipMoneyReceiveMst.Id);

                                if (paymentDtl != null)
                                {
                                    var paymentHistory = new IpPaymentHistory();
                                    if (ipMoneyReceiveMst.PayAmount > 0)
                                    {
                                        paymentHistory.PayType = "Pay";
                                        paymentHistory.Amount = ipMoneyReceiveMst.PayAmount;
                                    }
                                    else
                                    {
                                        paymentHistory.PayType = "Advance";
                                        paymentHistory.Amount = ipMoneyReceiveMst.AdvanceAmount;
                                    }

                                    paymentHistory.PayMethode = paymentDtl.PayMethode;
                                    paymentHistory.PayDate = paymentDtl.EntryDate.ToString("dd/MM/yyyy");


                                    historyList.Add(paymentHistory);
                                }
                            }

                        }


                        if (historyList.Count > 0)
                        {
                            paymentHistoryGridView.DataSource = historyList;
                            paymentHistoryGridView.DataBind();
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n" + ex, true);
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

                // Money Receive
                var moneyReceiveMst = new HospitalModels.Library.Billings.IpMoneyReceiveMst
                {
                    PatientId = Convert.ToInt32(idHiddenField.Value),
                    PatientType = "IP",
                    PayAmount = payAmountTextBox.Text == "" ? 0 : Convert.ToDecimal(payAmountTextBox.Text),
                    AdvanceAmount = 0,
                    SpecialDiscount =
                        specialDiscountTextBox.Text == "" ? 0 : Convert.ToDecimal(specialDiscountTextBox.Text)
                };


                // Calclution bill
                var billList = new List<MrParticular>();
                decimal totalBill = 0;

                // diagnosisBill
                var ipDiagnosisBill =
                    _ipDiagnosisBillManager.GetTotalDiagnosisBillByPatientIdAndPatientType(Convert.ToInt32(idHiddenField.Value), "IP");
                if (ipDiagnosisBill != null)
                {
                    ipDiagnosisBill.Serial = 1;
                    billList.Add(ipDiagnosisBill);
                    totalBill += ipDiagnosisBill.Amount;
                }


                // bedbill
                var ipBedBill = _ipBedBillManager.GetTotalBedBill(Convert.ToInt32(idHiddenField.Value));
                if (ipBedBill > 0)
                {
                    var mrParticular = new MrParticular()
                    {
                        Particular = "Bed Bill",
                        Amount = ipBedBill,
                        Serial = 2
                    };
                    billList.Add(mrParticular);
                    totalBill += ipBedBill;
                }

                //
                // Surgery Bill
                var ipSurgeryBill = _ipAssignSurgeryManager.GetTotalSurgeryBill(Convert.ToInt32(idHiddenField.Value));

                if (ipSurgeryBill != null)
                {
                    ipSurgeryBill.Serial = 3;
                    billList.Add(ipSurgeryBill);
                    totalBill += ipSurgeryBill.Amount;
                }

                // Other Bill
                var otherBillList = _ipOtherBillManager.GetAllOtherBillsByPatientIdAndType(Convert.ToInt32(idHiddenField.Value), "IP");
                var totalOtherBill = _ipOtherBillManager.GetTotalOtherBill(Convert.ToInt32(idHiddenField.Value), "IP");

                if (totalOtherBill > 0)
                {
                    int serial = 4;

                    var demo = otherBillList.GroupBy(c => c.OtherBillId)
                                .Select(c => new OtherBill()
                                {
                                    OtherBillName = c.First().OtherBillName,
                                    Price = c.Sum(p => p.Price)
                                });


                    billList.AddRange(demo.Select(otherBill => new MrParticular()
                    {
                        Particular = otherBill.OtherBillName,
                        Amount = otherBill.Price,
                        Serial = serial++
                    }));
                    totalBill += totalOtherBill;
                }
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

                        int rowAffected = _moneyReceiveManager.MoneyReceiveDtlSaveByCash(moneyReceiveDtl);
                        if (rowAffected > 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Payment Successfull');", true);
                            patientIdTextBox.ReadOnly = false;

                            // report value
                            var reportValue = MoneyReceiveReportValue();

                            Refress();

                            if (reportValue != null)
                            {
                                Response.Clear();
                                Response.ContentType = "application/pdf";
                                Response.AddHeader("content-disposition", "attachment; filename=Money Receive " + reportValue.PatientId + ".pdf");
                                Document document = new Document();
                                //document = new Document(PageSize.A4, 40f, 30f, 30f, 50f);
                                document = new Document(PageSize.A6, 30f, 20f, 20f, 40f);
                                PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);
                                document.Open();

                                _ipMoneyReceiveReport.GetBillReport(document, writer, reportValue,billList);

                                document.Close();
                                Response.Flush();
                                Response.End();
                                Refress();
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Payment Success but MoneyReceive Dtl Save Fail');", true);

                            patientIdTextBox.ReadOnly = false;
                            Refress();
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
                        int rowAffected = _moneyReceiveManager.MoneyReceiveDtlSaveByBank(moneyReceiveDtl);
                        if (rowAffected > 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Payment Successfull');", true);
                            patientIdTextBox.ReadOnly = false;

                            // report value
                            var reportValue = MoneyReceiveReportValue();

                            Refress();


                            if (reportValue != null)
                            {
                                Response.Clear();
                                Response.ContentType = "application/pdf";
                                Response.AddHeader("content-disposition", "attachment; filename=Money Receive " + reportValue.PatientId + ".pdf");
                                Document document = new Document();
                                //document = new Document(PageSize.A4, 40f, 30f, 30f, 50f);
                                 document = new Document(PageSize.A6, 30f, 20f, 20f, 40f);
                                PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);
                                document.Open();

                                _ipMoneyReceiveReport.GetBillReport(document, writer, reportValue,billList);

                                document.Close();
                                Response.Flush();
                                Response.End();
                            }

                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Payment Success but MoneyReceive Dtl Save Fail');", true);
                            patientIdTextBox.ReadOnly = false;
                        }

                    }

                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Payment Fail');", true);

                }

            }
        }

        private IpMoneyReceiveReportModel MoneyReceiveReportValue()
        {
            // for report
            decimal totalDiscount;
            if (specialDiscountTextBox.Text == "")
            {
                totalDiscount = Convert.ToInt32(totalDiscountTextBox.Text);
            }
            else
            {
                totalDiscount = Convert.ToInt32(totalDiscountTextBox.Text) +
                                Convert.ToInt32(specialDiscountTextBox.Text);
            }


            var reportValue = new IpMoneyReceiveReportModel();
            try { reportValue.Id = Convert.ToInt32(idHiddenField.Value); }
            catch { }
            reportValue.PatientId = patientIdTextBox.Text;
            reportValue.Name = patientNameTextBox.Text;
            reportValue.PhoneNo = phoneNoTextBox.Text;
            reportValue.TotalBill = Convert.ToDecimal(totalBillTextBox.Text);
            reportValue.TotalDiscount = totalDiscount;
            reportValue.PreviesTotalPaid = Convert.ToInt32(totalPaidTextBox.Text);


            if (payAmountTextBox.Text == "")
            {
                reportValue.NowPay = 0;
            }
            else
            {
                reportValue.NowPay = Convert.ToInt32(payAmountTextBox.Text);
            }
            
            reportValue.TotalDue = (reportValue.TotalBill - reportValue.TotalDiscount) -
                                   (reportValue.PreviesTotalPaid + reportValue.NowPay);
            return reportValue;
        }

        protected void reloadButton_Click(object sender, EventArgs e)
        {
            var pageName = System.IO.Path.GetFileName(Request.Url.ToString());
            Response.Redirect(pageName);
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