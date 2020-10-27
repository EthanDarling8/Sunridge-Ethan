using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Models.ViewModels;
using System.Linq;

namespace Sunridge.Pages.Home.Photos
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(IUnitOfWork unitOfWork,
            UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        [BindProperty]
        public PhotoGalleryVM PhotoVM { get; set; }

        //This is used by a partial view to generate cards for display based on selected category.
        [BindProperty]
        public PhotoAlbum PhotoAlbum { get; set; }

        [BindProperty]
        public Photo Photo { get; set; }

        public string ApplicationUserId { get; set; }




        /* Routing options:
         * 1. PhotoCategoryId: displays photo albums
         * 2. PhotoCategoryId + PhotoAlbumId: displays photos in that album
         *      "Back" button returns to gallery with selected category.
         * 3. PhotoAlumbId: displays photos in that album.
         *      "Back" button returns to gallery with no category selected. 
         * 4. None: displays a list of categories to filter by and all photo albums
         */
        public void OnGet(int PhotoCategoryId, int PhotoAlbumId)
        {
            // View initilization is required.
            PhotoVM = new PhotoGalleryVM();

            PhotoAlbum = new PhotoAlbum();


            // **** TODO **** Get UserId to display "Edit" button on Album if it is their album
            //Get Id of current user.
            ApplicationUserId = _userManager.GetUserId(User);


            //1
            if (PhotoCategoryId != 0 && PhotoAlbumId == 0)
            {
                PhotoVM.SelectedPhotoCategory = _unitOfWork.PhotoCategory.GetFirstOrDefault(c => c.Id == PhotoCategoryId);
                PhotoVM.PhotoCategoryList = _unitOfWork.PhotoCategory.GetAll(null, c => c.OrderBy(c => c.Name));
                PhotoVM.PhotoAlbumList = _unitOfWork.PhotoAlbum.GetAll(a => a.PhotoCategoryId == PhotoCategoryId, a => a.OrderBy(a => a.Title));
            }
            //2
            else if (PhotoCategoryId != 0 && PhotoAlbumId != 0)
            {
                PhotoVM.SelectedPhotoCategory = _unitOfWork.PhotoCategory.GetFirstOrDefault(c => c.Id == PhotoCategoryId);
                PhotoVM.SelectedPhotoAlbum = _unitOfWork.PhotoAlbum.GetFirstOrDefault(a => a.Id == PhotoAlbumId);
                PhotoVM.PhotoList = _unitOfWork.Photo.GetAll(p => p.PhotoAlbumId == PhotoAlbumId);
            }
            //3
            else if (PhotoCategoryId == 0 && PhotoAlbumId != 0)
            {
                PhotoVM.SelectedPhotoCategory = null;
                PhotoVM.SelectedPhotoAlbum = _unitOfWork.PhotoAlbum.GetFirstOrDefault(a => a.Id == PhotoAlbumId);
                PhotoVM.PhotoList = _unitOfWork.Photo.GetAll(p => p.PhotoAlbumId == PhotoAlbumId);
            }
            //4
            else
            {
                PhotoVM.SelectedPhotoCategory = null;
                PhotoVM.PhotoCategoryList = _unitOfWork.PhotoCategory.GetAll(null, c => c.OrderBy(c => c.Name));
                PhotoVM.PhotoAlbumList = _unitOfWork.PhotoAlbum.GetAll(null, a => a.OrderBy(a => a.Title));
            }
        }
    }    
}
