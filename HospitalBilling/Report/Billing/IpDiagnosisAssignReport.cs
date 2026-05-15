using HospitalBilling.BLL;
using HospitalModels.Library.Billings;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using ITextSharpLibrary;
using System;
using System.Collections.Generic;

namespace HospitalBilling.Report.Billing
{
    public class IpDiagnosisAssignReport
    {
        private readonly OrganizationManager _organizationManager;
        private readonly IndoorPatientManager _indoorPatientManager;

        public IpDiagnosisAssignReport()
        {
            _organizationManager = new OrganizationManager();
            _indoorPatientManager = new IndoorPatientManager();
        }

        public void GetBillReport(Document document, PdfWriter writer, List<DiagnosisBillDtl> dianDiagnosisBillDtl, int patientId)
        {
            var organization = _organizationManager.GetOrganizations();
            var patient = _indoorPatientManager.GetPatientById(patientId);

            // Report

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

            cell = new PdfPCell(ITextSharpFont.H6Bold("Image"))
            {
                HorizontalAlignment = 2,
                VerticalAlignment = 2,
                Rowspan = 2,
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

            document.Add(dth);

            var line = new LineSeparator(1f, 100, null, Element.ALIGN_CENTER, -2);
            document.Add(line);

            // for enpty space
            var dtempty = new PdfPTable(1);
            cell = new PdfPCell(ITextSharpFont.H7Normal("")) { BorderWidth = 0f, FixedHeight = 10f };

            dtempty.AddCell(cell);
            document.Add(dtempty);



            var titwidth2 = new float[] { 10, 40, 25, 25 };
            var dth2 = new PdfPTable(titwidth2) { WidthPercentage = 100 };

            cell = new PdfPCell(ITextSharpFont.H7Bold("Patient Id: "))
            {
                HorizontalAlignment = 2,
                VerticalAlignment = 2,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H7Bold(patient.PatientId))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Bold("Customer Copy"))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f,
                Rowspan = 2
            };
            dth2.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H7BoldItalic("Date: " + DateTime.Now.ToString("dd/MM/yyyy")))
            {
                HorizontalAlignment = 2,
                VerticalAlignment = 2,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);


            cell = new PdfPCell(ITextSharpFont.H7Bold("Name: "))
            {
                HorizontalAlignment = 2,
                VerticalAlignment = 2,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H7Bold(patient.Name))
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
                HorizontalAlignment = 2,
                VerticalAlignment = 2,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H7Bold("Phone No: "))
            {
                HorizontalAlignment = 2,
                VerticalAlignment = 2,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H7Bold(patient.PhoneNo))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);


            cell = new PdfPCell(ITextSharpFont.H6Bold("Indoor Patient"))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f,
                Rowspan = 2
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


            float[] titwidth3 = new float[4] { 8, 55, 20, 17 };
            PdfPTable dth3 = new PdfPTable(titwidth3);
            dth3.WidthPercentage = 100;

            int serial = 0;
            foreach (var billDtl in dianDiagnosisBillDtl)
            {
                serial++;

                cell = new PdfPCell(ITextSharpFont.H7Normal(serial.ToString()));
                cell.HorizontalAlignment = 1;
                cell.VerticalAlignment = 1;
                cell.FixedHeight = 20f;
                //cell.BorderColor = BaseColor.LIGHT_GRAY;
                dth3.AddCell(cell);

                cell = new PdfPCell(ITextSharpFont.H7Normal(billDtl.DiagnosisName));
                cell.HorizontalAlignment = 0;
                cell.VerticalAlignment = 0;
                cell.FixedHeight = 20f;
                //cell.BorderColor = BaseColor.LIGHT_GRAY;
                dth3.AddCell(cell);

                cell = new PdfPCell(ITextSharpFont.H7Normal(billDtl.DeliveryDate.ToString("dd/MM/yyyy")));
                cell.HorizontalAlignment = 1;
                cell.VerticalAlignment = 1;
                cell.FixedHeight = 20f;
                //cell.BorderColor = BaseColor.LIGHT_GRAY;
                dth3.AddCell(cell);

                cell = new PdfPCell(ITextSharpFont.H7Normal(billDtl.PayableAmount.ToString("N2")));
                cell.HorizontalAlignment = 1;
                cell.VerticalAlignment = 1;
                cell.FixedHeight = 20f;
                dth3.AddCell(cell);
            }

            document.Add(dth3);


        }
    }
}