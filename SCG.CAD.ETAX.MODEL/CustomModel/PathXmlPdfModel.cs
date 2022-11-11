using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public class PathXmlPdfModel
    {
        public PathXmlPdfModel()
        {
            this.Xmls = new List<string>();
            this.Pdfs = new List<string>();
        }
        public List<string> Xmls { get; set; }
        public List<string> Pdfs { get; set; }
    }
}
