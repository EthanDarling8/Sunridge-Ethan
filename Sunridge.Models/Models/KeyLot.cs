using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models.Models
{
    /// <summary>
    /// Bridge table between the Key and the Lot to which the Key has been issued
    /// </summary>
    public class KeyLot
    {
        #region Keys
        /// <summary>
        /// Primary key to the KeyLot table
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Foreign key to the Key table
        /// </summary>
        public int KeyId { get; set; }

        /// <summary>
        /// Foreign key to the Lot table
        /// </summary>
        public int LotId { get; set; }
        #endregion

        #region Properties
        /// <summary>
        /// Denotes whether the key has been issued (true) or returned (false)
        /// </summary>
        public bool Issued { get; set; }

        /// <summary>
        /// The date the key was issued
        /// </summary>
        public DateTime IssueDate { get; set; }

        /// <summary>
        /// The date the key was returned
        /// </summary>
        public DateTime? ReturnDate { get; set; }

        /// <summary>
        /// The amount paid for the Key
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal PaidAmount { get; set; } 
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Navigation property to the Key table
        /// </summary>
        [ForeignKey("KeyId")]
        public virtual Key Key { get; set; }

        /// <summary>
        /// Navigation property to the Lot table
        /// </summary>
        [ForeignKey("LotId")]
        public virtual Lot Lot { get; set; }
        #endregion
    }
}
