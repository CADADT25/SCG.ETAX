using SCG.CAD.ETAX.MODEL.etaxModel;

namespace SCG.CAD.ETAX.MODEL.CustomModel
{
    public class XMLSignModel
    {
        public ConfigXmlSign configXmlSign { get; set; }
        public List<FileXML> listFileXMLs { get; set; }
        public FileXML fileXML { get; set; }
    }
    public class FileXML
    {
        public string FullPath { get; set; }
        public string FileName { get; set; }
        public string Inbound { get; set; }
        public string Outbound { get; set; }
        public string Billno { get; set; }
    }
}
