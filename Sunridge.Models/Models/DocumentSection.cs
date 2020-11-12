using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunridge.Models
{
    public class DocumentSection
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid display order.")]
        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }


        //Link to DocumentCategory        
        [Display(Name = "Category")]
        public int DocumentCategoryId { get; set; }

        [Display(Name = "Category")]
        [ForeignKey("DocumentCategoryId")]
        public virtual DocumentCategory DocumentCategory { get; set; }
    }
}
