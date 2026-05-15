using HospitalBilling.BLL;
using HospitalBilling.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace HospitalBilling.UI
{
    public partial class BedTransferOrReleseForm : System.Web.UI.Page
    {
        private readonly IndoorPatientManager _indoorPatientManager = new IndoorPatientManager();
        private readonly CommonHospitalManager _commonHospitalManager = new CommonHospitalManager();
        private readonly WardManager _wardManager = new WardManager();
        private readonly BedManager _bedManager = new BedManager();
        private readonly CabinManager _cabinManager = new CabinManager();

        protected List<RoomType> GetRoom()
        {
            var roomList = new List<RoomType>
            {
                new RoomType() { Id = 1, Name = "Ward"},
                new RoomType(){Id = 2, Name = "Cabin"}
            };

            return roomList;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUserId"] != null && Session["LoginUserRole"] != null)
            {
                if (!IsPostBack)
                {
                    roomDropDownList.DataSource = GetRoom();
                    roomDropDownList.DataTextField = "Name";
                    roomDropDownList.DataValueField = "Id";
                    roomDropDownList.DataBind();
                    roomDropDownList.Items.Insert(0, "");
                    roomDropDownList.SelectedIndex = -1;

                    transferUpdatePanel.Visible = true;

                    WardUpdatePanel.Visible = true;
                    cabinUpdatePanel.Visible = false;

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

                    updateAdmitDateTextBox.Text = DateTime.Now.ToString("dd-MM-yyyy h:mm tt");
                }
            }
            else
            {
                Response.Redirect("../LoginForm.aspx");
            }

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

                var patient = _indoorPatientManager.GetPatientByPatientIdNamePhoneNo(patientId);
                if (patient != null)
                {
                    idHiddenField.Value = patient.Id.ToString();
                    patientIdTextBox.Text = patient.PatientId;
                    patientNameTextBox.Text = patient.Name;
                    phoneNoTextBox.Text = patient.PhoneNo;

                    int patientTableId = Convert.ToInt32(idHiddenField.Value);
                    var bedInfo = _indoorPatientManager.GetBedInfoByPatientId(patientTableId);
                    if (bedInfo != null)
                    {
                        IdHiddenField2.Value = bedInfo.Id.ToString();
                        bedTypeTextBox.Text = bedInfo.RoomType;
                        if (bedInfo.RoomType == "Ward")
                        {
                            bedNoTextBox.Text = bedInfo.BedName.ToString();
                        }
                        else
                        {
                            bedNoTextBox.Text = bedInfo.CabinName.ToString();
                        }

                        admitDateTextBox.Text = bedInfo.AdmitDate.ToString();

                        roomTypeHiddenField.Value = bedInfo.RoomType;
                        if (bedInfo.RoomType == "Ward")
                        {
                            bedNumberHiddenField.Value = bedInfo.BedId.ToString();
                        }
                        else
                        {
                            bedNumberHiddenField.Value = bedInfo.CabinId.ToString();
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

        protected void transferButton_Click(object sender, EventArgs e)
        {
            if (transferRadioButton.Checked)
            {
                if (idHiddenField.Value == "")
                {
                    messageLabel.CssClass = "alert alert-warning";
                    messageLabel.Text = "<strong>Warning!</strong> Please Search Patient First.";
                    searchTextBox.Focus();
                }
                //else if (priceTextBox.Text=="")
                //{
                //    messageLabel.CssClass = "alert alert-warning";
                //    messageLabel.Text = "<strong>Warning!</strong> Please Select Bed Or Cabin.";
                //}
                else if (String.IsNullOrEmpty(roomDropDownList.SelectedValue))
                {
                    messageLabel.CssClass = "alert alert-warning";
                    messageLabel.Text = "<strong>Warning!</strong> Please Select Bed Type.";
                    roomDropDownList.Focus();
                }
                else if (updateAdmitDateTextBox.Text == "")
                {
                    messageLabel.CssClass = "alert alert-warning";
                    messageLabel.Text = "<strong>Warning!</strong> Please Insert New Admit Date.";
                    updateAdmitDateTextBox.Focus();
                }
                else
                {
                    int patientId = Convert.ToInt32(idHiddenField.Value);

                    int bedTransferId = Convert.ToInt32(IdHiddenField2.Value);

                    PatientBedInfo aPatientBedInfo = new PatientBedInfo();
                    aPatientBedInfo.RoomType = roomDropDownList.SelectedItem.ToString();

                    string entryDate = updateAdmitDateTextBox.Text;
                    aPatientBedInfo.AdmitDate = DateTime.ParseExact(entryDate, "dd-MM-yyyy h:mm tt", CultureInfo.InvariantCulture);

                    int roomType = Convert.ToInt32(roomDropDownList.SelectedValue);
                    if (roomType == 1)
                    {
                        if (String.IsNullOrEmpty(wardDropDownList.SelectedValue))
                        {
                            messageLabel.CssClass = "alert alert-warning";
                            messageLabel.Text = "<strong>Warning!</strong> Please Select Ward.";
                            wardDropDownList.Focus();
                        }
                        else if (String.IsNullOrEmpty(bedDropDownList.SelectedValue))
                        {
                            messageLabel.CssClass = "alert alert-warning";
                            messageLabel.Text = "<strong>Warning!</strong> Please Select Bed.";
                            bedDropDownList.Focus();
                        }
                        else
                        {
                            aPatientBedInfo.WardId = Convert.ToInt32(wardDropDownList.SelectedValue);
                            aPatientBedInfo.BedId = Convert.ToInt32(bedDropDownList.SelectedValue);
                            aPatientBedInfo.BedPrice = Convert.ToDecimal(priceTextBox.Text);
                            aPatientBedInfo.CabinPrice = 0;
                        }
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(cabinTypeDropDownList.SelectedValue))
                        {
                            messageLabel.CssClass = "alert alert-warning";
                            messageLabel.Text = "<strong>Warning!</strong> Please Select Cabin Type.";
                            wardDropDownList.Focus();
                        }
                        else if (String.IsNullOrEmpty(cabinDropDownList.SelectedValue))
                        {
                            messageLabel.CssClass = "alert alert-warning";
                            messageLabel.Text = "<strong>Warning!</strong> Please Select Cabin.";
                            bedDropDownList.Focus();
                        }
                        else
                        {
                            aPatientBedInfo.CabinType = Convert.ToInt32(cabinTypeDropDownList.SelectedValue);
                            aPatientBedInfo.CabinId = Convert.ToInt32(cabinDropDownList.SelectedValue);
                            aPatientBedInfo.CabinPrice = Convert.ToDecimal(priceTextBox.Text);
                            aPatientBedInfo.BedPrice = 0;
                        }
                    }
                    string releseRoomType = roomTypeHiddenField.Value;
                    int releseBedId = Convert.ToInt32(bedNumberHiddenField.Value);
                    int rowAffected = _indoorPatientManager.BedRelese(bedTransferId, patientId, aPatientBedInfo, releseRoomType, releseBedId);


                    if (rowAffected > 0)
                    {
                        int transfer = _indoorPatientManager.SavePatientBedInfo(patientId, aPatientBedInfo);
                        if (transfer > 0)
                        {
                            Refress();
                            messageLabel.CssClass = "alert alert-success";
                            messageLabel.Text = "<strong>Success!</strong> Bed Transfer Success.";
                        }
                    }
                }
            }
            else
            {
                if (idHiddenField.Value == "")
                {
                    messageLabel.CssClass = "alert alert-warning";
                    messageLabel.Text = "<strong>Warning!</strong> Please Search Patient First.";
                    searchTextBox.Focus();
                }
                else if (updateAdmitDateTextBox.Text == "")
                {
                    messageLabel.CssClass = "alert alert-warning";
                    messageLabel.Text = "<strong>Warning!</strong> Please Insert Relese Date.";
                    updateAdmitDateTextBox.Focus();
                }
                else
                {
                    int patientId = Convert.ToInt32(idHiddenField.Value);

                    int bedTransferId = Convert.ToInt32(IdHiddenField2.Value);

                    PatientBedInfo aPatientBedInfo = new PatientBedInfo();

                    string entryDate = updateAdmitDateTextBox.Text;
                    aPatientBedInfo.AdmitDate = DateTime.ParseExact(entryDate, "dd-MM-yyyy h:mm tt", CultureInfo.InvariantCulture);

                    string releseRoomType = roomTypeHiddenField.Value;
                    int releseBedId = Convert.ToInt32(bedNumberHiddenField.Value);
                    int rowAffected = _indoorPatientManager.BedRelese(bedTransferId, patientId, aPatientBedInfo, releseRoomType, releseBedId);

                    if (rowAffected > 0)
                    {
                        Refress();

                        messageLabel.CssClass = "alert alert-success";
                        messageLabel.Text = "<strong>Success!</strong> Bed Relese Success.";
                    }
                }

            }

        }

        private void Refress()
        {
            idHiddenField.Value =
                IdHiddenField2.Value =
                    patientIdTextBox.Text =
                        patientNameTextBox.Text =
                            phoneNoTextBox.Text = admitDateTextBox.Text = "";

            priceTextBox.Text = "0";
            bedTypeTextBox.Text = bedNoTextBox.Text = "";

            roomDropDownList.DataSource = GetRoom();
            roomDropDownList.DataTextField = "Name";
            roomDropDownList.DataValueField = "Id";
            roomDropDownList.DataBind();
            roomDropDownList.Items.Insert(0, "");
            roomDropDownList.SelectedIndex = -1;

            WardUpdatePanel.Visible = true;
            cabinUpdatePanel.Visible = false;

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

            updateAdmitDateTextBox.Text = DateTime.Now.ToString("dd-MM-yyyy h:mm tt");
        }



        protected void transferRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            transferUpdatePanel.Visible = true;

            WardUpdatePanel.Visible = true;
            cabinUpdatePanel.Visible = false;
        }

        protected void releseRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            transferUpdatePanel.Visible = false;

            WardUpdatePanel.Visible = true;
            cabinUpdatePanel.Visible = false;
        }
    }
}