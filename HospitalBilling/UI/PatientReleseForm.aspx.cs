using HospitalBilling.BLL;
using HospitalBilling.BLL.MoneyReceives;
using HospitalBilling.BLL.SurgerysBills;
using HospitalManager.Library.Billings;
using HospitalModels.Library.Billings;
using HospitalModels.Library.CommonClass;
using System;
using System.Collections.Generic;
using System.Globalization;
using DiagnosisBillManager = HospitalBilling.BLL.DiagnosisBills.DiagnosisBillManager;

namespace HospitalBilling.UI
{
    public partial class PatientReleseForm : System.Web.UI.Page
    {
        private readonly IndoorPatientManager _indoorPatientManager = new IndoorPatientManager();
        private readonly IndoorPatientBillManager _indoorPatientBillManager = new IndoorPatientBillManager();
        private readonly DiagnosisBillManager _diagnosisBillManager;
        private readonly MoneyReceiveManager _moneyReceiveManager;
        private readonly IpOtherBillManager _ipOtherBillManager;
        private readonly IpMoneyReceiveManager _ipmoneyReceiveManager;

        IpAssignSurgeryManager _ipAssignSurgeryManager = new IpAssignSurgeryManager();
        
    



        public PatientReleseForm()
        {
            _diagnosisBillManager = new DiagnosisBillManager();
            _moneyReceiveManager = new MoneyReceiveManager();
            _ipOtherBillManager = new IpOtherBillManager();
            _ipmoneyReceiveManager = new IpMoneyReceiveManager();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["LoginUserId"] != null && Session["LoginUserRole"] != null)
                {
                    if (!IsPostBack)
                    {
                        releseDateTextBox.Text = DateTime.Now.ToString("dd-MM-yyyy h:mm tt");
                        //discountDiv.Visible = specialDiscountLable.Visible = specialDiscountTextBox.Visible = false;
                        //payDiv.Visible = payLabel.Visible = payTextBox.Visible = false;
                        payAmountTextBox.ReadOnly = true;
                        specialDiscountTextBox.ReadOnly = true;
                        paymentTypeDiv.Visible = false;

                        paymentTypeDropDownList.DataSource = GetPaymentTypes();
                        paymentTypeDropDownList.DataTextField = "Name";
                        paymentTypeDropDownList.DataValueField = "Id";
                        paymentTypeDropDownList.DataBind();
                    }
                }
                else
                {
                    Response.Redirect("../LoginForm.aspx");
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n" + ex, true);
            }



        }

        protected void searchLinkButton_Click(object sender, EventArgs e)
        {

            try
            {
                if (searchTextBox.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Pateint Id!!');", true);
                    searchTextBox.Focus();
                }
                else
                {
                    string patientId = searchTextBox.Text;

                    var patient = _indoorPatientManager.GetPatientByPatientIdNamePhoneNo(patientId);
                    if (patient != null)
                    {
                        idHiddenField.Value = patient.Id.ToString();
                        patientIdTextBox.Text = patient.PatientId;
                        patientNameTextBox.Text = patient.Name;
                        phoneNoTextBox.Text = patient.PhoneNo;

                        //bed id bed type info
                        var bedinfo = _indoorPatientManager.GetBedInfoByPatientId(patient.Id);
                        bedTypeHiddenField.Value = bedinfo.RoomType;
                        if (bedinfo.RoomType == "Ward")
                        {
                            bedIdHiddenField.Value = bedinfo.BedId.ToString();
                        }
                        else
                        {
                            bedIdHiddenField.Value = bedinfo.CabinId.ToString();
                        }



                        int patientTableId = Convert.ToInt32(idHiddenField.Value);

                        // get all bill info
                        var bedBill = _indoorPatientBillManager.GetTotalBedBill(patientTableId);

                        decimal totalSurgeryBill = 0;
                        // Surgery Bill
                        var ipSurgeryBill = _ipAssignSurgeryManager.GetTotalSurgeryBill(patient.Id);

                        if (ipSurgeryBill != null)
                        {

                            totalSurgeryBill += ipSurgeryBill.Amount;
                        }




                        var diagnosisBillList = _diagnosisBillManager.GetAllBillNoList(patient.Id, "IP");
                        var totalDiagnosisBill = _diagnosisBillManager.GetIPTotalPayableAmountByBillList(diagnosisBillList);

                        var otherBill = _ipOtherBillManager.GetTotalOtherBill(patient.Id, "IP");

                        var totalBill = bedBill + totalDiagnosisBill + totalSurgeryBill + otherBill;
                        var totalIpMoneyReceive = _moneyReceiveManager.GetIpAllPaymentInfo(patient.Id, "IP");
                        var totalDue = totalBill - totalIpMoneyReceive;

                        totalBillTextBox.Text = totalBill.ToString();
                        totalPayTextBox.Text = totalIpMoneyReceive.ToString();
                        totalDueTextBox.Text = totalDue.ToString();
                        if (totalDue != 0)
                        {
                            payAmountTextBox.ReadOnly = false;
                            specialDiscountTextBox.ReadOnly = false;
                            if (totalDue < 0)
                            {
                                specialDiscountTextBox.ReadOnly = true;
                            }
                        }
                    }

                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Can not Find Patient in This Patient Id!!');", true);
                        searchTextBox.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n" + ex, true);
            }

        }

        protected void releseButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (idHiddenField.Value == "")
                {
                    messageLabel.CssClass = "alert alert-warning";
                    messageLabel.Text = "<strong>Warning!</strong> Please Search Patient First.";
                    searchTextBox.Focus();
                }
                else if (releseDateTextBox.Text == "")
                {
                    messageLabel.CssClass = "alert alert-warning";
                    messageLabel.Text = "<strong>Warning!</strong> Please Insert Relese Date.";
                    releseDateTextBox.Focus();
                }
                else
                {
                    
                    decimal due = Convert.ToDecimal(totalDueTextBox.Text);
                    if (due != 0)
                    {
                        if (payAmountTextBox.Text == "")
                        {
                            if (due > 0)
                            {
                                messageLabel.CssClass = "alert alert-warning";
                                messageLabel.Text = "<strong>Warning!</strong> Your have Due amount. Please give Dueamount.";
                                payAmountTextBox.Focus();
                            }
                            else
                            {
                                messageLabel.CssClass = "alert alert-warning";
                                messageLabel.Text = "<strong>Warning!</strong> Your have Extra Payment. Please Take Extra payment .";
                                payAmountTextBox.Focus();
                            }
                        }
                        else if (paymentTypeDropDownList.SelectedItem.Text == "" || paymentTypeDropDownList.SelectedValue == "0")
                        {
                            messageLabel.CssClass = "alert alert-warning";
                            messageLabel.Text = "<strong>Warning!</strong> Please Selece Payment Methode.";
                            paymentTypeDropDownList.Focus();
                        }
                        else
                        {
                           
                            var pay = payAmountTextBox.Text;
                            var spacialdiscount = specialDiscountTextBox.Text;
                            decimal discount;
                            if ((String.IsNullOrEmpty(specialDiscountTextBox.Text)))
                            {
                                discount = 0;
                            }
                            else
                            {
                              discount  = Convert.ToDecimal(spacialdiscount);   
                            }
                            decimal payAmount = Convert.ToDecimal(pay);   
                            decimal Paybil = payAmount + discount;
                            if (due != Paybil)
                                {
                                    messageLabel.CssClass = "alert alert-warning";
                                    messageLabel.Text = "<strong>Warning!</strong> Your Due/Extra amount and Payamount are not equal.";
                                }

                                else
                                {


                                    int id = Convert.ToInt32(idHiddenField.Value);
                                    int status = 2;

                                    string date = releseDateTextBox.Text;
                                    DateTime releseDateTime = DateTime.ParseExact(date, "dd-MM-yyyy h:mm tt", CultureInfo.InvariantCulture);


                                    var moneyReceiveMst = new HospitalModels.Library.Billings.IpMoneyReceiveMst
                                    {
                                        PatientId = Convert.ToInt32(idHiddenField.Value),
                                        PatientType = "IP",
                                        PayAmount = payAmountTextBox.Text == "" ? 0 : Convert.ToDecimal(payAmountTextBox.Text),
                                        AdvanceAmount = 0,
                                        SpecialDiscount =
                                            specialDiscountTextBox.Text == "" ? 0 : Convert.ToDecimal(specialDiscountTextBox.Text)
                                    };

                                    int moneyReceiveId = _ipmoneyReceiveManager.MoneyReceiveMstSave(moneyReceiveMst);
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

                                            var rowAffected2 = _ipmoneyReceiveManager.MoneyReceiveDtlSaveByCash(moneyReceiveDtl);
                                            if (rowAffected2 > 0)
                                            {
                                                Refress();
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
                                            int rowAffected3 = _ipmoneyReceiveManager.MoneyReceiveDtlSaveByBank(moneyReceiveDtl);
                                            if (rowAffected3 > 0)
                                            {
                                                Refress();
                                                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Payment Successfull');", true);
                                                patientIdTextBox.ReadOnly = false;

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

                                    var rowAffected = _indoorPatientManager.PatientRelese(id, status, releseDateTime);

                                    if (rowAffected > 0)
                                    {
                                        // bed status false
                                        var bedType = bedTypeHiddenField.Value;
                                        var bedId = Convert.ToInt32(bedIdHiddenField.Value);
                                        _indoorPatientManager.BedRelese(bedType, bedId);

                                        Refress();

                                        messageLabel.CssClass = "alert alert-success";
                                        messageLabel.Text = "<strong>Success!</strong> Patient Relese Successfull.";
                                    }
                                }
                            
                        }

                    }
                    else
                    {
                        int id = Convert.ToInt32(idHiddenField.Value);
                        int status = 2;

                        string date = releseDateTextBox.Text;
                        DateTime releseDateTime = DateTime.ParseExact(date, "dd-MM-yyyy h:mm tt", CultureInfo.InvariantCulture);

                        int rowAffected = _indoorPatientManager.PatientRelese(id, status, releseDateTime);

                        if (rowAffected > 0)
                        {
                            // bed status false
                            // bed status false
                            var bedType = bedTypeHiddenField.Value;
                            var bedId = Convert.ToInt32(bedIdHiddenField.Value);
                            _indoorPatientManager.BedRelese(bedType, bedId);

                            Refress();

                            messageLabel.CssClass = "alert alert-success";
                            messageLabel.Text = "<strong>Success!</strong> Patient Relese Successfull.";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n" + ex, true);
            }
            
        }
        
        private void Refress()
        {
            idHiddenField.Value =
                IdHiddenField2.Value = patientIdTextBox.Text = patientNameTextBox.Text = phoneNoTextBox.Text = "";

            totalBillTextBox.Text =
                totalPayTextBox.Text = totalDueTextBox.Text = payAmountTextBox.Text = specialDiscountTextBox.Text = "";
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Refress();
        }
    }
}