﻿using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.API.Controllers
{
    public class RequestCartController : BaseController
    {
        private readonly IRequestCartRepository repo;
        public RequestCartController()
        {
            repo = new RequestCartRepository();
        }

        [HttpPost]
        [Route("Search")]
        public IActionResult Search(RequestCartSearchModel req)
        {
            var result = repo.SEARCH(req).Result;

            return Ok(result);
        }
        [HttpPost]
        [Route("SearchFull")]
        public IActionResult SearchFull(RequestCartSearchModel req)
        {
            var result = repo.SEARCH_FULL_DATA(req).Result;

            return Ok(result);
        }

        [HttpGet]
        [Route("GetListAll")]
        public IActionResult GetListAll()
        {
            var result = repo.GET_LIST().Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert(RequestCart param)
        {
            var result = repo.INSERT(param).Result;

            return Ok(result);
        }
        [HttpPost]
        [Route("InsertList")]
        public IActionResult InsertList(List<RequestCart> param)
        {
            var result = repo.INSERT(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(RequestCart param)
        {
            var result = repo.UPDATE(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(RequestCart param)
        {
            var result = repo.DELETE(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("MultiDelete")]
        public IActionResult MultiDelete(List<RequestCart> param)
        {
            var result = repo.DELETE(param).Result;

            return Ok(result);
        }
    }
}
