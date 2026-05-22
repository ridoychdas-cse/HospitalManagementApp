using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using HospitalBilling.BLL;
using HospitalBilling.Models;

namespace HospitalBilling.UI
{
    public partial class PackageTypeSetupForm : System.Web.UI.Page
    {
        private readonly PackageTypeManager _packageTypeManager=new PackageTypeManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUserId"] != null && Session["LoginUserRole"] != null)
            {
                if (!IsPostBack)
                {
                    packageTypeGridView.DataSource = _packageTypeManager.GetAllPackageTypes();
                    packageTypeGridView.DataBind();
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
                packageTypeGridView.DataSource = _packageTypeManager.GetAllPackageTypes();
                packageTypeGridView.DataBind();
            }
            else
            {
                var packageType = _packageTypeManager.GetAllPackageTypesByName(searchTextBox.Text);
                List<PackageType> packageTypeList = new List<PackageType>();
                packageTypeList.Add(packageType);
                packageTypeGridView.DataSource = packageTypeList;
                packageTypeGridView.DataBind();

            }
        }

        // Save in Database
        protected void saveLinkButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text=="")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Insert Type Name.";
            }
            else
            {
                var aPackageType = new PackageType();
                aPackageType.Name = nameTextBox.Text;
                aPackageType.ShortName = shortNameTextBox.Text;
                aPackageType.Description = descriptionTextBox.Text;

                bool isNameExist = _packageTypeManager.IsNameExist(aPackageType.Name);
                if (isNameExist)
                {
                    messageLabel.CssClass = "alert alert-warning";
                    messageLabel.Text = "<strong>Warning!</strong> This Name Alredy Exist in Database.";
                }
                else
                {
                    int rowAffected = _packageTypeManager.Save(aPackageType);
                    if (rowAffected > 0)
                    {
                        Refress();
                        messageLabel.CssClass = "alert alert-success";
                        messageLabel.Text = "<strong>Success!</strong> Save Package Type in Database.";
                    }
                    else
                    {
                        messageLabel.CssClass = "alert alert-warning";
                        messageLabel.Text = "<strong>Fail!</strong> Can'not Save Package Type in Database.";
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

            packageTypeGridView.DataSource = _packageTypeManager.GetAllPackageTypes();
            packageTypeGridView.DataBind();
        }

        // Update in Databse

        // Grid button
        protected void EditLinkButton_Click(object sender, EventArgs e)
        {
            message2Label.Text = "";
            LinkButton dl = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)dl.NamingContainer;
            Label lblID = packageTypeGridView.Rows[gvr.DataItemIndex].FindControl("idLabel") as Label;
            int id = Convert.ToInt32(lblID.Text);

            var aPackageType = _packageTypeManager.GetAllPackageTypesById(id);

            idHiddenField.Value = aPackageType.Id.ToString();
            editNameTextBox.Text = aPackageType.Name;
            editShortNameTextBox.Text = aPackageType.ShortName;
            editdescriptionTextBox.Text = aPackageType.Description;

            editModalPopupExtender.Show();
        }

        // popup button
        protected void updateLinkButton_Click(object sender, EventArgs e)
        {
            if (editNameTextBox.Text == "")
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning!</strong> Please Insert Type Name.";
            }
            else
            {

                PackageType aPackageType = new PackageType();
                aPackageType.Id = Convert.ToInt32(idHiddenField.Value);
                aPackageType.Name = editNameTextBox.Text;
                aPackageType.ShortName = editShortNameTextBox.Text;
                aPackageType.Description = editdescriptionTextBox.Text;

                int rowAffected = _packageTypeManager.Update(aPackageType);
                if (rowAffected > 0)
                {
                    EditPopupRefress();
                    message2Label.CssClass = "alert alert-success";
                    message2Label.Text = "<strong>Success!</strong> Update Package Type in Database.";
                }
                else
                {
                    message2Label.CssClass = "alert alert-warning";
                    message2Label.Text = "<strong>Fail!</strong> Can'not Update Package Type in Database.";
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

            packageTypeGridView.DataSource = _packageTypeManager.GetAllPackageTypes();
            packageTypeGridView.DataBind();
        }

        // Delete in Database
        // Grid Button
        protected void deleteLinkButton_Click(object sender, EventArgs e)
        {
            LinkButton dl = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)dl.NamingContainer;
            Label lblID = packageTypeGridView.Rows[gvr.DataItemIndex].FindControl("idLabel") as Label;
            int id = Convert.ToInt32(lblID.Text);

            idHiddenField.Value = id.ToString();
            deleteModalPopupExtender.Show();
        }

        // popup button
        protected void delete1LinkButton_Click(object sender, EventArgs e)
        {

            PackageType aPackageType = new PackageType();
            aPackageType.Id = Convert.ToInt32(idHiddenField.Value);
            _packageTypeManager.Delete(aPackageType);
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