using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static SiemensProjectManagement.Models.SampleModel;

namespace SiemensProjectManagement.Controllers
{
    public class SampleController : Controller
    {
     
        // GET: Sample
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Sample()
        {
            List<Emp> emplst = new List<Emp>();
            emplst.Add(new Emp { EmpId = 1, Name = "Ankur" });
            emplst.Add(new Emp { EmpId = 2, Name = "Ajith" });
            emplst.Add(new Emp { EmpId = 2, Name = "Monika" });
            emplst.Add(new Emp { EmpId = 4, Name = "Suresh" });
            emplst.Add(new Emp { EmpId = 5, Name = "Ashutosh" });


            ViewData["emp"] = emplst;
            ViewBag.user = emplst;
            TempData["emp"] = emplst;


            return View();
        }
        public ActionResult GetData(int EmpId)
        {
            List<Emp> emplst = new List<Emp>();
            emplst.Add(new Emp { EmpId = 1, Name = "Ankur" });
            emplst.Add(new Emp { EmpId = 2, Name = "Ajith" });
            emplst.Add(new Emp { EmpId = 2, Name = "Monika" });
            emplst.Add(new Emp { EmpId = 4, Name = "Suresh" });
            emplst.Add(new Emp { EmpId = 5, Name = "Ashutosh" });

            var Result = emplst.Select(x => x).Where(x=>x.EmpId == EmpId).ToList();

            return Json(Result.ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}