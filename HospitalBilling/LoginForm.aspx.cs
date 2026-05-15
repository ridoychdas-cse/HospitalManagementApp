using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HospitalBilling.BLL;
using HospitalBilling.Models;

namespace HospitalBilling
{
    public partial class LoginForm : System.Web.UI.Page
    {
        private readonly UserManager _userManager = new UserManager();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void loginButton_Click(object sender, EventArgs e)
        {
            if (userNameTextBox.Text == "")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = " Please Insert User Name.";
                userNameTextBox.Focus();
            }
            else if (passwordTextBox.Text == "")
            {
                messageLabel.CssClass = "alert alert-warning";
                messageLabel.Text = " Please Insert Password.";
                passwordTextBox.Focus();
            }
            else
            {
                User aUser = new User();
                aUser.UserName = userNameTextBox.Text.ToLower();
                aUser.Password = passwordTextBox.Text;

                var getUser = _userManager.GetUserByUserNameAndPassword(aUser.UserName, aUser.Password);

                if (getUser != null)
                {
                    Session["LoginUserId"] = getUser.Id;
                    Session["LoginUserRole"] = getUser.UserRoleId;
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    messageLabel.CssClass = "alert alert-danger";
                    messageLabel.Text = "Your User Name and Password not match.";
                    userNameTextBox.Text = passwordTextBox.Text = "";
                    userNameTextBox.Focus();
                }
            }
        }
    }
}