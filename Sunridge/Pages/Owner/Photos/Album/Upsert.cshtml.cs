using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Models.ViewModels;
using Sunridge.Utility;

namespace Sunridge.Pages.Owner.Photos.Album
{    
    [Authorize]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Models.Owner> _userManager;

        public UpsertModel(IUnitOfWork unitOfWork,
            UserManager<Models.Owner> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        [BindProperty]
        public PhotoAlbumVM PhotoAlbumObj { get; set; }        
        public int SelectedPhotoCategoryId { get; set; }        
        public string OwnerId { get; set; }
        public bool MyAlbums { get; set; }



        public IActionResult OnGet(int? selectedPhotoAlbumId, int selectedPhotoCategoryId, bool myAlbums = false)
        {
            //Used to pass selected categroy into page for button links
            SelectedPhotoCategoryId = selectedPhotoCategoryId;

            //Used to pass myAlbums into page for button links to preserve selection
            MyAlbums = myAlbums;


            //Get Id of current user.
            OwnerId = _userManager.GetUserId(User);

            PhotoAlbumObj = new PhotoAlbumVM()
            {
                PhotoAlbum = new PhotoAlbum(),
                PhotoCategoryList = _unitOfWork.PhotoCategory.GetListForDropDown()
            };           


            //Existing (edit)
            if (selectedPhotoAlbumId != null)
            {
                PhotoAlbumObj.PhotoAlbum = _unitOfWork.PhotoAlbum.GetFirstOrDefault(a => a.Id == selectedPhotoAlbumId);

                //selectedPhotoAlbumId does not exist or database fails
                if (PhotoAlbumObj.PhotoAlbum == null)
                {
                    return RedirectToPage("/Home/Photos/Index", new { selectedPhotoCategoryId = selectedPhotoCategoryId, myAlbums = myAlbums });
                }


                //Only allow admins and creator to access            
                if (User.IsInRole(SD.AdministratorRole) || PhotoAlbumObj.PhotoAlbum.OwnerId == OwnerId)
                {
                    //Maintain existing Id when editing as editing could be done by an admin.
                    OwnerId = PhotoAlbumObj.PhotoAlbum.OwnerId;
                }
                else
                {
                    return RedirectToPage("/Home/Photos/Index", new { selectedPhotoCategoryId = selectedPhotoCategoryId, myAlbums = myAlbums });
                }                              
            }


            //New ( [Authorize] already ensures an owner is accessing this page. ) 
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
                PhotoAlbumObj.PhotoAlbum.Owner = _unitOfWork.Owner.GetFirstOrDefault(u => u.Id == _userManager.GetUserId(User));

                _unitOfWork.PhotoAlbum.Add(PhotoAlbumObj.PhotoAlbum);
            }
            //Existing PhotoAlbum
            else
            {
                PhotoAlbumObj.PhotoAlbum.Owner = _unitOfWork.Owner.GetFirstOrDefault(u => u.Id == PhotoAlbumObj.PhotoAlbum.OwnerId);

                _unitOfWork.PhotoAlbum.Update(PhotoAlbumObj.PhotoAlbum);
            }

            _unitOfWork.Save();

            return RedirectToPage("/Home/Photos/Index", new { selectedPhotoAlbumId = selectedPhotoAlbumId, selectedPhotoCategoryId = selectedPhotoCategoryId });
        }
    }
}
