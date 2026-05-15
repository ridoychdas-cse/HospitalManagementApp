using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using HospitalBilling.BLL;
using HospitalBilling.Models;

namespace HospitalBilling.UI
{
    public partial class SurgeryTypeSetupForm : System.Web.UI.Page
    {
        private readonly SurgeryTypeManager _surgeryTypeManager=new SurgeryTypeManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUserId"] != null && Session["LoginUserRole"] != null)
            {
                if (!IsPostBack)
                {
                    sureryTypeGridView.DataSource = _surgeryTypeManager.GetAllSurgeryTypesList();
                    sureryTypeGridView.DataBind();
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
                sureryTypeGridView.DataSource = _surgeryTypeManager.GetAllSurgeryTypesList();
                sureryTypeGridView.DataBind();
            }
            else
            {
                var aBed = _surgeryTypeManager.GetSurgeryTypesByName(searchTextBox.Text);
                List<SurgeryType> surgeryTypeList = new List<SurgeryType>();
                surgeryTypeList.Add(aBed);
                sureryTypeGridView.DataSource = surgeryTypeList;
                sureryTypeGridView.DataBind();

            }

        }


        // Save in Database
        protected void saveLinkButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text=="")
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning!</strong> Please Insert Surgery Type Name.";
            }
            else
            {
                var aSurgeryType = new SurgeryType();
                aSurgeryType.Name = nameTextBox.Text;
                aSurgeryType.ShortName = shortNameTextBox.Text;
                aSurgeryType.Description = descriptionTextBox.Text;

                bool isNameExist = _surgeryTypeManager.IsNameExist(aSurgeryType.Name);
                if (isNameExist)
                {
                    messageLabel.CssClass = "alert alert-warning";
                    messageLabel.Text = "<strong>Warning!</strong> This Name Alredy Exist in Database.";
                }
                else
                {
                    int rowAffected = _surgeryTypeManager.Save(aSurgeryType);
                    if (rowAffected > 0)
                    {
                        Refress();
                        messageLabel.CssClass = "alert alert-success";
                        messageLabel.Text = "<strong>Success!</strong> Save Surgery Type in Database.";
                    }
                    else
                    {
                        messageLabel.CssClass = "alert alert-warning";
                        messageLabel.Text = "<strong>Fail!</strong> Can'not Save Surgery Type in Database.";
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
            idHiddenField.Value = nameTextBox.Text = shortNameTextBox.Text = descriptionTextBox.Text = "";

            sureryTypeGridView.DataSource = _surgeryTypeManager.GetAllSurgeryTypesList();
            sureryTypeGridView.DataBind();
        }

        
        // Update in Databse

        // Grid button
        protected void EditLinkButton_Click(object sender, EventArgs e)
        {
            message2Label.Text = "";
            LinkButton dl = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)dl.NamingContainer;
            Label lblID = sureryTypeGridView.Rows[gvr.DataItemIndex].FindControl("idLabel") as Label;
            int id = Convert.ToInt32(lblID.Text);

            var aDiagnosisType = _surgeryTypeManager.GetSurgeryTypesById(id);

            idHiddenField.Value = aDiagnosisType.Id.ToString();
            editNameTextBox.Text = aDiagnosisType.Name;
            editShortNameTextBox.Text = aDiagnosisType.ShortName;
            editdescriptionTextBox.Text = aDiagnosisType.Description;

            editModalPopupExtender.Show();
        }

        // popup button
        protected void updateLinkButton_Click(object sender, EventArgs e)
        {
            if (editNameTextBox.Text == "")
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning!</strong> Please Insert Surgery Type Name.";
            }
            else
            {
                int id = Convert.ToInt32(idHiddenField.Value);
                SurgeryType aSurgeryType = new SurgeryType();
                aSurgeryType.Id = id;
                aSurgeryType.Name = editNameTextBox.Text;
                aSurgeryType.ShortName = editShortNameTextBox.Text;
                aSurgeryType.Description = editdescriptionTextBox.Text;

                int rowAffected = _surgeryTypeManager.Update(aSurgeryType);
                if (rowAffected > 0)
                {
                    EditPopupRefress();
                    message2Label.CssClass = "alert alert-success";
                    message2Label.Text = "<strong>Success!</strong> Update Surgery Type in Database.";
                }
                else
                {
                    message2Label.CssClass = "alert alert-warning";
                    message2Label.Text = "<strong>Fail!</strong> Can'not Update Surgery Type in Database.";
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
            idHiddenField.Value = editNameTextBox.Text = editShortNameTextBox.Text = editdescriptionTextBox.Text = "";

            sureryTypeGridView.DataSource = _surgeryTypeManager.GetAllSurgeryTypesList();
            sureryTypeGridView.DataBind();
        }

        // Delete in Database
        // Grid Button
        protected void deleteLinkButton_Click(object sender, EventArgs e)
        {
            LinkButton dl = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)dl.NamingContainer;
            Label lblID = sureryTypeGridView.Rows[gvr.DataItemIndex].FindControl("idLabel") as Label;
            int id = Convert.ToInt32(lblID.Text);

            idHiddenField.Value = id.ToString();
            deleteModalPopupExtender.Show();
        }

        // popup button
        protected void delete1LinkButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(idHiddenField.Value);

            _surgeryTypeManager.Delete(id);
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