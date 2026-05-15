using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using HospitalBilling.BLL;
using HospitalBilling.Models;

namespace HospitalBilling.UI
{
    public partial class CabinSetupForm : System.Web.UI.Page
    {
        private readonly CabinManager _cabinManager=new CabinManager();
        private readonly CommonHospitalManager _commonHospitalManager=new CommonHospitalManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUserId"] != null && Session["LoginUserRole"] != null)
            {
                if (!IsPostBack)
                {
                    cabinGridView.DataSource = _cabinManager.GetAllCabinList();
                    cabinGridView.DataBind();

                    LoadAllDropdownList();
                }
            }
            else
            {
                Response.Redirect("../LoginForm.aspx");
            }
        }

        private void LoadAllDropdownList()
        {
            floorDropDownList.DataSource = _commonHospitalManager.GetAllFloors();
            floorDropDownList.DataTextField = "Name";
            floorDropDownList.DataValueField = "Id";
            floorDropDownList.DataBind();
            floorDropDownList.Items.Insert(0, "");
            floorDropDownList.SelectedIndex = -1;

            roomTypeDropDownList.DataSource = _commonHospitalManager.GetAllRoomTypes();
            roomTypeDropDownList.DataTextField = "Name";
            roomTypeDropDownList.DataValueField = "Id";
            roomTypeDropDownList.DataBind();
            roomTypeDropDownList.Items.Insert(0, "");
            roomTypeDropDownList.SelectedIndex = -1;

            editFloorDropDownList.DataSource = _commonHospitalManager.GetAllFloors();
            editFloorDropDownList.DataTextField = "Name";
            editFloorDropDownList.DataValueField = "Id";
            editFloorDropDownList.DataBind();
            editFloorDropDownList.Items.Insert(0, "");
            editFloorDropDownList.SelectedIndex = -1;

            editRoomTypeDropDownList.DataSource = _commonHospitalManager.GetAllRoomTypes();
            editRoomTypeDropDownList.DataTextField = "Name";
            editRoomTypeDropDownList.DataValueField = "Id";
            editRoomTypeDropDownList.DataBind();
            editRoomTypeDropDownList.Items.Insert(0, "");
            editRoomTypeDropDownList.SelectedIndex = -1;
        }

        // Search
        protected void searchLinkButton_Click(object sender, EventArgs e)
        {
            if (searchTextBox.Text == "")
            {
                cabinGridView.DataSource = _cabinManager.GetAllCabinList();
                cabinGridView.DataBind();

                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Cabin Number or Name!!');", true);
                searchTextBox.Focus();
            }
            else
            {
                var aCabinList = _cabinManager.GetCabinByNameOrType(searchTextBox.Text);
                if (aCabinList.Count>0)
                {
                    cabinGridView.DataSource = aCabinList;
                    cabinGridView.DataBind();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Can not Find Cabin in This Number or Name!!');", true);
                    searchTextBox.Focus();
                }
            }

        }

        // save in database
        protected void saveLinkButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text == "" || nameTextBox.Text=="C-")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Insert Cabin Name / Number.";
                nameTextBox.Focus();
            }
            else if (String.IsNullOrEmpty(floorDropDownList.SelectedValue))
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Select Floor Number.";
                floorDropDownList.Focus();
            }
            else if (String.IsNullOrEmpty(roomTypeDropDownList.SelectedValue))
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Select Room Type.";
                roomTypeDropDownList.Focus();
            }
            else if (priceDailyTextBox.Text == "")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Insert Cabin Price.";
                priceDailyTextBox.Focus();
            }
            else
            {
                Cabin aCabin = new Cabin();
                aCabin.Name = nameTextBox.Text;
                aCabin.FloorId = Convert.ToInt32(floorDropDownList.SelectedValue);
                aCabin.RoomTypeId = Convert.ToInt32(roomTypeDropDownList.SelectedValue);
                aCabin.PriceDaily = Convert.ToDecimal(priceDailyTextBox.Text);
                aCabin.Status = false;

                bool isNameExist = _cabinManager.IsNameExist(aCabin.Name, aCabin.FloorId);
                if (isNameExist)
                {
                    messageLabel.CssClass = "alert alert-warning";
                    messageLabel.Text = "<strong>Warning!</strong> This Name Alredy Exist in Database.";
                }
                else
                {
                    int rowAffected = _cabinManager.Save(aCabin);
                    if (rowAffected > 0)
                    {
                        Refress();
                        messageLabel.CssClass = "alert alert-success";
                        messageLabel.Text = "<strong>Success!</strong> Save Cabin in Database.";
                    }
                    else
                    {
                        messageLabel.CssClass = "alert alert-warning";
                        messageLabel.Text = "<strong>Fail!</strong> Can'not Save Cabin Bed in Database.";
                    }
                }
            }
            
        }

        protected void clearLinkButton_Click(object sender, EventArgs e)
        {
            LoadAllDropdownList();
            Refress();
        }

        private void Refress()
        {
            nameTextBox.Text = "C-";
            priceDailyTextBox.Text = "0";

            cabinGridView.DataSource = _cabinManager.GetAllCabinList();
            cabinGridView.DataBind();

            LoadAllDropdownList();

        }

        // Update in Database

        // Grid Button
        protected void EditLinkButton_Click(object sender, EventArgs e)
        {
            message2Label.Text = "";
            LinkButton dl = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)dl.NamingContainer;
            Label lblID = cabinGridView.Rows[gvr.DataItemIndex].FindControl("idLabel") as Label;
            int id = Convert.ToInt32(lblID.Text);

            var aCabin = _cabinManager.GetCabinById(id);
            idHiddenField.Value = aCabin.Id.ToString();
            editNameTextBox.Text = aCabin.Name;
            editFloorDropDownList.SelectedValue = aCabin.FloorId.ToString();
            editRoomTypeDropDownList.SelectedValue = aCabin.RoomTypeId.ToString();
            editPriceDailyTextBox.Text = aCabin.PriceDaily.ToString();

            editModalPopupExtender.Show();
        }
        // popup Button

        protected void updateLinkButton_Click(object sender, EventArgs e)
        {
            if (editNameTextBox.Text == "")
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning!</strong> Please Insert Cabin Name / Number.";
                editNameTextBox.Focus();
            }
            else if (String.IsNullOrEmpty(editFloorDropDownList.SelectedValue))
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning!</strong> Please Select Floor Number.";
                editFloorDropDownList.Focus();
            }
            else if (String.IsNullOrEmpty(editRoomTypeDropDownList.SelectedValue))
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning!</strong> Please Select Room Type.";
                editRoomTypeDropDownList.Focus();
            }
            else if (editPriceDailyTextBox.Text == "")
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning!</strong> Please Insert Cabin Price.";
                editPriceDailyTextBox.Focus();
            }

            else
            {
                int id = Convert.ToInt32(idHiddenField.Value);

                Cabin aCabin = new Cabin();
                aCabin.Id = id;
                aCabin.Name = editNameTextBox.Text;
                aCabin.FloorId = Convert.ToInt32(editFloorDropDownList.SelectedValue);
                aCabin.RoomTypeId = Convert.ToInt32(editRoomTypeDropDownList.SelectedValue);
                aCabin.PriceDaily = Convert.ToDecimal(editPriceDailyTextBox.Text);


                int rowAffected = _cabinManager.Update(aCabin);
                if (rowAffected > 0)
                {
                    EditPopupRefress();
                    message2Label.CssClass = "alert alert-success";
                    message2Label.Text = "<strong>Success!</strong> Update Cabin in Database.";
                }
                else
                {
                    message2Label.CssClass = "alert alert-warning";
                    message2Label.Text = "<strong>Fail!</strong> Can'not Update Cabin Bed in Database.";
                }
            }
            
            
        }

        protected void close2LinkButton_Click(object sender, EventArgs e)
        {
            var pageName = System.IO.Path.GetFileName(Request.Url.ToString());
            Response.Redirect(pageName);
        }

        private void EditPopupRefress()
        {
            idHiddenField.Value = "";
            editNameTextBox.Text = "C-";
            editPriceDailyTextBox.Text = "0";

            LoadAllDropdownList();


        }

        // Delete In Databse
        // Grid Button
        protected void deleteLinkButton_Click(object sender, EventArgs e)
        {
            LinkButton dl = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)dl.NamingContainer;
            Label lblID = cabinGridView.Rows[gvr.DataItemIndex].FindControl("idLabel") as Label;
            int id = Convert.ToInt32(lblID.Text);

            idHiddenField.Value = id.ToString();
            deleteModalPopupExtender.Show();
        }

        // popup button
        protected void delete1LinkButton_Click(object sender, EventArgs e)
        {
           
            Cabin aCabin=new Cabin();
            aCabin.Id= Convert.ToInt32(idHiddenField.Value);
            _cabinManager.Delete(aCabin);
            DeleteRefress();
            deleteModalPopupExtender.Hide();
        }

        private void DeleteRefress()
        {
            var pageName = System.IO.Path.GetFileName(Request.Url.ToString());
            Response.Redirect(pageName);
        }

        protected void close3LinkButton_Click(object sender, EventArgs e)
        {
            deleteModalPopupExtender.Hide();
            DeleteRefress();
        }
        
    }
}