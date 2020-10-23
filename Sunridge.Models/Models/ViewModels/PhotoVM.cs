using System.Collections.Generic;

namespace Sunridge.Models.ViewModels
{
    public class PhotoVM
    {
        public IEnumerable<PhotoCategory> PhotoCategoryList { get; set; }

        public PhotoCategory PhotoCategory { get; set; }


        public IEnumerable<PhotoAlbum> PhotoAlbumList { get; set; }

        public PhotoAlbum PhotoAlbum { get; set; }


        public IEnumerable<Photo> PhotoList { get; set; }
    }
}
