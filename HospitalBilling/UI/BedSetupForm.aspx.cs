using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using HospitalBilling.BLL;
using HospitalBilling.Models;

namespace HospitalBilling.UI
{
    public partial class BedSetupForm : System.Web.UI.Page
    {
        private readonly BedManager _bedManager=new BedManager();
        private readonly WardManager _wardManager=new WardManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUserId"] != null && Session["LoginUserRole"] !=null)
            {
                if (!IsPostBack)
                {
                    bedGridView.DataSource = _bedManager.GetAllBedList();
                    bedGridView.DataBind();

                    wardDropDownList.DataSource = _wardManager.GetAllWardList();
                    wardDropDownList.DataTextField = "Name";
                    wardDropDownList.DataValueField = "Id";
                    wardDropDownList.DataBind();
                    wardDropDownList.Items.Insert(0, "");
                    wardDropDownList.SelectedIndex = -1;

                    editWardDropDownList.DataSource = _wardManager.GetAllWardList();
                    editWardDropDownList.DataTextField = "Name";
                    editWardDropDownList.DataValueField = "Id";
                    editWardDropDownList.DataBind();
                    editWardDropDownList.Items.Insert(0, "");
                    editWardDropDownList.SelectedIndex = -1;
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
            if (searchTextBox.Text=="")
            {
                bedGridView.DataSource = _bedManager.GetAllBedList();
                bedGridView.DataBind();

                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Bed Number or Name!!');", true);
                searchTextBox.Focus();
            }
            else
            {
                var aBedList = _bedManager.GetbedByNameOrWardNo(searchTextBox.Text);
                if (aBedList.Count>0)
                {
                    bedGridView.DataSource = aBedList;
                    bedGridView.DataBind();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Can not Find Bed in This Number or Name!!');", true);
                    searchTextBox.Focus();
                }

            }

        }

        // save in database
        protected void saveLinkButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text=="" || nameTextBox.Text=="B-")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Insert Bed Name / Number.";
            }
            else if (String.IsNullOrEmpty(wardDropDownList.SelectedValue))
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Select Ward Number.";                     
            }
            else if (priceDailyTextBox.Text=="")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> Please Insert Bed Price.";
            }
            else
            {
                Bed aBed = new Bed();
                aBed.Name = nameTextBox.Text;
                aBed.WardId = Convert.ToInt32(wardDropDownList.SelectedValue);
                aBed.PriceDaily = Convert.ToDecimal(priceDailyTextBox.Text);
                aBed.Status = false;

                bool isNameExist = _bedManager.IsNameExist(aBed.Name, aBed.WardId);
                if (isNameExist)
                {
                    messageLabel.CssClass = "alert alert-warning";
                    messageLabel.Text = "<strong>Warning!</strong> This Name Alredy Exist in Database.";
                }
                else
                {
                    int rowAffected = _bedManager.Save(aBed);
                    if (rowAffected > 0)
                    {
                        Refress();
                        messageLabel.CssClass = "alert alert-success";
                        messageLabel.Text = "<strong>Success!</strong> Save Bed in Database.";
                    }
                    else
                    {
                        messageLabel.CssClass = "alert alert-warning";
                        messageLabel.Text = "<strong>Fail!</strong> Can'not Save Bed in Database.";
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
            nameTextBox.Text = "B-";
            priceDailyTextBox.Text = "0";

            bedGridView.DataSource = _bedManager.GetAllBedList();
            bedGridView.DataBind();

            wardDropDownList.DataSource = _wardManager.GetAllWardList();
            wardDropDownList.DataTextField = "Name";
            wardDropDownList.DataValueField = "Id";
            wardDropDownList.DataBind();
            wardDropDownList.Items.Insert(0, "");
            wardDropDownList.SelectedIndex = -1;
        }

        // Upadte In Database

        // Grid Button
        protected void EditLinkButton_Click(object sender, EventArgs e)
        {
            message2Label.Text = "";
            LinkButton dl = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)dl.NamingContainer;
            Label lblID = bedGridView.Rows[gvr.DataItemIndex].FindControl("idLabel") as Label;
            int id = Convert.ToInt32(lblID.Text);

            var aBed = _bedManager.GetBedById(id);
            idHiddenField.Value = aBed.Id.ToString();
            editNameTextBox.Text = aBed.Name;
            if (aBed.WardId!=0)
            {
                editWardDropDownList.SelectedValue = aBed.WardId.ToString();
            }
            editPriceDailyTextBox.Text = aBed.PriceDaily.ToString();

            editModalPopupExtender.Show();
        }

        // Popup Button
        protected void updateLinkButton_Click(object sender, EventArgs e)
        {
            if (editNameTextBox.Text == "" || editNameTextBox.Text=="B-")
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning!</strong> Please Insert Bed Name / Number.";
            }
            else if (String.IsNullOrEmpty(editWardDropDownList.SelectedValue))
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning!</strong> Please Select Ward Number.";
            }
            else if (editPriceDailyTextBox.Text == "")
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Warning!</strong> Please Insert Bed Price.";
            }
            else
            {
                int id = Convert.ToInt32(idHiddenField.Value);

                Bed aBed = new Bed();
                aBed.Id = id;
                aBed.Name = editNameTextBox.Text;
                aBed.WardId = Convert.ToInt32(editWardDropDownList.SelectedValue);
                aBed.PriceDaily = Convert.ToDecimal(editPriceDailyTextBox.Text);

                //bool isNameExist = _bedManager.IsNameExist(aBed.Name, aBed.WardId);
                //if (isNameExist)
                //{
                //    message2Label.CssClass = "alert alert-warning";
                //    message2Label.Text = "<strong>Warning!</strong> This Name Alredy Exist in Database.";
                //}
                //else
                //{
                int rowAffected = _bedManager.Update(aBed);
                if (rowAffected > 0)
                {
                    EditPopupRefress();
                    message2Label.CssClass = "alert alert-success";
                    message2Label.Text = "<strong>Success!</strong> Update Bed in Database.";
                }
                else
                {
                    message2Label.CssClass = "alert alert-warning";
                    message2Label.Text = "<strong>Fail!</strong> Can'not Update Bed in Database.";
                }
                //}
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
            editNameTextBox.Text = "B-";
            editPriceDailyTextBox.Text = "0";

            editWardDropDownList.DataSource = _wardManager.GetAllWardList();
            editWardDropDownList.DataTextField = "Name";
            editWardDropDownList.DataValueField = "Id";
            editWardDropDownList.DataBind();
            editWardDropDownList.Items.Insert(0, "");
            editWardDropDownList.SelectedIndex = -1;
        }

        

        // Delete In Databse
        // Grid Button
        protected void deleteLinkButton_Click(object sender, EventArgs e)
        {
            LinkButton dl = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)dl.NamingContainer;
            Label lblID = bedGridView.Rows[gvr.DataItemIndex].FindControl("idLabel") as Label;
            int id = Convert.ToInt32(lblID.Text);

            idHiddenField.Value = id.ToString();
            deleteModalPopupExtender.Show();
        }

        // popup button
        protected void delete1LinkButton_Click(object sender, EventArgs e)
        {
            Bed aBed = new Bed();
            aBed.Id= Convert.ToInt32(idHiddenField.Value);
            _bedManager.Delete(aBed);
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