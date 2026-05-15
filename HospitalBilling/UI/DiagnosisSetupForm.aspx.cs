using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using HospitalBilling.BLL;
using HospitalBilling.Models;

namespace HospitalBilling.UI
{
    public partial class DiagnosisSetupForm : System.Web.UI.Page
    {
        private readonly DiagnosisManager _diagnosisManager=new DiagnosisManager();
        private readonly DiagnosisTypeManager _diagnosisTypeManager=new DiagnosisTypeManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUserId"] != null && Session["LoginUserRole"] != null)
            {
                if (!IsPostBack)
                {
                    diagnosisGridView.DataSource = _diagnosisManager.GetAllDiagnosesList();
                    diagnosisGridView.DataBind();


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
            diagnosisTypeDropDownList.DataSource = _diagnosisTypeManager.GetAllDiagnosisTypes();
            diagnosisTypeDropDownList.DataTextField = "Name";
            diagnosisTypeDropDownList.DataValueField = "Id";
            diagnosisTypeDropDownList.DataBind();
            diagnosisTypeDropDownList.Items.Insert(0, "");
            diagnosisTypeDropDownList.SelectedIndex = -1;

            UOMDropDownList.DataSource = _diagnosisTypeManager.GetAllUOM();
            UOMDropDownList.DataTextField = "Name";
            UOMDropDownList.DataValueField = "Id";
            UOMDropDownList.DataBind();
            UOMDropDownList.Items.Insert(0, "");
            UOMDropDownList.SelectedIndex = -1;

            editDiagnosisTypeDropDownList.DataSource = _diagnosisTypeManager.GetAllDiagnosisTypes();
            editDiagnosisTypeDropDownList.DataTextField = "Name";
            editDiagnosisTypeDropDownList.DataValueField = "Id";
            editDiagnosisTypeDropDownList.DataBind();
            editDiagnosisTypeDropDownList.Items.Insert(0, "");
            editDiagnosisTypeDropDownList.SelectedIndex = -1;

            editUOMDropDownList.DataSource = _diagnosisTypeManager.GetAllUOM();
            editUOMDropDownList.DataTextField = "Name";
            editUOMDropDownList.DataValueField = "Id";
            editUOMDropDownList.DataBind();
            editUOMDropDownList.Items.Insert(0, "");
            editUOMDropDownList.SelectedIndex = -1;
        }

        // Search
        protected void searchLinkButton_Click(object sender, EventArgs e)
        {
            if (searchTextBox.Text == "")
            {
                diagnosisGridView.DataSource = _diagnosisManager.GetAllDiagnosesList();
                diagnosisGridView.DataBind();

                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Daignosis  Name Or Diagnsosi Type Name!!');", true);
                searchTextBox.Focus();
            }
            else
            {
                var diagnosisList = _diagnosisManager.GetDiagnosesByNameOrType(searchTextBox.Text);
                if (diagnosisList.Count>0)
                {
                    diagnosisGridView.DataSource = diagnosisList;
                    diagnosisGridView.DataBind();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Can not Find Diagnosis in This Name!!');", true);
                    searchTextBox.Focus();
                }

            }

        }

        // ********************************Start Save In  Database
        protected void saveLinkButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text == "")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Insert Type Name.";
                nameTextBox.Focus();
            }
            else if (String.IsNullOrEmpty(diagnosisTypeDropDownList.SelectedValue))
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Select Diagnosis Type.";
                diagnosisTypeDropDownList.Focus();
            }
            else if(regularFeeTextBox.Text=="")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Insert Regular Fee.";
                regularFeeTextBox.Focus();
            }
            //else if(discountTextBox.Text=="")
            //{
            //    messageLabel.CssClass = "alert alert-warning";
            //    messageLabel.Text = "<strong>Warning!</strong> Please Insert Disount Fee.";
            //}
            else if (totalFeeTextBox.Text == "")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Insert Total Fee.";
                regularFeeTextBox.Focus();
            }
            else
            {
                bool isNameExist = _diagnosisManager.IsNameExist(nameTextBox.Text);
                if (isNameExist)
                {
                    messageLabel.CssClass = "alert alert-danger";
                    messageLabel.Text = "<strong>Warning!</strong> This Name Already in Database.";
                    nameTextBox.Focus();
                }
                else
                {
                    Diagnosis aDiagnosis = new Diagnosis();
                    aDiagnosis.Name = nameTextBox.Text;
                    aDiagnosis.DiagnosisTypeId = Convert.ToInt32(diagnosisTypeDropDownList.SelectedValue);
                    aDiagnosis.Discription = descriptionTextBox.Text;
                    aDiagnosis.NormalValue = normalValueTextBox.Text;
                    aDiagnosis.RegularFee = Convert.ToDecimal(regularFeeTextBox.Text);
                    if (UOMDropDownList.SelectedValue == "")
                    {
                        aDiagnosis.UomId = 0;
                    }
                    else
                    {
                         aDiagnosis.UomId = Convert.ToInt32(UOMDropDownList.SelectedValue);
                    }
                   
                    if (discountTextBox.Text == "")
                    {
                        aDiagnosis.Discount = 0;
                    }
                    else
                    {
                        aDiagnosis.Discount = Convert.ToDecimal(discountTextBox.Text);
                    }
                    aDiagnosis.TotalFee = Convert.ToDecimal(totalFeeTextBox.Text);

                    int rowAffected = _diagnosisManager.Save(aDiagnosis);
                    if (rowAffected > 0)
                    {
                        Refress();
                        messageLabel.CssClass = "alert alert-success";
                        messageLabel.Text = "<strong>Success!</strong> Save Diagnosis in Database.";
                    }
                    else
                    {
                        messageLabel.CssClass = "alert alert-warning";
                        messageLabel.Text = "<strong>Fail!</strong> Can'not Save Diagnosis in Database.";
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
            nameTextBox.Text = discountTextBox.Text = descriptionTextBox.Text=normalValueTextBox.Text= "";
            regularFeeTextBox.Text = discountTextBox.Text = totalFeeTextBox.Text = "0";

            LoadAllDropdownList();

            diagnosisGridView.DataSource = _diagnosisManager.GetAllDiagnosesList();
            diagnosisGridView.DataBind();

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
            Label lblID = diagnosisGridView.Rows[gvr.DataItemIndex].FindControl("idLabel") as Label;
            int id = Convert.ToInt32(lblID.Text);

            var aDiagnosis = _diagnosisManager.GetDiagnosesById(id);

            idHiddenField.Value = aDiagnosis.Id.ToString();
            editNameTextBox.Text = aDiagnosis.Name;
            editDescriptionTextBox.Text = aDiagnosis.Discription;
            if (aDiagnosis.DiagnosisTypeId!=0)
            {
                editDiagnosisTypeDropDownList.SelectedValue = aDiagnosis.DiagnosisTypeId.ToString();
                
            }
            if (aDiagnosis.UomId != 0)
            {

                editUOMDropDownList.SelectedValue = aDiagnosis.UomId.ToString();

            }
            editRegularFeeTextBox.Text = aDiagnosis.RegularFee.ToString();
            editDiscountTextBox.Text = aDiagnosis.Discount.ToString();
            editTotalFeeTextBox.Text = aDiagnosis.TotalFee.ToString();
            editnormalValueTextBox.Text = aDiagnosis.NormalValue;

            editModalPopupExtender.Show();
        }
        // Popup Button
        protected void updateLinkButton_Click(object sender, EventArgs e)
        {
            if (editNameTextBox.Text == "")
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning!</strong> Please Insert Type Name.";
            }
            else if (String.IsNullOrEmpty(editDiagnosisTypeDropDownList.SelectedValue))
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning!</strong> Please Select Diagnosis Type.";
            }
            else if (editRegularFeeTextBox.Text == "")
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning!</strong> Please Insert Regular Fee.";
            }
            //else if (editDiscountTextBox.Text == "")
            //{
            //    message2Label.CssClass = "alert alert-warning";
            //    message2Label.Text = "<strong>Warning!</strong> Please Insert Disount Fee.";
            //}
            else if (editTotalFeeTextBox.Text == "")
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning!</strong> Please Insert Total Fee.";
            }
            else
            {
                int id = Convert.ToInt32(idHiddenField.Value);
                Diagnosis aDiagnosis = new Diagnosis();
                aDiagnosis.Id = id;
                aDiagnosis.Name = editNameTextBox.Text;
                aDiagnosis.DiagnosisTypeId = Convert.ToInt32(editDiagnosisTypeDropDownList.SelectedValue);
                aDiagnosis.Discription = editDescriptionTextBox.Text;
                aDiagnosis.NormalValue = editnormalValueTextBox.Text;
                aDiagnosis.RegularFee = Convert.ToDecimal(editRegularFeeTextBox.Text);
                if (editUOMDropDownList.SelectedValue == "")
                {
                    aDiagnosis.UomId = 0;
                }
                else
                {
                    aDiagnosis.UomId = Convert.ToInt32(editUOMDropDownList.SelectedValue);

                }
               
                if (editDiscountTextBox.Text=="")
                {
                    aDiagnosis.Discount = 0;
                }
                else
                {
                    aDiagnosis.Discount = Convert.ToDecimal(editDiscountTextBox.Text);
                    
                }
                aDiagnosis.TotalFee = Convert.ToDecimal(editTotalFeeTextBox.Text);

                int rowAffected = _diagnosisManager.Update(aDiagnosis);
                if (rowAffected > 0)
                {
                    EditPopupRefress();
                    message2Label.CssClass = "alert alert-success";
                    message2Label.Text = "<strong>Success!</strong> Update Diagnosis in Database.";
                }
                else
                {
                    message2Label.CssClass = "alert alert-warning";
                    message2Label.Text = "<strong>Fail!</strong> Can'not Update Diagnosis in Database.";
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
            idHiddenField.Value = editNameTextBox.Text = editDescriptionTextBox.Text =editnormalValueTextBox.Text= "";
            editRegularFeeTextBox.Text = editDiscountTextBox.Text = editTotalFeeTextBox.Text = "";

            LoadAllDropdownList();
        }

        protected void editRegularFeeTextBox_TextChanged(object sender, EventArgs e)
        {
            editTotalFeeTextBox.Text = editRegularFeeTextBox.Text;
            editRegularFeeTextBox.Text = "";

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
            Label lblID = diagnosisGridView.Rows[gvr.DataItemIndex].FindControl("idLabel") as Label;
            int id = Convert.ToInt32(lblID.Text);

            idHiddenField.Value = id.ToString();
            deleteModalPopupExtender.Show();
        }
        // popup button
        protected void delete1LinkButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(idHiddenField.Value);

            _diagnosisManager.Delete(id);
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