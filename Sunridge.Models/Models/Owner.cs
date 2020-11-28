using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunridge.Models
{
    public class Owner : IdentityUser
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Occupation { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        public string Apartment { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }

        public string State { get; set; }

        [Display(Name = "Emergency Contact")]
        public string EmergencyContact { get; set; }

        [Display(Name = "Emergency Contact Number")]
        public string EmergencyContactNumber { get; set; }


        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }

        #region Bridging Tables Collections
        //Collection to handle the Many to Many relationship between Owner and Lot
        public virtual ICollection<Lot_Owner> Lot_Owners { get; set; }
        #endregion
    }
}
