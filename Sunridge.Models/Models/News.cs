using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DisplayDate { get; set; } = DateTime.Today;
        [NotMapped]
        public string FormatDate { get; set; }
        public string Attachment { get; set; }
        [NotMapped]
        public string DisplayName { get; set; }
        public bool Archived { get; set; } = false;
    }
}
