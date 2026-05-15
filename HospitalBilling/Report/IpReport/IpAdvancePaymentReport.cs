using HospitalBilling.BLL;
using HospitalBilling.Report.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using ITextSharpLibrary;
using System;

namespace HospitalBilling.Report.IpReport
{
    public class IpAdvancePaymentReport
    {
        private readonly OrganizationManager _organizationManager;

        public IpAdvancePaymentReport()
        {
            _organizationManager = new OrganizationManager();
        }

        internal void GetReport(iTextSharp.text.Document document, iTextSharp.text.pdf.PdfWriter writer, IpAdvancePaymentReportModel reportModel, string patientType)
        {
            var organization = _organizationManager.GetOrganizations();
            // Report

            // ******************************* Header ***************
            PdfPCell cell;
            var titwidth = new float[] { 80, 20 };

            var dth = new PdfPTable(titwidth) { WidthPercentage = 100 };

            cell = new PdfPCell(ITextSharpFont.H6Bold(organization.Name))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth.AddCell(cell);

            // Image Cell
            byte[] logo = _organizationManager.GetImageById(organization.Id);
            var gif = Image.GetInstance(logo);
            gif.Alignment = Image.LEFT_ALIGN;
            gif.ScalePercent(11f);

            cell = new PdfPCell(gif)
            {
                HorizontalAlignment = 2,
                VerticalAlignment = 2,
                Rowspan = 3,
                BorderWidth = 0f
            };
            dth.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H7Normal(organization.Address))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H7Normal("Mobile No: " + organization.PhoneNo + ", 01992457259"))
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

            // ***************************** Body

            var titwidth1 = new float[] { 50, 50 };
            var dth1 = new PdfPTable(titwidth1) { WidthPercentage = 100 };

            cell = new PdfPCell(ITextSharpFont.H6Bold("Date: " + DateTime.Now.ToString("dd/MM/yyyy")))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth1.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Bold(patientType))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth1.AddCell(cell);

            document.Add(dth1);
            document.Add(dtempty);

            // Patient Info
            var titwidth2 = new float[] { 45, 55 };
            var dth2 = new PdfPTable(titwidth2) { WidthPercentage = 100 };

            //cell = new PdfPCell(ITextSharpFont.H6Bold("Bill No: "))
            //{
            //    HorizontalAlignment = 0,
            //    VerticalAlignment = 0,
            //    BorderWidth = 0f
            //};
            //dth2.AddCell(cell);

            //cell = new PdfPCell(ITextSharpFont.H6Bold(reportModel.OpBillInfoReportModel.BillNo))
            //{
            //    HorizontalAlignment = 0,
            //    VerticalAlignment = 0,
            //    BorderWidth = 0f
            //};
            //dth2.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Bold("Patient Id: "))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Bold(reportModel.PatientId))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);


            cell = new PdfPCell(ITextSharpFont.H6Normal("Name: "))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal(reportModel.PatientName))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);


            //cell = new PdfPCell(ITextSharpFont.H6Bold(patientType))
            //{
            //    HorizontalAlignment = 0,
            //    VerticalAlignment = 0,
            //    BorderWidth = 0f,
            //    Rowspan = 2
            //};
            //dth2.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal("Phone No: "))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal(reportModel.PhoneNo))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);

            document.Add(dth2);
            document.Add(line);
            document.Add(dtempty);



            // Bill info
            var titwidth3 = new float[] { 45, 55 };
            var dth3 = new PdfPTable(titwidth2) { WidthPercentage = 100 };

            cell = new PdfPCell(ITextSharpFont.H6Normal("Admit Date: "))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth3.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal(reportModel.AdmitDate.ToString("dd-MM-yyyy h:mm tt")))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth3.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal("Room Type: "))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth3.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal(reportModel.RoomType))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth3.AddCell(cell);


            cell = new PdfPCell(ITextSharpFont.H6Normal("Bed Number: "))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth3.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal(reportModel.BedNumber))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth3.AddCell(cell);




            cell = new PdfPCell(ITextSharpFont.H6Normal("Advance Amount: "))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth3.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal(reportModel.AdvancePayment))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth3.AddCell(cell);

            document.Add(dth3);
            document.Add(dtempty);



            // ****************************** footer
            Rectangle page = document.PageSize;
            var footer = new PdfPTable(1) { TotalWidth = page.Width };

            var c = new PdfPCell(ITextSharpFont.H7Normal("Your Trusted Medical Partner"))
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
            footer2.WriteSelectedRows(0, -1, 0, 60, writer.DirectContent);
        }
    }
}