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
    
    public partial class GetWishAutoMappedDetails_Result
    {
        public int WishlistId { get; set; }
        public string WishTypeName { get; set; }
        public string ProjectName { get; set; }
        public string AssetType_Name { get; set; }
        public string Asset { get; set; }
        public int RequestDuration { get; set; }
        public int AvailableDuration { get; set; }
        public Nullable<int> AvailableQuantity { get; set; }
        public string Description { get; set; }
    }
}
