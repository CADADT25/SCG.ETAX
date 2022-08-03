using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.MODEL.CustomModel
{
    public class APIGetHSMSerialModel
    {
        public string totalHsmSerial { get; set; }
        public string resultCode { get; set; }
        public string resultDes { get; set; }
        public HSMSerialList hsmSerialList { get; set; }
    }

    public class HSMSerialList
    {
        public string hsmSerial { get; set; }
    }
}
