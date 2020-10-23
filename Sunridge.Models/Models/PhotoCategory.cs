using System.ComponentModel.DataAnnotations;

/* Photos will be nested by category
 * Owners will be able to add new Albums to a Category, or edit their existing albums. * 
 */
namespace Sunridge.Models
{
    public class PhotoCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
