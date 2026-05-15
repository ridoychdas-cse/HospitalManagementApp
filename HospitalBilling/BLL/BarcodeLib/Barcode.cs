#region Assembly BarcodeLib.dll, v2.0.50727
// G:\Ridoy Project\KoryoClinic\Running\HospitalManagementApp(02-02-2023)\HospitalManagementApp\HospitalBilling\bin\BarcodeLib.dll
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace HospitalBilling.BLL.BarcodeLib
{
    public class Barcode : IDisposable
    {
        //public Barcode();
        //public Barcode(string data);
        //public Barcode(string data, TYPE iType);

        public AlignmentPositions Alignment { get; set; }
        public Color BackColor { get; set; }
        public string Country_Assigning_Manufacturer_Code { get; set; }
        public byte[] Encoded_Image_Bytes { get; set; }
        public Image EncodedImage { get; set; }
        public TYPE EncodedType { get; set; }
        public string EncodedValue { get; set; }
        public double EncodingTime { get; set; }
        public List<string> Errors { get; set; }
        public Color ForeColor { get; set; }
        public int Height { get; set; }
        public ImageFormat ImageFormat { get; set; }
        public bool IncludeLabel { get; set; }
        public Font LabelFont { get; set; }
        public LabelPositions LabelPosition { get; set; }
        public string RawData { get; set; }
        public RotateFlipType RotateFlipType { get; set; }
        public static Version Version { get; set; }
        public int Width { get; set; }
        public string XML { get; set; }

       public void Dispose()
       {}
       // public static Image DoEncode(TYPE iType, string Data);
        //public static Image DoEncode(TYPE iType, string Data, bool IncludeLabel);
       // public static Image DoEncode(TYPE iType, string Data, ref string XML);
       // public static Image DoEncode(TYPE iType, string Data, bool IncludeLabel, Color DrawColor, Color BackColor);
       // public static Image DoEncode(TYPE iType, string Data, bool IncludeLabel, int Width, int Height);
        //public static Image DoEncode(TYPE iType, string Data, bool IncludeLabel, Color DrawColor, Color BackColor, int Width, int Height);
       // public static Image DoEncode(TYPE iType, string Data, bool IncludeLabel, Color DrawColor, Color BackColor, int Width, int Height, ref string XML);
       // public Image Encode(TYPE iType, string StringToEncode);
        //public Image Encode(TYPE iType, string StringToEncode, Color ForeColor, Color BackColor);
      //  public Image Encode(TYPE iType, string StringToEncode, int Width, int Height);
      //  public Image Encode(TYPE iType, string StringToEncode, Color ForeColor, Color BackColor, int Width, int Height);
       // public byte[] GetImageData(SaveTypes savetype);
       //public static Image GetImageFromXML(BarcodeXML internalXML);

       public void GetSizeOfImage(ref double Width, ref double Height, bool Metric) { }
        public void SaveImage(Stream stream, SaveTypes FileType) { }
        public void SaveImage(string Filename, SaveTypes FileType) { }
    }
}