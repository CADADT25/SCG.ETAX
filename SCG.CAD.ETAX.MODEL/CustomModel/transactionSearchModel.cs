using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public class transactionSearchModel
    {
        public string tranSearchBillingNo { get; set; }
        public string tranSearchStatus { get; set; }
        public string tranSearchIcO2c { get; set; }
        public string tranSearchCustomerCode { get; set; }
        public string tranSearchCompanyCode { get; set; }
        public string tranSearchOutputType { get; set; }
        public string tranSearchDocumentType { get; set; }
        public string tranSearchDateType { get; set; }
        public string tranSearchDateBetween { get; set; }
        public string tranSearchDataSource { get; set; }
        public string tranSearchSellOrg { get; set; }

    }
}
