using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers.Etax
{
    [SessionExpire]
    public class DirectoryServerController : Controller
    {
        [SessionExpire]
        public IActionResult GetPathServer_Modal()
        {
            return View();
        }
        public JsonResult GetPathServer_Forward(string currentPath, string name)
        {
            var data = new DirectoryServerDataModel();
            var path = "";
            var configTask = Task.Run(() => ApiHelper.GetURI("api/ConfigGlobal/GetDetailByName?cate=APPLICATION&name=ROOTPATHOFSERVER")).Result;
            var config = new ConfigGlobal();
            if (configTask.STATUS)
            {
                config = JsonConvert.DeserializeObject<ConfigGlobal>(configTask.OUTPUT_DATA.ToString());
                path = config.ConfigGlobalValue;
            }

            if (!string.IsNullOrEmpty(currentPath))
                path = currentPath;

            if (!string.IsNullOrEmpty(name))
                path = Path.Combine(path, name);

            data.CurrentPath = path;
            data.CurrentFolder = path.Split("\\").Last();

            var filePaths = Directory.GetDirectories(path, "*").ToList();

            if (filePaths != null)
            {
                filePaths.ForEach(t =>
                {
                    data.ChildFolderList.Add(t.Split("\\").Last());
                });
            }

            return Json(data);
        }
        public JsonResult GetPathServer_Back(string path)
        {
            var data = new DirectoryServerDataModel();

            data.CurrentPath = path;
            data.CurrentFolder = path.Split("\\").Last();

            var filePaths = Directory.GetDirectories(path, "*").ToList();

            if (filePaths != null)
            {
                filePaths.ForEach(t =>
                {
                    data.ChildFolderList.Add(t.Split("\\").Last());
                });
            }

            return Json(data);
        }
    }
}
