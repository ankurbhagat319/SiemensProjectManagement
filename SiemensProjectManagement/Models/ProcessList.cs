//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SiemensProjectManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProcessList
    {
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public Nullable<int> TransferId { get; set; }
        public Nullable<int> Responsible_UserId { get; set; }
        public Nullable<int> Requester_UserId { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        public Nullable<bool> IsRejected { get; set; }
        public Nullable<bool> IsCancelled { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual AssestTransfer AssestTransfer { get; set; }
    }
}
