using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiemensProjectManagement.Models;
//using ProjectMangementAPI.Models;


namespace SiemensProjectManagement.Controllers
{
    public class PlcVirtualizationController : Controller
    {
        // GET: PlcVirtualization
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PlcVirtualization()
        {
            //ProjectManagementDBEntities db = new ProjectManagementDBEntities();
          
            return View();
        }
        public ActionResult SampleLedCheck()
        {
            return View();
        }

        public ActionResult GetPlcdata(int plcid)
        {
           
            List<PlcVirtualizationModel> plcdet = new List<PlcVirtualizationModel>();
            ProjectManagementDB dbcontext = new ProjectManagementDB();
            plcdet = dbcontext.PlcDetails.Select(x => new PlcVirtualizationModel
            {
                PlcId = x.PlcId,
                Rack_No = x.PlcInfo.Rack_No,
                Plc_SerialNo = x.SerialNo,
                IpAddress = x.IpAddress,
                DeviceID = x.PlcInfo.DeviceId,
                PLC_Name = x.PLC_Name,
                PLC_Type = x.PLC_Type,
                Mlfb = x.Mlfb,
                Mac_Address = x.Mac_Address,
                Firmware = x.Firmware,
                Operating_State = x.Operating_State,
                Station_Name = x.Station_Name,
                SMC_SerialNo = x.SMC_SerialNo,
                Plant_DesignationId = x.Plant_DesignationId,
                Location_Id = x.Location_Id,
                IsPingable = x.IsPingable,
                IsFaulty = x.IsFaulty

            }).ToList();

            return Json(plcdet.ToArray(),JsonRequestBehavior.AllowGet);
        }
    }
}