using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public class SignDocumentResponse
    {
        public string PdfSignedEncodeBase64 { get; set; }
        public string XmlSignedEncodeBase64 { get; set; }
    }
}
