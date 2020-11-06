using System.ComponentModel.DataAnnotations;

namespace Sunridge.Models
{
    public class DocumentCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
