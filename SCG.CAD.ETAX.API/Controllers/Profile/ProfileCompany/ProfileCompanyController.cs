using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace SCG.CAD.ETAX.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ProfileCompanyController : BaseController
    {
        private readonly IProfileCompanyRepository repo;

        public ProfileCompanyController()
        {
            repo = new ProfileCompanyRepository();
        }


        [HttpGet]
        [Route("GetListAll")]
        public IActionResult GetListAll()
        {
            var result = repo.GET_LIST().Result;

            return Ok(result);
        }

        [HttpGet]
        [Route("GetDetail")]
        public IActionResult GetDetail(int id)
        {
            var result = repo.GET_DETAIL(id).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert(ProfileCompany param)
        {
            var result = repo.INSERT(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(ProfileCompany param)
        {
            var result = repo.UPDATE(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(ProfileCompany param)
        {
            var result = repo.DELETE(param).Result;

            return Ok(result);
        }
        [HttpGet]
        [Route("ExportDataProfileCompany")]
        public IActionResult ExportDataProfileCompany()
        {
            var result = repo.ExportDataProfileCompany().Result;

            return Ok(result);
        }


    }
}
