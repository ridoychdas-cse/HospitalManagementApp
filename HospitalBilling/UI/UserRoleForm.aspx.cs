using HospitalBilling.BLL;
using HospitalBilling.Models;
using System;
using System.Web.UI.WebControls;

namespace HospitalBilling.UI
{
    public partial class UserRoleForm : System.Web.UI.Page
    {
        private readonly UserRoleManager _userRoleManager;

        public UserRoleForm()
        {
            _userRoleManager = new UserRoleManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUserId"] != null && Session["LoginUserRole"] != null)
            {
                roleGridView.DataSource = _userRoleManager.GetAllUserRole();
                roleGridView.DataBind();
            }
            else
            {
                Response.Redirect("../LoginForm.aspx");
            }

        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Role Name!!');", true);
                nameTextBox.Focus();
            }
            else
            {
                var role = new UserRole()
                {
                    Name = nameTextBox.Text,
                    Description = descriptionTextBox.Text
                };
                int rowAffected = _userRoleManager.Save(role);
                if (rowAffected > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Successfully save User Role in Database!!');", true);
                    Refress();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('User Role save Fail!!');", true);

                }
            }
        }

        private void Refress()
        {
            idHiddenField.Value = nameTextBox.Text = descriptionTextBox.Text = "";

            roleGridView.DataSource = _userRoleManager.GetAllUserRole();
            roleGridView.DataBind();
        }

        protected void updateButton_Click(object sender, EventArgs e)
        {
            if (idHiddenField.Value == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select Role Name First Then Update!!');", true);

            }
            else if (nameTextBox.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Role Name!!');", true);
                nameTextBox.Focus();
            }
            else
            {
                int id = Convert.ToInt32(idHiddenField.Value);
                var role = new UserRole()
                {
                    Id = id,
                    Name = nameTextBox.Text,
                    Description = descriptionTextBox.Text
                };
                int rowAffected = _userRoleManager.Update(role);
                if (rowAffected > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Successfully Update User Role in Database!!');", true);
                    Refress();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('User Role Update Fail!!');", true);

                }
            }
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            if (idHiddenField.Value == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select Role Name First Then Delete!!');", true);

            }
            else
            {
                UserRole role = new UserRole();
                role.Id = Convert.ToInt32(idHiddenField.Value);
                int rowAffected = _userRoleManager.Delete(role);
                if (rowAffected > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Successfully Delete User Role in Database!!');", true);
                    Refress();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('User Role Delete Fail!!');", true);

                }
            }
        }

        protected void organizationGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idLeable = ((Label)roleGridView.SelectedRow.FindControl("idLabel")).Text;
            int id = Convert.ToInt32(idLeable);

            var role = _userRoleManager.GetUserRoleById(id);
            if (role != null)
            {
                idHiddenField.Value = role.Id.ToString();
                nameTextBox.Text = role.Name;
                descriptionTextBox.Text = role.Description;
            }
        }

        protected void reloadButton_Click(object sender, EventArgs e)
        {
            var pageName = System.IO.Path.GetFileName(Request.Url.ToString());
            Response.Redirect(pageName);
        }

    }
}