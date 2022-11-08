using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public class RequestHistoryDataModel
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public string Action { get; set; }
        public string? Reason { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
