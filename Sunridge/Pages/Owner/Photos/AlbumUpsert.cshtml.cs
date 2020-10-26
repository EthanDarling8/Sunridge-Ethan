using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Models.ViewModels;

namespace Sunridge.Pages.Owner.Photos
{
    // **** ToDo **** [Authorize]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public UpsertModel(IUnitOfWork unitOfWork,
            UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        [BindProperty]
        public PhotoAlbumVM PhotoAlbumObj { get; set; }

        public string ApplicationUserId { get; set; }




        public IActionResult OnGet(int? PhotoAlbumId)
        {
            PhotoAlbumObj = new PhotoAlbumVM()
            {
                PhotoAlbum = new PhotoAlbum(),
                PhotoCategoryList = _unitOfWork.PhotoCategory.GetListForDropDown()
            };


            //Get Id of current user.
            ApplicationUserId = _userManager.GetUserId(User);


            //Edit existing
            if (PhotoAlbumId != null)
            {
                PhotoAlbumObj.PhotoAlbum = _unitOfWork.PhotoAlbum.GetFirstOrDefault(a => a.Id == PhotoAlbumId);

                //Specified PhotoAlbumId does not exist or database fails
                if (PhotoAlbumObj.PhotoAlbum == null)
                {
                    //Returns a 404 error page.
                    return NotFound();
                }
            }

            return Page();
        }




        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //New PhotoAlbum
            if (PhotoAlbumObj.PhotoAlbum.Id == 0)
            {
                _unitOfWork.PhotoAlbum.Add(PhotoAlbumObj.PhotoAlbum);
            }
            //Existing PhotoAlbum
            else
            {
                _unitOfWork.PhotoAlbum.Update(PhotoAlbumObj.PhotoAlbum);
            }

            _unitOfWork.Save();

            return RedirectToPage("./Index");
        }
    }
}
