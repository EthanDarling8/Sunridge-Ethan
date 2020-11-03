using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sunridge.Models
{
    public class Banner
    {
        [Key]
        public int Id { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public bool Archived { get; set; } = false;
    }
}
