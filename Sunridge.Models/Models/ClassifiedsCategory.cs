using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;
using System.Text;

namespace Sunridge.Models
{
    public class ClassifiedsCategory
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
