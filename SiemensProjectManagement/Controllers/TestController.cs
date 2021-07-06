using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static SiemensProjectManagement.Models.SampleModel;

namespace SiemensProjectManagement.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            List<Emp> emplst = new List<Emp>();
            emplst.Add(new Emp { EmpId = 1, Name = "Ankur" });
            emplst.Add(new Emp { EmpId = 2, Name = "Ajith" });
            emplst.Add(new Emp { EmpId = 2, Name = "Monika" });
            emplst.Add(new Emp { EmpId = 4, Name = "Suresh" });
            emplst.Add(new Emp { EmpId = 5, Name = "Ashutosh" });

            ViewData["emp"] = emplst;


            return View();
        }
        public ActionResult Test()
        {
            List<Emp> emplst = new List<Emp>();
            emplst.Add(new Emp { EmpId = 1, Name = "Ankur" });
            emplst.Add(new Emp { EmpId = 2, Name = "Ajith" });
            emplst.Add(new Emp { EmpId = 2, Name = "Monika" });
            emplst.Add(new Emp { EmpId = 4, Name = "Suresh" });
            emplst.Add(new Emp { EmpId = 5, Name = "Ashutosh" });

            ViewData["emp"] = emplst;


            return View();
        }

    }
}