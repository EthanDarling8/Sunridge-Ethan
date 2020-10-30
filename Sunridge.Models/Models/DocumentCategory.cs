using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sunridge.Models
{
    public class DocumentCategory
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
