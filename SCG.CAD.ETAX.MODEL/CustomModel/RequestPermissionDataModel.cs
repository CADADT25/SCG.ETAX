﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public class RequestPermissionDataModel
    {
        public List<string> CompanyCodeList { get; set; }
        public int? Level { get; set; }
    }
}