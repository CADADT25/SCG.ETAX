using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public class ManageRequestCartModel
    {
        public ManageRequestCartModel()
        {
            this.RequestCarts = new List<RequestCart>();
        }
        public List<RequestCart> RequestCarts { get; set; }
        public string Manager { get; set; }
        public string Action { get; set; }
    }
}
