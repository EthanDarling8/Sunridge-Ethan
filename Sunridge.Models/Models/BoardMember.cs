using System.ComponentModel.DataAnnotations;

namespace Sunridge.Models {
    public class BoardMember {
        [Key] public int Id { get; set; }
        
        public string Position { get; set; }
        public string Image { get; set; }
    }
}