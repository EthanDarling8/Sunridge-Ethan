using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunridge.Models
{
    public class DocumentFileKeyword
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Keyword { get; set; }


        //Link to DocumentFile
        public int DocumentFileId { get; set; }

        [Display(Name = "File")]
        [ForeignKey("DocumentFileId")]
        public virtual DocumentFile DocumentFile { get; set; }
    }
}
