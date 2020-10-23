using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunridge.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }

        //Location of original image 
        [Required]
        public string Image { get; set; }

        //Location of converted thumbnail of Image (reduced size)
        [Required]
        public string Thumb { get; set; }


        //Link to PhotoAlbum
        [Required]
        public int PhotoAlbumId { get; set; }

        [ForeignKey("PhotoAlbumId")]
        public virtual PhotoAlbum PhotoAlbum { get; set; }
    }
}
