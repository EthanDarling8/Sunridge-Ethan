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
        public readonly IUnitOfWork _unitOfWork;
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
        public PhotoAlbum PhotoAlbum { get; set; }

        public bool MyAlbums { get; set; }

        public Photo Photo { get; set; }

        public ApplicationUser CurrentApplicationUser { get; set; }




        /* Routing options:
         * 1. myAlbums: only display ablums created by the logged in user
         * 2. selectedPhotoCategoryId: displays photo albums for that category
         * 3. selectedPhotoCategoryId + selectedPhotoAlbumId: displays photos in that album
         *      "Back" button returns to gallery with selected category.
         * 4. selectedPhotoAlbumId: displays photos in that album.
         *      "Back" button returns to gallery with no category selected. 
         * 5. None: displays a list of categories to filter by and all photo albums
         */
        public void OnGet(int selectedPhotoCategoryId, int selectedPhotoAlbumId, bool myAlbums = false)
        {
            // View initilization is required.
            PhotoVM = new PhotoGalleryVM();
            PhotoAlbum = new PhotoAlbum();
            MyAlbums = myAlbums;
            
            //Get Id of current user for displaying edit/add buttons
            CurrentApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == _userManager.GetUserId(User));

            //Category list should always include everything.
            //Use where in the html to display what is needed.
            PhotoVM.PhotoCategoryList = _unitOfWork.PhotoCategory.GetAll(null, c => c.OrderBy(c => c.Name));


            //1
            if (myAlbums == true)
            {
                PhotoVM.PhotoAlbumList = _unitOfWork.PhotoAlbum.GetAll(a => a.ApplicationUserId == CurrentApplicationUser.Id, a => a.OrderBy(a => a.Title));                
            }
            else
            {
                PhotoVM.PhotoAlbumList = _unitOfWork.PhotoAlbum.GetAll(null, a => a.OrderBy(a => a.Title));
            }
            
            //Category selection does NOT display if myAlbums == true.
            //2
            if (selectedPhotoCategoryId != 0 && selectedPhotoAlbumId == 0)
            {
                PhotoVM.SelectedPhotoCategory = _unitOfWork.PhotoCategory.GetFirstOrDefault(c => c.Id == selectedPhotoCategoryId);               
            }
            //3
            else if (selectedPhotoCategoryId != 0 && selectedPhotoAlbumId != 0)
            {
                PhotoVM.SelectedPhotoCategory = _unitOfWork.PhotoCategory.GetFirstOrDefault(c => c.Id == selectedPhotoCategoryId);
                PhotoVM.SelectedPhotoAlbum = _unitOfWork.PhotoAlbum.GetFirstOrDefault(a => a.Id == selectedPhotoAlbumId);
                PhotoVM.PhotoList = _unitOfWork.Photo.GetAll(p => p.PhotoAlbumId == selectedPhotoAlbumId);
                PhotoVM.AlbumCreator = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == PhotoVM.SelectedPhotoAlbum.ApplicationUserId);
            }
            //4
            else if (selectedPhotoCategoryId == 0 && selectedPhotoAlbumId != 0)
            {
                PhotoVM.SelectedPhotoCategory = null;
                PhotoVM.SelectedPhotoAlbum = _unitOfWork.PhotoAlbum.GetFirstOrDefault(a => a.Id == selectedPhotoAlbumId);
                PhotoVM.PhotoList = _unitOfWork.Photo.GetAll(p => p.PhotoAlbumId == selectedPhotoAlbumId);
                PhotoVM.AlbumCreator = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == PhotoVM.SelectedPhotoAlbum.ApplicationUserId);
            }
            //5
            else
            {
                PhotoVM.SelectedPhotoCategory = null;
            }
        }
    }    
}
