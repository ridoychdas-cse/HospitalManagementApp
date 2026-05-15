using HospitalManager.Library.SetupForm;
using HospitalModels.Library.Billings;
using System;
using System.Web.UI.WebControls;

namespace HospitalBilling.UI
{
    public partial class OtherBillSetupForm : System.Web.UI.Page
    {
        private readonly OtherBillManager _otherBillManager;

        public OtherBillSetupForm()
        {
            _otherBillManager = new OtherBillManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Refress();
            }
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Bill Name!!');", true);
                nameTextBox.Focus();
            }
            else
            {
                var otherBill = new OtherBillType()
                {
                    Name = nameTextBox.Text
                };

                int rowAffected = _otherBillManager.Save(otherBill);
                if (rowAffected > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Successfully save Other Bill!!');", true);
                    Refress();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Save Fail in Database!!');", true);
                }
            }
        }

        private void Refress()
        {
            idHiddenField.Value = nameTextBox.Text = "";

            otherBillGridView.DataSource = _otherBillManager.GetAllOtherBills();
            otherBillGridView.DataBind();
        }

        protected void updateButton_Click(object sender, EventArgs e)
        {
            if (idHiddenField.Value == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select Bill Name First Then Update!!');", true);
            }
            else if (nameTextBox.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Bill Name!!');", true);
                nameTextBox.Focus();
            }
            else
            {
                int id = Convert.ToInt32(idHiddenField.Value);
                var otherBill = new OtherBillType()
                {
                    Id = id,
                    Name = nameTextBox.Text
                };

                int rowAffected = _otherBillManager.Update(id, otherBill);
                if (rowAffected > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Successfully Update Other Bill!!');", true);
                    Refress();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Update Fail in Database!!');", true);
                }

            }
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            if (idHiddenField.Value == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select Bill Name First Then Update!!');", true);
            }
            else if (nameTextBox.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Bill Name!!');", true);
                nameTextBox.Focus();
            }
            else
            {
                int id = Convert.ToInt32(idHiddenField.Value);
                int rowAffected = _otherBillManager.Delete(id);
                if (rowAffected > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Successfully Delete Other Bill!!');", true);
                    Refress();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Delete Fail in Database!!');", true);
                }
            }
        }

        protected void reloadButton_Click(object sender, EventArgs e)
        {
            var pageName = System.IO.Path.GetFileName(Request.Url.ToString());
            Response.Redirect(pageName);
        }

        protected void otherBillGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idLeable = ((Label)otherBillGridView.SelectedRow.FindControl("idLabel")).Text;
            int id = Convert.ToInt32(idLeable);

            var bill = _otherBillManager.GetOtherBillById(id);
            if (bill == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Can not Find Other Bill in Database!!');", true);
            }
            else
            {
                idHiddenField.Value = bill.Id.ToString();
                nameTextBox.Text = bill.Name;
            }
        }
    }
}