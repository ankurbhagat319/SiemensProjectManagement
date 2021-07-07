using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiemensProjectManagement.Models
{
    public class PlcVirtualizationModel
    {

               public int? PlcId { get; set; }
               public string Rack_No             {get;set;}
              public string Plc_SerialNo { get; set; }
               public string IpAddress           {get;set;}
               public int? DeviceID            {get;set;}
               public string PLC_Name            {get;set;}
               public string PLC_Type            {get;set;}
               public string Mlfb                {get;set;}
               public string Mac_Address         {get;set;}
               public string Firmware            {get;set;}
               public string Operating_State     {get;set;}
               public string Station_Name        {get;set;}
               public string SMC_SerialNo        {get;set;}
               public string Plant_DesignationId {get;set;}
               public string Location_Id         {get;set;}
               public bool? IsPingable          {get;set;}
               public bool? IsFaulty            {get;set;}
    }
}