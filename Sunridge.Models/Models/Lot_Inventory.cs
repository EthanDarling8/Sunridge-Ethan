using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunridge.Models
{
    public class Lot_Inventory
    {

        #region Lot (Foreign Key)
        #region Lot_Inventory-LotId (FK)
        // Lot_Inventory-LotId: The Primary Key of the Lot Table
        [Display(Name = "Lot Id")]
        public int LotId { get; set; }
        #endregion
        #region Lot-Model
        //Lot-LotId: The LotId Foreign Key to get info about Lot.
        [ForeignKey("LotId")]
        public virtual Lot Lot { get; set; }
        #endregion
        #endregion

        #region Inventory (Foreign Key)
        #region Lot_Inventory-InventoryId (FK)
        // Lot_Inventory-LotId: The Primary Key of the Inventory Table
        [Display(Name = "Inventory Id")]
        public int InventoryId { get; set; }
        #endregion
        #region Lot-Model
        //Inventory-InventoryId: The InventoryId Foreign Key to get info about Inventory.
        [ForeignKey("InventoryId")]
        public virtual Inventory Inventory { get; set; }
        #endregion
        #endregion 

        #region Table Diagram
        // EXAMPLE:
        //  _______________________
        // | Lot Id | Inventory Id |
        // |________|______________|
        // | 1      | 1            |
        // |--------|--------------|
        // | 1      | 2            | 
        // |--------|--------------|
        // | 1      | 3            | // Here we see that Lot 1 has Inventory Items 1, 2, &  3.
        // |--------|--------------|
        // | 2      | 1            |
        // |--------|--------------|
        // | 2      | 3            | // Meanwhile Lot 2 only has Inventory Items 1 & 3.
        // |________|______________|
        #endregion
    }
}
