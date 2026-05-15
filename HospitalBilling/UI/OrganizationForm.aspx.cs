using HospitalBilling.BLL;
using HospitalBilling.Models;
using System;
using System.Web.UI.WebControls;

namespace HospitalBilling.UI
{
    public partial class OrganizationForm : System.Web.UI.Page
    {
        private readonly OrganizationManager _organizationManager;

        public OrganizationForm()
        {
            _organizationManager = new OrganizationManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var organizations = _organizationManager.GetOrganizationsList();

                if (organizations.Count > 0)
                {
                    saveButton.Visible = false;
                }
                organizationGridView.DataSource = organizations;
                organizationGridView.DataBind();
            }
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            if (orgNameTextBox.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Organization Name!!');", true);
                orgNameTextBox.Focus();
            }
            else if (phoneNoTextBox.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Organization Phone No!!');", true);
                phoneNoTextBox.Focus();
            }
            else if (addressTextBox.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Organization Address!!');", true);
                addressTextBox.Focus();
            }
            else
            {
                var organization = new Organization()
                {
                    Name = orgNameTextBox.Text,
                    ShortName = orgShortNameTextBox.Text,
                    PhoneNo = phoneNoTextBox.Text,
                    Email = emailTextBox.Text,
                    Address = addressTextBox.Text,
                    OrganizationSpeech = speechTextBox.Text,
                    Description = descriptionTextBox.Text
                };


                if (ViewState["patientImage"] != null)
                {
                    organization.Image = (byte[])ViewState["patientImage"];
                }

                int rowAffected = _organizationManager.Save(organization);

                if (rowAffected > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Successfully save Organization in Database!!');", true);
                    Refress();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Organization save Fail!!');", true);

                }
            }

        }




        protected void organizationGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idLeable = ((Label)organizationGridView.SelectedRow.FindControl("idLabel")).Text;
            int id = Convert.ToInt32(idLeable);

            var organization = _organizationManager.GetOrganizationById(id);

            if (organization != null)
            {
                idHiddenField.Value = organization.Id.ToString();
                orgNameTextBox.Text = organization.Name;
                orgShortNameTextBox.Text = organization.ShortName;
                phoneNoTextBox.Text = organization.PhoneNo;
                emailTextBox.Text = organization.Email;
                addressTextBox.Text = organization.Address;
                speechTextBox.Text = organization.OrganizationSpeech;
                descriptionTextBox.Text = organization.Description;

            }

        }

        protected void updateButton_Click(object sender, EventArgs e)
        {
            if (idHiddenField.Value == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select an Organization Then Update!!');", true);

            }
            else if (orgNameTextBox.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Organization Name!!');", true);
                orgNameTextBox.Focus();
            }
            else if (phoneNoTextBox.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Organization Phone No!!');", true);
                phoneNoTextBox.Focus();
            }
            else if (addressTextBox.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Organization Address!!');", true);
                addressTextBox.Focus();
            }
            else
            {

                var organization = new Organization()
                {
                    Name = orgNameTextBox.Text,
                    ShortName = orgShortNameTextBox.Text,
                    PhoneNo = phoneNoTextBox.Text,
                    Email = emailTextBox.Text,
                    Address = addressTextBox.Text,
                    OrganizationSpeech = speechTextBox.Text,
                    Description = descriptionTextBox.Text
                };

                if (ViewState["patientImage"] != null)
                {
                    organization.Image = (byte[])ViewState["patientImage"];
                }

                var id = Convert.ToInt32(idHiddenField.Value);
                int rowAffected = _organizationManager.Update(organization, id);
                if (rowAffected > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Successfully Update Organization in Database!!');", true);
                    Refress();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Organization Update Fail!!');", true);

                }
            }
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            if (idHiddenField.Value == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Select an Organization Then Update!!');", true);

            }
            else
            {
                int id = Convert.ToInt32(idHiddenField.Value);
                int rowAffected = _organizationManager.DeleteUpdate(id);
                if (rowAffected > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Successfully Delete Organization in Database!!');", true);
                    Refress();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Organization Delete Fail!!');", true);

                }
            }
        }

        private void Refress()
        {
            idHiddenField.Value = "";

            orgNameTextBox.Text =
                orgShortNameTextBox.Text =
                    phoneNoTextBox.Text =
                        emailTextBox.Text = addressTextBox.Text = speechTextBox.Text = descriptionTextBox.Text = "";

            organizationGridView.DataSource = _organizationManager.GetOrganizationsList();
            organizationGridView.DataBind();


        }

        protected void reloadButton_Click(object sender, EventArgs e)
        {
            var pageName = System.IO.Path.GetFileName(Request.Url.ToString());
            Response.Redirect(pageName);
        }

        protected void lbImgUpload_Click(object sender, EventArgs e)
        {
            try
            {

                const int width = 145;
                const int height = 165;
                byte[] stdphoto;
                using (var img = DataManager.ResizeImage(new System.Drawing.Bitmap(imageFileUpload.PostedFile.InputStream), width, height, DataManager.ResizeOptions.ExactWidthAndHeight))
                {
                    imageFileUpload.PostedFile.InputStream.Close();
                    stdphoto = DataManager.ConvertImageToByteArray(img, System.Drawing.Imaging.ImageFormat.Png);
                    ViewState["patientImage"] = stdphoto;
                    img.Dispose();
                }
                //string base64String = Convert.ToBase64String(stdphoto, 0, stdphoto.Length);
                //patientImage.ImageUrl = "data:image/png;base64," + base64String;


            }
            catch (FormatException fex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Warning", "alert('" + fex.Message + "');", true);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Database"))
                    ClientScript.RegisterStartupScript(this.GetType(), "Warning", "alert('Database Maintain Error. Contact to the Software Provider..!!');", true);
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "Warning", "alert('There is some problem to load image please select jpeg png etc format. Try again properly.!!');", true);
            }
        }
    }
}