using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;

namespace Sunridge.Pages.Admin.Lot.Files {
    public class Index : PageModel {
        public int LotId;
       public void OnGet()
       {
            if (!String.IsNullOrEmpty(Request.Query["lotid"]))
            {
                LotId = Int32.Parse(Request.Query["lotid"]);
            }
        }
    }
}