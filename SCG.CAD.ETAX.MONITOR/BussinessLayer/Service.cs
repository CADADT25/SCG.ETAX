using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.MONITOR.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

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
            catch
            {
                // ...
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
            catch
            {
                // ...
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

    }
}
