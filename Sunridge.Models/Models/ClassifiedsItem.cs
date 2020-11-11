using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text;

namespace Sunridge.Models
{
    public class ClassifiedsItem
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; } //Cabins, Lots, Recreational Vehicles, Services, etc.
        [Display(Name = "Subcategory"), AllowNull]
        public int SubcategoryId { get; set; } //ATVs, Motorcycles, UTVs, Boats, Trailers, Plumbing, RealEstate, etc.
        
        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string Images { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }
        public string TimeAdded { get; set; }
        
        [ForeignKey("CategoryId")]
        public virtual ClassifiedsCategory Category { get; set; }

        [ForeignKey("SubcategoryId")]
        public virtual ClassifiedsSubcategory Subcategory { get; set; }

        
        
    }
}
