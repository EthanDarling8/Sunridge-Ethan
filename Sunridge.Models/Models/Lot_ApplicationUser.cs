using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunridge.Models
{
    public class Lot_ApplicationUser
    {
        #region Lot_ApplicationUser-Id
        //Lot_ApplicationUser-Id: The Id of the Lot_ApplicationUser Bridging Table.
        public int Id { get; set; }
        #endregion

        #region Lot-LotId (FK)
        //Lot-LotId: The LotId Foreign Key to get info about Lot.
        [ForeignKey("LotId")]
        public virtual Lot Lot { get; set; }
        #endregion
        #region Lot_ApplicationUser-LotId
        // Lot_ApplicationUser-LotId: The Primary Key of the Lot Table
        [Display(Name = "Lot Id")]
        public int LotId { get; set; }
        #endregion

        #region Lot-ApplicationUserId (FK) [DISABLED]
        //Lot-LotId: The ApplicationUserId Foreign Key to get info about ApplicationUser.
        /*

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        */
        #endregion
        #region Lot_ApplicationUser-ApplicationUserId
        // Lot_ApplicationUser-LotId: The Primary Key of the ApplicationUser Table
        [Display(Name = "User Id")]
        public int ApplicationUserId { get; set; }
        #endregion

        #region Table Diagram
        // EXAMPLE:
        //  _____________________________
        // | Lot Id | ApplicationUser Id |
        // |________|____________________|
        // | 1      | 1                  |
        // |--------|--------------------|
        // | 1      | 2                  |  // Note how Lot 1 is shared between ApplicationUser 1 & 2.
        // |--------|--------------------|
        // | 2      | 3                  |
        // |________|____________________|
        #endregion
    }
}
