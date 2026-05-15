using iTextSharp.text;
using iTextSharp.text.pdf;
using ITextSharpLibrary;
using System;

namespace HospitalBilling.Report
{
    public partial class RptOutdoorTokenForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["LoginUserId"] != null && Session["LoginUserRole"] != null)
            //{
            //    string id, name;
            //    id = Session["patientId"].ToString();
            //    name = Session["name"].ToString();
            //    RunReport(id, name);
            //}
            //else
            //{
            //    Response.Redirect("/UI/Login.aspx");
            //}
        }

        private void RunReport(string patientId, string patientName)
        {
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment; filename=Outdoor Patient.pdf");
            Document document = new Document();
            document = new Document(PageSize.A4, 40f, 30f, 30f, 50f);
            PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);
            document.Open();
            PdfPCell cell;

            float[] titwidth = new float[2] { 20, 200 };
            PdfPTable dth = new PdfPTable(titwidth);
            dth.WidthPercentage = 100;

            cell = new PdfPCell(ITextSharpFont.H4Bold("Patient Id: "));
            cell.BorderWidth = 0f;
            //cell.FixedHeight = 10f;
            cell.HorizontalAlignment = 0;
            cell.VerticalAlignment = 1;
            dth.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H4Bold(patientId));
            cell.BorderWidth = 0f;
            //cell.FixedHeight = 10f;
            cell.HorizontalAlignment = 0;
            cell.VerticalAlignment = 1;
            dth.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H4Bold("Name: "));
            cell.BorderWidth = 0f;
            //cell.FixedHeight = 10f;
            cell.HorizontalAlignment = 0;
            cell.VerticalAlignment = 1;
            dth.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H4Bold(patientName));
            cell.BorderWidth = 0f;
            //cell.FixedHeight = 10f;
            cell.HorizontalAlignment = 0;
            cell.VerticalAlignment = 1;
            dth.AddCell(cell);

            document.Add(dth);

            document.Close();
            Response.Flush();
            Response.End();
        }

        protected void reportButton_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment; filename=Common Report " + "" + ".pdf");
            Document document = new Document();
            document = new Document(PageSize.A4, 40f, 30f, 30f, 50f);
            PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);
            document.Open();
            CommonReport commonReport = new CommonReport();
            commonReport.GetBillReport(document, writer, 2);

            document.Close();
            Response.Flush();
            Response.End();
        }

    }
}