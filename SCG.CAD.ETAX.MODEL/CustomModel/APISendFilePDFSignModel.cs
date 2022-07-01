using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.MODEL.CustomModel
{
    public class APISendFilePDFSignModel
    {
        //‘eToken’,’PSE,’Luna’’
        public string hsmName { get; set; }
        //HSM Serail (slot)
        public string hsmSerial { get; set; }
        //HSM Slot password
        public string slotPassword { get; set; }
        //Key Alias
        public string keyAlias { get; set; }
        //Certificate serial number(hex number)
        public string certSerial { get; set; }
        //file pdf for sign in format byte[] encode base64
        public string fileEncode { get; set; }
        //‘text’, ‘image’, ‘hide’
        public string signatureType { get; set; }
        //Coordinate of signature (Upper Left X)
        public int coordinateUpperLeftX { get; set; }
        //Coordinate of signature (Upper Left Y)
        public int coordinateUpperLeftY { get; set; }
        //Page show signature (‘first’, ‘last’ and  ‘number’ e.g. 1,2,3 )
        public string page { get; set; }
        //Font name 
        public string fontName { get; set; }
        //Font size 
        public int fontSize { get; set; }
        //‘blod’, ‘plain’, ‘italic’ (default is plain)
        public string fontType { get; set; }
        //Space between line (default is 0)
        public int fontSpace { get; set; }
        //Hex color code e.g.tangerine yellow = ‘FFCC00’
        public string fontColor { get; set; }
        //file image for show on signature in format byte[] encode base64
        public string imageEncode { get; set; }
        //Reason of digital signature
        public string reason { get; set; }
        //Location of digital signature
        public string location { get; set; }
    }
}
