using HospitalBilling.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalBilling.UI
{
    public partial class DiagnosisBillReportForm : System.Web.UI.Page
    {

       
        private readonly DiagnosisBillManager _diagnosisBillManager;

        public DiagnosisBillReportForm()
        {
            _diagnosisBillManager = new DiagnosisBillManager();
         
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginUserId"] != null && Session["LoginUserRole"] != null)
            {
                if (!IsPostBack)
                {
                    startDateTextBox.Text = endTextBox.Text= DateTime.Now.ToString("dd/MM/yyyy");
                    
                }
            }
            else
            {
                Response.Redirect("../LoginForm.aspx");
            }
        }

        protected void searchLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(startDateTextBox.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please Start Date!!');", true);
                    startDateTextBox.Focus();
                }
                if (string.IsNullOrEmpty(endTextBox.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please End Date!!');", true);
                    startDateTextBox.Focus();
                }
                DataTable data = _diagnosisBillManager.GetTotalDiagnosisBillSummery(startDateTextBox.Text, endTextBox.Text, typeDropDownList.SelectedValue);

                DiagnosisBillSummeryGridView.DataSource = data;
                DiagnosisBillSummeryGridView.DataBind();
                ShowFooterTotal(data);
            }
            catch
            {

            }
        }


        private void ShowFooterTotal(DataTable dtStock)
        {

            decimal totPrice = 0, totDiscount = 0, totTotalPayableAmount = 0, totTotalDiagnosis = 0, totDamages = 0, totLost = 0, totGift = 0, totClosingStock = 0, totCartonStock = 0;


            totPrice = Convert.ToDecimal(dtStock.Compute("Sum(Price)", ""));
            totDiscount = Convert.ToDecimal(dtStock.Compute("Sum(Discount)", ""));
            totTotalPayableAmount = Convert.ToDecimal(dtStock.Compute("Sum(TotalPayableAmount)", ""));
            totTotalDiagnosis = Convert.ToDecimal(dtStock.Compute("Sum(TotalDiagnosis)", ""));


            System.Web.UI.WebControls.GridViewRow row = new System.Web.UI.WebControls.GridViewRow(0, 0, System.Web.UI.WebControls.DataControlRowType.Footer, System.Web.UI.WebControls.DataControlRowState.Normal);
            System.Web.UI.WebControls.TableCell cell;
            cell = new System.Web.UI.WebControls.TableCell();
            cell.Text = "<h4>Total</h4>";
             cell.ColumnSpan = 2;
            cell.Font.Bold = true;
            cell.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            row.Cells.Add(cell);

            
             cell = new System.Web.UI.WebControls.TableCell();
            cell.Text = "<h4>" + totTotalDiagnosis.ToString("N0") + "</h4>";
            cell.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            row.Cells.Add(cell);

            cell = new System.Web.UI.WebControls.TableCell();
            cell.Text = "<h4>" + totPrice.ToString("N0") + "</h4>";
            cell.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            row.Cells.Add(cell);

            cell = new System.Web.UI.WebControls.TableCell();
            cell.Text = "<h4>" + totDiscount.ToString("N2") + "</h4>";
            cell.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            row.Cells.Add(cell);


            cell = new System.Web.UI.WebControls.TableCell();
            cell.Text = "<h4>" + totTotalPayableAmount.ToString("N2") + "</h4>";
            cell.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            row.Cells.Add(cell);

           

            row.Font.Bold = true;
            row.BackColor = System.Drawing.Color.LightGray;
            if (DiagnosisBillSummeryGridView.Rows.Count > 0)
            {
                DiagnosisBillSummeryGridView.Controls[0].Controls.Add(row);
            }

        }

    }
}