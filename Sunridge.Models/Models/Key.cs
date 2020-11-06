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
        /// <summary>
        /// The year the Key was issued
        /// </summary>
        [Key, Column(Order = 1)]
        public int Year { get; set; }
        
        /// <summary>
        /// The nth Key that was issued in a year
        /// </summary>
        [Key, Column(Order = 2)]
        public int SerialNumber { get; set; }
        #endregion
    }
}
