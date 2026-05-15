
using HospitalBilling.BLL;
using HospitalBilling.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using ITextSharpLibrary;
using System;
using System.Collections.Generic;

namespace HospitalBilling.Report.OpReport
{
    public class OpMoneyReceiveReport
    {
        private readonly OrganizationManager _organizationManager;

        public OpMoneyReceiveReport()
        {
            _organizationManager = new OrganizationManager();
        }

        //internal void GetReport(iTextSharp.text.Document document, iTextSharp.text.pdf.PdfWriter writer, Models.MoneyReceiveReportModel reportModel, string patientType, byte[] logoBytes)
        //{
        //    var organization = _organizationManager.GetOrganizations();
        //    // Report

        //    // ******************************* Header ***************
        //    PdfPCell cell;
        //    var titwidth = new float[] { 80, 20 };

        //    var dth = new PdfPTable(titwidth) { WidthPercentage = 100 };

        //    cell = new PdfPCell(ITextSharpFont.H6Bold(organization.Name))
        //    {
        //        HorizontalAlignment = 0,
        //        VerticalAlignment = 0,
        //        BorderWidth = 0f
        //    };
        //    dth.AddCell(cell);

        //    // Image Cell
        //    //byte[] logo = _organizationManager.GetImageById(organization.Id);
        //    //var gif = Image.GetInstance(logo);
        //    //gif.Alignment = Image.LEFT_ALIGN;
        //    //gif.ScalePercent(30f);

        //    //cell = new PdfPCell(gif)
        //    //{
        //    //    HorizontalAlignment = 2,
        //    //    VerticalAlignment = 2,
        //    //    Rowspan = 3,
        //    //    BorderWidth = 0f
        //    //};
        //    //dth.AddCell(cell);

        //    byte[] logo = _organizationManager.GetImageById(organization.Id);
        //    var gif = Image.GetInstance(logoBytes);
        //    gif.Alignment = Image.LEFT_ALIGN;
        //    gif.ScalePercent(30f);

        //    cell = new PdfPCell(gif)
        //    {
        //        HorizontalAlignment = 2,
        //        VerticalAlignment = 2,
        //        Rowspan = 3,
        //        BorderWidth = 0f
        //    };
        //    dth.AddCell(cell);

        //    cell = new PdfPCell(ITextSharpFont.H7Normal(organization.Address))
        //    {
        //        HorizontalAlignment = 0,
        //        VerticalAlignment = 0,
        //        BorderWidth = 0f
        //    };
        //    dth.AddCell(cell);

        //    cell = new PdfPCell(ITextSharpFont.H7Normal("Mobile No: " + organization.PhoneNo + ",Tel-+880255033396"))
        //    {
        //        HorizontalAlignment = 0,
        //        VerticalAlignment = 0,
        //        BorderWidth = 0f
        //    };
        //    dth.AddCell(cell);

        //    document.Add(dth);

        //    var line = new LineSeparator(1f, 100, null, Element.ALIGN_CENTER, -2);
        //    document.Add(line);

        //    // for enpty space
        //    var dtempty = new PdfPTable(1);
        //    cell = new PdfPCell(ITextSharpFont.H7Normal("")) { BorderWidth = 0f, FixedHeight = 10f };

        //    dtempty.AddCell(cell);

        //    // ***************************** Body

        //    var titwidth1 = new float[] { 50, 50 };
        //    var dth1 = new PdfPTable(titwidth1) { WidthPercentage = 100 };

        //    cell = new PdfPCell(ITextSharpFont.H6Bold("Date: " + DateTime.Now.ToString("dd/MM/yyyy")))
        //    {
        //        HorizontalAlignment = 0,
        //        VerticalAlignment = 0,
        //        BorderWidth = 0f
        //    };
        //    dth1.AddCell(cell);

        //    cell = new PdfPCell(ITextSharpFont.H6Bold(patientType))
        //    {
        //        HorizontalAlignment = 0,
        //        VerticalAlignment = 0,
        //        BorderWidth = 0f
        //    };
        //    dth1.AddCell(cell);

        //    document.Add(dth1);
        //    document.Add(dtempty);

        //    // Patient Info
        //    var titwidth2 = new float[] { 30, 70 };
        //    var dth2 = new PdfPTable(titwidth2) { WidthPercentage = 100 };

        //    cell = new PdfPCell(ITextSharpFont.H6Bold("Bill No: "))
        //    {
        //        HorizontalAlignment = 0,
        //        VerticalAlignment = 0,
        //        BorderWidth = 0f
        //    };
        //    dth2.AddCell(cell);

        //    cell = new PdfPCell(ITextSharpFont.H6Bold(reportModel.OpBillInfoReportModel.BillNo))
        //    {
        //        HorizontalAlignment = 0,
        //        VerticalAlignment = 0,
        //        BorderWidth = 0f
        //    };
        //    dth2.AddCell(cell);

        //    cell = new PdfPCell(ITextSharpFont.H6Bold("Patient Id: "))
        //    {
        //        HorizontalAlignment = 0,
        //        VerticalAlignment = 0,
        //        BorderWidth = 0f
        //    };
        //    dth2.AddCell(cell);

        //    cell = new PdfPCell(ITextSharpFont.H6Bold(reportModel.Patient.PatientId))
        //    {
        //        HorizontalAlignment = 0,
        //        VerticalAlignment = 0,
        //        BorderWidth = 0f
        //    };
        //    dth2.AddCell(cell);


        //    cell = new PdfPCell(ITextSharpFont.H6Normal("Name: "))
        //    {
        //        HorizontalAlignment = 0,
        //        VerticalAlignment = 0,
        //        BorderWidth = 0f
        //    };
        //    dth2.AddCell(cell);

        //    cell = new PdfPCell(ITextSharpFont.H6Normal(reportModel.Patient.Name))
        //    {
        //        HorizontalAlignment = 0,
        //        VerticalAlignment = 0,
        //        BorderWidth = 0f
        //    };
        //    dth2.AddCell(cell);


        //    //cell = new PdfPCell(ITextSharpFont.H6Bold(patientType))
        //    //{
        //    //    HorizontalAlignment = 0,
        //    //    VerticalAlignment = 0,
        //    //    BorderWidth = 0f,
        //    //    Rowspan = 2
        //    //};
        //    //dth2.AddCell(cell);

        //    cell = new PdfPCell(ITextSharpFont.H6Normal("Phone No: "))
        //    {
        //        HorizontalAlignment = 0,
        //        VerticalAlignment = 0,
        //        BorderWidth = 0f
        //    };
        //    dth2.AddCell(cell);

        //    cell = new PdfPCell(ITextSharpFont.H6Normal(reportModel.Patient.PhoneNo))
        //    {
        //        HorizontalAlignment = 0,
        //        VerticalAlignment = 0,
        //        BorderWidth = 0f
        //    };
        //    dth2.AddCell(cell);

        //    document.Add(dth2);
        //    document.Add(line);
        //    document.Add(dtempty);



        //    // Bill info
        //    var titwidth3 = new float[] { 30, 70 };
        //    var dth3 = new PdfPTable(titwidth2) { WidthPercentage = 100 };

        //    cell = new PdfPCell(ITextSharpFont.H6Normal("Total Bill: "))
        //    {
        //        HorizontalAlignment = 0,
        //        VerticalAlignment = 0,
        //        BorderWidth = 0f
        //    };
        //    dth3.AddCell(cell);

        //    cell = new PdfPCell(ITextSharpFont.H6Normal(reportModel.OpBillInfoReportModel.TotalPayableAmount + ".00"))
        //    {
        //        HorizontalAlignment = 0,
        //        VerticalAlignment = 0,
        //        BorderWidth = 0f
        //    };
        //    dth3.AddCell(cell);

        //    cell = new PdfPCell(ITextSharpFont.H6Normal("Previous Pay: "))
        //    {
        //        HorizontalAlignment = 0,
        //        VerticalAlignment = 0,
        //        BorderWidth = 0f
        //    };
        //    dth3.AddCell(cell);

        //    cell = new PdfPCell(ITextSharpFont.H6Normal(reportModel.OpBillInfoReportModel.TotalPay + ".00"))
        //    {
        //        HorizontalAlignment = 0,
        //        VerticalAlignment = 0,
        //        BorderWidth = 0f
        //    };
        //    dth3.AddCell(cell);


        //    cell = new PdfPCell(ITextSharpFont.H6Normal("Now Pay: "))
        //    {
        //        HorizontalAlignment = 0,
        //        VerticalAlignment = 0,
        //        BorderWidth = 0f
        //    };
        //    dth3.AddCell(cell);

        //    cell = new PdfPCell(ITextSharpFont.H6Normal(reportModel.OpBillInfoReportModel.NowPay + ".00"))
        //    {
        //        HorizontalAlignment = 0,
        //        VerticalAlignment = 0,
        //        BorderWidth = 0f
        //    };
        //    dth3.AddCell(cell);

        //    //cell = new PdfPCell(ITextSharpFont.H6Normal("Discount: "))
        //    //{
        //    //    HorizontalAlignment = 0,
        //    //    VerticalAlignment = 0,
        //    //    BorderWidth = 0f
        //    //};
        //    //dth3.AddCell(cell);

        //    //cell = new PdfPCell(ITextSharpFont.H6Normal(reportModel.OpBillInfoReportModel.SpecialDiscount + ".00"))
        //    //{
        //    //    HorizontalAlignment = 0,
        //    //    VerticalAlignment = 0,
        //    //    BorderWidth = 0f
        //    //};
        //    //dth3.AddCell(cell);


        //    cell = new PdfPCell(ITextSharpFont.H6Normal("Total Due: "))
        //    {
        //        HorizontalAlignment = 0,
        //        VerticalAlignment = 0,
        //        BorderWidth = 0f
        //    };
        //    dth3.AddCell(cell);

        //    cell = new PdfPCell(ITextSharpFont.H6Normal(reportModel.OpBillInfoReportModel.TotalDue + ".00"))
        //    {
        //        HorizontalAlignment = 0,
        //        VerticalAlignment = 0,
        //        BorderWidth = 0f
        //    };
        //    dth3.AddCell(cell);

        //    document.Add(dth3);
        //    document.Add(dtempty);






        //    float[] widthsig = new float[] { 20, 20 };
        //    PdfPTable pdtsig = new PdfPTable(widthsig);
        //    pdtsig.WidthPercentage = 100;
        //    cell = new PdfPCell(FormatPhrase(""));
        //    cell.HorizontalAlignment = 0;
        //    cell.VerticalAlignment = 1;
        //    cell.Border = 0;

        //    pdtsig.AddCell(cell);
        //    cell = new PdfPCell(FormatPhrase(""));
        //    cell.HorizontalAlignment = 0;
        //    cell.VerticalAlignment = 1;
        //    cell.Border = 0;
        //    cell.Colspan=10;

        //    pdtsig.AddCell(cell);
        //    cell = new PdfPCell(FormatPhrase(""));
        //    cell.HorizontalAlignment = 0;
        //    cell.VerticalAlignment = 1;
        //    cell.Border = 0;

        //    pdtsig.AddCell(cell);
        //    cell = new PdfPCell(FormatPhrase(""));
        //    cell.HorizontalAlignment = 0;
        //    cell.VerticalAlignment = 1;
        //    cell.Border = 0;

        //    pdtsig.AddCell(cell);
        //    cell = new PdfPCell(FormatPhrase(""));
        //    cell.HorizontalAlignment = 0;
        //    cell.VerticalAlignment = 1;
        //    cell.Border = 0;
        //    //cell.FixedHeight = 18f;
        //    //cell.BorderColor = BaseColor.LIGHT_GRAY;
        //    pdtsig.AddCell(cell);
        //    cell = new PdfPCell(FormatPhrase("Money Receiver"));
        //    cell.HorizontalAlignment = 1;
        //    cell.VerticalAlignment = 1;
        //    cell.Border = 1;
        //    //cell.FixedHeight = 18f;
        //    //cell.BorderColor = BaseColor.LIGHT_GRAY;
        //    pdtsig.AddCell(cell);
        //    document.Add(pdtsig);








        //    // ****************************** footer
        //    ////Rectangle page = document.PageSize;
        //    ////var footer = new PdfPTable(1) { TotalWidth = page.Width };

        //    ////var c = new PdfPCell(ITextSharpFont.H7Normal("System Create By Netsoft Solution Ltd. Contact Us: http://www.netsoftbd.com/contact.php"))
        //    ////{
        //    ////    Border = Rectangle.NO_BORDER,
        //    ////    VerticalAlignment = Element.ALIGN_BOTTOM,
        //    ////    HorizontalAlignment = Element.ALIGN_LEFT,
        //    ////    PaddingLeft = 30
        //    ////};
        //    ////footer.AddCell(c);
        //    ////footer.WriteSelectedRows(0, -1, 0, 20, writer.DirectContent);

        //    ////Rectangle page2 = document.PageSize;
        //    ////var footer2 = new PdfPTable(1) { TotalWidth = page2.Width - 40 };
        //    ////c = new PdfPCell(ITextSharpFont.H7Normal("Money Receiver"))
        //    ////{
        //    ////    Border = Rectangle.NO_BORDER,
        //    ////    VerticalAlignment = Element.ALIGN_BOTTOM,
        //    ////    HorizontalAlignment = Element.ALIGN_RIGHT
        //    ////};
        //    ////footer2.AddCell(c);
        //    ////footer2.WriteSelectedRows(0, -1, 0, 60, writer.DirectContent);
        //}


        internal void GetReport(iTextSharp.text.Document document, iTextSharp.text.pdf.PdfWriter writer, Models.MoneyReceiveReportModel reportModel, string patientType, List<HospitalModels.Library.Billings.DiagnosisBillDtl> billList)
        {
            var organization = _organizationManager.GetOrganizations();
            // Report

            // ******************************* Header ***************
            PdfPCell cell;
            byte[] logo = _organizationManager.GetGlLogo("2");
            iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(logo);
            gif.Alignment = iTextSharp.text.Image.MIDDLE_ALIGN;
            gif.ScalePercent(11f);
            var titwidth = new float[] { 80, 20 };

            var dth = new PdfPTable(titwidth) { WidthPercentage = 100 };

            cell = new PdfPCell(ITextSharpFont.H6Bold(organization.Name))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth.AddCell(cell);

           
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
            var titwidth2 = new float[] { 30, 70 };
            var dth2 = new PdfPTable(titwidth2) { WidthPercentage = 100 };

            cell = new PdfPCell(ITextSharpFont.H6Bold("Bill No: "))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Bold(reportModel.OpBillInfoReportModel.BillNo))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Bold("Patient Id: "))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Bold(reportModel.Patient.PatientId))
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

            cell = new PdfPCell(ITextSharpFont.H6Normal(reportModel.Patient.Name))
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

            cell = new PdfPCell(ITextSharpFont.H6Normal(reportModel.Patient.PhoneNo))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);

            document.Add(dth2);
            document.Add(line);
            document.Add(dtempty);




            float[] widthdtl = new float[3] { 8, 72, 20 };
            PdfPTable pdtdtl = new PdfPTable(widthdtl);
            pdtdtl.WidthPercentage = 100;
            pdtdtl.HeaderRows = 1;

            cell = new PdfPCell(FormatHeaderPhrase("Sl"));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.FixedHeight = 20f;
            cell.BorderColor = BaseColor.LIGHT_GRAY;
            pdtdtl.AddCell(cell);
            cell = new PdfPCell(FormatHeaderPhrase("Diagnosis Name"));
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

            foreach (var bill in billList)
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









            // Bill info
            var titwidth3 = new float[] { 90, 10 };
            var dth3 = new PdfPTable(titwidth3) { WidthPercentage = 100 };

            cell = new PdfPCell(ITextSharpFont.H6Normal("Gross Amount: "))
            {
                HorizontalAlignment = 2,
                VerticalAlignment = 2,
                BorderWidth = 0f
            };
            dth3.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal(reportModel.OpBillInfoReportModel.TotalPayableAmount + ".00"))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth3.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal("Previous Pay: "))
            {
                HorizontalAlignment = 2,
                VerticalAlignment = 2,
                BorderWidth = 0f
            };
            dth3.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal(reportModel.OpBillInfoReportModel.TotalPay + ".00"))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth3.AddCell(cell);


            cell = new PdfPCell(ITextSharpFont.H6Normal("Net Receivable: "))
            {
                HorizontalAlignment = 2,
                VerticalAlignment = 2,
                BorderWidth = 0f
            };
            dth3.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal(reportModel.OpBillInfoReportModel.NowPay + ".00"))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth3.AddCell(cell);

            if (reportModel.OpBillInfoReportModel.SpecialDiscount=="0")
            {
                cell = new PdfPCell(ITextSharpFont.H6Normal(""))
                {
                    HorizontalAlignment = 0,
                    VerticalAlignment = 0,
                    BorderWidth = 0f
                };
                //dth3.AddCell(cell);

                cell = new PdfPCell(ITextSharpFont.H6Normal(""))
                {
                    HorizontalAlignment = 0,
                    VerticalAlignment = 0,
                    BorderWidth = 0f
                };
                //dth3.AddCell(cell);
            }
            else
            {
                cell = new PdfPCell(ITextSharpFont.H6Normal("(-)Discount: "))
                {
                    HorizontalAlignment = 2,
                    VerticalAlignment = 0,
                    BorderWidth = 0f
                };
                dth3.AddCell(cell);

                cell = new PdfPCell(ITextSharpFont.H6Normal(reportModel.OpBillInfoReportModel.SpecialDiscount + ".00"))
                {
                    HorizontalAlignment = 0,
                    VerticalAlignment = 0,
                    BorderWidth = 0f
                };
                dth3.AddCell(cell);
            }
           


            cell = new PdfPCell(ITextSharpFont.H6Normal("Total Due: "))
            {
                HorizontalAlignment = 2,
                VerticalAlignment = 2,
                BorderWidth = 0f
            };
            dth3.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal(reportModel.OpBillInfoReportModel.TotalDue + ".00"))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth3.AddCell(cell);

            document.Add(dth3);
            document.Add(dtempty);






            float[] widthsig = new float[] { 20, 20 };
            PdfPTable pdtsig = new PdfPTable(widthsig);
            pdtsig.WidthPercentage = 100;
            cell = new PdfPCell(FormatPhrase(""));
            cell.HorizontalAlignment = 0;
            cell.VerticalAlignment = 1;
            cell.Border = 0;

            pdtsig.AddCell(cell);
            cell = new PdfPCell(FormatPhrase(""));
            cell.HorizontalAlignment = 0;
            cell.VerticalAlignment = 1;
            cell.Border = 0;
            cell.Colspan = 10;

            pdtsig.AddCell(cell);
            cell = new PdfPCell(FormatPhrase(""));
            cell.HorizontalAlignment = 0;
            cell.VerticalAlignment = 1;
            cell.Border = 0;

            pdtsig.AddCell(cell);
            cell = new PdfPCell(FormatPhrase(""));
            cell.HorizontalAlignment = 0;
            cell.VerticalAlignment = 1;
            cell.Border = 0;

            pdtsig.AddCell(cell);
            cell = new PdfPCell(FormatPhrase(""));
            cell.HorizontalAlignment = 0;
            cell.VerticalAlignment = 1;
            cell.Border = 0;
            //cell.FixedHeight = 18f;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            pdtsig.AddCell(cell);
            cell = new PdfPCell(FormatPhrase("Money Receiver"));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.Border = 1;
            //cell.FixedHeight = 18f;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            pdtsig.AddCell(cell);
            document.Add(pdtsig);




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




           
            ////Rectangle page = document.PageSize;
            ////var footer = new PdfPTable(1) { TotalWidth = page.Width };

            ////var c = new PdfPCell(ITextSharpFont.H7Normal("System Create By Netsoft Solution Ltd. Contact Us: http://www.netsoftbd.com/contact.php"))
            ////{
            ////    Border = Rectangle.NO_BORDER,
            ////    VerticalAlignment = Element.ALIGN_BOTTOM,
            ////    HorizontalAlignment = Element.ALIGN_LEFT,
            ////    PaddingLeft = 30
            ////};
            ////footer.AddCell(c);
            ////footer.WriteSelectedRows(0, -1, 0, 20, writer.DirectContent);

            ////Rectangle page2 = document.PageSize;
            ////var footer2 = new PdfPTable(1) { TotalWidth = page2.Width - 40 };
            ////c = new PdfPCell(ITextSharpFont.H7Normal("Money Receiver"))
            ////{
            ////    Border = Rectangle.NO_BORDER,
            ////    VerticalAlignment = Element.ALIGN_BOTTOM,
            ////    HorizontalAlignment = Element.ALIGN_RIGHT
            ////};
            ////footer2.AddCell(c);
            ////footer2.WriteSelectedRows(0, -1, 0, 60, writer.DirectContent);
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