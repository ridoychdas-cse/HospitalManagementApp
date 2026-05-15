using HospitalBilling.BLL;
using HospitalBilling.Models;
using HospitalBilling.Report.IpReport;
using HospitalBilling.Report.Models;
using HospitalManager.Library.Billings;
using HospitalModels.Library.Billings;
using HospitalModels.Library.CommonClass;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace HospitalBilling.UI
{
    public partial class IndoorPatientForm : System.Web.UI.Page
    {
        private readonly IndoorPatientManager _indoorPatientManager = new IndoorPatientManager();

        private readonly CommonHospitalManager _commonHospitalManager = new CommonHospitalManager();
        private readonly DoctorManager _doctorManager = new DoctorManager();
        private readonly ReferenceByManager _referenceByManager = new ReferenceByManager();
        private readonly WardManager _wardManager = new WardManager();
        private readonly BedManager _bedManager = new BedManager();
        private readonly CabinManager _cabinManager = new CabinManager();


        private readonly IpMoneyReceiveManager _moneyReceiveManager;

        public IndoorPatientForm()
        {

            _moneyReceiveManager = new IpMoneyReceiveManager();
        }

        public List<PaymentType> GetPaymentTypes()
        {
            var paymentTypeList = new List<PaymentType>
            {
                //new PaymentType {Id = 0, Name = ""},
                new PaymentType {Id = 1, Name = "Cash"},
                new PaymentType {Id = 2, Name = "Bank"}
            };
            return paymentTypeList;
        }

        public List<Relation> GetRelations()
        {
            var relationList = new List<Relation>
            {
                new Relation{Id = 1, Name = "Father"},
                new Relation{Id = 2, Name = "Brother"},
                new Relation{Id = 3, Name = "Mother"},
                new Relation{Id = 4, Name = "Sister"},
                new Relation{Id = 5, Name = "Husband"},
                new Relation{Id = 5, Name = "Wife"},
                new Relation{Id = 5, Name = "Father In Law"},
                new Relation{Id = 5, Name = "Mother In Law"},
                new Relation{Id = 6, Name = "Cousin"},
                new Relation{Id = 7, Name = "Other"}
                
            };
            return relationList;
        }



        protected List<RoomType> GetRoom()
        {
            var roomList = new List<RoomType>
            {
                new RoomType { Id = 1, Name = "Ward"},
                new RoomType {Id = 2, Name = "Cabin"}
            };

            return roomList;
        }

        protected void lbImgUpload_Click(object sender, EventArgs e)
        {
            try
            {

                const int width = 145;
                const int height = 165;
                byte[] stdphoto;
                using (var img = DataManager.ResizeImage(new System.Drawing.Bitmap(imageFileUpload.PostedFile.InputStream), width, height, DataManager.ResizeOptions.ExactWidthAndHeight))
                {
                    imageFileUpload.PostedFile.InputStream.Close();
                    stdphoto = DataManager.ConvertImageToByteArray(img, System.Drawing.Imaging.ImageFormat.Png);
                    ViewState["patientImage"] = stdphoto;
                    img.Dispose();
                }
                string base64String = Convert.ToBase64String(stdphoto, 0, stdphoto.Length);
                patientImage.ImageUrl = "data:image/png;base64," + base64String;


            }
            catch (FormatException fex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Warning", "alert('" + fex.Message + "');", true);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Database"))
                    ClientScript.RegisterStartupScript(this.GetType(), "Warning", "alert('Database Maintain Error. Contact to the Software Provider..!!');", true);
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "Warning", "alert('There is some problem to load image please select jpeg png etc format. Try again properly.!!');", true);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUserId"] != null && Session["LoginUserRole"] != null)
            {
                if (!IsPostBack)
                {
                    WardUpdatePanel.Visible = true;
                    cabinUpdatePanel.Visible = false;

                    GetAllDropdown();
                    entryDateTextBox.Text = DateTime.Now.ToString("dd-MM-yyyy h:mm tt");

                    patientIdTextBox.Text = _indoorPatientManager.AutoId();

                    ViewState["patientImage"] = "";


                }
            }
            else
            {
                Response.Redirect("../LoginForm.aspx");

            }
        }

        private void GetAllDropdown()
        {
            bloodGroupDropDownList.DataSource = _commonHospitalManager.GetallBloodGroups();
            bloodGroupDropDownList.DataTextField = "Name";
            bloodGroupDropDownList.DataValueField = "Id";
            bloodGroupDropDownList.DataBind();
            bloodGroupDropDownList.Items.Insert(0, "");
            bloodGroupDropDownList.SelectedIndex = -1;

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

            roomDropDownList.DataSource = GetRoom();
            roomDropDownList.DataTextField = "Name";
            roomDropDownList.DataValueField = "Id";
            roomDropDownList.DataBind();
            roomDropDownList.Items.Insert(0, "");
            roomDropDownList.SelectedIndex = -1;


            wardDropDownList.DataSource = _wardManager.GetAllWardList();
            wardDropDownList.DataTextField = "Name";
            wardDropDownList.DataValueField = "Id";
            wardDropDownList.DataBind();
            wardDropDownList.Items.Insert(0, "");
            wardDropDownList.SelectedIndex = -1;

            cabinTypeDropDownList.DataSource = _commonHospitalManager.GetAllRoomTypes();
            cabinTypeDropDownList.DataTextField = "Name";
            cabinTypeDropDownList.DataValueField = "Id";
            cabinTypeDropDownList.DataBind();
            cabinTypeDropDownList.Items.Insert(0, "");
            cabinTypeDropDownList.SelectedIndex = -1;


            bedDropDownList.DataSource = null;
            bedDropDownList.DataBind();
            bedDropDownList.SelectedIndex = -1;

            cabinDropDownList.DataSource = null;
            cabinDropDownList.DataBind();
            cabinDropDownList.SelectedIndex = -1;


            statisDropDownList.DataSource = _commonHospitalManager.GetStatus();
            statisDropDownList.DataTextField = "Name";
            statisDropDownList.DataValueField = "Id";
            statisDropDownList.DataBind();
            statisDropDownList.Items.Insert(0, "");
            statisDropDownList.SelectedIndex = -1;


            paymentTypeDiv.Visible = false;

            paymentTypeDropDownList.DataSource = GetPaymentTypes();
            paymentTypeDropDownList.DataTextField = "Name";
            paymentTypeDropDownList.DataValueField = "Id";
            paymentTypeDropDownList.DataBind();

            relationDropDownList.DataSource = GetRelations();
            relationDropDownList.DataTextField = "Name";
            relationDropDownList.DataValueField = "Id";
            relationDropDownList.DataBind();

            string query = "select DIVISION_CODE,DIVISION_NAME from DIVISION_CODE order by 1";
            DropDown.PopulationDropDownList(divisionDropdown, "DIVISION_CODE", query, "DIVISION_NAME", "DIVISION_CODE");
            divisionDropdown.SelectedIndex = -1;
        }

        protected void roomDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            priceTextBox.Text = "";
            priceUpdatePanel.Update();

            int roomType = Convert.ToInt32(roomDropDownList.SelectedValue);

            if (roomType == 1)
            {
                WardUpdatePanel.Visible = true;
                cabinUpdatePanel.Visible = false;
            }
            else
            {
                WardUpdatePanel.Visible = false;
                cabinUpdatePanel.Visible = true;
            }
            roomSelectUpdatePanel.Update();
        }

        protected void wardDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int waredId = Convert.ToInt32(wardDropDownList.SelectedValue);
            var bedList = _bedManager.GetBedByWardId(waredId);
            if (bedList != null)
            {
                bedDropDownList.DataSource = bedList;
                bedDropDownList.DataTextField = "Name";
                bedDropDownList.DataValueField = "Id";
                bedDropDownList.DataBind();
                bedDropDownList.Items.Insert(0, "");
                bedDropDownList.SelectedIndex = -1;
            }
        }

        protected void bedDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(bedDropDownList.SelectedValue);
            var bed = _bedManager.GetBedById(id);
            if (bed != null)
            {
                priceTextBox.Text = "";
                priceTextBox.Text = bed.PriceDaily.ToString();
                priceUpdatePanel.Update();
            }
        }

        protected void cabinTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int roomTypeId = Convert.ToInt32(cabinTypeDropDownList.SelectedValue);
            var cabin = _cabinManager.GetCabinByRoomTypeId(roomTypeId);
            if (cabin != null)
            {
                cabinDropDownList.DataSource = cabin;
                cabinDropDownList.DataTextField = "Name";
                cabinDropDownList.DataValueField = "Id";
                cabinDropDownList.DataBind();
                cabinDropDownList.Items.Insert(0, "");
                cabinDropDownList.SelectedIndex = -1;
            }
        }

        protected void cabinDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(cabinDropDownList.SelectedValue);
            var cabin = _cabinManager.GetCabinById(id);
            if (cabin != null)
            {
                priceTextBox.Text = "";
                priceTextBox.Text = cabin.PriceDaily.ToString();
                priceUpdatePanel.Update();
            }


        }

        protected void saveLinkButton_Click(object sender, EventArgs e)
        {
            try 
            {
                if (patientIdTextBox.Text == "")
                {
                    patientIdTextBox.Text = _indoorPatientManager.AutoId();
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Use This Auto Patient Id!!');", true);

                }
                else if (nameTextBox.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Patient Name!!');", true);
                    nameTextBox.Focus();
                }

                else if (entryDateTextBox.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select Admit Date!!');", true);
                    entryDateTextBox.Focus();
                }

                else if (gNameTextBox.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Gurdian Name!!');", true);
                    gNameTextBox.Focus();
                }
                else if (String.IsNullOrEmpty(bloodGroupDropDownList.SelectedValue))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Selece Blood Group!!');", true);
                    bloodGroupDropDownList.Focus();

                }
                else if (String.IsNullOrEmpty(divisionDropdown.SelectedValue))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Selece Division !!');", true);
                    divisionDropdown.Focus();

                }
                else if (String.IsNullOrEmpty(relationDropDownList.SelectedValue))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Relation!!');", true);
                    relationDropDownList.Focus();
                }

                else
                {
                    if (roomDropDownList.SelectedValue == 1.ToString())
                    {
                        if (String.IsNullOrEmpty(wardDropDownList.SelectedValue))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "ale",
                                "alert('Please Select Ward Name or Number!!');", true);
                            wardDropDownList.Focus();
                        }
                        else if (String.IsNullOrEmpty(bedDropDownList.SelectedValue))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "ale",
                                "alert('Please Select Bed Name or Number!!');", true);
                            bedDropDownList.Focus();
                        }
                        else if (String.IsNullOrEmpty(statisDropDownList.SelectedValue))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select Status!!');",
                                true);
                            statisDropDownList.Focus();
                        }
                        else
                        {
                            SavePatientOutterMethode();

                            //report
                            Report();
                           
                            ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Successfully Save in Database!!');", true);

                        }
                    }
                    else if (roomDropDownList.SelectedValue == 2.ToString())
                    {
                        if (String.IsNullOrEmpty(cabinTypeDropDownList.SelectedValue))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "ale",
                                "alert('Please Select Cabin Type!!');", true);
                            wardDropDownList.Focus();
                        }
                        else if (String.IsNullOrEmpty(cabinDropDownList.SelectedValue))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "ale",
                                "alert('Please Select Cabin Name or Number!!');", true);
                            bedDropDownList.Focus();
                        }
                        else if (String.IsNullOrEmpty(statisDropDownList.SelectedValue))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select Status!!');",
                                true);
                            statisDropDownList.Focus();
                        }
                        else
                        {
                            SavePatientOutterMethode();

                            // report
                            Report();

                            Refress();
                            ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Successfully Save in Database!!');", true);
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select Room Type!!');",
                            true);
                        roomDropDownList.Focus();
                    }
                }
            }
            catch(Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n"+ex, true);
            }
            
        }

        private void Report()
        {

            try
            {
                var reportModel = new IpAdvancePaymentReportModel()
                {
                    PatientId = patientIdTextBox.Text,
                    PatientName = nameTextBox.Text,
                    PhoneNo = phoneNoTextBox.Text,
                };

                if (roomDropDownList.SelectedValue == "1")
                {
                    reportModel.RoomType = "Ward";
                    reportModel.BedNumber = bedDropDownList.SelectedItem.Text;
                }
                else if (roomDropDownList.SelectedValue == "2")
                {
                    reportModel.RoomType = "Cabin";
                    reportModel.BedNumber = cabinDropDownList.SelectedItem.Text;
                }

                decimal advanceamount = 0;
                if (advancePaymentTextBox.Text != "")
                {
                    advanceamount = Convert.ToDecimal(advancePaymentTextBox.Text);
                }
                reportModel.AdvancePayment = advanceamount.ToString("N2");

                string entryDate = entryDateTextBox.Text;
                reportModel.AdmitDate = DateTime.ParseExact(entryDate, "dd-MM-yyyy h:mm tt", CultureInfo.InvariantCulture);


                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition",
                    "attachment; filename=Admition Report " + reportModel.PatientId + ".pdf");
                var document = new Document(PageSize.A6, 30f, 20f, 20f, 40f);
                var writer = PdfWriter.GetInstance(document, Response.OutputStream);
                document.Open();

                var assignReport = new IpAdvancePaymentReport();
                assignReport.GetReport(document, writer, reportModel, "Indoor Patient");

                document.Close();
                Response.Flush();
                Response.End();
            }

            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n" + ex, true);
            }
            
        }

        // save in database
        private void SavePatientOutterMethode()
        {
            try
            {
                var aPatient = new IndoorPatient();


                // image
                if (ViewState["patientImage"].ToString() != "")
                {
                    aPatient.Image = (byte[])ViewState["patientImage"];
                }

                aPatient.PatientId = patientIdTextBox.Text;
                aPatient.Name = nameTextBox.Text;
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
                aPatient.Gender = gender;
                aPatient.Age = ageTextBox.Text;
                aPatient.BloodId = Convert.ToInt32(bloodGroupDropDownList.SelectedValue);
                aPatient.PhoneNo = phoneNoTextBox.Text;
                aPatient.Address = addressTextBox.Text;
                aPatient.DivisinId = Convert.ToInt32(divisionDropdown.SelectedValue);
                aPatient.DistrictId = Convert.ToInt32(DistrictsDropdown.SelectedValue);
                aPatient.ThanaId = Convert.ToInt32(ThanaDropdown.SelectedValue);
                string entryDate = entryDateTextBox.Text;
                aPatient.EntryDate = DateTime.ParseExact(entryDate, "dd-MM-yyyy h:mm tt", CultureInfo.InvariantCulture);

                aPatient.Remark = remarkTextBox.Text;

                aPatient.GName = gNameTextBox.Text;
                aPatient.GPhoneNo = gPhoneNoTextBox.Text;
                if (gMaleRadioButton.Checked)
                {
                    gender = "Male";
                }
                else if (gFemaleRadioButton.Checked)
                {
                    gender = "Female";
                }
                else
                {
                    gender = "Other";
                }
                aPatient.GGender = gender;
                aPatient.GAge = gAgeTextBox.Text;
                aPatient.Relation = relationDropDownList.SelectedItem.Text;
                aPatient.GAddress = gAddressTextBox.Text;

                if (consultantDropDownList.SelectedValue != "")
                {
                    aPatient.ConsultantId = Convert.ToInt32(consultantDropDownList.SelectedValue);
                }
                if (referanceByDropDownList.SelectedValue != "")
                {
                    aPatient.ReferenceById = Convert.ToInt32(referanceByDropDownList.SelectedValue);

                }

                PatientBedInfo aPatientBedInfo = new PatientBedInfo();
                aPatientBedInfo.RoomType = roomDropDownList.SelectedItem.ToString();


                aPatientBedInfo.AdmitDate = DateTime.ParseExact(entryDate, "dd-MM-yyyy h:mm tt", CultureInfo.InvariantCulture);

                int roomType = Convert.ToInt32(roomDropDownList.SelectedValue);
                if (roomType == 1)
                {
                    aPatientBedInfo.WardId = Convert.ToInt32(wardDropDownList.SelectedValue);
                    aPatientBedInfo.BedId = Convert.ToInt32(bedDropDownList.SelectedValue);
                    aPatientBedInfo.BedPrice = Convert.ToDecimal(priceTextBox.Text);
                    aPatientBedInfo.CabinPrice = 0;
                }
                else
                {
                    aPatientBedInfo.CabinType = Convert.ToInt32(cabinTypeDropDownList.SelectedValue);
                    aPatientBedInfo.CabinId = Convert.ToInt32(cabinDropDownList.SelectedValue);
                    aPatientBedInfo.CabinPrice = Convert.ToDecimal(priceTextBox.Text);
                    aPatientBedInfo.BedPrice = 0;
                }

                aPatient.StatusId = Convert.ToInt32(statisDropDownList.SelectedValue);

                bool isPatientIdExist = _indoorPatientManager.IsPatientIdExist(aPatient.PatientId);
                if (isPatientIdExist)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('This Patient Id Alredy Exist!!');", true);
                }
                else
                {
                    int rowAffected = _indoorPatientManager.Save(aPatient, aPatientBedInfo);
                    if (rowAffected > 0)
                    {
                        var patient = _indoorPatientManager.GetIndoorPatientByPatientId(aPatient.PatientId);


                        // Payment System
                        if (advancePaymentTextBox.Text != "")
                        {
                            var moneyReceiveMst = new HospitalModels.Library.Billings.IpMoneyReceiveMst()
                            {
                                PatientId = patient.Id,
                                PatientType = "IP",
                                PayAmount = 0,
                                AdvanceAmount = Convert.ToDecimal(advancePaymentTextBox.Text),
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
                                        //Refress();
                                        ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Paient save and Payment Successfull');", true);
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
                                    rowAffected = _moneyReceiveManager.MoneyReceiveDtlSaveByBank(moneyReceiveDtl);
                                    if (rowAffected > 0)
                                    {
                                        //Refress();
                                        ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Patient save and Payment Successfull');", true);
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
                                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Patient save but Payment Fail');", true);

                            }
                        }

                       
                        ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Successfully Save Patient In Database!!');", true);
                    }
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
                string patientId = patientIdTextBox.Text;
                if (patientId == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Patient Id!!');", true);
                    patientIdTextBox.Focus();
                }
                else
                {
                    var indoorPatient = _indoorPatientManager.GetIndoorPatientByPatientId(patientId);
                    if (indoorPatient != null)
                    {

                        if (indoorPatient.Image.Length > 4)
                        {

                            byte[] patinetImage = indoorPatient.Image;
                            ViewState["patientImage"] = patinetImage;
                            var base64String = Convert.ToBase64String(patinetImage, 0, patinetImage.Length);
                            patientImage.ImageUrl = "data:image/jpeg;base64," + base64String;
                        }



                        idHiddenField.Value = indoorPatient.Id.ToString();
                        patientIdTextBox.Text = indoorPatient.PatientId;
                        nameTextBox.Text = indoorPatient.Name;

                        if (indoorPatient.Gender == "Male")
                        {
                            maleRadioButton.Checked = true;
                        }
                        else if (indoorPatient.Gender == "Female")
                        {
                            femaleRadioButton.Checked = true;
                        }
                        else
                        {
                            otherRadioButton.Checked = true;
                        }

                        ageTextBox.Text = indoorPatient.Age;
                        bloodGroupDropDownList.SelectedValue = indoorPatient.BloodId.ToString();
                        phoneNoTextBox.Text = indoorPatient.PhoneNo;
                        addressTextBox.Text = indoorPatient.Address;
                        entryDateTextBox.Text = indoorPatient.EntryDate.ToString("dd-MM-yyyy h:mm tt");
                        remarkTextBox.Text = indoorPatient.Remark;

                        gNameTextBox.Text = indoorPatient.GName;
                        gPhoneNoTextBox.Text = indoorPatient.GPhoneNo;

                        if (indoorPatient.GGender == "Male")
                        {
                            gMaleRadioButton.Checked = true;
                        }
                        else if (indoorPatient.GGender == "Female")
                        {
                            gFemaleRadioButton.Checked = true;
                        }
                        else
                        {
                            gOtherRadioButton.Checked = true;
                        }

                        gAgeTextBox.Text = indoorPatient.GAge;
                        //relationTextBox.Text = indoorPatient.Relation;
                        gAddressTextBox.Text = indoorPatient.GAddress;


                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Can not Fiend This Patient in Database!!');", true);
                        patientIdTextBox.Focus();
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
                patientIdTextBox.Text =
                    nameTextBox.Text =
                        ageTextBox.Text =
                            phoneNoTextBox.Text =
                                addressTextBox.Text =
                                    remarkTextBox.Text =
                                        gNameTextBox.Text =
                                            gPhoneNoTextBox.Text =
                                                gAgeTextBox.Text = gAddressTextBox.Text = advancePaymentTextBox.Text = "";
            //relationTextBox.Text 

            patientIdTextBox.Text = _indoorPatientManager.AutoId();


            entryDateTextBox.Text = DateTime.Now.ToString("dd-MM-yyyy h:mm tt");
            GetAllDropdown();
            priceTextBox.Text = "0";
            priceUpdatePanel.Update();
            roomSelectUpdatePanel.Update();
        }

        protected void resetLinkButton_Click(object sender, EventArgs e)
        {
            Refress();
        }

        protected void clearLinkButton_Click(object sender, EventArgs e)
        {
            var pageName = System.IO.Path.GetFileName(Request.Url.ToString());
            Response.Redirect(pageName);
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

        protected void reportLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                string patientId = patientIdTextBox.Text;
                if (patientId == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Patient Id!!');", true);
                    patientIdTextBox.Focus();
                }
                else
                {
                    var indoorPatient = _indoorPatientManager.GetIndoorPatientByPatientId(patientId);
                    if (indoorPatient != null)
                    {


                    }

                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n" + ex, true);
            }

        }



        protected void refSaveLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                ReferenceBy referenceBy = new ReferenceBy();
                referenceBy.Name = refNameTextBox.Text;

                int mstId = _referenceByManager.SaveAndGetId(referenceBy);
                refByHiddenField.Value = mstId.ToString();
                if (mstId > 0)
                {
                    referanceByDropDownList.DataSource = _referenceByManager.GetAllReferenceByList();
                    referanceByDropDownList.DataTextField = "Name";
                    referanceByDropDownList.DataValueField = "Id";
                    referanceByDropDownList.DataBind();
                    referanceByDropDownList.Items.Insert(0, "");
                    referanceByDropDownList.SelectedIndex = -1;

                    
                }

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n" + ex, true);
            }

        }

        protected void closeLinkButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(refByHiddenField.Value);

            referanceByDropDownList.SelectedValue = id.ToString();
        }

        protected void divisionDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            var divisionCode = divisionDropdown.SelectedValue;
            DistrictsDropdown.Items.Clear();
            string query1 = "select '' DISTRICT_CODE, '' District_Name union Select DISTRICT_CODE,District_Name from  DISTRICT_CODE  where DIVISION_CODE='" + divisionCode + "' Order by District_Name ASC";
            DropDown.PopulationDropDownList(DistrictsDropdown, "DISTRICT_CODE", query1, "District_Name", "DISTRICT_CODE");
            DistrictsDropdown.SelectedIndex = -1;
        }

        protected void DistrictsDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            var districtCode = DistrictsDropdown.SelectedValue;
            ThanaDropdown.Items.Clear();
            string query2 = "select '' THANA_CODE, '' THANA_NAME union Select THANA_CODE , THANA_NAME from THANA_CODE where DISTRICT_CODE='" + districtCode + "' Order by THANA_NAME ASC";
            DropDown.PopulationDropDownList(ThanaDropdown, "THANA_CODE", query2, "THANA_NAME", "THANA_CODE");
            ThanaDropdown.SelectedIndex = -1;
        }
    }
}