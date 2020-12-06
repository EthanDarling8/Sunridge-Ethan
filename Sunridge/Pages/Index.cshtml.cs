using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public List<Banner> BannerList { get; set; }

        //Used to dynamically link to the rules category within documents
        public int RulesId { get; set; }




        public void OnGet()
        {
            BannerList = _unitOfWork.Banner.GetAll().ToList();

            //Used to dynamically link to the rules category within documents
            DocumentCategory tempDocumentCategory = new DocumentCategory();
            tempDocumentCategory = _unitOfWork.DocumentCategory.GetFirstOrDefault(c => c.Name.ToLower() == "rules".ToLower());
            //The the rules category does not exist, RulesId will be 0, thus loading the documents page with no category selected.
            if(tempDocumentCategory != null)
            {
                RulesId = tempDocumentCategory.Id;
            }            
        }
    }
}
