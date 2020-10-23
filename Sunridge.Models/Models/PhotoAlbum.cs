using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunridge.Models
{
    public class PhotoAlbum
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        //Link to ApplicationUser
        [Required]
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }


        //Link to PhotoCategory
        [Required]
        public int PhotoCategoryId { get; set; }

        [ForeignKey("PhotoCategoryId")]
        public virtual PhotoCategory PhotoCategory { get; set; }
    }
}
