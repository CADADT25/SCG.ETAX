using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ConfigMftsCompressPrintSettingController : BaseController
    {
        private readonly IConfigMftsCompressPrintSettingRepository repo;

        public ConfigMftsCompressPrintSettingController()
        {
            repo = new ConfigMftsCompressPrintSettingRepository();
        }


        [HttpGet]
        [Route("GetListAll")]
        public IActionResult GetTaxCodeAll()
        {
            var result = repo.GET_LIST().Result;

            return Ok(result);
        }

        [HttpGet]
        [Route("GetDetail")]
        public IActionResult GetTaxCodeDetail(int id)
        {
            var result = repo.GET_DETAIL(id).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert(ConfigMftsCompressPrintSetting param)
        {
            var result = repo.INSERT(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(ConfigMftsCompressPrintSetting param)
        {
            var result = repo.UPDATE(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(ConfigMftsCompressPrintSetting param)
        {
            var result = repo.DELETE(param).Result;

            return Ok(result);
        }


        [HttpPost]
        [Route("UpdateOneTime")]
        public IActionResult UpdateOneTime(ConfigMftsCompressPrintSetting param)
        {
            var result = repo.UPDATE_ONETIME(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("UpdateAnyTime")]
        public IActionResult UpdateAnyTime(ConfigMftsCompressPrintSetting param)
        {
            var result = repo.UPDATE_ANYTIME(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteOneTime")]
        public IActionResult DeleteOneTime(DeleteOnetime param)
        {
            var result = repo.DELETE_ONETIME(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteAnyTime")]
        public IActionResult DeleteAnyTime(DeleteAnytime param)
        {
            var result = repo.DELETE_ANYTIME(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("UpdateNextTime")]
        public IActionResult UpdateNextTime(ConfigNextTime param)
        {
            var result = repo.UPDATE_NEXTTIME(param).Result;

            return Ok(result);
        }


    }
}
