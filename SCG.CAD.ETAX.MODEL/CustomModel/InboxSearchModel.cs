using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public class InboxSearchModel
    {
        public string EmailUser { get; set; }
        public string? RequestNo { get; set; }
        public List<string>? RequestAction { get; set; }   
        public List<string>? RequestStatus { get; set; }
    }
}
