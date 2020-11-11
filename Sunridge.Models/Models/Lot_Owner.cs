using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunridge.Models
{
    public class Lot_Owner
    {
        [Key]
        public int ID { get; set; }

        #region Lot (Foreign Key)
        #region Lot-Model
        //Lot-LotId: The LotId Foreign Key to get info about Lot.
        [ForeignKey("LotId")]
        public virtual Lot Lot { get; set; }
        #endregion
        #region Lot_Owner-LotId (FK)
        // Lot_Owner-LotId: The Primary Key of the Lot Table
        [Display(Name = "Lot Id")]
        public int LotId { get; set; }
        #endregion
        #endregion

        #region Owner (Foreign Key)
        #region Lot-Model
        //Lot-LotId: The OwnerId Foreign Key to get info about Owner.
        [ForeignKey("OwnerId")]
        public virtual Owner Owner { get; set; }
        #endregion
        #region Lot_Owner-OwnerId (FK)
        // Lot_Owner-LotId: The Primary Key of the Owner Table
        [Display(Name = "User Id")]
        public string OwnerId { get; set; }
        #endregion
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
