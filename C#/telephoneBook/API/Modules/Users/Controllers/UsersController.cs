
using System;
using System.Linq;

using BLL.Users;
using BLL.Users.Services;

using DAL.Entities;

using Microsoft.AspNetCore.Mvc;

using UTIL.methods;

namespace telephoneBook.Modules.News.Controllers {

    public class UsersController : Controller {
        private IUserServices _IUserServices;
        
        private IUserServices UserServices 
            => _IUserServices = _IUserServices 
            ?? new UserServices();

        [HttpGet, Route("Api/ListUsers")]
        public JsonResult ListNews() {
            /*
            var newAdd = new User();
            newAdd.name  = Populate.GenerateNames(2);
            newAdd.email = Populate.GenerateMail(newAdd.name);
            newAdd.city = "hamlet";
            newAdd.telephoneNumber = "+551234565454";
            newAdd.state = "CA";
            newAdd.surname = newAdd.name.Split(" ")[1];
            newAdd.createdDate = DateTime.Now;
            newAdd.active = "Y";
            newAdd.excludeDate = null;

            UserServices.Save(newAdd);
            */
                
            var list = UserServices.List().ToList();

            return Json(list);
        }

        [HttpGet, Route("Api/get")]
        public JsonResult Get() {

            return Json(null);
        }

        // GET Api/values/5
        [HttpGet, Route("Api/get/{id}")]
        public ActionResult<string> Get(int id) {

            return "value";
        }

        [HttpPost, Route("Api/post")]
        public void Post(string value) { }

        // PUT Api/values/5
        [HttpPut, Route("Api/put/{id}")]
        public void Put(int id, string value) { }

        // DELETE Api/values/5
        [HttpDelete, Route("Api/delete/{id}")]
        public void Delete(int id) { }
    }

}