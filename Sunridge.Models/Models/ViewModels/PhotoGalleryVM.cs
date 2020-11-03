using System.Collections.Generic;

namespace Sunridge.Models.ViewModels
{
    public class PhotoGalleryVM
    {
        //All the categories
        public IEnumerable<PhotoCategory> PhotoCategoryList { get; set; }

        public PhotoCategory SelectedPhotoCategory { get; set; }

        //Creator of an album
        public ApplicationUser AlbumCreator { get; set; }

        //All the albums for the selected category (if no category selected, display all albums)
        public IEnumerable<PhotoAlbum> PhotoAlbumList { get; set; }

        public PhotoAlbum SelectedPhotoAlbum { get; set; }

        //All the photos for the selected album
        public IEnumerable<Photo> PhotoList { get; set; }
    }
}
