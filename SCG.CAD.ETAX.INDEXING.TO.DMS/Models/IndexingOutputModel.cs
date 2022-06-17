using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.INDEXING.TO.DMS.Models
{
    public class IndexingOutputModel
    {
        public string FIleName { get; set; }
        public List<string> FIleValues { get; set; }
    }

    public class LoginIndexFIle
    {
        public string ImageName { get; set;}
        public string ImageDocDd { get; set; }
        public string Result { get; set; }
        public string Massage { get; set; }
    }
}
