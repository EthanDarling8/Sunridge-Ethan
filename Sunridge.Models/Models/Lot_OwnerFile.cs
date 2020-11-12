﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunridge.Models
{
    public class Lot_OwnerFile
    {
        #region Lot_OwnerFile-Id (PK)
        // Lot-Id: The Primary Key of the Lot_OwnerFile Table (autogenerated)
        [Key]
        public int Id { get; set; }
        #endregion
        #region Lot_OwnerFile-Title
        //Lot-LotNumber: The Lot_OwnerFile's Title
        [Display(Name = "Title")]                         // Set the AutoGenerated label to "Title"
        public string Title { get; set; }
        #endregion
        #region Lot_OwnerFile-Description
        //Lot-LotNumber: The Lot_OwnerFile's Description
        [Display(Name = "Description")]                         // Set the AutoGenerated label to "Description"
        public string Description { get; set; }
        #endregion
        #region Lot_OwnerFile-File
        //Lot-LotNumber: The Lot_OwnerFile's location on server
        [Display(Name = "File")]                         // Set the AutoGenerated label to "File"
        public string File { get; set; }
        #endregion

        #region Lot_Owner (Foreign Key)
        #region Lot_OwnerFile-Lot_Owner (Composite FK)
        // Lot_OwnerFile-LotId: The Composite Foreign Key of the Lot_Owner Table

        [ForeignKey("Lot_OwnerId"), Column(Order = 0)]
        public int LotId { get; set; }
        [ForeignKey("Lot_OwnerId"), Column(Order = 1)]
        public string OwnerId { get; set; }
        #endregion
        #region Lot_Owner-Model
        //Lot-Id: The Lot_Owner Model.
        public virtual Lot_Owner Lot_Owner { get; set; }
        #endregion
        #endregion

        #region Table Diagram
        // EXAMPLE:
        //  ___________________________________________________________________________________________
        // | ID     | Item Name       | Title          | Description      | File       | Lot_OwnerId   |
        // |________|_________________|________________|__________________|____________|_______________|
        // | 1      | FileName1       | FileTitle1     | FileDescription1 | URL1       | 1             |
        // |--------|-----------------|----------------|------------------|------------|---------------|
        // | 2      | FileName2       | FileTitle2     | FileDescription2 | URL2       | 2             |
        // |--------|-----------------|----------------|------------------|------------|---------------|
        // | 3      | FileName3       | FileTitle3     | FileDescription3 | URL3       | 3             |
        // |________|_________________|________________|__________________|____________|_______________|
        #endregion
    }
}
