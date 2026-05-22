using HospitalBilling.BLL;
using HospitalBilling.Models;
using System;
using System.Web.UI.WebControls;

namespace HospitalBilling.UI
{
    public partial class ReferenceByForm : System.Web.UI.Page
    {
        private readonly ReferenceByManager _referenceByManager;
        public ReferenceByForm()
        {
            _referenceByManager = new ReferenceByManager();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                referenceGridView.DataSource = _referenceByManager.GetAllReferenceByList();
                referenceGridView.DataBind();
            }
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Person Name!!');", true);
                nameTextBox.Focus();
            }
            else
            {
                var reference = new ReferenceBy()
                {
                    Name = nameTextBox.Text
                };
                int rowAffected = _referenceByManager.Save(reference);
                if (rowAffected > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Successfully save Reference in Database!!');", true);
                    Refress();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Reference save Fail!!');", true);

                }
            }
        }



        protected void updateButton_Click(object sender, EventArgs e)
        {
            if (idHiddenField.Value == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select Person First Then Update!!');", true);
            }
            else if (nameTextBox.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Person Name!!');", true);
                nameTextBox.Focus();
            }
            else
            {
                var reference = new ReferenceBy()
                {
                    Id = Convert.ToInt32(idHiddenField.Value),
                    Name = nameTextBox.Text
                };
                int rowAffected = _referenceByManager.Update(reference);
                if (rowAffected > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Successfully Update Reference in Database!!');", true);
                    Refress();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Reference Update Fail!!');", true);

                }
            }
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            if (idHiddenField.Value == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select Person First Then Update!!');", true);
            }
            else
            {
                ReferenceBy aSurgeryType=new ReferenceBy();
                aSurgeryType.Id = Convert.ToInt32(idHiddenField.Value);

                int rowAffected = _referenceByManager.Delete(aSurgeryType);
                if (rowAffected > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Successfully Delete Reference in Database!!');", true);
                    Refress();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Reference Delete Fail!!');", true);

                }
            }
        }

        protected void organizationGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idLeable = ((Label)referenceGridView.SelectedRow.FindControl("idLabel")).Text;
            int id = Convert.ToInt32(idLeable);

            var reference = _referenceByManager.GetReferenceById(id);
            if (reference != null)
            {
                idHiddenField.Value = reference.Id.ToString();
                nameTextBox.Text = reference.Name;
            }
        }

        private void Refress()
        {
            idHiddenField.Value = nameTextBox.Text = "";

            referenceGridView.DataSource = _referenceByManager.GetAllReferenceByList();
            referenceGridView.DataBind();
        }

        protected void reloadButton_Click(object sender, EventArgs e)
        {
            var pageName = System.IO.Path.GetFileName(Request.Url.ToString());
            Response.Redirect(pageName);
        }
    }
}