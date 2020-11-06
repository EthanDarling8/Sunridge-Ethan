using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sunridge.Models.Models;

namespace Sunridge.Models {
    public class OwnerBoardMember {
        
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("BoardMemberId")]
        public virtual BoardMember BoardMember { get; set; }
        [Display(Name = "Board Member")]
        public string BoardMemberId { get; set; }
        
        [ForeignKey("OwnerId")]
        public virtual Owner Owner { get; set; }
        [Display(Name = "Owner")]
        public string OwnerId { get; set; }

    }
}