using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.MODEL.CustomModel
{
    public class APISendFileXMLSignModel
    {
        // 0 test, 1 prod
        public string environment { get; set; }
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
        //Tax Invoice = ‘388’,’T02’,’T03’,’T04’ Receipt = ‘T01’ Debit Credit Note = ‘80’,’81’
        public string documentType { get; set; }
        //file pdf for sign in format byte[] encode base64
        public string fileEncode { get; set; }
    }
}
