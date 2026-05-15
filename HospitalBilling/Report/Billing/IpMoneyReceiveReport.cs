using HospitalBilling.BLL;
using HospitalBilling.BLL.SurgerysBills;
using HospitalBilling.Report.Billing;
using HospitalBilling.Report.Models;
using HospitalManager.Library.Billings;
using HospitalModels.Library.Billings;
using HospitalModels.Library.CommonClass;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using ITextSharpLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalBilling.Report.Billing
{

    public class IpMoneyReceiveReport
    {
        private readonly OrganizationManager _organizationManager;
        private readonly IndoorPatientManager _indoorPatientManager;
        private readonly IpDiagnosisBillManager _ipDiagnosisBillManager;
        private readonly IpBedBillManager _ipBedBillManager;
        private readonly IpPaymentHistoryManager _ipPaymentHistoryManager;
        private readonly IpMoneyReceiveManager _moneyReceiveManager;
        private readonly IpOtherBillManager _ipOtherBillManager;
        IpAssignSurgeryManager _ipAssignSurgeryManager = new IpAssignSurgeryManager();
        private readonly IpMoneyReceiveReport _ipMoneyReceiveReport;

        public IpMoneyReceiveReport()
        {
            _organizationManager = new OrganizationManager();
        }


        public void GetBillReport(Document document, PdfWriter writer, IpMoneyReceiveReportModel reportModel, List<MrParticular> billList)
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
            //iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(logo);
            //gif.Alignment = iTextSharp.text.Image.MIDDLE_ALIGN;

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

            cell = new PdfPCell(ITextSharpFont.H7Normal("Mobile No: " + organization.PhoneNo + ""))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Bold("Customer Copy"))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f,
               // Rowspan = 2
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



            var titwidth2 = new float[] { 30, 70};
            var dth2 = new PdfPTable(titwidth2) { WidthPercentage = 100 };


            cell = new PdfPCell(ITextSharpFont.H6Bold("Indoor Patient"))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f,
                
            };
            dth2.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H7BoldItalic("Date: " + DateTime.Now.ToString("dd/MM/yyyy")))
            {
                HorizontalAlignment = 2,
                VerticalAlignment = 2,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);


            cell = new PdfPCell(ITextSharpFont.H7Bold("Patient Id : "))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H7Bold(reportModel.PatientId))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);



            //cell = new PdfPCell(ITextSharpFont.H6Bold("Customer Copy"))
            //{
            //    HorizontalAlignment = 0,
            //    VerticalAlignment = 0,
            //    BorderWidth = 0f,
            //    Rowspan = 2
            //};
            //dth2.AddCell(cell);

          


            cell = new PdfPCell(ITextSharpFont.H7Bold("Name : "))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H7Bold(reportModel.Name))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);


            //cell = new PdfPCell(FormatHeaderPhrase(""));
            //cell.HorizontalAlignment = 2;
            //cell.VerticalAlignment = 2;
            //cell.BorderWidth = 0f;
            //dth2.AddCell(cell);

           

            cell = new PdfPCell(ITextSharpFont.H7Normal(""))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H7Normal(""))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);


            document.Add(dth2);
            document.Add(line);
            document.Add(dtempty);



            float[] widthdtl = new float[3] { 8, 72,20 };
            PdfPTable pdtdtl = new PdfPTable(widthdtl);
            pdtdtl.WidthPercentage = 100;
            pdtdtl.HeaderRows = 1;

            cell = new PdfPCell(FormatHeaderPhrase("Sl"));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.FixedHeight = 20f;
            cell.BorderColor = BaseColor.LIGHT_GRAY;
            pdtdtl.AddCell(cell);
            cell = new PdfPCell(FormatHeaderPhrase("Bill Type"));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.FixedHeight = 20f;
            cell.BorderColor = BaseColor.LIGHT_GRAY;
            pdtdtl.AddCell(cell);
            cell = new PdfPCell(FormatHeaderPhrase("Amount"));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.FixedHeight = 20f;
            cell.BorderColor = BaseColor.LIGHT_GRAY;
            pdtdtl.AddCell(cell);

            foreach(var bill in billList)
            {
                cell = new PdfPCell(FormatPhrase(bill.Serial.ToString()));
                cell.HorizontalAlignment = 1;
                cell.VerticalAlignment = 1;
                cell.BorderColor = BaseColor.LIGHT_GRAY;
                pdtdtl.AddCell(cell);

                cell = new PdfPCell(FormatPhrase(bill.Particular.ToString()));
                cell.HorizontalAlignment = 1;
                cell.VerticalAlignment = 1;
                cell.BorderColor = BaseColor.LIGHT_GRAY;
                pdtdtl.AddCell(cell);

                cell = new PdfPCell(FormatPhrase(bill.Amount.ToString()));
                cell.HorizontalAlignment = 1;
                cell.VerticalAlignment = 1;
                cell.BorderColor = BaseColor.LIGHT_GRAY;
                pdtdtl.AddCell(cell);

            }
            document.Add(pdtdtl);



            var titwidth3 = new float[] { 85, 15 };
            var dth3 = new PdfPTable(titwidth3) { WidthPercentage = 100 };

            cell = new PdfPCell(ITextSharpFont.H7Normal("Total Net Fee : "))
            {
                HorizontalAlignment = 2,
                VerticalAlignment = 2,
                BorderWidth = 0f
            };
            dth3.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H7Normal(reportModel.TotalBill.ToString("N2")))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth3.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H7Normal("Total Discount : "))
            {
                HorizontalAlignment = 2,
                VerticalAlignment = 2,
                BorderWidth = 0f
            };
            dth3.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H7Normal(reportModel.TotalDiscount.ToString("N2")))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth3.AddCell(cell);

            //cell = new PdfPCell(ITextSharpFont.H7Normal("Total Payable Amount : "))
            //{
            //    HorizontalAlignment = 2,
            //    VerticalAlignment = 2,
            //    BorderWidth = 0f
            //};
            //dth3.AddCell(cell);

            //cell = new PdfPCell(ITextSharpFont.H7Normal(totalPaid.ToString("N2")))
            //{
            //    HorizontalAlignment = 2,
            //    VerticalAlignment = 2,
            //    BorderWidth = 0f
            //};
            //dth3.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H7Normal("Previous Pay : "))
            {
                HorizontalAlignment = 2,
                VerticalAlignment = 2,
                BorderWidth = 0f
            };
            dth3.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H7Normal(reportModel.PreviesTotalPaid.ToString("N2")))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth3.AddCell(cell);


            cell = new PdfPCell(ITextSharpFont.H7Normal("Net Payable : "))
            {
                HorizontalAlignment = 2,
                VerticalAlignment = 2,
                BorderWidth = 0f
            };
            dth3.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H7Normal(reportModel.NowPay.ToString("N2")))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth3.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H7Normal("Total Due : "))
            {
                HorizontalAlignment = 2,
                VerticalAlignment = 2,
                BorderWidth = 0f
            };
            dth3.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H7Normal(reportModel.TotalDue.ToString("N2")))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth3.AddCell(cell);


            document.Add(dth3);

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

        }

        private static Phrase FormatHeaderPhrase(string value)
        {
            return new Phrase(value, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, iTextSharp.text.Font.BOLD));
        }
        private static Phrase FormatPhrase(string value)
        {
            return new Phrase(value, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11));
        }

      
    }
}