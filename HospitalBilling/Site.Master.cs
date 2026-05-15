using System;

namespace HospitalBilling
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void logoutLinkButton_Click(object sender, EventArgs e)
        {
            Session["LoginUserId"] = null;
            Session["LoginUserRole"] = null;

            // visual Studio
            //Response.Redirect("/LoginForm.aspx");

            // IIS
            Response.Redirect("~/LoginForm.aspx");
        }
    }
}