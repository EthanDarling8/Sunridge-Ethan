using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Models.ViewModels;
using Sunridge.Utility;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.Pages.Home.Photos
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Models.Owner> _userManager;

        public IndexModel(IUnitOfWork unitOfWork,
            UserManager<Models.Owner> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        [BindProperty]
        public PhotoGalleryVM PhotoGalleryVM { get; set; }

        //This is used by a partial view to generate cards for display
        public PhotoAlbum PhotoAlbum { get; set; }

        public bool MyAlbums { get; set; }

        //This is used by a partial view to generate cards for display
        public Photo Photo { get; set; }

        public Models.Owner CurrentOwner { get; set; }




        /* Routing options:
         * 1. myAlbums: only display ablums created by the logged in user
         * 2. selectedPhotoCategoryId: displays photo albums for that category
         * 3. selectedPhotoCategoryId + selectedPhotoAlbumId: displays photos in that album
         *      "Back" button returns to gallery with selected category.
         * 4. selectedPhotoAlbumId: displays photos in that album.
         *      "Back" button returns to gallery with no category selected. 
         * 5. None: displays a list of categories to filter by and all photo albums
         */
        public IActionResult OnGet(int selectedPhotoCategoryId, int selectedPhotoAlbumId, bool myAlbums = false)
        {
            // View initilization is required.
            PhotoGalleryVM = new PhotoGalleryVM();
            PhotoAlbum = new PhotoAlbum();
            MyAlbums = myAlbums;
            
            //Get Id of current user for displaying edit/add buttons
            CurrentOwner = _unitOfWork.Owner.GetFirstOrDefault(u => u.Id == _userManager.GetUserId(User));

            //Category list should always include everything.
            //Use where in the html to display what is needed.
            PhotoGalleryVM.PhotoCategoryList = _unitOfWork.PhotoCategory.GetAll(null, c => c.OrderBy(c => c.Name));


            //Prevent display or usage of empty categories manually entered into URL
            if (selectedPhotoCategoryId != 0)
            {
                if (_unitOfWork.PhotoAlbum.GetFirstOrDefault(a => a.PhotoCategoryId == selectedPhotoCategoryId) == null)
                {
                    return RedirectToPage("Index");
                }
            }


            //1
            //Only allow logged in users to access "My Albums"
            if (myAlbums == true && User.Identity.IsAuthenticated && selectedPhotoAlbumId == 0)
            {
                PhotoGalleryVM.PhotoAlbumList = _unitOfWork.PhotoAlbum.GetAll(a => a.OwnerId == CurrentOwner.Id, a => a.OrderBy(a => a.Title), "Owner");

                //Prevents undesired page loads if category or album ID's are manually entered too.
                return Page();
            }
            //Prevent breaking page display by manually entering myAblums=True when not logged in.
            else if (myAlbums == true && !User.Identity.IsAuthenticated)
            {
                return RedirectToPage("Index");
            }
            else
            {
                //Only display albums with photos in them unless logged in as admin or the album owner

                //Get all albums
                IEnumerable<PhotoAlbum> allAlbumList = _unitOfWork.PhotoAlbum.GetAll(null, a => a.OrderBy(a => a.Title), "Owner");

                //Store desired albums here
                ICollection<PhotoAlbum> filteredAlbumList = new List<PhotoAlbum>();

                //Add only albums that have photos in them unless logged in as admin or the album owner
                foreach (PhotoAlbum tempAlbum in allAlbumList)
                {
                    if (_unitOfWork.Photo.GetFirstOrDefault(p => p.PhotoAlbumId == tempAlbum.Id) != null ||
                        (CurrentOwner != null && tempAlbum.OwnerId == CurrentOwner.Id) ||
                        User.IsInRole(SD.AdministratorRole))
                    {
                        filteredAlbumList.Add(tempAlbum);
                    }
                }

                //Copy desired albums into ViewModel.
                PhotoGalleryVM.PhotoAlbumList = filteredAlbumList;
            }


            //Category selection does NOT display if myAlbums == true.
            //2
            if (selectedPhotoCategoryId != 0 && selectedPhotoAlbumId == 0)
            {
                //Prevent invalid id's from being manually entered into URL                
                if (_unitOfWork.PhotoCategory.GetFirstOrDefault(c => c.Id == selectedPhotoCategoryId) == null)
                {
                    return RedirectToPage("Index");
                }             

                PhotoGalleryVM.SelectedPhotoCategory = _unitOfWork.PhotoCategory.GetFirstOrDefault(c => c.Id == selectedPhotoCategoryId);               
            }
            //3
            else if (selectedPhotoCategoryId != 0 && selectedPhotoAlbumId != 0)
            {
                //Prevent invalid id's from being manually entered into URL
                if (_unitOfWork.PhotoCategory.GetFirstOrDefault(c => c.Id == selectedPhotoCategoryId) == null ||
                    _unitOfWork.PhotoAlbum.GetFirstOrDefault(a => a.Id == selectedPhotoAlbumId) == null)
                {
                    return RedirectToPage("Index");
                }

                PhotoGalleryVM.SelectedPhotoCategory = _unitOfWork.PhotoCategory.GetFirstOrDefault(c => c.Id == selectedPhotoCategoryId);
                PhotoGalleryVM.SelectedPhotoAlbum = _unitOfWork.PhotoAlbum.GetFirstOrDefault(a => a.Id == selectedPhotoAlbumId, "Owner");
                PhotoGalleryVM.PhotoList = _unitOfWork.Photo.GetAll(p => p.PhotoAlbumId == selectedPhotoAlbumId);
            }
            //4
            else if (selectedPhotoCategoryId == 0 && selectedPhotoAlbumId != 0)
            {
                //Prevent invalid id's from being manually entered into URL
                if(_unitOfWork.PhotoAlbum.GetFirstOrDefault(a => a.Id == selectedPhotoAlbumId) == null)
                {
                    return RedirectToPage("Index");
                }    

                PhotoGalleryVM.SelectedPhotoCategory = null;
                PhotoGalleryVM.SelectedPhotoAlbum = _unitOfWork.PhotoAlbum.GetFirstOrDefault(a => a.Id == selectedPhotoAlbumId, "Owner");
                PhotoGalleryVM.PhotoList = _unitOfWork.Photo.GetAll(p => p.PhotoAlbumId == selectedPhotoAlbumId);
            }
            //5
            else
            {
                PhotoGalleryVM.SelectedPhotoCategory = null;
            }

            return Page();
        }
    }    
}
