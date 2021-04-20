using System.ComponentModel.DataAnnotations;

/* Albums will be nested by category
 * Owners will be able to add new Albums to a Category, or edit their existing albums.
 * Only admins can add/edit categories.
 */
namespace Sunridge.Models
{
    public class PhotoCategory
    {
        [Key]
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 1)]
        [Required]        
        public string Name { get; set; }
    }
}
