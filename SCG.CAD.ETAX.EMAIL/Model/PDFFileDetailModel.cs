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
        public string RenameFileName { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Doctype { get; set; }
    }
}
