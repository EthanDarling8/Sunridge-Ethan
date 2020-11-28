using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunridge.Models
{
    public class DocumentFile
    {
        [Key]
        public int Id { get; set; }

        [StringLength(120, MinimumLength = 1)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Search Keywords")]
        public string Keywords { get; set; }

        //Store the extension for displaying file type information and icon
        [Required]
        public string Extension { get; set; }

        public string Description { get; set; }
        
        [Display(Name = "Published Date")]
        public DateTime PublishedDate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid display order.")]
        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }

        [Required]
        public string File { get; set; }


        //Link to DocumentCategory
        [Display(Name = "Category")]
        public int DocumentCategoryId { get; set; }

        [Display(Name = "Category")]
        [ForeignKey("DocumentCategoryId")]
        public virtual DocumentCategory DocumentCategory { get; set; }
    }
}
