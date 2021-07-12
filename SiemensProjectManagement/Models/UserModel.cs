using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiemensProjectManagement.Models
{
    public class UserModel
    {
        public IEnumerable<User> Users { get; set; }

        public int selectedUserID { get; set; }

        public string selectedUsername { get; set; }

    }
}