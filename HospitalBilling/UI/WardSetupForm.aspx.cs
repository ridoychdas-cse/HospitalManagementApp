using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using HospitalBilling.BLL;
using HospitalBilling.Models;

namespace HospitalBilling.UI
{
    public partial class WardSetupForm : System.Web.UI.Page
    {
        private readonly WardManager _wardManager=new WardManager();
        private readonly HospitalDepartmentManager _hospitalDepartmentManager=new HospitalDepartmentManager();
        private readonly CommonHospitalManager _commonHospitalManager=new CommonHospitalManager();
        private readonly CommonModel _commonModel=new CommonModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUserId"] != null && Session["LoginUserRole"] !=null)
            {
                if (!IsPostBack)
                {
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
            wardGridView.DataSource = _wardManager.GetAllWardList();
            wardGridView.DataBind();

            departmentDropDownList.DataSource = _hospitalDepartmentManager.GetAllDepartment();
            departmentDropDownList.DataTextField = "Name";
            departmentDropDownList.DataValueField = "Id";
            departmentDropDownList.DataBind();
            departmentDropDownList.Items.Insert(0, "");
            departmentDropDownList.SelectedIndex = -1;

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

            wardForDropDownList.DataSource = _commonModel.GetAllWardFors();
            wardForDropDownList.DataTextField = "Name";
            wardForDropDownList.DataValueField = "Id";
            wardForDropDownList.DataBind();
            wardForDropDownList.Items.Insert(0, "");
            wardForDropDownList.SelectedIndex = -1;


            editDepartmentTypeDropDownList.DataSource = _hospitalDepartmentManager.GetAllDepartment();
            editDepartmentTypeDropDownList.DataTextField = "Name";
            editDepartmentTypeDropDownList.DataValueField = "Id";
            editDepartmentTypeDropDownList.DataBind();
            editDepartmentTypeDropDownList.Items.Insert(0, "");
            editDepartmentTypeDropDownList.SelectedIndex = -1;

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

            editWardForDropDownList.DataSource = _commonModel.GetAllWardFors();
            editWardForDropDownList.DataTextField = "Name";
            editWardForDropDownList.DataValueField = "Id";
            editWardForDropDownList.DataBind();
            editWardForDropDownList.Items.Insert(0, "");
            editWardForDropDownList.SelectedIndex = -1;
        }


        // Search
        protected void searchLinkButton_Click(object sender, EventArgs e)
        {
            if (searchTextBox.Text == "")
            {
                wardGridView.DataSource = _wardManager.GetAllWardList();
                wardGridView.DataBind();

                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Ward Number or Name!!');", true);
                searchTextBox.Focus();
            }
            else
            {
                var wardList = _wardManager.GetAllWardList(searchTextBox.Text);
                if (wardList.Count>0)
                {
                    wardGridView.DataSource = wardList;
                    wardGridView.DataBind();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Can not Find Ward Number or Name in Database!!');", true);
                    searchTextBox.Focus();
                }

            }

        }



        // Save in databae
        protected void saveLinkButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text=="" || nameTextBox.Text=="W-")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Insert Ward Name.";
                nameTextBox.Focus();
            }
            //else if (String.IsNullOrEmpty(departmentDropDownList.SelectedValue))
            //{
            //    messageLabel.CssClass = "alert alert-warning";
            //    messageLabel.Text = "<strong>Warning!</strong> Please Select Department.";
            //    departmentDropDownList.Focus();
                
            //}
            else if (String.IsNullOrEmpty(floorDropDownList.SelectedValue))
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Select Floor.";
                floorDropDownList.Focus();
                
            }
            else if (String.IsNullOrEmpty(wardForDropDownList.SelectedValue))
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Select Ward For.";
                wardForDropDownList.Focus();
            }
            else if (String.IsNullOrEmpty(roomTypeDropDownList.SelectedValue))
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Select Room Type.";
                roomTypeDropDownList.Focus();
            }
            else
            {
                Ward aWard = new Ward();
                aWard.Name = nameTextBox.Text;
                if (departmentDropDownList.SelectedValue!="")
                {
                    aWard.DepartmentId = Convert.ToInt32(departmentDropDownList.SelectedValue);
                }
                aWard.FloorId = Convert.ToInt32(floorDropDownList.SelectedValue);
                aWard.WardFor = wardForDropDownList.SelectedValue;
                aWard.RoomTypeId = Convert.ToInt32(roomTypeDropDownList.Text);
                aWard.Details = detailsTextBox.Text;

                bool isNameExist = _wardManager.IsNameExist(aWard.Name);
                if (isNameExist)
                {
                    messageLabel.CssClass = "alert alert-warning";
                    messageLabel.Text = "<strong>Warning!</strong> This Name Already Exist.";
                }
                else
                {
                    int rowAffected = _wardManager.Save(aWard);
                    if (rowAffected > 0)
                    {
                        Refress();
                        messageLabel.CssClass = "alert alert-success";
                        messageLabel.Text = "<strong>Success!</strong> Save Ward in Database.";
                    }
                    else
                    {
                        messageLabel.CssClass = "alert alert-warning";
                        messageLabel.Text = "<strong>Fail!</strong> Can'not Save Ward in Database.";
                    }
                }
            }
            
            
        }

        protected void clearLinkButton_Click(object sender, EventArgs e)
        {
            Refress();
        }

        private void Refress()
        {
            nameTextBox.Text = "W-";
                detailsTextBox.Text= "";

            LoadAllDropdownList();
        }

        // Update Database

        // Grid Button
        protected void EditLinkButton_Click(object sender, EventArgs e)
        {
            message2Label.Text = "";

            editDepartmentTypeDropDownList.Items.Insert(0, "");
            editDepartmentTypeDropDownList.SelectedIndex = -1;

            LinkButton dl = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)dl.NamingContainer;
            Label lblID = wardGridView.Rows[gvr.DataItemIndex].FindControl("idLabel") as Label;
            int id = Convert.ToInt32(lblID.Text);

            var aWard = _wardManager.GetWardById(id);

            idHiddenField.Value = aWard.Id.ToString();
            editNameTextBox.Text = aWard.Name;
            if (aWard.DepartmentId!=0)
            {
                editDepartmentTypeDropDownList.SelectedValue = aWard.DepartmentId.ToString();
            }
            editFloorDropDownList.SelectedValue = aWard.FloorId.ToString();
            editWardForDropDownList.SelectedValue= aWard.WardFor;
            editRoomTypeDropDownList.SelectedValue = aWard.RoomTypeId.ToString();
            editDescriptionTextBox.Text = aWard.Details;

            editModalPopupExtender.Show();
        }

        // Popup Button
        protected void updateLinkButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(idHiddenField.Value);

            Ward aWard=new Ward();
            aWard.Id = id;
            aWard.Name = editNameTextBox.Text;
            if (editDepartmentTypeDropDownList.SelectedValue!="")
            {
                aWard.DepartmentId = Convert.ToInt32(editDepartmentTypeDropDownList.SelectedValue);
            }
            aWard.FloorId = Convert.ToInt32(editFloorDropDownList.SelectedValue);
            aWard.WardFor = editWardForDropDownList.SelectedValue;
            aWard.RoomTypeId = Convert.ToInt32(editRoomTypeDropDownList.SelectedValue);
            aWard.Details = editDescriptionTextBox.Text;

            if (editNameTextBox.Text == "" || editNameTextBox.Text=="W-")
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning!</strong> Please Insert Ward Name.";
                editNameTextBox.Focus();
            }
            else if (String.IsNullOrEmpty(editFloorDropDownList.SelectedValue))
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning!</strong> Please Select Floor.";
                editFloorDropDownList.Focus();

            }
            else if (editWardForDropDownList.Text == "")
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning!</strong> Please Insert Ward For.";
                editWardForDropDownList.Focus();
            }
            else if (String.IsNullOrEmpty(editRoomTypeDropDownList.SelectedValue))
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning!</strong> Please Select Room Type.";
                editRoomTypeDropDownList.Focus();
            }
            else
            {
                //bool isNameExist = _wardManager.IsNameExist(aWard.Name, aWard.FloorId);
                //if (isNameExist)
                //{
                //    messageLabel.CssClass = "alert alert-warning";
                //    messageLabel.Text = "<strong>Warning!</strong> This Name Already Exist.";
                //}
                //else
                //{
                    int rowAffected = _wardManager.Update(aWard);
                    if (rowAffected > 0)
                    {
                        EditPopupRefress();
                        message2Label.CssClass = "alert alert-success";
                        message2Label.Text = "<strong>Success!</strong> Update Ward in Database.";
                    }
                    else
                    {
                        message2Label.CssClass = "alert alert-warning";
                        message2Label.Text = "<strong>Fail!</strong> Can'not Update Ward in Database.";
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
            idHiddenField.Value = editDescriptionTextBox.Text = "";
                editNameTextBox.Text = "W-"; 

            LoadAllDropdownList();
        }

        // Delete In Database

        // Grid Button
        protected void deleteLinkButton_Click(object sender, EventArgs e)
        {
            LinkButton dl = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)dl.NamingContainer;
            Label lblID = wardGridView.Rows[gvr.DataItemIndex].FindControl("idLabel") as Label;
            int id = Convert.ToInt32(lblID.Text);

            idHiddenField.Value = id.ToString();
            deleteModalPopupExtender.Show();
        }

        // Popup Button
        protected void delete1LinkButton_Click(object sender, EventArgs e)
        {
            
            Ward aWard = new Ward();
            aWard.Id= Convert.ToInt32(idHiddenField.Value);
            _wardManager.Delete(aWard);
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