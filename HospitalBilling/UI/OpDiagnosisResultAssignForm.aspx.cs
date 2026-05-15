using HospitalBilling.BLL;
using HospitalBilling.Models;
using HospitalBilling.Report.OpReport;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using ITextSharpLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalBilling.UI
{
    public partial class OpDiagnosisResultAssignForm : System.Web.UI.Page
    {
        public  OutdoorPatientManager _outdoorPatientManager=new OutdoorPatientManager();
        private readonly OrganizationManager _organizationManager=new OrganizationManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
              {

              }
        }

        protected void searchLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                string searchInput = BillNoTextBox.Text;
                if (searchInput == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ale",
                        "alert('Please Insert Pateint Id/ Name/ Phone No!!');", true);
                    BillNoTextBox.Focus();
                }
                else
                {
                    var patient = _outdoorPatientManager.GetPatientByBillNo(searchInput);
                    if (patient == null)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "ale",
                            "alert('Can not Find Patient by This Pateint Id/ Name/ Phone No!!');", true);
                        BillNoTextBox.Focus();
                    }
                    else
                    {
                        BillNoTextBox.ReadOnly = true;

                        idHiddenField.Value = patient.BillNoId.ToString();
                        //patientIdTextBox.Text = patient.PatientId;
                        patientNameTextBox.Text = patient.Name;
                        phoneNoTextBox.Text = patient.PhoneNo;
                        DataTable data = _outdoorPatientManager.GetDiagnosis(idHiddenField.Value);
                        diagnosisBillGridView.DataSource = data;
                        diagnosisBillGridView.DataBind();
                        ViewState["ReferenceById"] = patient.ReferenceById;
                        saveAssignButton.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n" + ex, true);
            }
        }

        protected void reportButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("OpDiagnosisResultAssignForm.aspx");
        }

        protected void saveAssignButton_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in diagnosisBillGridView.Rows)
                {

                    Label lblDtlId = (Label)row.FindControl("DtlId");
                    TextBox txtResultValueTextBox = (TextBox)row.FindControl("ResultValueTextBox");

                    int DtlId = Convert.ToInt32(lblDtlId.Text);
                    string ResultValue = txtResultValueTextBox.Text;

                    int count = _outdoorPatientManager.UpdateDiagnosisBillResult(DtlId, ResultValue);


                }

                Report();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n" + ex, true);
            }

           

        }

        private void Report()
        {
            string searchInput = BillNoTextBox.Text;
            var patient = _outdoorPatientManager.GetPatientByBillNo(searchInput);

            const string patientType = "Outdoor Patient";

            //var filePath = Server.MapPath("~/image/logo2.png");
            //var fileBytes = File.ReadAllBytes(filePath);

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition",
                "attachment; filename=Report Result " + BillNoTextBox.Text + ".pdf");
            var document = new Document(PageSize.A6, 20f, 20f, 20f, 25f);
            var writer = PdfWriter.GetInstance(document, Response.OutputStream);
            document.Open();

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

            // Image Cell
            //byte[] logo = _organizationManager.GetImageById(organization.Id);
            //var gif = Image.GetInstance(logo);
            //gif.Alignment = Image.LEFT_ALIGN;
            //gif.ScalePercent(30f);

            //cell = new PdfPCell(gif)
            //{
            //    HorizontalAlignment = 2,
            //    VerticalAlignment = 2,
            //    Rowspan = 3,
            //    BorderWidth = 0f
            //};
            //dth.AddCell(cell);

            //byte[] logo = _organizationManager.GetImageById(organization.Id);
            //var gif = Image.GetInstance(logoBytes);
            //gif.Alignment = Image.LEFT_ALIGN;
            //gif.ScalePercent(30f);

            cell = new PdfPCell(gif)
            {
                HorizontalAlignment = 2,
                VerticalAlignment = 2,
                Rowspan = 4,
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

            cell = new PdfPCell(ITextSharpFont.H7Normal("Mobile No: " + organization.PhoneNo + ",Tel-+880255033396"))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Bold("Laboratory Report"))
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


            cell = new PdfPCell(ITextSharpFont.H6Bold(""))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                Colspan = 2,
                BorderWidth = 0f
                ,
                FixedHeight = 5f 

            };
            dth1.AddCell(cell);



            PdfContentByte cb = new PdfContentByte(writer);
            iTextSharp.text.pdf.Barcode128 bc = new Barcode128();
            bc.TextAlignment = Element.ALIGN_CENTER;
            bc.Code = BillNoTextBox.Text;
            bc.StartStopText = false;
            bc.CodeType = iTextSharp.text.pdf.Barcode128.CODE128;
            bc.X = 1f;
            bc.Extended = true;

            iTextSharp.text.Image PatImage1 = bc.CreateImageWithBarcode(cb, iTextSharp.text.BaseColor.BLACK, iTextSharp.text.BaseColor.WHITE);
            PatImage1.ScaleAbsolute(480f, 175.25f);
            PdfPCell palletBarcodeCell = new PdfPCell(PatImage1);
            palletBarcodeCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
            palletBarcodeCell.FixedHeight = 20;
            palletBarcodeCell.BorderWidth = 0f;
            palletBarcodeCell.HorizontalAlignment = 0;
            palletBarcodeCell.VerticalAlignment = 0;
            palletBarcodeCell.Colspan = 2;
            dth1.AddCell(palletBarcodeCell);
      

            cell = new PdfPCell(ITextSharpFont.H6Bold("Date: " + DateTime.Now.ToString("dd/MM/yyyy")))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                Colspan=2,
                BorderWidth = 0f
                
            };
            dth1.AddCell(cell);




  




           

            cell = new PdfPCell(ITextSharpFont.H6Bold("Patient Type: " + patientType))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                Colspan = 2,
                BorderWidth = 0f
              
            };
            dth1.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Bold(""))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                Colspan = 2,
                BorderWidth = 0f

            };
            dth1.AddCell(cell);

            document.Add(dth1);
          //  document.Add(dtempty);

            // Patient Info
            var titwidth2 = new float[] { 20, 30,20,30 };
            var dth2 = new PdfPTable(titwidth2) { WidthPercentage = 100 };

            cell = new PdfPCell(ITextSharpFont.H6Normal("Bill No: "))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal(BillNoTextBox.Text))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal("Patient Id: "))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Normal(patient.TypeWisePatientId))
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

            cell = new PdfPCell(ITextSharpFont.H6Normal(patient.Name))
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

            cell = new PdfPCell(ITextSharpFont.H6Normal(patient.PhoneNo))
            {
                HorizontalAlignment = 0,
                VerticalAlignment = 0,
                BorderWidth = 0f
            };
            dth2.AddCell(cell);

            document.Add(dth2);
            document.Add(line);
            document.Add(dtempty);







            // diagnosis list
            float[] titwidth3 = new float[4] { 8, 45,20, 30};
            PdfPTable dth3 = new PdfPTable(titwidth3);
            dth3.WidthPercentage = 100;

            cell = new PdfPCell(ITextSharpFont.H6Bold("Sl."));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.FixedHeight = 20f;
            dth3.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Bold("Diagnosis Name"));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.FixedHeight = 20f;
            dth3.AddCell(cell);


            cell = new PdfPCell(ITextSharpFont.H6Bold("Result Value"))
            {
                HorizontalAlignment = 1,
                VerticalAlignment = 1,
                FixedHeight = 20f
            };
            dth3.AddCell(cell);

            cell = new PdfPCell(ITextSharpFont.H6Bold("Reference Value"))
            {
                HorizontalAlignment = 1,
                VerticalAlignment = 1,
                FixedHeight = 20f
            };
            dth3.AddCell(cell);

            int serial = 0;
            DataTable data = _outdoorPatientManager.GetDiagnosis(idHiddenField.Value);
            foreach (DataRow billDtl in data.Rows)
            {
                serial++;

                cell = new PdfPCell(ITextSharpFont.H6Normal(serial.ToString()));
                cell.HorizontalAlignment = 1;
                cell.VerticalAlignment = 1;
                cell.FixedHeight = 20f;
                //cell.BorderColor = BaseColor.LIGHT_GRAY;
                dth3.AddCell(cell);

                cell = new PdfPCell(ITextSharpFont.H6Normal(billDtl["DiagnosisName"].ToString()));
                cell.HorizontalAlignment = 0;
                cell.VerticalAlignment = 0;
                cell.FixedHeight = 20f;
                //cell.BorderColor = BaseColor.LIGHT_GRAY;
                dth3.AddCell(cell);


                cell = new PdfPCell(ITextSharpFont.H6Normal(billDtl["rptResultValue"].ToString()));
                cell.HorizontalAlignment = 1;
                cell.VerticalAlignment = 1;
                cell.FixedHeight = 20f;
                //cell.BorderColor = BaseColor.LIGHT_GRAY;
                dth3.AddCell(cell);


                cell = new PdfPCell(ITextSharpFont.H6Normal(billDtl["rptNormalValue"].ToString()));
                cell.HorizontalAlignment = 1;
                cell.VerticalAlignment = 1;
                cell.FixedHeight = 20f;
                //cell.BorderColor = BaseColor.LIGHT_GRAY;
                dth3.AddCell(cell);
            }

            document.Add(dth3);




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
            cell = new PdfPCell(FormatPhrase(""));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.Border = 1;
            //cell.FixedHeight = 18f;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            pdtsig.AddCell(cell);
            document.Add(pdtsig);

            document.Close();
            Response.Flush();
            Response.End();
        }

        private static Phrase FormatPhrase(string value)
        {
            return new Phrase(value, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10));
        }
    }
}