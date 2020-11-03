using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
    public class ClassifiedsSubcategory
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public string Name { get; set; }

        [ForeignKey("CategoryId")]
        public virtual ClassifiedsCategory Category { get; set; }
    }
}
