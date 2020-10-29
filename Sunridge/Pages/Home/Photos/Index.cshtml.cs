using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Models.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ApplicationUser AlbumCreator { get; set; }

        [BindProperty]
        public Photo Photo { get; set; }

        public ApplicationUser CurrentApplicationUser { get; set; }




        /* Routing options:
         * 1. PhotoCategoryId: displays photo albums
         * 2. PhotoCategoryId + PhotoAlbumId: displays photos in that album
         *      "Back" button returns to gallery with selected category.
         * 3. PhotoAlumbId: displays photos in that album.
         *      "Back" button returns to gallery with no category selected. 
         * 4. None: displays a list of categories to filter by and all photo albums
         */
        public void OnGet(int selectedPhotoCategoryId, int selectedPhotoAlbumId, bool myAlbums = false)
        {
            // View initilization is required.
            PhotoVM = new PhotoGalleryVM();
            PhotoAlbum = new PhotoAlbum();


            // **** TODO **** Get UserId to display "Edit" button on Album if it is their album
            //Get Id of current user.
            CurrentApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == _userManager.GetUserId(User));

            //Category and Album lists should always include everything.
            //Use where in the html to display what is needed.
            PhotoVM.PhotoCategoryList = _unitOfWork.PhotoCategory.GetAll(null, c => c.OrderBy(c => c.Name));


            //Only display ablums for the logged in user
            if (myAlbums == true)
            {
                PhotoVM.PhotoAlbumList = _unitOfWork.PhotoAlbum.GetAll(a => a.ApplicationUserId == CurrentApplicationUser.Id, a => a.OrderBy(a => a.Title));
            }
            else
            {
                PhotoVM.PhotoAlbumList = _unitOfWork.PhotoAlbum.GetAll(null, a => a.OrderBy(a => a.Title));
            }
            

            //1
            if (selectedPhotoCategoryId != 0 && selectedPhotoAlbumId == 0)
            {
                PhotoVM.SelectedPhotoCategory = _unitOfWork.PhotoCategory.GetFirstOrDefault(c => c.Id == selectedPhotoCategoryId);               
            }
            //2
            else if (selectedPhotoCategoryId != 0 && selectedPhotoAlbumId != 0)
            {
                PhotoVM.SelectedPhotoCategory = _unitOfWork.PhotoCategory.GetFirstOrDefault(c => c.Id == selectedPhotoCategoryId);
                PhotoVM.SelectedPhotoAlbum = _unitOfWork.PhotoAlbum.GetFirstOrDefault(a => a.Id == selectedPhotoAlbumId);
                PhotoVM.PhotoList = _unitOfWork.Photo.GetAll(p => p.PhotoAlbumId == selectedPhotoAlbumId);
            }
            //3
            else if (selectedPhotoCategoryId == 0 && selectedPhotoAlbumId != 0)
            {
                PhotoVM.SelectedPhotoCategory = null;
                PhotoVM.SelectedPhotoAlbum = _unitOfWork.PhotoAlbum.GetFirstOrDefault(a => a.Id == selectedPhotoAlbumId);
                PhotoVM.PhotoList = _unitOfWork.Photo.GetAll(p => p.PhotoAlbumId == selectedPhotoAlbumId);
            }
            //4
            else
            {
                PhotoVM.SelectedPhotoCategory = null;
            }
        }
    }    
}
