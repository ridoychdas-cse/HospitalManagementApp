using System;

namespace HospitalBilling
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUserId"] != null && Session["LoginUserRole"] != null)
            {
            }
            else
            {
                Response.Redirect("LoginForm.aspx");
            }
        }
    }
}