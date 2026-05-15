using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using HospitalBilling.BLL;
using HospitalBilling.Models;

namespace HospitalBilling.UI
{
    public partial class PackageSetupForm : System.Web.UI.Page
    {
        private readonly PackageTypeManager _packageTypeManager=new PackageTypeManager();
        private readonly PackageManager _packageManager=new PackageManager();
        private readonly DiagnosisManager _diagnosisManager=new DiagnosisManager();
        private readonly SurgeryManager _surgeryManager=new SurgeryManager();

        private List<ServiceType> GetAllServiceTypes()
        {
            var serviceList = new List<ServiceType>
            {
                new ServiceType() { Id = 1, Name = "Diagnosis"},
                new ServiceType(){Id = 2, Name = "Surgery"}
            };
            return serviceList;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUserId"] != null && Session["LoginUserRole"] != null)
            {
                if (!IsPostBack)
                {
                    RefressDtl();



                    packageGridView.DataSource = _packageManager.GetPackageMstsList();
                    packageGridView.DataBind();

                    packageTypeDropDownList.DataSource = _packageTypeManager.GetAllPackageTypes();
                    packageTypeDropDownList.DataTextField = "Name";
                    packageTypeDropDownList.DataValueField = "Id";
                    packageTypeDropDownList.DataBind();

                    editPackageTypeDropDownList.DataSource = _packageTypeManager.GetAllPackageTypes();
                    editPackageTypeDropDownList.DataTextField = "Name";
                    editPackageTypeDropDownList.DataValueField = "Id";
                    editPackageTypeDropDownList.DataBind();

                    serviceTypeDropDownList.DataSource = GetAllServiceTypes();
                    serviceTypeDropDownList.DataTextField = "Name";
                    serviceTypeDropDownList.DataValueField = "Id";
                    serviceTypeDropDownList.DataBind();
                    serviceTypeDropDownList.Items.Insert(0, "Select Service Type");
                    serviceTypeDropDownList.SelectedIndex = -1;
                }
            }
            else
            {
                Response.Redirect("../LoginForm.aspx");
            }
        }

        protected void serviceTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = serviceTypeDropDownList.SelectedItem.Text;

            if (name == "Diagnosis")
            {
                serviceDropDownList.DataSource = _diagnosisManager.GetAllDiagnosesList();
                serviceDropDownList.DataTextField = "Name";
                serviceDropDownList.DataValueField = "Id";
                serviceDropDownList.DataBind();
            }
            else if (name == "Surgery")
            {
                serviceDropDownList.DataSource = _surgeryManager.GetAllSurgeryList();
                serviceDropDownList.DataTextField = "Name";
                serviceDropDownList.DataValueField = "Id";
                serviceDropDownList.DataBind();
            }
        }

        private void RefressDtl()
        {
            ViewState["PackageDtl"] = null;
            DataTable packageDtlDataTable = new DataTable();
            packageDtlDataTable.Columns.Add("ServiceType");
            packageDtlDataTable.Columns.Add("ServiceId");
            packageDtlDataTable.Columns.Add("ServiceName");

            ViewState["PackageDtl"] = packageDtlDataTable;
        }

        // Search
        protected void searchLinkButton_Click(object sender, EventArgs e)
        {
            if (searchTextBox.Text == "")
            {
                packageGridView.DataSource = _packageManager.GetPackageMstsList();
                packageGridView.DataBind();
            }
            else
            {
                var aPackage = _packageManager.GetPackageMstsListByName(searchTextBox.Text);
                var packageMstList = new List<PackageMst>();
                packageMstList.Add(aPackage);
                packageGridView.DataSource = packageMstList;
                packageGridView.DataBind();

            }

        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(serviceTypeDropDownList.SelectedValue))
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Select Service Type.";
            }
            else if (String.IsNullOrEmpty(serviceDropDownList.SelectedValue))
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Select Service Name.";
            }
            else
            {
                var aPackageDtl = new PackageDtl();
                aPackageDtl.ServiceType = serviceTypeDropDownList.SelectedItem.Text;
                aPackageDtl.ServiceId = Convert.ToInt32(serviceDropDownList.SelectedValue);
                aPackageDtl.ServiceName = serviceDropDownList.SelectedItem.Text;

                DataTable dtList = _packageManager.GetPackageDtlList(aPackageDtl, ViewState["PackageDtl"]);
                packageDtlGridView.DataSource = dtList;
                ViewState["PackageDtl"] = dtList;
                packageDtlGridView.DataBind();
            }
            
        }

        // ********************************Start Save In  Database
        protected void saveLinkButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(packageTypeDropDownList.SelectedValue))
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Select Package Type.";
            }
            else if (nameTextBox.Text == "")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Insert Package Name.";
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
                var aPackageMst = new PackageMst();
                aPackageMst.Name = nameTextBox.Text;
                aPackageMst.ShortName = shortNameTextBox.Text;
                aPackageMst.PackageTypeId = Convert.ToInt32(packageTypeDropDownList.SelectedValue);
                aPackageMst.Description = descriptionTextBox.Text;
                aPackageMst.RegularFee = Convert.ToDecimal(regularFeeTextBox.Text);
                aPackageMst.Discount = Convert.ToDecimal(discountTextBox.Text);
                aPackageMst.TotalFee = Convert.ToDecimal(totalFeeTextBox.Text);

                bool isNameExist = _packageManager.IsNameExist(aPackageMst.Name);
                if (isNameExist)
                {
                    messageLabel.CssClass = "alert alert-warning";
                    messageLabel.Text = "<strong>Warning!</strong> This Name Alredy Exist in Database.";
                }
                else
                {
                    DataTable packageDtl = (DataTable)ViewState["PackageDtl"];

                    int rowAffected = _packageManager.Save(aPackageMst, packageDtl);
                    if (rowAffected > 0)
                    {
                        Refress();
                        messageLabel.CssClass = "alert alert-success";
                        messageLabel.Text = "<strong>Success!</strong> Save Package in Database.";
                    }
                    else
                    {
                        messageLabel.CssClass = "alert alert-warning";
                        messageLabel.Text = "<strong>Fail!</strong> Can'not Save Package in Database.";
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
            nameTextBox.Text = shortNameTextBox.Text = discountTextBox.Text = descriptionTextBox.Text = "";
            regularFeeTextBox.Text = discountTextBox.Text = totalFeeTextBox.Text = "0";

            packageGridView.DataSource = _packageManager.GetPackageMstsList();
            packageGridView.DataBind();

            packageTypeDropDownList.DataSource = _packageTypeManager.GetAllPackageTypes();
            packageTypeDropDownList.DataTextField = "Name";
            packageTypeDropDownList.DataValueField = "Id";
            packageTypeDropDownList.DataBind();

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
            Label lblID = packageGridView.Rows[gvr.DataItemIndex].FindControl("idLabel") as Label;
            int id = Convert.ToInt32(lblID.Text);

            var aSurgery = _packageManager.GetPackageMstsListById(id);

            idHiddenField.Value = aSurgery.Id.ToString();
            editNameTextBox.Text = aSurgery.Name;
            editShortNameTextBox.Text = aSurgery.ShortName;
            editDescriptionTextBox.Text = aSurgery.Description;
            editPackageTypeDropDownList.SelectedValue = aSurgery.PackageTypeId.ToString();
            editRegularFeeTextBox.Text = aSurgery.RegularFee.ToString();
            editDiscountTextBox.Text = aSurgery.Discount.ToString();
            editTotalFeeTextBox.Text = aSurgery.TotalFee.ToString();


            editModalPopupExtender.Show();
        }
        // Popup Button
        protected void updateLinkButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(editPackageTypeDropDownList.SelectedValue))
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Select Package Type.";
            }
            else if (editNameTextBox.Text == "")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Insert Package Name.";
            }
            else if (editRegularFeeTextBox.Text == "")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Insert Regular Fee.";
            }
            else if (editDiscountTextBox.Text == "")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Insert Disount Fee.";
            }
            else if (editTotalFeeTextBox.Text == "")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Insert Total Fee.";
            }
            else
            {
                int id = Convert.ToInt32(idHiddenField.Value);
                var aSurgery = new PackageMst();
                aSurgery.Id = id;
                aSurgery.Name = editNameTextBox.Text;
                aSurgery.ShortName = editShortNameTextBox.Text;
                aSurgery.PackageTypeId = Convert.ToInt32(editPackageTypeDropDownList.SelectedValue);
                aSurgery.Description = editDescriptionTextBox.Text;
                aSurgery.RegularFee = Convert.ToDecimal(editRegularFeeTextBox.Text);
                aSurgery.Discount = Convert.ToDecimal(editRegularFeeTextBox.Text);
                aSurgery.TotalFee = Convert.ToDecimal(editTotalFeeTextBox.Text);

                int rowAffected = _packageManager.Update(id, aSurgery, new DataTable("S"));
                if (rowAffected > 0)
                {
                    EditPopupRefress();
                    message2Label.CssClass = "alert alert-success";
                    message2Label.Text = "<strong>Success!</strong> Update Package in Database.";
                }
                else
                {
                    message2Label.CssClass = "alert alert-warning";
                    message2Label.Text = "<strong>Fail!</strong> Can'not Update Package in Database.";
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

            editPackageTypeDropDownList.DataSource = _packageTypeManager.GetAllPackageTypes();
            editPackageTypeDropDownList.DataTextField = "Name";
            editPackageTypeDropDownList.DataValueField = "Id";
            editPackageTypeDropDownList.DataBind();
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
            Label lblID = packageGridView.Rows[gvr.DataItemIndex].FindControl("idLabel") as Label;
            int id = Convert.ToInt32(lblID.Text);

            idHiddenField.Value = id.ToString();
            deleteModalPopupExtender.Show();
        }
        // popup button
        protected void delete1LinkButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(idHiddenField.Value);

            _packageManager.Delete(id);
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

        
        // Details

        protected void detailsLinkButton_Click(object sender, EventArgs e)
        {
            LinkButton dl = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)dl.NamingContainer;
            Label lblID = packageGridView.Rows[gvr.DataItemIndex].FindControl("idLabel") as Label;
            int id = Convert.ToInt32(lblID.Text);

            var packageDetails = _packageManager.GetAllPackageDtlsListByMstId(id);

            if (packageDetails!=null)
            {
                detailsModalPopupExtender.Show();
                packageDetailsGridView.DataSource = packageDetails;
                packageDetailsGridView.DataBind();
            }
        }

        protected void close4LinkButton_Click(object sender, EventArgs e)
        {
            detailsModalPopupExtender.Hide();
        }
        
    }
}