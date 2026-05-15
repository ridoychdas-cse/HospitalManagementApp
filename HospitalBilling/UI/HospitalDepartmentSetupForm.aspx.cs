using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using HospitalBilling.BLL;
using HospitalBilling.Models;

namespace HospitalBilling.UI
{
    public partial class HospitalDepartmentSetupForm : System.Web.UI.Page
    {
        private readonly HospitalDepartmentManager _hospitalDepartmentManager=new HospitalDepartmentManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUserId"] != null && Session["LoginUserRole"] != null)
            {
                if (!IsPostBack)
                {
                    hospitalDepartmentGridView.DataSource = _hospitalDepartmentManager.GetAllDepartment();
                    hospitalDepartmentGridView.DataBind();
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
            if (searchTextBox.Text == "")
            {
                hospitalDepartmentGridView.DataSource = _hospitalDepartmentManager.GetAllDepartment();
                hospitalDepartmentGridView.DataBind();

                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Department Name or Short Name!!');", true);
                searchTextBox.Focus();
            }
            else
            {
                var departmentList = _hospitalDepartmentManager.GetDepartmentByNameOrShortName(searchTextBox.Text);
                if (departmentList.Count>0)
                {
                    hospitalDepartmentGridView.DataSource = departmentList;
                    hospitalDepartmentGridView.DataBind();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Can not Fiend Department in This Name!!');", true);
                    
                }

            }

        }


        // Save in Database
        protected void saveLinkButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text=="")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning</strong> Please Insert Department Name!.";
                nameTextBox.Focus();
            }
            else if (shortNameTextBox.Text == "")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning</strong> Please Insert Department Short Name!.";
                shortNameTextBox.Focus();
            }
            else
            {
                bool isShortNameExist = _hospitalDepartmentManager.IsShortNameExist(shortNameTextBox.Text);
                if (isShortNameExist)
                {
                    messageLabel.CssClass = "alert alert-danger";
                    messageLabel.Text = "<strong>Fail!</strong> Department ShortName Already in Database.";
                }
                else
                {
                    HospitalDepartment aHospitalDepartment = new HospitalDepartment();
                    aHospitalDepartment.Name = nameTextBox.Text;
                    aHospitalDepartment.ShortName = shortNameTextBox.Text;
                    aHospitalDepartment.Details = detailsTextBox.Text;

                    int rowAffected = _hospitalDepartmentManager.Save(aHospitalDepartment);
                    if (rowAffected > 0)
                    {
                        Refress();
                        messageLabel.CssClass = "alert alert-success";
                        messageLabel.Text = "<strong>Success!</strong> Save Department in Database.";
                    }
                    else
                    {
                        messageLabel.CssClass = "alert alert-danger";
                        messageLabel.Text = "<strong>Fail!</strong> Can'not Save Department in Database.";
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
            nameTextBox.Text = shortNameTextBox.Text= detailsTextBox.Text = "";

            hospitalDepartmentGridView.DataSource = _hospitalDepartmentManager.GetAllDepartment();
            hospitalDepartmentGridView.DataBind();
        }

        // Update in databse
        // grid button
        protected void EditLinkButton_Click(object sender, EventArgs e)
        {
            message2Label.Text = "";
            LinkButton dl = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)dl.NamingContainer;
            Label lblID = hospitalDepartmentGridView.Rows[gvr.DataItemIndex].FindControl("idLabel") as Label;
            int id = Convert.ToInt32(lblID.Text);

            var aDepartment = _hospitalDepartmentManager.GetDepartmentById(id);
            idHiddenField.Value = aDepartment.Id.ToString();
            editNameTextBox.Text = aDepartment.Name;
            editShortTextBox.Text = aDepartment.ShortName;
            editDetailsTextBox.Text = aDepartment.Details;

            editModalPopupExtender.Show();
        }
        // popup

        protected void updateLinkButton_Click(object sender, EventArgs e)
        {
            if (editNameTextBox.Text == "")
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning</strong> Please Insert Department Name!.";
            }
            else if (editShortTextBox.Text=="")
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning</strong> Please Insert Department Short Name!.";
            }
            else
            {
                
                    HospitalDepartment aHospitalDepartment = new HospitalDepartment();
                    aHospitalDepartment.Id = Convert.ToInt32(idHiddenField.Value);
                    aHospitalDepartment.Name = editNameTextBox.Text;
                    aHospitalDepartment.ShortName = editShortTextBox.Text;
                    aHospitalDepartment.Details = editDetailsTextBox.Text;

                    int rowAffected = _hospitalDepartmentManager.Update(aHospitalDepartment);
                    if (rowAffected > 0)
                    {
                        EditPopupRefress();
                        message2Label.CssClass = "alert alert-success";
                        message2Label.Text = "<strong>Success!</strong> Update Department in Database.";
                    }
                    else
                    {
                        message2Label.CssClass = "alert alert-danger";
                        message2Label.Text = "<strong>Fail!</strong> Can'not Update Department in Database.";
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
            idHiddenField.Value = editNameTextBox.Text = editShortTextBox.Text= editDetailsTextBox.Text = "";
        }

        // delete in databse

        // grid button
        protected void deleteLinkButton_Click(object sender, EventArgs e)
        {
            LinkButton dl = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)dl.NamingContainer;
            Label lblID = hospitalDepartmentGridView.Rows[gvr.DataItemIndex].FindControl("idLabel") as Label;
            int id = Convert.ToInt32(lblID.Text);

            idHiddenField.Value = id.ToString();

            deleteModalPopupExtender.Show();
        }

        // popup button
        protected void delete1LinkButton_Click(object sender, EventArgs e)
        {
           
            HospitalDepartment objHospitalDepartment = new HospitalDepartment();
            objHospitalDepartment.Id= Convert.ToInt32(idHiddenField.Value);
            _hospitalDepartmentManager.Delete(objHospitalDepartment);
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