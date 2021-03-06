using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunridge.Models
{
    public class Lot
    {
        #region Lot-Id (PK)
        // Lot-Id: The Primary Key of the Lot Table (autogenerated)
        [Key]
        public int Id { get; set; }
        #endregion
        #region Lot-LotNumber
        //Lot-LotNumber: The Lot Number
        [Display(Name = "Lot Number")]                         // Set the AutoGenerated label to "Lot Number"
        public string LotNumber { get; set; }
        #endregion
        #region Lot-TaxId
        //Lot-LotNumber: The Lot Tax Id
        // Is Tax Id a Foreign Key?
        [Display(Name = "Tax Id")]                             // Set the AutoGenerated label to "Tax Id"
        public string TaxId { get; set; }
        #endregion
        #region Lot-Address
        //Lot-LotNumber: The Lot Tax Id
        [Display(Name = "Street Address")]                             // Set the AutoGenerated label to "Street Address"
        public string Address { get; set; }
        #endregion
        #region Bridging Tables Collections
        public virtual ICollection<Lot_Owner> Lot_Owners { get; set; } // The Collection to hold the relationships between Lot and Owner
        public virtual ICollection<Lot_Inventory> Lot_Inventories { get; set; } // The Ecollection to hold the relationships between Lot and Inventory
        #endregion

        [NotMapped]
        public string Lot_Inventory { get; set; }
        [NotMapped]
        public string Lot_Owner { get; set; }

        #region Table Diagram
        // EXAMPLE:
        //  ______________________________________________________________
        // | ID     | Lot Number      | Tax Id         | Address          |
        // |________|_________________|________________|__________________|
        // | 1      | LotNumber1      | TaxId1         | Address1         |
        // |--------|-----------------|----------------|------------------|
        // | 2      | LotNumber2      | TaxId2         | Address2         |
        // |--------|-----------------|----------------|------------------|
        // | 3      | LotNumber3      | TaxId3         | Address3         |
        // |________|_________________|________________|__________________|
        #endregion
    }
}
