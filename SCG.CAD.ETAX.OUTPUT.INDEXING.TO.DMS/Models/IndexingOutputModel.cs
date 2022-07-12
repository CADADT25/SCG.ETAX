using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.OUTPUT.INDEXING.TO.DMS.Models
{
    public class IndexingOutputModel
    {
        public string SourceName { get; set; }
        public string Path { get; set; }
        public List<FileDetail> FileDetails { get; set; }
    }

    public class LoginIndexFIle
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public string ImageName { get; set;}
        public string ImageDocDd { get; set; }
        public string Result { get; set; }
        public string Massage { get; set; }
    }

    public class FileDetail
    {
        public string FileName { get; set; }
        public List<string> FileValues { get; set; }
    }
}
