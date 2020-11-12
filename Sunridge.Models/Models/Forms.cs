using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
    public class Forms
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Owner")]
        public string OwnerId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateResolved { get; set; }
        public FormType FormType { get; set; }
        public string Description { get; set; }
        public string Equipment_Sugestions { get; set; }
        public bool Resolved { get; set; } = false;
        [Display(Name = "Resolved By")]
        public string AdminId { get; set; }

        [ForeignKey("OwnerId")]
        public virtual Owner Owner { get; set; }
        [ForeignKey("AdminId")]
        public virtual Owner Admin { get; set; }
    }

    public enum FormType
    {
        [Description("Suggestion / Complaint")]
        SuggestionComplaint = 0,
        [Description("Work in Kind")]
        WorkInkind = 1
    }
}
