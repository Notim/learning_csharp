using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace telephoneBook.Controllers {

    public class ValuesController : Controller {
        [HttpGet, Route("Api/ListNews")]
        public JsonResult ListNews() {
            return Json(
                new {
                    teste  = "teste",
                    teste2 = "teste",
                    teste3 = "teste"
                }
            );
        }

        [HttpGet]
        public JsonResult Get() {
            return Json(null);
        }

        // GET Api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id) {

            return "value";
        }

        [HttpPost]
        public void Post(string value) { }

        // PUT Api/values/5
        [HttpPut("{id}")]
        public void Put(int id, string value) { }

        // DELETE Api/values/5
        [HttpDelete]
        public void Delete(int id) { }
    }

}