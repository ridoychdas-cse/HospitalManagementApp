using HospitalManager.Library;
using System;
using System.Linq;

namespace HospitalBilling.UI
{
    public partial class DailyMoneyCalclutionForm : System.Web.UI.Page
    {
        private readonly MoneyReceiveHistoryManager _mrHistoryManager;

        public DailyMoneyCalclutionForm()
        {
            _mrHistoryManager = new MoneyReceiveHistoryManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void searchLinkButton_Click(object sender, EventArgs e)
        {
           try
           {
               if (startDateTextBox.Text == "" || endDateTextBox.Text == "")
               {
                   ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Insert Date!!');", true);
                   startDateTextBox.Focus();
               }
               else
               {
                   string startDate = startDateTextBox.Text;
                   string endDate = endDateTextBox.Text;

                   var mrHistory = _mrHistoryManager.GetAllByDate(startDate, endDate).ToList();
                   if (mrHistory.Count > 0)
                   {
                       decimal total = mrHistory.Sum(moneyReceiveHistory => moneyReceiveHistory.PayAmount);

                       totalTextBox.Text = total + ".00";

                       moneyReceiveGridView.DataSource = mrHistory;
                       moneyReceiveGridView.DataBind();
                   }
                   else
                   {
                       ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Can not Find Money Receive in This Date!!');", true);
                       startDateTextBox.Focus();
                   }

               }
           }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n" + ex, true);
            }
        }
    }
}