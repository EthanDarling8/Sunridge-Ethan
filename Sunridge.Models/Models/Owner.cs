using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunridge.Models
{
    public class Owner : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }
    }
}
