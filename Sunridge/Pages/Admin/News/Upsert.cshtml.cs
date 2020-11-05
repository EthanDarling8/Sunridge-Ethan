using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;

namespace Sunridge.Pages.Admin.News
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public Models.News NewsObj { get; set; }

        public IActionResult OnGet(int? id)
        {
            NewsObj = new Models.News();

            if (id != null)
            {
                NewsObj = _unitOfWork.News.GetFirstOrDefault(u => u.Id == id);
                if (NewsObj == null)
                {
                    return NotFound();
                }
            }

            return Page();
        }

        public IActionResult OnPost()
        {

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (NewsObj.Id == 0)
            {
                _unitOfWork.News.Add(NewsObj);
                _unitOfWork.Save();
            }
            else
            {
                var objFromDb = _unitOfWork.News.Get(NewsObj.Id);
                _unitOfWork.News.Update(NewsObj);
            }

            return RedirectToPage("./Index");
        }

    }
}
