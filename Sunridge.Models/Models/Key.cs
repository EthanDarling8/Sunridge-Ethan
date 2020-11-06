using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models.Models
{
    /// <summary>
    /// The Key to a Lot
    /// </summary>
    public class Key
    {
        #region Properties
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The year the Key was issued
        /// </summary>
        public int Year { get; set; }
        
        /// <summary>
        /// The nth Key that was issued in a year
        /// </summary>
        public int SerialNumber { get; set; }
        #endregion
    }
}
