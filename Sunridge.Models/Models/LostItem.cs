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
        #region LostItem-Status
        //LostItem-Status: The Lost Item's Status as either a Lost item or a Found item.
        [Display(Name = "Status")]                         // Set the AutoGenerated label to "Status"
        public string Status { get; set; }
        #endregion
        #region LostItem-Date
        // LostItem-Date: The Date the LostItem was found
        [Display(Name = "Date")]                                // Set the AutoGenerated label to "Date"
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]  // Set the Date Formatting to the standard Month/Day/Year. (Ex: Jan 1, 2021)
        public DateTime Date { get; set; }
        #endregion
        #region LostItem-Name
        //LostItem-Description: The display name made by a User describing the lost item they found.
        [Display(Name = "Name")]                         // Set the AutoGenerated label to "Name"
        public string Name { get; set; }
        #endregion
        #region LostItem-Description
        //LostItem-Description: The written description left by a User describing the lost item they found.
        [Display(Name = "Description")]                         // Set the AutoGenerated label to "Description"
        public string Description { get; set; }
        #endregion
        #region LostItem-ExpirationDate
        //LostItem-ExpirationDate: The Expiration Date for the row (after so many days LostItem's Active field is set to FALSE).
        [Display(Name = "Expiration Date")]                         // Set the AutoGenerated label to "Expiration Date"
        public bool ExpirationDate { get; set; }
        #endregion
        #region LostItem-Active
        //LostItem-Description: The Status of Activity for a Lost and Found item.
        [Display(Name = "Active")]                         // Set the AutoGenerated label to "Active"
        public bool Active { get; set; }
        #endregion
        #region ApplicationUser-Id (FK)
        //LostItem-UserId: The UserId Foreign Key to get info about User who found the item.
        [Display(Name = "User Id")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual Owner Owner { get; set; }
        #endregion
    }
}
