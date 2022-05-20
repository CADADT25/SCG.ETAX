using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.EMAIL.Model
{
    public class PDFFileDetailModel
    {
        public string FullPath { get; set; }
        public string BillingNo { get; set; }
        public DateTime BillingDate { get; set; }
    }
}
