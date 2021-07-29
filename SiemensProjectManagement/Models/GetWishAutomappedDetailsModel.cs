using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiemensProjectManagement.Models
{
    public class GetWishAutomappedDetailsModel
    {
        public int WishlistId { get; set; }
        public string WishTypeName { get; set; }
        public string ProjectName { get; set; }
        public string AssetType_Name { get; set; }
        public string Asset { get; set; }
        public int AvailableQuantity { get; set; }
        public string Description { get; set; }
        public int? RequestDuration { get; set; }
        public int? AvailableDuration { get; set; }
    }
}