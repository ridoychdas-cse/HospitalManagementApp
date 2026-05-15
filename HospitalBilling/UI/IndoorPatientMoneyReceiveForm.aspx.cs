using HospitalBilling.BLL;
using HospitalBilling.BLL.MoneyReceives;
using HospitalBilling.Models.MoneyReceives;
using HospitalBilling.Report;
using System;
using DiagnosisBillManager = HospitalBilling.BLL.DiagnosisBills.DiagnosisBillManager;

namespace HospitalBilling.UI
{
    public partial class IndoorPatientMoneyReceiveForm : System.Web.UI.Page
    {
        private readonly IndoorPatientManager _indoorPatientManager = new IndoorPatientManager();
        private readonly IndoorPatientBillManager _indoorPatientBillManager = new IndoorPatientBillManager();
        private readonly DiagnosisBillManager _diagnosisBillManager;
        private readonly MoneyReceiveManager _moneyReceiveManager;

        public IndoorPatientMoneyReceiveForm()
        {
            _diagnosisBillManager = new DiagnosisBillManager();
            _moneyReceiveManager = new MoneyReceiveManager();
           
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void searchLinkButton_Click(object sender, EventArgs e)
        {
            if (searchTextBox.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Pateint Id!!');", true);
                searchTextBox.Focus();
            }
            else
            {
                string patientId = searchTextBox.Text;

                var patient = _indoorPatientManager.GetIndoorPatientByPatientId(patientId);
                if (patient != null)
                {
                    idHiddenField.Value = patient.Id.ToString();
                    patientIdTextBox.Text = patient.PatientId;
                    patientNameTextBox.Text = patient.Name;
                    phoneNoTextBox.Text = patient.PhoneNo;

                    int patientTableId = Convert.ToInt32(idHiddenField.Value);

                    // get all bill info
                    var bedBill = _indoorPatientBillManager.GetTotalBedBill(patientTableId);

                    var diagnosisBillList = _diagnosisBillManager.GetAllBillNoList(patient.Id, "IP");
                    var totalDiagnosisBill = _diagnosisBillManager.GetIPTotalPayableAmountByBillList(diagnosisBillList);

                    var totalBill = bedBill + totalDiagnosisBill;
                    var totalIpMoneyReceive = _moneyReceiveManager.GetIpAllPaymentInfo(patient.Id, "IP");
                    var totalDue = totalBill - totalIpMoneyReceive;

                    if (totalDue > 0)
                    {
                        discountDiv.Visible = specialDiscountLable.Visible = specialDiscountTextBox.Visible = true;
                        payDiv.Visible = payLabel.Visible = payTextBox.Visible = true;
                    }

                    bedBillTextBox.Text = bedBill.ToString();
                    diagnosisbillTextBox.Text = totalDiagnosisBill.ToString();
                    totalBillTextBox.Text = totalBill.ToString();
                    totalPayTextBox.Text = totalIpMoneyReceive.ToString();
                    totalDueTextBox.Text = totalDue.ToString();
                    totalDueTextBox.Text = totalDue.ToString();



                }

                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale",
                        "alert('Can not Find Patient in This Patient Id!!');", true);
                    searchTextBox.Focus();
                }
            }
        }

        protected void paymentButton_Click(object sender, EventArgs e)
        {
            if (idHiddenField.Value == "")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Search Patient First.";
                searchTextBox.Focus();
            }
            else
            {
                int patientId = Convert.ToInt32(idHiddenField.Value);
                const string patientType = "IP";

                decimal payAmount = payTextBox.Text == "" ? 0 : Convert.ToDecimal(payTextBox.Text);
                decimal advanceAmount = advanceAmountTextBox.Text == ""
                    ? 0
                    : Convert.ToDecimal(advanceAmountTextBox.Text);

                decimal specialDiscount = specialDiscountTextBox.Text == ""
                    ? 0
                    : Convert.ToDecimal(specialDiscountTextBox.Text);

                decimal vat = ipVatTextBox.Text == "" ? 0 : Convert.ToDecimal(ipVatTextBox.Text);

                var moneyReceiveMst = new MoneyReceiveMst()
                {
                    PatientId = patientId,
                    PatientType = patientType,
                    PayAmount = payAmount,
                    AdvanceAmount = advanceAmount,
                    SpecialDiscount = specialDiscount,
                    Vat = vat
                };

                var moneyreceiveDtl = new MoneyReceiveDtl()
                {
                    PayMethode = "Cash",
                    BankName = "",
                    ChequeNo = "",
                    ChequeDate = null,
                    EntryDate = DateTime.Now
                };

                int rowAffected = _moneyReceiveManager.MoneySave(moneyReceiveMst, moneyreceiveDtl);

                if (rowAffected > 0)
                {
                    var patientIdinText = patientIdTextBox.Text;
                    var paymentnHistory = new PaymentHistory()
                    {
                        DiagnosisBill = diagnosisbillTextBox.Text,
                        BedBill = bedBillTextBox.Text,
                        TotalBill = totalBillTextBox.Text,
                        TotalPay = totalPayTextBox.Text,
                        TotalDue = totalDueTextBox.Text,
                    };



                    //Response.Clear();
                    //Response.ContentType = "application/pdf";
                    //Response.AddHeader("content-disposition", "attachment; filename=Money Receive " + patientIdinText + ".pdf");
                    //var document = new Document();
                    //document = new Document(PageSize.A4, 40f, 30f, 30f, 50f);
                    //PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);
                    //document.Open();

                    //_ipMoneyReceiveRepost.GetBillReport(document, writer, moneyReceiveMst, moneyreceiveDtl, paymentnHistory);

                    //document.Close();
                    //Response.Flush();
                    //Response.End();


                    Refress();
                }

            }
        }



        private void Refress()
        {
            idHiddenField.Value =
                patientIdTextBox.Text =
                    patientNameTextBox.Text =
                        phoneNoTextBox.Text =
                            bedBillTextBox.Text =
                                diagnosisbillTextBox.Text =
                                    totalBillTextBox.Text =
                                        totalPayTextBox.Text =
                                            totalDueTextBox.Text =
                                                payTextBox.Text =
                                                    advanceAmountTextBox.Text =
                                                        specialDiscountTextBox.Text = ipVatTextBox.Text = "";
        }
    }
}