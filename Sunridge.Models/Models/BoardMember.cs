using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunridge.Models {
    public class BoardMember {
        
        [Key]
        public int Id { get; set; }
        public string Position { get; set; }
        public string Image { get; set; }

        
        [ForeignKey("OwnerId")]
        public virtual Owner Owner { get; set; }
        [Display(Name = "Owner")]
        public string OwnerId { get; set; }
    }
}