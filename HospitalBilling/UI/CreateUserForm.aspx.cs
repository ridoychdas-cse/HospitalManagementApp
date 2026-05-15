using HospitalBilling.BLL;
using HospitalBilling.Models;
using System;
using System.Web.UI.WebControls;

namespace HospitalBilling.UI
{
    public partial class CreateUserForm : System.Web.UI.Page
    {
        private readonly UserManager _userManager = new UserManager();
        private readonly UserRoleManager _userRoleManager = new UserRoleManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUserId"] != null && Session["LoginUserRole"] != null)
            {
                if (!IsPostBack)
                {
                    userGridView.DataSource = _userManager.GetAllUserList();
                    userGridView.DataBind();

                    userRoleDropDownList.DataSource = _userRoleManager.GetAllUserRole();
                    userRoleDropDownList.DataTextField = "Name";
                    userRoleDropDownList.DataValueField = "Id";
                    userRoleDropDownList.DataBind();

                    editUserRoleDropDownList.DataSource = _userRoleManager.GetAllUserRole();
                    editUserRoleDropDownList.DataTextField = "Name";
                    editUserRoleDropDownList.DataValueField = "Id";
                    editUserRoleDropDownList.DataBind();
                }
            }
            else
            {
                Response.Redirect("../LoginForm.aspx");
            }
        }

        protected void searchLinkButton_Click(object sender, EventArgs e)
        {
            string searchInput = searchTextBox.Text;
            if (searchInput == "")
            {

                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert User Name or Phone No!!');", true);
                searchTextBox.Focus();
                userGridView.DataSource = _userManager.GetAllUserList();
                userGridView.DataBind();
            }
            else
            {
                var user = _userManager.GetUserListBySearchInput(searchInput);
                if (user != null)
                {
                    userGridView.DataSource = user;
                    userGridView.DataBind();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Can not fiend  User by this Name or Phone No!!');", true);
                    userGridView.DataSource = _userManager.GetAllUserList();
                    userGridView.DataBind();
                }
            }
        }

        // save in database
        protected void saveLinkButton_Click(object sender, EventArgs e)
        {
            User aUser = new User();
            aUser.FirstName = firstNameTextBox.Text;
            aUser.LastName = lastNameTextBox.Text;
            aUser.PhoneNo = phoneNoTextBox.Text;
            aUser.UserName = userNameTextBox.Text.ToLower();
            aUser.Password = passwordTextBox.Text;
            aUser.ConfirmPassword = confirmPasswordTextBox.Text;
            aUser.UserRoleId = Convert.ToInt32(userRoleDropDownList.SelectedValue);

            bool isNameExist = _userManager.IsNameExist(aUser.UserName);
            if (isNameExist)
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = "<strong>Warning!</strong> This Name Alredy Exist in Database.";
            }
            else
            {
                int rowAffected = _userManager.Save(aUser);
                if (rowAffected > 0)
                {
                    Refress();
                    messageLabel.CssClass = "alert alert-success";
                    messageLabel.Text = "<strong>Success!</strong> Save User in Database.";
                }
                else
                {
                    messageLabel.CssClass = "alert alert-warning";
                    messageLabel.Text = "<strong>Fail!</strong> Can'not Save User in Database.";
                }
            }
        }

        protected void clearLinkButton_Click(object sender, EventArgs e)
        {
            Refress();
        }

        private void Refress()
        {
            idHiddenField.Value =
                firstNameTextBox.Text =
                    lastNameTextBox.Text =
                        phoneNoTextBox.Text =
                            userNameTextBox.Text = passwordTextBox.Text = confirmPasswordTextBox.Text = "";

            userRoleDropDownList.DataSource = _userRoleManager.GetAllUserRole();
            userRoleDropDownList.DataTextField = "Name";
            userRoleDropDownList.DataValueField = "Id";
            userRoleDropDownList.DataBind();

            userGridView.DataSource = _userManager.GetAllUserList();
            userGridView.DataBind();
        }

        // Upadte In Database

        // Grid Button
        protected void EditLinkButton_Click(object sender, EventArgs e)
        {
            message2Label.Text = "";
            LinkButton dl = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)dl.NamingContainer;
            Label lblID = userGridView.Rows[gvr.DataItemIndex].FindControl("idLabel") as Label;
            int id = Convert.ToInt32(lblID.Text);

            var aUser = _userManager.GetUserById(id);
            idHiddenField.Value = aUser.Id.ToString();
            editFirstNameTextBox.Text = aUser.FirstName;
            editLastNameTextBox.Text = aUser.LastName;
            editPhoneNoTextBox.Text = aUser.PhoneNo;
            editUserNameTextBox.Text = aUser.UserName.ToLower();
            editPasswordTextBox.Text = aUser.Password;

            editUserRoleDropDownList.SelectedValue = aUser.UserRoleId.ToString();


            editModalPopupExtender.Show();
        }

        // Popup Button
        protected void updateLinkButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(idHiddenField.Value);

            var aUser = new User
            {
                Id = id,
                FirstName = editFirstNameTextBox.Text,
                LastName = editLastNameTextBox.Text,
                PhoneNo = editPhoneNoTextBox.Text,
                UserName = editUserNameTextBox.Text.ToLower(),
                Password = editPasswordTextBox.Text,
                ConfirmPassword = editConfirmPasswordTextBox.Text,
                UserRoleId = Convert.ToInt32(editUserRoleDropDownList.SelectedValue)
            };

            //bool isNameExist = _userManager.IsNameExist(aUser.UserName);
            //if (isNameExist)
            //{
            //    message2Label.CssClass = "alert alert-warning";
            //    message2Label.Text = "<strong>Warning!</strong> This Name Alredy Exist in Database.";
            //}
            //else
            //{
            int rowAffected = _userManager.Update(aUser.Id, aUser);
            if (rowAffected > 0)
            {
                EditPopupRefress();
                message2Label.CssClass = "alert alert-success";
                message2Label.Text = "<strong>Success!</strong> Update User in Database.";
            }
            else
            {
                message2Label.CssClass = "alert alert-warning";
                message2Label.Text = "<strong>Fail!</strong> Can'not Update User in Database.";
            }
            //}
        }

        protected void close2LinkButton_Click(object sender, EventArgs e)
        {
            var pageName = System.IO.Path.GetFileName(Request.Url.ToString());
            Response.Redirect(pageName);
        }

        private void EditPopupRefress()
        {
            idHiddenField.Value =
                editFirstNameTextBox.Text =
                    editLastNameTextBox.Text =
                        editPhoneNoTextBox.Text =
                            editUserNameTextBox.Text = editPasswordTextBox.Text = editConfirmPasswordTextBox.Text = "";

            editUserRoleDropDownList.DataSource = _userRoleManager.GetAllUserRole();
            editUserRoleDropDownList.DataTextField = "Name";
            editUserRoleDropDownList.DataValueField = "Id";
            editUserRoleDropDownList.DataBind();

            userGridView.DataSource = _userManager.GetAllUserList();
            userGridView.DataBind();
        }


        // Delete In Databse
        // Grid Button
        protected void deleteLinkButton_Click(object sender, EventArgs e)
        {
            LinkButton dl = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)dl.NamingContainer;
            Label lblID = userGridView.Rows[gvr.DataItemIndex].FindControl("idLabel") as Label;
            int id = Convert.ToInt32(lblID.Text);

            _userManager.Delete(id);
            DeleteRefress();

            //idHiddenField.Value = id.ToString();

            //deleteModalPopupExtender.Show();
        }

        // popup button
        protected void delete1LinkButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(idHiddenField.Value);

            _userManager.Delete(id);
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