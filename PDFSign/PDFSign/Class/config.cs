using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFSign.Class
{
    public class config
    {
        private string getinboundpath;
        public string GetInBoundPath
        {
            get
            {
                return getinboundpath;
            }
            set
            {
                getinboundpath = ConfigurationSettings.AppSettings["InBoundPath"];
            }
        }

        private string getoutboundpath;
        public string GetOutBoundPath
        {
            get
            {
                return getoutboundpath;
            }
            set
            {
                getoutboundpath = ConfigurationSettings.AppSettings["OutBoundPath"];
            }
        }

        private string getfiletypepath;
        public string GetFileTypePDF
        {
            get
            {
                return getfiletypepath;
            }
            set
            {
                getfiletypepath = ConfigurationSettings.AppSettings["FileTypePDF"];
            }
        }
    }
}
