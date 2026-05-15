#region Assembly BarcodeLib.dll, v2.0.50727
// G:\Ridoy Project\KoryoClinic\Running\HospitalManagementApp(02-02-2023)\HospitalManagementApp\HospitalBilling\bin\BarcodeLib.dll
#endregion

using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;


namespace HospitalBilling.BLL.BarcodeLib
{
    [Serializable]
    [XmlRoot("BarcodeXML")]
    [DesignerCategory("code")]
    [HelpKeyword("vs.data.DataSet")]
    [ToolboxItem(true)]
    [XmlSchemaProvider("GetTypedDataSetSchema")]

    public class BarcodeXML : DataSet
    {
    }
    //public class BarcodeXML : DataSet
    //{
    //    [DebuggerNonUserCode]
    //    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //    public BarcodeXML();
    //    [DebuggerNonUserCode]
    //    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //    protected BarcodeXML(SerializationInfo info, StreamingContext context);

    //    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //    [DebuggerNonUserCode]
    //    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    //    [Browsable(false)]
    //    public BarcodeXML.BarcodeDataTable Barcode { get; }
    //    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //    [DebuggerNonUserCode]
    //    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //    public DataRelationCollection Relations { get; }
    //    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    //    [DebuggerNonUserCode]
    //    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //    [Browsable(true)]
    //    public override SchemaSerializationMode SchemaSerializationMode { get; set; }
    //    [DebuggerNonUserCode]
    //    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //    public DataTableCollection Tables { get; }

    //    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //    [DebuggerNonUserCode]
    //    public override DataSet Clone();
    //    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //    [DebuggerNonUserCode]
    //    protected override XmlSchema GetSchemaSerializable();
    //    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //    [DebuggerNonUserCode]
    //    public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs);
    //    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //    [DebuggerNonUserCode]
    //    protected override void InitializeDerivedDataSet();
    //    [DebuggerNonUserCode]
    //    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //    protected override void ReadXmlSerializable(XmlReader reader);
    //    [DebuggerNonUserCode]
    //    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //    protected override bool ShouldSerializeRelations();
    //    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //    [DebuggerNonUserCode]
    //    protected override bool ShouldSerializeTables();

    //    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //    public delegate void BarcodeRowChangeEventHandler(object sender, BarcodeXML.BarcodeRowChangeEvent e);

    //    [Serializable]
    //    [XmlSchemaProvider("GetTypedTableSchema")]
    //    public class BarcodeDataTable : DataTable, IEnumerable
    //    {
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public BarcodeDataTable();
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        protected BarcodeDataTable(SerializationInfo info, StreamingContext context);

    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public DataColumn AlignmentColumn { get; }
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public DataColumn BackcolorColumn { get; }
    //        [Browsable(false)]
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public int Count { get; }
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public DataColumn CountryAssigningManufacturingCodeColumn { get; }
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public DataColumn EncodedValueColumn { get; }
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public DataColumn EncodingTimeColumn { get; }
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public DataColumn ForecolorColumn { get; }
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public DataColumn ImageColumn { get; }
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public DataColumn ImageFormatColumn { get; }
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public DataColumn ImageHeightColumn { get; }
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public DataColumn ImageWidthColumn { get; }
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public DataColumn IncludeLabelColumn { get; }
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public DataColumn LabelFontColumn { get; }
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public DataColumn LabelPositionColumn { get; }
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public DataColumn RawDataColumn { get; }
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public DataColumn RotateFlipTypeColumn { get; }
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public DataColumn TypeColumn { get; }

    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public BarcodeXML.BarcodeRow this[int index] { get; }

    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public event BarcodeXML.BarcodeRowChangeEventHandler BarcodeRowChanged;
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public event BarcodeXML.BarcodeRowChangeEventHandler BarcodeRowChanging;
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public event BarcodeXML.BarcodeRowChangeEventHandler BarcodeRowDeleted;
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public event BarcodeXML.BarcodeRowChangeEventHandler BarcodeRowDeleting;

    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public void AddBarcodeRow(BarcodeXML.BarcodeRow row);
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public BarcodeXML.BarcodeRow AddBarcodeRow(string Type, string RawData, string EncodedValue, double EncodingTime, bool IncludeLabel, string Forecolor, string Backcolor, string CountryAssigningManufacturingCode, int ImageWidth, int ImageHeight, string Image, RotateFlipType RotateFlipType, int LabelPosition, int Alignment, string LabelFont, string ImageFormat);
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public override DataTable Clone();
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        protected override DataTable CreateInstance();
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public virtual IEnumerator GetEnumerator();
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        protected override Type GetRowType();
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs);
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public BarcodeXML.BarcodeRow NewBarcodeRow();
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        protected override DataRow NewRowFromBuilder(DataRowBuilder builder);
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        protected override void OnRowChanged(DataRowChangeEventArgs e);
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        protected override void OnRowChanging(DataRowChangeEventArgs e);
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        protected override void OnRowDeleted(DataRowChangeEventArgs e);
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        protected override void OnRowDeleting(DataRowChangeEventArgs e);
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public void RemoveBarcodeRow(BarcodeXML.BarcodeRow row);
    //    }

    //    public class BarcodeRow : DataRow
    //    {
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public int Alignment { get; set; }
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public string Backcolor { get; set; }
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public string CountryAssigningManufacturingCode { get; set; }
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public string EncodedValue { get; set; }
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public double EncodingTime { get; set; }
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public string Forecolor { get; set; }
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public string Image { get; set; }
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public string ImageFormat { get; set; }
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public int ImageHeight { get; set; }
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public int ImageWidth { get; set; }
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public bool IncludeLabel { get; set; }
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public string LabelFont { get; set; }
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public int LabelPosition { get; set; }
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public string RawData { get; set; }
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public RotateFlipType RotateFlipType { get; set; }
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public string Type { get; set; }

    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public bool IsAlignmentNull();
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public bool IsBackcolorNull();
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public bool IsCountryAssigningManufacturingCodeNull();
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public bool IsEncodedValueNull();
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public bool IsEncodingTimeNull();
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public bool IsForecolorNull();
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public bool IsImageFormatNull();
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public bool IsImageHeightNull();
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public bool IsImageNull();
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public bool IsImageWidthNull();
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public bool IsIncludeLabelNull();
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public bool IsLabelFontNull();
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public bool IsLabelPositionNull();
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public bool IsRawDataNull();
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public bool IsRotateFlipTypeNull();
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public bool IsTypeNull();
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public void SetAlignmentNull();
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public void SetBackcolorNull();
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public void SetCountryAssigningManufacturingCodeNull();
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public void SetEncodedValueNull();
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public void SetEncodingTimeNull();
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public void SetForecolorNull();
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public void SetImageFormatNull();
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public void SetImageHeightNull();
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public void SetImageNull();
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public void SetImageWidthNull();
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public void SetIncludeLabelNull();
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public void SetLabelFontNull();
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public void SetLabelPositionNull();
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public void SetRawDataNull();
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public void SetRotateFlipTypeNull();
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public void SetTypeNull();
    //    }

    //    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //    public class BarcodeRowChangeEvent : EventArgs
    //    {
    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public BarcodeRowChangeEvent(BarcodeXML.BarcodeRow row, DataRowAction action);

    //        [DebuggerNonUserCode]
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        public DataRowAction Action { get; }
    //        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    //        [DebuggerNonUserCode]
    //        public BarcodeXML.BarcodeRow Row { get; }
    //    }
    //}
}