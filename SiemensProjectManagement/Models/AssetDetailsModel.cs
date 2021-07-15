using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiemensProjectManagement.Models
{
    public class AssetDetailsModel
    {
      														
          public int? UserID { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public int? ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Type { get; set; }
        public Nullable<int> AssetType_Id { get; set; }
        public string AssetType_Name { get; set; }
        public string Manufacturer { get; set; }
        public string Resources_Class { get; set; }
        public string Serial_No { get; set; }
        public string HostName { get; set; }
        public string SpiridonNo { get; set; }
        public string Location { get; set; }
        public string PRNO { get; set; }
        public string PONO { get; set; }
        public string WarrantyStartDate { get; set; }
        public string AgeOfAsset { get; set; }
        public string ExpireBy { get; set; }
        public string Owner { get; set; }
        public string RAM { get; set; }
        public string Storage { get; set; }
        public string Processor { get; set; }
        public string CPUClockSpeed { get; set; }
        public string PhysicalCores { get; set; }
        public string NIC_Count { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool ISActive { get; set; }
        public bool ToBeReplaced { get; set; }
        public bool Acknowledge { get; set; }

    }
}