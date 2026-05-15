using HospitalBilling.BLL;
using HospitalBilling.BLL.IndoorPatientBills;
using HospitalBilling.Models.IndoorPatientBills;
using HospitalBilling.Report;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;

namespace HospitalBilling.UI
{
    public partial class IndoorPatientBillForm : System.Web.UI.Page
    {
        private readonly IndoorPatientManager _indoorPatientManager = new IndoorPatientManager();
        private readonly IndoorPatientBillManager _indoorPatientBillManager = new IndoorPatientBillManager();

        private readonly IpManager _ipManager;

        public IndoorPatientBillForm()
        {
            _ipManager = new IpManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUserId"] != null && Session["LoginUserRole"] != null)
            {
            }
            else
            {
                Response.Redirect("../LoginForm.aspx");

            }

        }


        protected void searchLinkButton_Click(object sender, EventArgs e)
        {
            string searchInpur = searchTextBox.Text;


            var patient = _indoorPatientManager.GetPatientByPatientIdNamePhoneNo(searchInpur);
            if (patient != null)
            {
                idHiddenField.Value = patient.Id.ToString();
                patientIdTextBox.Text = patient.PatientId;
                nameTextBox.Text = patient.Name;
                phoneNoTextBox.Text = patient.PhoneNo;

                int id = Convert.ToInt32(idHiddenField.Value);

                var billInfo = _ipManager.IndoorPatientBillHistory(id, "IP");

                if (billInfo != null)
                {
                    TotalDiagnosisBillTextBox.Text = billInfo.DiagosisBill.ToString();
                    TotalBedBillTextBox.Text = billInfo.BedBill.ToString();
                    TotalBillTextBox.Text = billInfo.TotalBill.ToString();
                    TotalPaidAmountTextBox.Text = billInfo.TotalPayAmount.ToString();
                    TotalDueAmountTextBox.Text = billInfo.TotalDue.ToString();

                }

                var billinfo = _indoorPatientBillManager.GetAllBedBillByPatientId(id);
                if (billinfo != null)
                {
                    var totalPayabelAmount = _indoorPatientBillManager.GetTotalBedBill(id);
                    TotalBillTextBox.Text = totalPayabelAmount.ToString("N0");
                    billGridView.DataSource = billinfo;
                    billGridView.DataBind();
                }


                ////     depadent on totalbilltextbox
                //    var billHistory = _indoorPatientBillManager.GetTotalPaymentHistory(id);
                //    paidAmountTextBox.Text = billHistory.TotalPayAmount.ToString();
                //    dueAmountTextBox.Text =  (totalPayabelAmount- billHistory.TotalPayAmount).ToString("N0");

                //    
                //}
            }

        }

        protected void payButton_Click(object sender, EventArgs e)
        {
            if (idHiddenField.Value == "")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Search a Patient First Then Pay.";
            }
            else if (payAmountTextBox.Text == "")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Insert Pay Amount.";
            }
            else
            {
                int id = Convert.ToInt32(idHiddenField.Value);
                decimal payAmount = Convert.ToDecimal(payAmountTextBox.Text);

                var ipPayment = new IpPayment
                {
                    PatientId = id,
                    PatientType = "IP",
                    PayAmount = payAmount,
                    AdvanceAmount = 0,
                    SpacialDiscount = 0,
                    Vat = 0
                };
                int rowAffected = _ipManager.PayAmountSave(ipPayment);
                if (rowAffected > 0)
                {
                    messageLabel.Text = "Payment Successfull";
                }
                else
                {
                    messageLabel.Text = "payment fail";
                }

                //BedBill aBedBill = new BedBill();

                //var paymentHistory = _indoorPatientBillManager.GetBedBillMrHistory(id);
                //if (paymentHistory != null)
                //{
                //    // update
                //    decimal previesPay = paymentHistory.TotalPayAmount;
                //    aBedBill.PayAmount = previesPay + payAmount;

                //    _indoorPatientBillManager.UpdatebedBillMr(id, aBedBill);

                //    // Report
                //    patientId = patientIdTextBox.Text;
                //    Report(patientId);
                //}
                //else
                //{
                //    aBedBill.PatientId = id;
                //    aBedBill.PayAmount = payAmount;

                //    _indoorPatientBillManager.SaveBedBillMr(aBedBill);
                //    // Report
                //    patientId = patientIdTextBox.Text;
                //    Report(patientId);
                //}
            }

        }

       
    }
}