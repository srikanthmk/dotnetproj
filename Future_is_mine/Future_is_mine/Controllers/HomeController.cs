using Future_is_mine.SQLOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Future_is_mine.Controllers
{
    public class HomeController : Controller
    {
        Employee_CURD _Employee_CURD;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Jobs() {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public JsonResult DisplayJob()
        {
            string query = "select job_id,job_info,left(job_description,200) as job_description,jobtype,location from job_description ";
            _Employee_CURD = new Employee_CURD();
            var data = _Employee_CURD.GetJodDetails(query);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}