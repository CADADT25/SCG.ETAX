﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCG.CAD.ETAX.API.Repositories.Profile.ConnectHSM;

namespace SCG.CAD.ETAX.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ConnectHSMController : BaseController
    {
        private readonly IConnectHSMRepository repo;

        public ConnectHSMController()
        {
            repo = new ConnectHSMRepository();
        }

        [HttpGet]
        [Route("GetKeyAlias")]
        public IActionResult GetKeyAlias(string hsmName, string hsmSerial)
        {
            var result = repo.GetKeyAlias(hsmName, hsmSerial).Result;

            return Ok(result);
        }
        [HttpGet]
        [Route("GetHSMSerial")]
        public IActionResult GetHSMSerial(string hsmName)
        {
            var result = repo.GetHSMSerial(hsmName).Result;

            return Ok(result);
        }
        [HttpPost]
        [Route("PostKeyAlias")]
        public IActionResult PostKeyAlias(APIPostKeyAliasModel data)
        {
            var result = repo.GetKeyAlias(data.hsmName, data.hsmSerial).Result;

            return Ok(result);
        }
        [HttpPost]
        [Route("PostHSMSerial")]
        public IActionResult PostHSMSerial(APIPostHSMSerialModel data)
        {
            var result = repo.GetHSMSerial(data.hsmName).Result;

            return Ok(result);
        }
    }
}
