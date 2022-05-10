using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public class transactionSearchModel
    {
        string tranSearchBillingNo { get; set; }
        string tranSearchStatus { get; set; }
        string tranSearchIcO2c { get; set; }
        string tranSearchCustomerCode { get; set; }
        string tranSearchCompanyCode { get; set; }
        string tranSearchOutputType { get; set; }
        string tranSearchDocumentType { get; set; }
        string tranSearchDateType { get; set; }
        string tranSearchDateBetween { get; set; }
        string tranSearchDataSource { get; set; }
        string tranSearchSellOrg { get; set; }
    }

    public class transactionSearchError
    {
        string tranSearchErrorBillingNo { get; set; }
        string tranSearchErrorDetail { get; set; }

    }
}
