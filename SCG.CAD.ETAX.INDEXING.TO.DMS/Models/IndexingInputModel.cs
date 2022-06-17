using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.INDEXING.TO.DMS.Models
{
    public class IndexingInputModel
    {
        public string CompanyCode { get; set; }
        public string SourceNameInput { get; set; }
        public string SourceNameOutput { get; set; }
        public string OcType { get; set; }
        public List<ImageDocType> ImageDocTypes { get; set; }
    }

    public class ImageDocType
    {
        public string BillNo { get; set; }
        public string Year { get; set; }
        public string PathFilePDF { get; set; }
        public string ReName { get; set; }
        public string ImageDocumentType { get; set; }
        public string FiDocNumber { get; set; }
    }
}
