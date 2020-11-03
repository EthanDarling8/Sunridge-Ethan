using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Models.ViewModels;

namespace Sunridge.Pages.Owner.Photos.Album
{
    // **** ToDo **** setup user id check so only admin and creator can edit
    [Authorize]
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
        public int SelectedPhotoCategoryId { get; set; }        
        public string ApplicationUserId { get; set; }




        public IActionResult OnGet(int? selectedPhotoAlbumId, int selectedPhotoCategoryId)
        {
            PhotoAlbumObj = new PhotoAlbumVM()
            {
                PhotoAlbum = new PhotoAlbum(),
                PhotoCategoryList = _unitOfWork.PhotoCategory.GetListForDropDown()
            };


            


            //Edit existing
            if (selectedPhotoAlbumId != null)
            {
                PhotoAlbumObj.PhotoAlbum = _unitOfWork.PhotoAlbum.GetFirstOrDefault(a => a.Id == selectedPhotoAlbumId);                
                //Maintain existing Id when editing as editing could be done by an admin.
                ApplicationUserId = PhotoAlbumObj.PhotoAlbum.ApplicationUserId;

                //Specified PhotoAlbumId does not exist or database fails
                if (PhotoAlbumObj.PhotoAlbum == null)
                {
                    //Returns a 404 error page.
                    return NotFound();
                }
            }
            else
            {
                //Get Id of current user if making a new album
                ApplicationUserId = _userManager.GetUserId(User);
            }

            //Always preserve selected category
            SelectedPhotoCategoryId = selectedPhotoCategoryId;
            return Page();
        }




        public IActionResult OnPost(int selectedPhotoAlbumId, int selectedPhotoCategoryId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //New PhotoAlbum
            if (PhotoAlbumObj.PhotoAlbum.Id == 0)
            {
                //Store application user so their name can be displayed on the album.
                PhotoAlbumObj.PhotoAlbum.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == _userManager.GetUserId(User));

                _unitOfWork.PhotoAlbum.Add(PhotoAlbumObj.PhotoAlbum);
            }
            //Existing PhotoAlbum
            else
            {
                PhotoAlbumObj.PhotoAlbum.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == PhotoAlbumObj.PhotoAlbum.ApplicationUserId);

                _unitOfWork.PhotoAlbum.Update(PhotoAlbumObj.PhotoAlbum);
            }

            _unitOfWork.Save();

            return RedirectToPage("/Home/Photos/Index", new { selectedPhotoAlbumId = selectedPhotoAlbumId, selectedPhotoCategoryId = selectedPhotoCategoryId });
        }
    }
}
