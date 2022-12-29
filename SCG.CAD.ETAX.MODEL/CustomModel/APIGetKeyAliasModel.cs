using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.MODEL.CustomModel
{
    public class APIGetKeyAliasModel
    {
        public string totalCertInfo { get; set; }
        public string resultCode { get; set; }
        public string resultDes { get; set; }
        public List<CertInfoList> certInfoList { get; set; }
    }

    public class CertInfoList
    {
        public string certSerial { get; set; }
        public string keyAlias { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string commonName { get; set; }
    }
}
