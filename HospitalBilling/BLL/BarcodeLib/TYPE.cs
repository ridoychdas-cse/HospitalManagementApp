using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalBilling.BLL.BarcodeLib
{
    public enum TYPE
    {
        UNSPECIFIED = 0,
        UPCA = 1,
        UPCE = 2,
        UPC_SUPPLEMENTAL_2DIGIT = 3,
        UPC_SUPPLEMENTAL_5DIGIT = 4,
        EAN13 = 5,
        EAN8 = 6,
        Interleaved2of5 = 7,
        Standard2of5 = 8,
        Industrial2of5 = 9,
        CODE39 = 10,
        CODE39Extended = 11,
        Codabar = 12,
        PostNet = 13,
        BOOKLAND = 14,
        ISBN = 15,
        JAN13 = 16,
        MSI_Mod10 = 17,
        MSI_2Mod10 = 18,
        MSI_Mod11 = 19,
        MSI_Mod11_Mod10 = 20,
        Modified_Plessey = 21,
        CODE11 = 22,
        USD8 = 23,
        UCC12 = 24,
        UCC13 = 25,
        LOGMARS = 26,
        CODE128 = 27,
        CODE128A = 28,
        CODE128B = 29,
        CODE128C = 30,
        ITF14 = 31,
        CODE93 = 32,
        TELEPEN = 33,
        FIM = 34,
        PHARMACODE = 35,
    }
}