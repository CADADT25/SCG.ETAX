using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public class RequestRelateDataModel
    {
        public RequestRelateDataModel()
        {
            this.RequestHistorys = new List<RequestHistory>();
            this.RequestItems = new List<RequestItem>();
        }
        public Guid RequestId { get; set; }
        public string RequestNo { get; set; }
        public string RequestAction { get; set; }
        public string StatusCode { get; set; }
        public string CompanyCode { get; set; }
        public string ManagerName { get; set; }
        public string ManagerEmail { get; set; }
        public string RequesterName { get; set; }
        public string RequesterEmail { get; set; }
        public string OfficerEmail { get; set; }
        public string OfficerName { get; set; }
        public DateTime RequestDate { get; set; }
        public List<RequestHistory> RequestHistorys { get; set; }
        public List<RequestItem> RequestItems { get; set; }
    }
}
