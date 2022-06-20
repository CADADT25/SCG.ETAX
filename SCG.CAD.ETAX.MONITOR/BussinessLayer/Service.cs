using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.MONITOR.Controllers;
using System.ServiceProcess;

namespace SCG.CAD.ETAX.MONITOR.BussinessLayer
{
    public class Service
    {
        List<ConfigGlobal> configGlobal = new List<ConfigGlobal>();
        ConfigGlobalController configGlobalController = new ConfigGlobalController();
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

                throw ex;
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
                throw ex;
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
                throw ex;
            }
        }

        public List<ConfigGlobal> GetConfigGlobal()
        {
            try
            {
                configGlobal = configGlobalController.List().Result;
                return configGlobal;
            }
            catch (Exception ex)
            {
                throw ex;
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
                throw ex;
            }
            return result;
        }

        public string ReadFileOnly(string path)
        {
            string text = "";
            try
            {
                if (!String.IsNullOrEmpty(path))
                {
                    text = File.ReadAllText(path);
                }

                //using (FileStream stream = File.Open("path to file", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                //{
                //    using (StreamReader reader = new StreamReader(stream))
                //    {
                //        while (!reader.EndOfStream)
                //        {

                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw ex;
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
