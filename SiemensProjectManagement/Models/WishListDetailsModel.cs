using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiemensProjectManagement.Models
{
    public class WishListDetailsModel
    {
        public int WishlistId { get; set; }
        public string WishTypeName { get; set; }
        public string UserName { get; set; }
        public string ProjectName { get; set; }
        public string AssetType_Name { get; set; }
        public string Asset { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string RequestDuration { get; set; }
    }
}