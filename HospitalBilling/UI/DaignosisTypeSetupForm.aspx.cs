using HospitalBilling.BLL;
using HospitalBilling.Models;
using System;
using System.Web.UI.WebControls;

namespace HospitalBilling.UI
{
    public partial class DaignosisTypeSetupForm : System.Web.UI.Page
    {
        private readonly DiagnosisTypeManager _diagnosisTypeManager = new DiagnosisTypeManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUserId"] != null && Session["LoginUserRole"] != null)
            {
                if (!IsPostBack)
                {
                    diagnosisTypeGridView.DataSource = _diagnosisTypeManager.GetAllDiagnosisTypes();
                    diagnosisTypeGridView.DataBind();
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
                diagnosisTypeGridView.DataSource = _diagnosisTypeManager.GetAllDiagnosisTypes();
                diagnosisTypeGridView.DataBind();

                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Daignosis Type Name or Short Name!!');", true);
                searchTextBox.Focus();
            }
            else
            {
                var diagnosisTypeList = _diagnosisTypeManager.GetDiagnosisTypeByNameOrShortName(searchTextBox.Text);

                if (diagnosisTypeList.Count > 0)
                {
                    diagnosisTypeGridView.DataSource = diagnosisTypeList;
                    diagnosisTypeGridView.DataBind();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Can not Find Diagnosis Type in This Name!!');", true);
                    searchTextBox.Focus();
                }
            }

        }

        // Popup Form Button
        protected void saveLinkButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text == "")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Insert Type Name.";
                nameTextBox.Focus();
            }
            else
            {
                bool isNameExist = _diagnosisTypeManager.IsNameExist(nameTextBox.Text);
                if (isNameExist)
                {
                    messageLabel.CssClass = "alert alert-danger";
                    messageLabel.Text = "<strong>Warning!</strong> This Name Already in Database.";
                    nameTextBox.Focus();
                }
                else
                {
                    var aDiagnosisType = new DiagnosisType();
                    aDiagnosisType.Name = nameTextBox.Text;
                    aDiagnosisType.ShortName = shortNameTextBox.Text;
                    aDiagnosisType.Details = detailsTextBox.Text;

                    int rowAffected = _diagnosisTypeManager.Save(aDiagnosisType);
                    if (rowAffected > 0)
                    {
                        Refress();
                        messageLabel.CssClass = "alert alert-success";
                        messageLabel.Text = "<strong>Success!</strong> Save Diagnosis Type in Database.";
                    }
                    else
                    {
                        messageLabel.CssClass = "alert alert-warning";
                        messageLabel.Text = "<strong>Fail!</strong> Can'not Save Diagnosis Type in Database.";
                    }
                }

            }

        }

        protected void updateLinkButton_Click(object sender, EventArgs e)
        {
            if (editNameTextBox.Text == "")
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning!</strong> Please Insert Type Name.";
                editNameTextBox.Focus();
            }
            else
            {
                int id = Convert.ToInt32(idHiddenField.Value);
                var aDiagnosisType = new DiagnosisType();
                aDiagnosisType.Id = id;
                aDiagnosisType.Name = editNameTextBox.Text;
                aDiagnosisType.ShortName = editShortNameTextBox.Text;
                aDiagnosisType.Details = editDetailsTextBox.Text;

                int rowAffected = _diagnosisTypeManager.Update(aDiagnosisType);
                if (rowAffected > 0)
                {
                    EditPopupRefress();
                    message2Label.CssClass = "alert alert-success";
                    message2Label.Text = "<strong>Success!</strong> Update Diagnosis Type in Database.";
                }
                else
                {
                    message2Label.CssClass = "alert alert-warning";
                    message2Label.Text = "<strong>Fail!</strong> Can'not Update Diagnosis Type in Database.";
                }
            }
        }

        protected void delete1LinkButton_Click(object sender, EventArgs e)
        {
           

            DiagnosisType aDiagnosisType = new DiagnosisType();
            aDiagnosisType.Id= Convert.ToInt32(idHiddenField.Value);
            _diagnosisTypeManager.Delete(aDiagnosisType);
            DeleteRefress();
            deleteModalPopupExtender.Hide();
        }

        // Close Button
        protected void closeLinkButton_Click(object sender, EventArgs e)
        {
            var pageName = System.IO.Path.GetFileName(Request.Url.ToString());
            Response.Redirect(pageName);
        }

        protected void close2LinkButton_Click(object sender, EventArgs e)
        {

            var pageName = System.IO.Path.GetFileName(Request.Url.ToString());
            Response.Redirect(pageName);
        }

        protected void close3LinkButton_Click(object sender, EventArgs e)
        {
            deleteModalPopupExtender.Hide();
            DeleteRefress();
        }
        // End Popup Form

        // Gridview Button ********************************
        protected void EditLinkButton_Click(object sender, EventArgs e)
        {
            message2Label.Text = "";
            LinkButton dl = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)dl.NamingContainer;
            Label lblID = diagnosisTypeGridView.Rows[gvr.DataItemIndex].FindControl("idLabel") as Label;
            int id = Convert.ToInt32(lblID.Text);

            var aDiagnosisType = _diagnosisTypeManager.GetDiagnosisTypeById(id);

            idHiddenField.Value = aDiagnosisType.Id.ToString();
            editNameTextBox.Text = aDiagnosisType.Name;
            editShortNameTextBox.Text = aDiagnosisType.ShortName;
            editDetailsTextBox.Text = aDiagnosisType.Details;

            editModalPopupExtender.Show();


        }

        protected void deleteLinkButton_Click(object sender, EventArgs e)
        {
            LinkButton dl = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)dl.NamingContainer;
            Label lblID = diagnosisTypeGridView.Rows[gvr.DataItemIndex].FindControl("idLabel") as Label;
            int id = Convert.ToInt32(lblID.Text);

            idHiddenField.Value = id.ToString();
            deleteModalPopupExtender.Show();

        }

        // End Gridview Button ********************************



        // --------------- Refress Form
        protected void clearLinkButton_Click(object sender, EventArgs e)
        {
            Refress();
        }

        private void Refress()
        {
            idHiddenField.Value = nameTextBox.Text = shortNameTextBox.Text = detailsTextBox.Text = "";

            diagnosisTypeGridView.DataSource = _diagnosisTypeManager.GetAllDiagnosisTypes();
            diagnosisTypeGridView.DataBind();
        }

        private void EditPopupRefress()
        {
            idHiddenField.Value = editNameTextBox.Text = editShortNameTextBox.Text = editDetailsTextBox.Text = "";

            diagnosisTypeGridView.DataSource = _diagnosisTypeManager.GetAllDiagnosisTypes();
            diagnosisTypeGridView.DataBind();
        }

        private void DeleteRefress()
        {
            var pageName = System.IO.Path.GetFileName(Request.Url.ToString());
            Response.Redirect(pageName);
        }
    }
}