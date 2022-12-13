using SCG.CAD.ETAX.MODEL.etaxModel;

namespace SCG.CAD.ETAX.MODEL.CustomModel
{
    public class PDFSignModel
    {
        public ConfigPdfSign configPdfSign { get; set; }
        public List<FilePDF> listFilePDFs { get; set; }
    }
    public class FilePDF
    {
        public string FullPath { get; set; }
        public string FileName { get; set; }
        public string Inbound { get; set; }
        public string Outbound { get; set; }
        public string Billno { get; set; }
        public string Comcode { get; set; }
    }
}
