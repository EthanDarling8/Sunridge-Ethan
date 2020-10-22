﻿using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunridge.Models
{
    public class LostItem
    {
        #region LostItem-Id (PK)
        // LostItem-Id: The Primary Key of the LostItem Table
        [Key]                                                   // Ensure that the Id is the Primary Key
        public int Id { get; set; }
        #endregion
        #region LostItem-Image
        // The Image of the found LostItem.
        public string Image { get; set; }
        #endregion
        #region LostItem-Date
        // LostItem-Date: The Date the LostItem was found
        [Display(Name = "Date")]                                // Set the AutoGenerated label to "Date"
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]  // Set the Date Formatting to the standard Month/Day/Year. (Ex: Jan 1, 2021)
        public DateTime Date { get; set; }
        #endregion
        #region LostItem-Description
        //LostItem-Description: The written description left by a User describing the lost item they found.
        [Display(Name = "Description")]                         // Set the AutoGenerated label to "Date"
        public string Description { get; set; }
        #endregion
    }
}
