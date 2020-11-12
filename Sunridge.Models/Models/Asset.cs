using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
    public class Asset
    {
        #region Properties
        /// <summary>
        /// Primary key to the Asset table
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name of the asset
        /// </summary>
        [Display(Name = "Asset Name")]
        public string Name { get; set; }

        /// <summary>
        /// Purchase price of the asset
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Purchase Price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Description of the asset
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Status of the asset
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Date the asset was purchased
        /// </summary>
        public DateTime Date { get; set; }
        #endregion
    }
}
