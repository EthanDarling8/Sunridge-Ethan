using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Sunridge.Models {
    public class FireInfo {
        [Key] public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DisplayDate { get; set; }
        public string Attachment { get; set; }
        public bool Archived { get; set; } = false;
    }
}