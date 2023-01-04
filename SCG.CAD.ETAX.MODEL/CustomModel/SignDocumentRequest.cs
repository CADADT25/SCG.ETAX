using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public class SignDocumentRequest
    {
        public string Company { get; set; }
        public string BillingNumber { get; set; }
        //public string PdfFileName{ get; set; }
        public string PdfEncodeBase64 { get; set; }
        //public string TextFileName { get; set; }
        public string TextEncodeBase64 { get; set; }
    }
}
