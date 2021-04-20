using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunridge.Models
{
    public class DocumentSectionText
    {
        [Key]
        public int Id { get; set; }

        [StringLength(120, MinimumLength = 1)]
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid display order.")]
        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }

        //Cannot make required without some fancy tinyMCE validation
        public string Text { get; set; }


        //Link to Document Section
        [Display(Name = "Section")]
        public int DocumentSectionId { get; set; }

        [Display(Name = "Section")]
        [ForeignKey("DocumentSectionId")]
        public virtual DocumentSection DocumentSection { get; set; }
    }
}
