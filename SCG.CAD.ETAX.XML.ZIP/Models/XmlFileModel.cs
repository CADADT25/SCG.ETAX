using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.XML.ZIP.Models
{
    public class XmlFileModel
    {
        public string CompanyCode { get; set; }
        public string InputPath { get; set; }
        public string OutPath { get; set; }
        public List<ListByDocumentType> listByDocumentTypes { get; set; }
    }
    public class ListByDocumentType
    {
        public string DocumentType { get; set; }
        public List<XmlFileDetail> XmlFileDetails { get; set; }
    }
    public class XmlFileDetail
    {
        public string BillingNo { get; set; }
        public string FileName { get; set; }
        public string FullPath { get; set; }
    }
}
