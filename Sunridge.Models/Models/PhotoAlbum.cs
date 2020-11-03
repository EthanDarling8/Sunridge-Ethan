using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;

namespace Sunridge.Models
{
    public class PhotoAlbum
    {
        [Key]
        public int Id { get; set; }

        
        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        //Location of a thumbnail for preview displays
        //This link is added when the first photo is added
        public string Thumb { get; set; }


        //Link to ApplicationUser
        [Required]
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual Owner Owner { get; set; }


        //Link to PhotoCategory
        [Display(Name = "Category")]
        [Required]
        public int PhotoCategoryId { get; set; }

        [ForeignKey("PhotoCategoryId")]
        public virtual PhotoCategory PhotoCategory { get; set; }
    }
}
