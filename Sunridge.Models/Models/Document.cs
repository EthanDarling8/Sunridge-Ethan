using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Text { get; set; }

        public string File { get; set; }


        //Link to Document Category
        public int DocumentCategoryId { get; set; }

        [ForeignKey("DocumentCategoryId")]
        public virtual DocumentCategory DocumentCategory { get; set; }
    }
}
