using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.MONITOR.Models
{
    public enum MonitorProgram
    {
        Monitor_EMAIL = 1,
        Monitor_PDFSign = 2,
        Monitor_PRINTZip = 3,
        Monitor_XMLGenerator = 4,
        Monitor_XMLSign = 5,
        Monitor_XMLZip = 6,
        Monitor_INPUTINDEXING = 7,
        Monitor_OUTPUTINDEXING = 8,
        NotMonitor = 99
    }
}
