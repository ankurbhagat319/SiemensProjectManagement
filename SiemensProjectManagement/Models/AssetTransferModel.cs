using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiemensProjectManagement.Models
{
    public class AssetTransferModel
    {
        public string TransferId { get; set; }
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string AssetId { get; set; }
        public string AssetType_Id { get; set; }
        public string Responsible_UserId { get; set; }
       
        public string Requester_UserId { get; set; }
        
        public string Transfer_State { get; set; }
        public string Responsible_Comments { get; set; }
        public string Requester_Comments { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string IsCancelled { get; set; }
        public string IsAcknowledeged { get; set; }
        public string IsActive { get; set; }

        public string Responsible_Name { get; set; }

        public string Requester_Name { get; set; }
        public string AssetTypeName { get; internal set; }
    }
}