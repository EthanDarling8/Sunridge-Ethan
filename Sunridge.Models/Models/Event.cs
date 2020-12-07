using System;
using System.ComponentModel.DataAnnotations;

namespace Sunridge.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string Image { get; set; }

        [Required]
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        [Required]
        [Display(Name = "All Day")]
        public bool AllDay { get; set; }
    }
}