using SCG.CAD.ETAX.MODEL.etaxModel;

namespace SCG.CAD.ETAX.PDF.SIGN.Models
{
    public class PDFSignModel
    {
        public ConfigPdfSign configPdfSign { get; set; }
        public List<ListFilePDF> listFilePDFs { get; set; }
    }
    public class ListFilePDF
    {
        public string FullPath { get; set; }
        public string FileName { get; set; }
        public string Inbound { get; set; }
        public string Outbound { get; set; }
        public string Billno { get; set; }
    }

}
