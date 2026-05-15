using HospitalBilling.BLL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using ITextSharpLibrary;

namespace HospitalBilling.Report
{
    public class CommonReport
    {
        private readonly OrganizationManager _organizationManager;

        public CommonReport()
        {
            _organizationManager = new OrganizationManager();
        }

        public void GetBillReport(Document document, PdfWriter writer, int patientId)
        {
            var organization = _organizationManager.GetOrganizations();
            // Report

            // ******************************* Header ***************
            PdfPCell cell;
            var titwidth = new float[] { 80, 20 };

            var dth = new PdfPTable(titwidth) { WidthPercentage = 100 };

            cell = new PdfPCell(ITextSharpFont.H5Bold(organization.Name))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth.AddCell(cell);

            // Image Cell
            byte[] logo = _organizationManager.GetImageById(organization.Id); ;
            iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(logo);
            gif.Alignment = iTextSharp.text.Image.LEFT_ALIGN;
            gif.ScalePercent(30f);

            cell = new PdfPCell(gif)
            {
                HorizontalAlignment = 2,
                VerticalAlignment = 2,
                Rowspan = 3,
                BorderWidth = 0f
            };
            dth.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal(organization.Address))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal("Mobile No: " + organization.PhoneNo + ", 01992457259"))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth.AddCell(cell);

            document.Add(dth);

            var line = new LineSeparator(1f, 100, null, Element.ALIGN_CENTER, -2);
            document.Add(line);

            // for enpty space
            var dtempty = new PdfPTable(1);
            cell = new PdfPCell(ITextSharpFont.H7Normal("")) { BorderWidth = 0f, FixedHeight = 10f };

            dtempty.AddCell(cell);
            document.Add(dtempty);

            // ***************************** Body


            // ****************************** footer
            Rectangle page = document.PageSize;
            var footer = new PdfPTable(1) { TotalWidth = page.Width };

            var c = new PdfPCell(ITextSharpFont.H7Normal("System Create By Netsoft Solution Ltd. Contact Us: http://www.netsoftbd.com/contact.php"))
            {
                Border = Rectangle.NO_BORDER,
                VerticalAlignment = Element.ALIGN_BOTTOM,
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingLeft = 30
            };
            footer.AddCell(c);
            footer.WriteSelectedRows(0, -1, 0, 20, writer.DirectContent);

            Rectangle page2 = document.PageSize;
            var footer2 = new PdfPTable(1) { TotalWidth = page2.Width - 40 };
            c = new PdfPCell(ITextSharpFont.H7Normal("Money Receiver"))
            {
                Border = Rectangle.NO_BORDER,
                VerticalAlignment = Element.ALIGN_BOTTOM,
                HorizontalAlignment = Element.ALIGN_RIGHT
            };
            footer2.AddCell(c);
            footer2.WriteSelectedRows(0, -1, 0, 100, writer.DirectContent);
        }
    }
}