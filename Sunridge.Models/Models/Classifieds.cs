using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.Models
{
    public class Classifieds
    {
        public int Id { get; set; }

        public string Category { get; set; } //Cabins, Lots, Recreational Vehicles, Services, etc.

        public string Subcategory { get; set; } //ATVs, Motorcycles, UTVs, Boats, Trailers, Plumbing, RealEstate, etc.

        public string Title { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string Images { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }
    }
}
