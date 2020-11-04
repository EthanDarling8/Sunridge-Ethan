using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunridge.Models
{
    public class Lot_Inventory
    {
        #region Lot_Inventory-Id
        //Lot_Inventory-Id: The Id of the Lot_Inventory Bridging Table.
        public int Id { get; set; }
        #endregion

        #region Lot_Inventory-LotId
        // Lot_Inventory-LotId: The Primary Key of the Lot Table
        [Display(Name = "Lot Id")]
        public int LotId { get; set; }
        #endregion
        #region Lot-LotId (FK)
        //Lot-LotId: The LotId Foreign Key to get info about Lot.
        [ForeignKey("LotId")]
        public virtual Lot Lot { get; set; }
        #endregion

        #region Lot_Inventory-InventoryId
        // Lot_Inventory-LotId: The Primary Key of the Inventory Table
        [Display(Name = "Inventory Id")]
        public int InventoryId { get; set; }
        #endregion
        #region Lot-InventoryId (FK)
        //Inventory-InventoryId: The InventoryId Foreign Key to get info about Inventory.
        [ForeignKey("InventoryId")]
        public virtual Inventory Inventory { get; set; }
        #endregion

        #region Table Diagram
        // EXAMPLE:
        //  _______________________
        // | Lot Id | Invenotry Id |
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
