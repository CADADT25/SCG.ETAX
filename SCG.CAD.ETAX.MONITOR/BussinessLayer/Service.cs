using SCG.CAD.ETAX.MODEL.etaxModel;
using System.Net.Http.Headers;
using System.ServiceProcess;

namespace SCG.CAD.ETAX.MONITOR.BussinessLayer
{
    public class Service
    {
        List<ConfigGlobal> configGlobal = new List<ConfigGlobal>();
        public string GetStatusService(string serviceName)
        {
            try
            {
                ServiceController[] services = ServiceController.GetServices();
                var servicesStatus = services.ToDictionary(s => s.ServiceName, s => s.Status);

                // try to find service name
                foreach (ServiceController service in services)
                {
                    if (service.ServiceName == serviceName)
                        return service.Status.ToString();
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
            return "";
        }

        public void StartService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        public void StopService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        public List<string> ReadAllLogFile(string path)
        {
            List<string> result = new List<string>();
            DirectoryInfo directoryInfo;
            string fileType = "*.txt"; 
            List<FileInfo> listpath;
            try
            {
                if (Directory.Exists(path))
                {
                    directoryInfo = new DirectoryInfo(path);
                    listpath = directoryInfo.GetFiles(fileType)
                     .OrderByDescending(f => f.LastWriteTime).ToList();

                    result = listpath.Select(x=> x.FullName).ToList();
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
            return result;
        }

        public string ReadFileOnly(string path)
        {
            string text = "";
            try
            {
                //if (!String.IsNullOrEmpty(path))
                //{
                //    if (File.Exists(path))
                //    {
                //        text = File.ReadAllText(path);
                //    }
                //}
                if (!String.IsNullOrEmpty(path))
                {
                    using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            text = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //ShowMessageBox(ex.Message);
            }
            return text;
        }

        public async Task SetDelay()
        {
            await Task.Delay(10000);
        }

        public void ShowMessageBox(string message)
        {
            MessageBox.Show(message,"Error");
        }
    }
}
