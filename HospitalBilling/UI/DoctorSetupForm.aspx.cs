using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HospitalBilling.BLL;
using HospitalBilling.Models;

namespace HospitalBilling.UI
{
    public partial class DoctorSetupForm : System.Web.UI.Page
    {
        private readonly DoctorManager _doctorManager=new DoctorManager();
        private readonly HospitalDepartmentManager _hospitalDepartmentManager=new HospitalDepartmentManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUserId"] != null && Session["LoginUserRole"] != null)
            {
                if (!IsPostBack)
                {
                    doctorGridView.DataSource = _doctorManager.GetAllDoctorList();
                    doctorGridView.DataBind();

                    departmentDropDownList.DataSource = _hospitalDepartmentManager.GetAllDepartment();
                    departmentDropDownList.DataTextField = "Name";
                    departmentDropDownList.DataValueField = "Id";
                    departmentDropDownList.DataBind();
                    departmentDropDownList.Items.Insert(0, "");
                    departmentDropDownList.SelectedIndex = -1;

                    editDepartmentDropDownList.DataSource = _hospitalDepartmentManager.GetAllDepartment();
                    editDepartmentDropDownList.DataTextField = "Name";
                    editDepartmentDropDownList.DataValueField = "Id";
                    editDepartmentDropDownList.DataBind();
                    editDepartmentDropDownList.Items.Insert(0, "");
                    editDepartmentDropDownList.SelectedIndex = -1;
                }
            }
            else
            {
                Response.Redirect("../LoginForm.aspx");
            }

        }

        // Search
        protected void searchLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (searchTextBox.Text == "")
                {
                    doctorGridView.DataSource = _doctorManager.GetAllDoctorList();
                    doctorGridView.DataBind();

                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Doctor Name!!');", true);
                    searchTextBox.Focus();
                }
                else
                {
                    var aDoctor = _doctorManager.GetDoctorName(searchTextBox.Text);
                    if (aDoctor != null)
                    {
                        doctorGridView.DataSource = aDoctor;
                        doctorGridView.DataBind();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Can not Fiend Doctor by This Name Name!!');", true);
                        searchTextBox.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n" + ex, true);
            }
           

        }

        // save in database
        protected void saveLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (nameTextBox.Text == "")
                {
                    messageLabel.CssClass = "alert alert-warning";
                    messageLabel.Text = "<strong>Warning!</strong> Please Insert Doctor Name.";
                }
                //else if (String.IsNullOrEmpty(departmentDropDownList.SelectedValue))
                //{
                //    messageLabel.CssClass = "alert alert-warning";
                //    messageLabel.Text = "<strong>Warning!</strong> Please Select Department.";
                //}
                //else if (phoneNoTextBox.Text == "")
                //{
                //    messageLabel.CssClass = "alert alert-warning";
                //    messageLabel.Text = "<strong>Warning!</strong> Please Insert Phone No.";
                //}
                else
                {
                    var aDoctor = new Doctor();
                    aDoctor.Name = nameTextBox.Text;
                    aDoctor.PhoneNo = phoneNoTextBox.Text;
                    if (departmentDropDownList.SelectedValue != "")
                    {
                        aDoctor.DepartmentId = Convert.ToInt32(departmentDropDownList.SelectedValue);
                    }
                    aDoctor.Specialty = specialityTextBox.Text;
                    aDoctor.ProfileBrief = profileBriefTextBox.Text;
                    aDoctor.Designation = designationTextBox.Text;
                    bool isNameExist = _doctorManager.IsNameExist(aDoctor.Name);
                    if (isNameExist)
                    {
                        messageLabel.CssClass = "alert alert-warning";
                        messageLabel.Text = "<strong>Warning!</strong> This Name Alredy Exist in Database.";
                    }
                    else
                    {
                        int rowAffected = _doctorManager.Save(aDoctor);
                        if (rowAffected > 0)
                        {
                            Refress();
                            messageLabel.CssClass = "alert alert-success";
                            messageLabel.Text = "<strong>Success!</strong> Save Doctor in Database.";
                        }
                        else
                        {
                            messageLabel.CssClass = "alert alert-warning";
                            messageLabel.Text = "<strong>Fail!</strong> Can'not Save Doctor in Database.";
                        }
                    }
                }
            }
             catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n" + ex, true);
            }
            
        }

        protected void clearLinkButton_Click(object sender, EventArgs e)
        {
            Refress();
        }

        private void Refress()
        {
            nameTextBox.Text=phoneNoTextBox.Text=specialityTextBox.Text=profileBriefTextBox.Text =designationTextBox.Text="";
            
            doctorGridView.DataSource = _doctorManager.GetAllDoctorList();
            doctorGridView.DataBind();

            departmentDropDownList.DataSource = _hospitalDepartmentManager.GetAllDepartment();
            departmentDropDownList.DataTextField = "Name";
            departmentDropDownList.DataValueField = "Id";
            departmentDropDownList.DataBind();
            departmentDropDownList.Items.Insert(0, "");
            departmentDropDownList.SelectedIndex = -1;
        }

        // Upadte In Database

        // Grid Button
        protected void EditLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                message2Label.Text = "";
                LinkButton dl = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)dl.NamingContainer;
                Label lblID = doctorGridView.Rows[gvr.DataItemIndex].FindControl("idLabel") as Label;
                int id = Convert.ToInt32(lblID.Text);

                var aDoctor = _doctorManager.GetDoctorId(id);
                idHiddenField.Value = aDoctor.Id.ToString();
                editNameTextBox.Text = aDoctor.Name;
                editPhoneNoTextBox.Text = aDoctor.PhoneNo;
                editDesignationTextBox.Text = aDoctor.Designation;
                if (aDoctor.DepartmentId != 0)
                {
                    editDepartmentDropDownList.SelectedValue = aDoctor.DepartmentId.ToString();

                }
                editSpecialityTextBox.Text = aDoctor.Specialty;
                editProfileBriefTextBox.Text = aDoctor.ProfileBrief;

                editModalPopupExtender.Show();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n" + ex, true);
            }
            
        }

        // Popup Button
        protected void updateLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (editNameTextBox.Text == "")
                {
                    message2Label.CssClass = "alert alert-warning";
                    message2Label.Text = "<strong>Warning!</strong> Please Insert Dorctor Name.";
                }
                //else if (String.IsNullOrEmpty(editDepartmentDropDownList.SelectedValue))
                //{
                //    message2Label.CssClass = "alert alert-warning";
                //    message2Label.Text = "<strong>Warning!</strong> Please Select Department.";
                //}
                //else if (editPhoneNoTextBox.Text == "")
                //{
                //    message2Label.CssClass = "alert alert-warning";
                //    message2Label.Text = "<strong>Warning!</strong> Please Insert Phone No.";
                //}
                else
                {
                    int id = Convert.ToInt32(idHiddenField.Value);

                    var aDoctor = new Doctor();
                    aDoctor.Id = id;
                    aDoctor.Name = editNameTextBox.Text;
                    aDoctor.PhoneNo = editPhoneNoTextBox.Text;
                    aDoctor.DepartmentId = Convert.ToInt32(editDepartmentDropDownList.SelectedValue);
                    aDoctor.Specialty = editSpecialityTextBox.Text;
                    aDoctor.ProfileBrief = editProfileBriefTextBox.Text;
                    aDoctor.Designation = editDesignationTextBox.Text;
                    //bool isNameExist = _bedManager.IsNameExist(aBed.Name, aBed.WardId);
                    //if (isNameExist)
                    //{
                    //    message2Label.CssClass = "alert alert-warning";
                    //    message2Label.Text = "<strong>Warning!</strong> This Name Alredy Exist in Database.";
                    //}
                    //else
                    //{
                    int rowAffected = _doctorManager.Update(aDoctor);
                    if (rowAffected > 0)
                    {
                        EditPopupRefress();
                        message2Label.CssClass = "alert alert-success";
                        message2Label.Text = "<strong>Success!</strong> Update Doctor in Database.";
                    }
                    else
                    {
                        message2Label.CssClass = "alert alert-warning";
                        message2Label.Text = "<strong>Fail!</strong> Can'not Update Doctor in Database.";
                    }
                    //}
                }
            }
             catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n" + ex, true);
            }

           



        }

        protected void close2LinkButton_Click(object sender, EventArgs e)
        {
            var pageName = System.IO.Path.GetFileName(Request.Url.ToString());
            Response.Redirect(pageName);
        }

        private void EditPopupRefress()
        {
            idHiddenField.Value = editNameTextBox.Text= editPhoneNoTextBox.Text = editSpecialityTextBox.Text = editProfileBriefTextBox.Text=editDesignationTextBox.Text="";


            editDepartmentDropDownList.DataSource = _hospitalDepartmentManager.GetAllDepartment();
            editDepartmentDropDownList.DataTextField = "Name";
            editDepartmentDropDownList.DataValueField = "Id";
            editDepartmentDropDownList.DataBind();
            editDepartmentDropDownList.Items.Insert(0, "");
            editDepartmentDropDownList.SelectedIndex = -1;
        }



        // Delete In Databse
        // Grid Button
        protected void deleteLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton dl = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)dl.NamingContainer;
                Label lblID = doctorGridView.Rows[gvr.DataItemIndex].FindControl("idLabel") as Label;
                int id = Convert.ToInt32(lblID.Text);

                idHiddenField.Value = id.ToString();
                deleteModalPopupExtender.Show();
            }
            catch
            {

            }
           
        }

        // popup button
        protected void delete1LinkButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(idHiddenField.Value);

            _doctorManager.Delete(id);
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