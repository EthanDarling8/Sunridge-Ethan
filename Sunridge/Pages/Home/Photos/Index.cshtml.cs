using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Models.ViewModels;
using System.Collections.Generic;
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




        public string UserId { get; set; }


        [BindProperty]
        public PhotoVM PhotoVM { get; set; }


        // **** TODO **** Delete this if it isn't used on the page.
        // [BindProperty]
        // public Photo PhotoObj { get; set; }




        /* Routing options:
         * 1. PhotoCategoryId: displays photo albums
         * 2. PhotoCategoryId + PhotoAlbumId: displays photos in that album
         *      (need CategoryId for "Back" button)
         * 3. None: displays a list of categories to filter by and all photo albums
         */
        public void OnGet(int? PhotoCategoryId, int? PhotoAlbumId)
        {
            // **** ToDo **** Delete this if initilization is not required.
            //PhotoVM = new PhotoVM();


            // **** TODO **** Get UserId to display "Edit" button on Albumb if it is their album
            //Get Id of current user.
            UserId = _userManager.GetUserId(User);


            //1
            if (PhotoCategoryId != null && PhotoAlbumId == null)
            {
                PhotoVM.PhotoCategory = _unitOfWork.PhotoCategory.GetFirstOrDefault(c => c.Id == PhotoCategoryId);
                PhotoVM.PhotoAlbumList = _unitOfWork.PhotoAlbum.GetAll(a => a.PhotoCategoryId == PhotoCategoryId, a => a.OrderBy(a => a.Name));
            }
            //2
            else if (PhotoCategoryId != null && PhotoAlbumId != null)
            {
                PhotoVM.PhotoAlbum = _unitOfWork.PhotoAlbum.GetFirstOrDefault(a => a.Id == PhotoAlbumId);
                PhotoVM.PhotoList = _unitOfWork.Photo.GetAll(p => p.PhotoAlbumId == PhotoAlbumId);
            }
            //3
            else
            {
                PhotoVM.PhotoCategoryList = _unitOfWork.PhotoCategory.GetAll(null, c => c.OrderBy(c => c.Name));
                PhotoVM.PhotoAlbumList = _unitOfWork.PhotoAlbum.GetAll(null, a => a.OrderBy(a => a.Name));
            }
        }
    }
}
