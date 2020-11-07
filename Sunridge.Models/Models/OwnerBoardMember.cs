using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sunridge.Models.Models;

namespace Sunridge.Models {
    public class OwnerBoardMember {
        
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("BoardMemberId")]
        public virtual BoardMember BoardMember { get; set; }
        [Display(Name = "Board Member Id")]
        public int BoardMemberId { get; set; }
        
        [ForeignKey("OwnerId")]
        public virtual Owner Owner { get; set; }
        [Display(Name = "User Id")]
        public string OwnerId { get; set; }

    }
}