using HospitalBilling.BLL;
using HospitalBilling.Report.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using ITextSharpLibrary;
using System;



namespace HospitalBilling.Report.OpReport
{
    public class OpDiagnosisAssignReport
    {
        private readonly OrganizationManager _organizationManager;

        private readonly OutdoorPatientManager _outdoorPatientManager;
        private HospitalBilling.BLL.BarcodeLib.Barcode barcode = new HospitalBilling.BLL.BarcodeLib.Barcode();

        public OpDiagnosisAssignReport()
        {
            _organizationManager = new OrganizationManager();
            _outdoorPatientManager = new OutdoorPatientManager();
        }

        public void GetReport(Document document, PdfWriter writer, OpDiagnosisAssignReportModel assignReportModel)
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

            cell = new PdfPCell(ITextSharpFont.H4Bold(organization.Name))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth.AddCell(cell);

            // Image Cell
            //byte[] logo = _organizationManager.GetImageById(organization.Id);
            //var gif = Image.GetInstance(logoBytes);
            //gif.Alignment = Image.LEFT_ALIGN;
            //gif.ScalePercent(30f);

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

            cell = new PdfPCell(ITextSharpFont.H6Normal("Mobile No: " + organization.PhoneNo + ", Tel-+880255033396"))
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

            // Patient Info
            var titwidth2 = new float[] { 15,5, 35, 15, 30 };
            var dth2 = new PdfPTable(titwidth2) { WidthPercentage = 100 };

            

          



            //new change

            //cell = new PdfPCell(gifBarcode);
            //cell.BorderWidth = 0f;

            //cell.HorizontalAlignment = 0;
            //cell.Colspan = 5;
            //dth2.AddCell(cell);



            PdfContentByte cb = new PdfContentByte(writer);
            iTextSharp.text.pdf.Barcode128 bc = new Barcode128();
            bc.TextAlignment = Element.ALIGN_CENTER;
            bc.Code = assignReportModel.OpDiagnosisMoneyReportModel.BillNo;
            bc.StartStopText = false;
            bc.CodeType = iTextSharp.text.pdf.Barcode128.CODE128;
            bc.X = 1f;
            bc.Extended = true;

            iTextSharp.text.Image PatImage1 = bc.CreateImageWithBarcode(cb, iTextSharp.text.BaseColor.BLACK, iTextSharp.text.BaseColor.WHITE);
            PatImage1.ScaleAbsolute(480f, 175.25f);
            PdfPCell palletBarcodeCell = new PdfPCell(PatImage1);
            palletBarcodeCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
            palletBarcodeCell.Colspan = 5;
            palletBarcodeCell.FixedHeight = 40;
            palletBarcodeCell.BorderWidth = 0f;
            palletBarcodeCell.HorizontalAlignment = 0;
            palletBarcodeCell.VerticalAlignment = 0;
           
            dth2.AddCell(palletBarcodeCell);



         
            cell = new PdfPCell(ITextSharpFont.H6Bold("Bill No "))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);
            cell = new PdfPCell(ITextSharpFont.H6Bold(":"))
            {
                HorizontalAlignment = 1,
                VerticalAlignment = 1,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);
            cell = new PdfPCell(ITextSharpFont.H6Bold(assignReportModel.OpDiagnosisMoneyReportModel.BillNo))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Bold("Money Receipt "))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f,
                Rowspan = 2
            };
            dth2.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Bold("Date: " + DateTime.Now.ToString("dd/MM/yyyy")))
            {
                HorizontalAlignment = 2,
                VerticalAlignment = 2,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);





            cell = new PdfPCell(ITextSharpFont.H6Bold("Patient Id "))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);
             cell = new PdfPCell(ITextSharpFont.H6Bold(":"))
            {
                HorizontalAlignment = 1,
                VerticalAlignment = 1,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);
            cell = new PdfPCell(ITextSharpFont.H6Bold(assignReportModel.OutdoorPatient.PatientId))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f,
                Colspan=2
            };
            dth2.AddCell(cell);


            cell = new PdfPCell(ITextSharpFont.H7Normal(""))
            {
                HorizontalAlignment = 2,
                VerticalAlignment = 2,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);




            cell = new PdfPCell(ITextSharpFont.H6Normal("Name"))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);
            cell = new PdfPCell(ITextSharpFont.H6Bold(":"))
            {
                HorizontalAlignment = 1,
                VerticalAlignment = 1,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);
            cell = new PdfPCell(ITextSharpFont.H6Normal(assignReportModel.OutdoorPatient.Name))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
                ,Colspan=3
            };
            dth2.AddCell(cell);



           







            cell = new PdfPCell(ITextSharpFont.H6Normal("Gender"))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);
            cell = new PdfPCell(ITextSharpFont.H6Bold(":"))
            {
                HorizontalAlignment = 1,
                VerticalAlignment = 1,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);
            cell = new PdfPCell(ITextSharpFont.H6Normal(assignReportModel.OutdoorPatient.Gender))
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
                BorderWidth = 0f,Colspan=2
            };
            dth2.AddCell(cell);




            cell = new PdfPCell(ITextSharpFont.H6Normal("Age"))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);
            cell = new PdfPCell(ITextSharpFont.H6Bold(":"))
            {
                HorizontalAlignment = 1,
                VerticalAlignment = 1,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);
            cell = new PdfPCell(ITextSharpFont.H6Normal(assignReportModel.OutdoorPatient.Age))
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
            cell = new PdfPCell(ITextSharpFont.H7Normal(""))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);





            












            cell = new PdfPCell(ITextSharpFont.H6Normal("Phone No"))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);
            cell = new PdfPCell(ITextSharpFont.H6Bold(":"))
            {
                HorizontalAlignment = 1,
                VerticalAlignment = 1,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);
            cell = new PdfPCell(ITextSharpFont.H6Normal(assignReportModel.OutdoorPatient.PhoneNo))
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
            cell = new PdfPCell(ITextSharpFont.H7Normal(""))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);


            cell = new PdfPCell(ITextSharpFont.H6Normal("Reference By"))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);
            cell = new PdfPCell(ITextSharpFont.H6Bold(":"))
            {
                HorizontalAlignment = 1,
                VerticalAlignment = 1,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);
            cell = new PdfPCell(ITextSharpFont.H6Normal(assignReportModel.OutdoorPatient.ReferenceByName))
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
            cell = new PdfPCell(ITextSharpFont.H7Normal(""))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);



            cell = new PdfPCell(ITextSharpFont.H6Bold("Consult By"))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f,
                // Rowspan = 2
            };
            dth2.AddCell(cell);
             cell = new PdfPCell(ITextSharpFont.H6Bold(":"))
            {
                HorizontalAlignment = 1,
                VerticalAlignment = 1,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);
            cell = new PdfPCell(ITextSharpFont.H6Bold(assignReportModel.OutdoorPatient.DoctioName))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f,
                Colspan=3
            };
            dth2.AddCell(cell);






            document.Add(dth2);
            //document.Add(line);
            document.Add(dtempty);


            // diagnosis list
            float[] titwidth3 = new float[4] { 8, 55, 20, 17 };
            PdfPTable dth3 = new PdfPTable(titwidth3);
            dth3.WidthPercentage = 100;

            cell = new PdfPCell(ITextSharpFont.H6Bold("Serial"));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.FixedHeight = 20f;
            dth3.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Bold("Diagnosis Name"));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.FixedHeight = 20f;
            dth3.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Bold("Delevary Date"));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.FixedHeight = 20f;
            dth3.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Bold("Total Price (TK.)"))
            {
                HorizontalAlignment = 1,
                VerticalAlignment = 1,
                FixedHeight = 20f
            };
            dth3.AddCell(cell);

            int serial = 0;
            foreach (var billDtl in assignReportModel.DiagnosisBillDtls)
            {
                serial++;

                cell = new PdfPCell(ITextSharpFont.H6Normal(serial.ToString()));
                cell.HorizontalAlignment = 1;
                cell.VerticalAlignment = 1;
                cell.FixedHeight = 20f;
                //cell.BorderColor = BaseColor.LIGHT_GRAY;
                dth3.AddCell(cell);

                cell = new PdfPCell(ITextSharpFont.H6Normal(billDtl.DiagnosisName));
                cell.HorizontalAlignment = 0;
                cell.VerticalAlignment = 0;
                cell.FixedHeight = 20f;
                //cell.BorderColor = BaseColor.LIGHT_GRAY;
                dth3.AddCell(cell);

                cell = new PdfPCell(ITextSharpFont.H6Normal(billDtl.DeliveryDate.ToString("dd/MM/yyyy")));
                cell.HorizontalAlignment = 1;
                cell.VerticalAlignment = 1;
                cell.FixedHeight = 20f;
                //cell.BorderColor = BaseColor.LIGHT_GRAY;
                dth3.AddCell(cell);

                cell = new PdfPCell(ITextSharpFont.H6Normal(billDtl.PayableAmount.ToString("N2")));
                cell.HorizontalAlignment = 2;
                cell.VerticalAlignment = 2;
                cell.FixedHeight = 20f;
                dth3.AddCell(cell);
            }

            document.Add(dth3);

            // money calclution
            float[] titwidth4 = new float[3] { 63, 20, 17 };
            PdfPTable dth4 = new PdfPTable(titwidth4);
            dth4.WidthPercentage = 100;

            cell = new PdfPCell(ITextSharpFont.H6Normal(""));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.BorderWidth = 0f;
            dth4.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal("Total Diagnosis Fee: "));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;

            dth4.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal(assignReportModel.OpDiagnosisMoneyReportModel.TotalNetPrice));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            dth4.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal(""));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.BorderWidth = 0f;
            dth4.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal("Consultant Fee: "));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            dth4.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal(assignReportModel.OpDiagnosisMoneyReportModel.ConsultantFee));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            dth4.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal(""));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.BorderWidth = 0f;
            dth4.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal("Discount: "));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            dth4.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal(assignReportModel.OpDiagnosisMoneyReportModel.SpecialDiscount));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            dth4.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal(""));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.BorderWidth = 0f;
            dth4.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal("Vat : "));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            dth4.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal(assignReportModel.OpDiagnosisMoneyReportModel.Vat));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            dth4.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal(""));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.BorderWidth = 0f;
            dth4.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal("Total Payable Amount: "));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            dth4.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal(assignReportModel.OpDiagnosisMoneyReportModel.TotalPayableAmount));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            dth4.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal(""));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.BorderWidth = 0f;
            dth4.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal("Now Pay: "));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            dth4.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal(assignReportModel.OpDiagnosisMoneyReportModel.NowReceive));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            dth4.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal(""));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.BorderWidth = 0f;
            dth4.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal("Due Amount: "));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            dth4.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal(assignReportModel.OpDiagnosisMoneyReportModel.TotalDue));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            cell.Colspan = 10;
            dth4.AddCell(cell);

            document.Add(dth4);












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
            footer2.WriteSelectedRows(0, -1, 0, 100, writer.DirectContent);
        }


        public static Image AddBarCode(ref PdfWriter Writer, string Text, bool ShowText, float ScaleWidth, float ScaleHeight)
        {

            
            PdfContentByte cb = Writer.DirectContent;
            Barcode39 bc39 = new Barcode39();
            bc39.Code = Text;
            // comment next line to show barcode text   
            if (!ShowText) bc39.Font = null;
            Image barCodeImage = bc39.CreateImageWithBarcode(cb, null, null);
            barCodeImage.Alignment = PdfAppearance.ALIGN_CENTER;
            barCodeImage.ScalePercent(ScaleWidth, ScaleHeight);
            return barCodeImage;
        }
        private static Phrase FormatPhrase(string value)
        {
            return new Phrase(value, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10));
        }
    }
}