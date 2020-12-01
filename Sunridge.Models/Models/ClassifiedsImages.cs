using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;
using System.Text;

namespace Sunridge.Models
{
    public class ClassifiedsImages
    {
        [Key]
        public int Id { get; set; }

        public string ImagePath { get; set; }

        [Required]
        public int ClassifiedsItemId { get; set; }

        [ForeignKey("ClassifiedsItemId")]
        public virtual ClassifiedsItem ClassifiedsItem { get; set; }
    }
}
