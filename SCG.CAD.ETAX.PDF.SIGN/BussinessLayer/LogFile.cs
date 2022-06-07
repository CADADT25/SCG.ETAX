using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.PDF.SIGN.BussinessLayer
{
    public class LogFile
    {
        public void InsertLog(string pathlog, string text)
        {
            if (!Directory.Exists(pathlog))
            {
                Directory.CreateDirectory(pathlog);
            }
            string filename = DateTime.Now.ToString("yyyyMMdd") + ".txt";
            pathlog = pathlog + filename;
            if (!File.Exists(pathlog))
            {
                using (FileStream fs = new FileStream(pathlog, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    StreamWriter write = new StreamWriter(fs);
                    write.BaseStream.Seek(0, SeekOrigin.End);
                    write.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff") + " : Start First Run");
                    write.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff") + " : " + text);
                    write.Flush();
                    write.Close();
                    fs.Close();
                }
            }
            else
            {
                using (FileStream fs = new FileStream(pathlog, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    StreamWriter write = new StreamWriter(fs);
                    write.BaseStream.Seek(0, SeekOrigin.End);
                    write.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff") + " : " + text);
                    write.Flush();
                    write.Close();
                    fs.Close();
                }
            }
        }
    }
}
