using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models {
    public class Owner : IdentityUser {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public bool BoardMember { get; set; }
        public string Position { get; set; }

        [NotMapped]
        public string FullName {
            get { return FirstName + " " + LastName; }
        }
    }
}
