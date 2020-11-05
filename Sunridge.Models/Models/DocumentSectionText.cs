using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunridge.Models
{
    public class DocumentSectionText
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int DisplayOrder { get; set; }

        [Required]
        public string Text { get; set; }


        //Link to Document Section
        public int DocumentSectionId { get; set; }

        [ForeignKey("DocumentSectionId")]
        public virtual DocumentSection DocumentSection { get; set; }
    }
}
