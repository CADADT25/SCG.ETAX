using Microsoft.AspNetCore.Mvc;
using static SCG.CAD.ETAX.MODEL.Revenue.ETDA.CodeList.City.TISICityNameModel;
using static SCG.CAD.ETAX.MODEL.Revenue.ETDA.CodeList.Provice.ThaiISOCountrySubdivisionCodeModel;
using static SCG.CAD.ETAX.MODEL.Revenue.ETDA.CodeList.SubDivision.TISICitySubDivisionNameModel;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class ProfileSellerController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult _Content()
        {
            return View();
        }

        public IActionResult _Modal()
        {
            return View();
        }



        public async Task<JsonResult> Detail(int id)
        {
            List<ProfileSeller> tran = new List<ProfileSeller>();

            Response resp = new Response();

            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileSeller/GetDetail?id= " + id + " "));

                if (task.STATUS)
                {

                    tran = JsonConvert.DeserializeObject<List<ProfileSeller>>(task.OUTPUT_DATA.ToString());

                    result = JsonConvert.SerializeObject(tran[0]);

                }
                else
                {
                    ViewBag.Error = task.MESSAGE;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }

            return Json(result);
        }

        public async Task<JsonResult> List()
        {
            Response resp = new Response();

            List<ProfileSeller> tran = new List<ProfileSeller>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileSeller/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileSeller>>(task.OUTPUT_DATA.ToString());
                }
                else
                {
                    ViewBag.Error = task.MESSAGE;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }


            return Json(new { data = tran });
        }

        public async Task<JsonResult> Insert(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileSeller/Insert", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Update(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileSeller/Update", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Delete(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileSeller/Delete", httpContent));

            return Json(task);
        }

        public async Task<ActionResult> ExportToCsv()
        {
            Response resp = new Response();

            List<ProfileSeller> tran = new List<ProfileSeller>();

            var strBuilder = new StringBuilder();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileSeller/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileSeller>>(task.OUTPUT_DATA.ToString());

                    if (tran.Count() > 0)
                    {
                        strBuilder.AppendLine("" +
                            "SellerNo," +
                            "CompanyCode," +
                            "BranchCode," +
                            "Province," +
                            "District," +
                            "SubDistrict," +
                            "Road," +
                            "Building," +
                            "AddressNumber," +
                            "SellerEmail," +
                            "CreateBy," +
                            "CreateDate," +
                            "UpdateBy," +
                            "UpdateDate," +
                            "Isactive");


                        foreach (var item in tran)
                        {
                            strBuilder.AppendLine($"" +
                                $"{item.SellerNo}," +
                                $"{item.CompanyCode}," +
                                $"{item.BranchCode}," +
                                $"{item.Province}," +
                                $"{item.District}," +
                                $"{item.SubDistrict}," +
                                $"{item.Road}," +
                                $"{item.Building}," +
                                $"{item.Addressnumber}," +
                                $"{item.SellerEmail}," +
                                $"{item.CreateBy}," +
                                $"{item.CreateDate}," +
                                $"{item.UpdateBy}," +
                                $"{item.UpdateDate}," +
                                $"{item.Isactive}");
                        }

                        resp.STATUS = true;
                    }
                    else
                    {
                        resp.STATUS = false;
                    }
                }
                else
                {
                    ViewBag.Error = task.MESSAGE;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
            }

            return File(Encoding.UTF8.GetBytes(strBuilder.ToString()), "text/csv", "scg-etax-ProfileSeller.csv");

        }


        public async Task<JsonResult> GetProvince()
        {
            Response resp = new Response();

            List<ETDAProvice> tran = new List<ETDAProvice>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ETDA/GetProviceFromETDA"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ETDAProvice>>(task.OUTPUT_DATA.ToString());

                    tran = tran.ToList();
                }
                else
                {
                    ViewBag.Error = task.MESSAGE;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }


            return Json(tran);
        }

        public async Task<JsonResult> GetDistrict(string proviceCode)
        {
            Response resp = new Response();

            List<ETDADistrict> tran = new List<ETDADistrict>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ETDA/GetDistrictFromETDA?ProviceCode=" + proviceCode + ""));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ETDADistrict>>(task.OUTPUT_DATA.ToString());

                    tran = tran.ToList();
                }
                else
                {
                    ViewBag.Error = task.MESSAGE;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }

            return Json(tran);
        }

        public async Task<JsonResult> GetSubDistrict(string districtCode)
        {
            Response resp = new Response();

            List<ETDASubDistrict> tran = new List<ETDASubDistrict>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ETDA/GetSubDistrictFromETDA?DistrictCode=" + districtCode + ""));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ETDASubDistrict>>(task.OUTPUT_DATA.ToString());

                    tran = tran.ToList();
                }
                else
                {
                    ViewBag.Error = task.MESSAGE;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }

            return Json(tran);
        }

    }
}
