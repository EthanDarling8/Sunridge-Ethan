using System.ComponentModel.DataAnnotations;

namespace Sunridge.Models
{
    public class DocumentCategory
    {
        [Key]
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string Name { get; set; }
    }
}
