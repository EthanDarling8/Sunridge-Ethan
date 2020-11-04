using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunridge.Models
{
    public class Lot_Owner
    {
        #region Lot_Owner-Id
        //Lot_Owner-Id: The Id of the Lot_Owner Bridging Table.
        public int Id { get; set; }
        #endregion

        #region Lot-LotId (FK)
        //Lot-LotId: The LotId Foreign Key to get info about Lot.
        [ForeignKey("LotId")]
        public virtual Lot Lot { get; set; }
        #endregion
        #region Lot_Owner-LotId
        // Lot_Owner-LotId: The Primary Key of the Lot Table
        [Display(Name = "Lot Id")]
        public int LotId { get; set; }
        #endregion

        #region Lot-OwnerId (FK)
        //Lot-LotId: The OwnerId Foreign Key to get info about Owner.
        [ForeignKey("OwnerId")]
        public virtual Owner Owner { get; set; }
        #endregion
        #region Lot_Owner-OwnerId
        // Lot_Owner-LotId: The Primary Key of the Owner Table
        [Display(Name = "User Id")]
        public int OwnerId { get; set; }
        #endregion

        #region Table Diagram
        // EXAMPLE:
        //  _____________________________
        // | Lot Id | Owner Id           |
        // |________|____________________|
        // | 1      | 1                  |
        // |--------|--------------------|
        // | 1      | 2                  |  // Note how Lot 1 is shared between Owner 1 & 2.
        // |--------|--------------------|
        // | 2      | 3                  |
        // |________|____________________|
        #endregion
    }
}
