using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public class RequestDataModel
    {
        public RequestDataModel()
        {
            this.RequestCartList = new List<RequestCartDataModel>();
        }
        public string Action { get; set; }
        public string Manager { get; set; }
        public string UserBy { get; set; }
        public List<RequestCartDataModel> RequestCartList { get; set; }
        public List<PathXmlPdfModel> PathList { get; set; }
    }
}
