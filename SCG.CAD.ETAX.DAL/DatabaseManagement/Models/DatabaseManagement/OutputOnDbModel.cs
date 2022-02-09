using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.DAL.MODEL
{
    public class OutputOnDbModel
    {
        public bool StatusOnDb { get; set; }
        public string? ClassOnDb { get; set; }
        public string? MethodOnDb { get; set; }
        public DataTable? ResultOnDb { get; set; }
        public string? MessageOnDb { get; set; }
        public string? TotalCountOnDb { get; set; }
    }
}
