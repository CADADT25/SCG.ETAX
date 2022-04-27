using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigMftsIndexGenerationSettingOutputController : ControllerBase
    {


        private readonly IConfigMftsIndexGenerationSettingOutputRepository repo;

        public ConfigMftsIndexGenerationSettingOutputController()
        {
            repo = new ConfigMftsIndexGenerationSettingOutputRepository();
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
        public IActionResult Insert(ConfigMftsIndexGenerationSettingOutput param)
        {
            var result = repo.INSERT(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(ConfigMftsIndexGenerationSettingOutput param)
        {
            var result = repo.UPDATE(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(ConfigMftsIndexGenerationSettingOutput param)
        {
            var result = repo.DELETE(param).Result;

            return Ok(result);
        }


    }
}
