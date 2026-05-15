using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using HospitalBilling.BLL;
using HospitalBilling.Models;

namespace HospitalBilling.UI
{
    public partial class SurgerySetupForm : System.Web.UI.Page
    {
        private readonly SurgeryManager _surgeryManager=new SurgeryManager();
        private readonly SurgeryTypeManager _surgeryTypeManager=new SurgeryTypeManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUserId"] != null && Session["LoginUserRole"] != null)
            {
                if (!IsPostBack)
                {
                    surgeryGridView.DataSource = _surgeryManager.GetAllSurgeryList();
                    surgeryGridView.DataBind();

                    surgeryTypeDropDownList.DataSource = _surgeryTypeManager.GetAllSurgeryTypesList();
                    surgeryTypeDropDownList.DataTextField = "Name";
                    surgeryTypeDropDownList.DataValueField = "Id";
                    surgeryTypeDropDownList.DataBind();

                    editSurgeryTypeDropDownList.DataSource = _surgeryTypeManager.GetAllSurgeryTypesList();
                    editSurgeryTypeDropDownList.DataTextField = "Name";
                    editSurgeryTypeDropDownList.DataValueField = "Id";
                    editSurgeryTypeDropDownList.DataBind();
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
                surgeryGridView.DataSource = _surgeryManager.GetAllSurgeryList();
                surgeryGridView.DataBind();
            }
            else
            {
                var aBed = _surgeryManager.GetSurgeryByName(searchTextBox.Text);
                List<Surgery> surgeryList = new List<Surgery>();
                surgeryList.Add(aBed);
                surgeryGridView.DataSource = surgeryList;
                surgeryGridView.DataBind();

            }

        }


        // ********************************Start Save In  Database
        protected void saveLinkButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text == "")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Type Name.";
            }
            else if (String.IsNullOrEmpty(surgeryTypeDropDownList.SelectedValue))
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Select Sergery Type.";
            }
            else if (regularFeeTextBox.Text == "")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Insert Regular Fee.";
            }
            else if (discountTextBox.Text == "")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Insert Disount Fee.";
            }
            else if (totalFeeTextBox.Text == "")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Insert Total Fee.";
            }
            else
            {
                Surgery aSurgery = new Surgery();
                aSurgery.Name = nameTextBox.Text;
                aSurgery.ShortName = shortNameTextBox.Text;
                aSurgery.SurgeryTypeId = Convert.ToInt32(surgeryTypeDropDownList.SelectedValue);
                aSurgery.Description = descriptionTextBox.Text;
                aSurgery.RegularFee = Convert.ToDecimal(regularFeeTextBox.Text);
                aSurgery.Discount = Convert.ToDecimal(discountTextBox.Text);
                aSurgery.TotalFee = Convert.ToDecimal(totalFeeTextBox.Text);

                bool isNameExist = _surgeryManager.IsNameExist(aSurgery.Name);
                if (isNameExist)
                {
                    messageLabel.CssClass = "alert alert-warning";
                    messageLabel.Text = "<strong>Warning!</strong> This Name Alredy Exist in Database.";
                }
                else
                {
                    int rowAffected = _surgeryManager.Save(aSurgery);
                    if (rowAffected > 0)
                    {
                        Refress();
                        messageLabel.CssClass = "alert alert-success";
                        messageLabel.Text = "<strong>Success!</strong> Save Surgery in Database.";
                    }
                    else
                    {
                        messageLabel.CssClass = "alert alert-warning";
                        messageLabel.Text = "<strong>Fail!</strong> Can'not Save Surgery in Database.";
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
            nameTextBox.Text = shortNameTextBox.Text= discountTextBox.Text = descriptionTextBox.Text= "";
            regularFeeTextBox.Text = discountTextBox.Text = totalFeeTextBox.Text = "0";

            surgeryGridView.DataSource = _surgeryManager.GetAllSurgeryList();
            surgeryGridView.DataBind();

            surgeryTypeDropDownList.DataSource = _surgeryTypeManager.GetAllSurgeryTypesList();
            surgeryTypeDropDownList.DataTextField = "Name";
            surgeryTypeDropDownList.DataValueField = "Id";
            surgeryTypeDropDownList.DataBind();

        }
        protected void regularFeeTextBox_TextChanged(object sender, EventArgs e)
        {
            totalFeeTextBox.Text = regularFeeTextBox.Text;
        }

        protected void discountTextBox_TextChanged(object sender, EventArgs e)
        {
            decimal regularFee = Convert.ToDecimal(regularFeeTextBox.Text);
            decimal discountFee = Convert.ToDecimal(discountTextBox.Text);
            decimal totalFee = regularFee - discountFee;
            if (totalFee > 0)
            {
                totalFeeTextBox.Text = totalFee.ToString();
            }
            else
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Your Total Fee Less Then 0.";
            }
        }
        // End Save

        // ****************************************Update In Database

        // Grid View Button
        protected void EditLinkButton_Click(object sender, EventArgs e)
        {
            message2Label.Text = "";
            LinkButton dl = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)dl.NamingContainer;
            Label lblID = surgeryGridView.Rows[gvr.DataItemIndex].FindControl("idLabel") as Label;
            int id = Convert.ToInt32(lblID.Text);

            var aSurgery = _surgeryManager.GetSurgeryById(id);

            idHiddenField.Value = aSurgery.Id.ToString();
            editNameTextBox.Text = aSurgery.Name;
            editShortNameTextBox.Text = aSurgery.ShortName;
            editDescriptionTextBox.Text = aSurgery.Description;
            editSurgeryTypeDropDownList.SelectedValue = aSurgery.SurgeryTypeId.ToString();
            editRegularFeeTextBox.Text = aSurgery.RegularFee.ToString();
            editDiscountTextBox.Text = aSurgery.Discount.ToString();
            editTotalFeeTextBox.Text = aSurgery.TotalFee.ToString();


            editModalPopupExtender.Show();
        }
        // Popup Button
        protected void updateLinkButton_Click(object sender, EventArgs e)
        {
            if (editNameTextBox.Text == "")
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning!</strong> Please Type Name.";
            }
            else if (String.IsNullOrEmpty(editSurgeryTypeDropDownList.SelectedValue))
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning!</strong> Please Select Diagnosis Type.";
            }
            else if (editRegularFeeTextBox.Text == "")
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning!</strong> Please Insert Regular Fee.";
            }
            else if (editDiscountTextBox.Text == "")
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning!</strong> Please Insert Disount Fee.";
            }
            else if (editTotalFeeTextBox.Text == "")
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning!</strong> Please Insert Total Fee.";
            }
            else
            {
                int id = Convert.ToInt32(idHiddenField.Value);
                Surgery aSurgery = new Surgery();
                aSurgery.Id = id;
                aSurgery.Name = editNameTextBox.Text;
                aSurgery.ShortName = editShortNameTextBox.Text;
                aSurgery.SurgeryTypeId = Convert.ToInt32(editSurgeryTypeDropDownList.SelectedValue);
                aSurgery.Description = editDescriptionTextBox.Text;
                aSurgery.RegularFee = Convert.ToDecimal(editRegularFeeTextBox.Text);
                aSurgery.Discount = Convert.ToDecimal(editRegularFeeTextBox.Text);
                aSurgery.TotalFee = Convert.ToDecimal(editTotalFeeTextBox.Text);

                int rowAffected = _surgeryManager.Update(aSurgery);
                if (rowAffected > 0)
                {
                    EditPopupRefress();
                    message2Label.CssClass = "alert alert-success";
                    message2Label.Text = "<strong>Success!</strong> Update Surgery in Database.";
                }
                else
                {
                    message2Label.CssClass = "alert alert-warning";
                    message2Label.Text = "<strong>Fail!</strong> Can'not Update Surgery in Database.";
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
            idHiddenField.Value = editNameTextBox.Text = editDescriptionTextBox.Text = "";
            editRegularFeeTextBox.Text = editDiscountTextBox.Text = editTotalFeeTextBox.Text = "";

            editSurgeryTypeDropDownList.DataSource = _surgeryTypeManager.GetAllSurgeryTypesList();
            editSurgeryTypeDropDownList.DataTextField = "Name";
            editSurgeryTypeDropDownList.DataValueField = "Id";
            editSurgeryTypeDropDownList.DataBind();
        }

        protected void editRegularFeeTextBox_TextChanged(object sender, EventArgs e)
        {
            editTotalFeeTextBox.Text = editRegularFeeTextBox.Text;
        }

        protected void editDiscountTextBox_TextChanged(object sender, EventArgs e)
        {
            decimal regularFee = Convert.ToDecimal(editRegularFeeTextBox.Text);
            decimal discountFee = Convert.ToDecimal(editDiscountTextBox.Text);
            decimal totalFee = regularFee - discountFee;
            if (totalFee > 0)
            {
                editTotalFeeTextBox.Text = totalFee.ToString();
            }
            else
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning!</strong> Your Total Fee Less Then 0.";
            }
        }

        // end Update

        // **********************************************Delete in Database
        // grid View button
        protected void deleteLinkButton_Click(object sender, EventArgs e)
        {
            LinkButton dl = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)dl.NamingContainer;
            Label lblID = surgeryGridView.Rows[gvr.DataItemIndex].FindControl("idLabel") as Label;
            int id = Convert.ToInt32(lblID.Text);

            idHiddenField.Value = id.ToString();
            deleteModalPopupExtender.Show();
        }
        // popup button
        protected void delete1LinkButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(idHiddenField.Value);

            _surgeryManager.Delete(id);
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