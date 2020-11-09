using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunridge.Models
{
    public class Owner : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }

        #region Bridging Tables Collections
        //Collection to handle the Many to Many relationship between Owner and Lot
        public virtual ICollection<Lot_Owner> Lot_Owners { get; set; }
        #endregion
    }
}
