using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public class DirectoryServerDataModel
    {
        public DirectoryServerDataModel()
        {
            this.ChildFolderList = new List<string>();
        }
        public string CurrentFolder { get; set; }
        public string CurrentPath { get; set; }
        public List<string> ChildFolderList { get; set; }
    }
}
