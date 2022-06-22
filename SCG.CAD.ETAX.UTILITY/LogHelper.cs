using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.UTILITY
{
    public class LogHelper
    {
        public void InsertLog(string pathlog, string text)
        {
            try
            {
                if (!Directory.Exists(pathlog))
                {
                    Directory.CreateDirectory(pathlog);
                }
                string filename = DateTime.Now.ToString("yyyyMMdd") + ".txt";
                pathlog = pathlog + filename;
                if (!File.Exists(pathlog))
                {
                    while (!IsFileLocked(pathlog))
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
            catch (Exception ex)
            {

            }
        }

        public bool IsFileLocked(string pathfile)
        {
            FileStream stream = null;
            bool checkfile = false;
            try
            {
                stream = new FileStream(pathfile, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                checkfile = true;
                //stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return checkfile;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return checkfile;
        }
    }
}
