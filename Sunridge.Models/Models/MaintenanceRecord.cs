using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
    public class MaintenanceRecord
    {
        #region Keys
        /// <summary>
        /// Primary key to the MaintenanceRecord table
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Foreign key to the Asset table
        /// </summary>
        public int AssetId { get; set; }
        #endregion

        #region Properties
        /// <summary>
        /// The date the maintenance was completed
        /// </summary>
        public DateTime DateCompleted { get; set; }

        /// <summary>
        /// Description of the maintenance
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The amount paid for the maintenance
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Navigation property to the Asset table
        /// </summary>
        [ForeignKey("AssetId")]
        public virtual Asset Asset { get; set; }
        #endregion
    }
}
