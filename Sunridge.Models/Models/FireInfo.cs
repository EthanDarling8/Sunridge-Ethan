using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Sunridge.Models {
    public class FireInfo {
        [Key] public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Upload)] public string File { get; set; }
        [DataType(DataType.Url)] public string Link1 { get; set; }
        [DataType(DataType.Url)] public string Link2 { get; set; }
        [DataType(DataType.PhoneNumber)]  public string Phone { get; set; }
        [DataType(DataType.EmailAddress)] public string Email { get; set; }
        public string PostDate { get; set; }
    }
}