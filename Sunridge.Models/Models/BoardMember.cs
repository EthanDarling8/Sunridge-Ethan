using System.ComponentModel.DataAnnotations;

namespace Sunridge.Models {
    public class BoardMember {
        [Key] public string Id { get; set; }
        
        public string Position { get; set; }
        public string Image { get; set; }
    }
}