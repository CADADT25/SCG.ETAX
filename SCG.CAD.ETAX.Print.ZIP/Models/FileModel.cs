using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.XML.PRINT.ZIP.Models
{
    public class FileModel
    {
        public string CompanyCode { get; set; }
        public string InputPath { get; set; }
        public string OutPath { get; set; }
        public List<Filedetail> FileDetails { get; set; }
    }
    public class Filedetail
    {
        public string BillingNo { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }
}
