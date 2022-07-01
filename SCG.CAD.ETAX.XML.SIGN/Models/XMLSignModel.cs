using SCG.CAD.ETAX.MODEL.etaxModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.XML.SIGN.Models
{
    public class XMLSignModel
    {
        public ConfigXmlSign configPdfSign { get; set; }
        public List<ListFileXML> listFileXMLs { get; set; }
    }
    public class ListFileXML
    {
        public string FullPath { get; set; }
        public string FileName { get; set; }
        public string Inbound { get; set; }
        public string Outbound { get; set; }
        public string Billno { get; set; }
    }
}
