using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class PhotoCategoryRepository : Repository<PhotoCategory>, IPhotoCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        //Inherits the base options listed in services in startup.cs
        public PhotoCategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        // This complicated method was supposed to prevent the following error:
        // "is of type 'System.Int32' but must be of type 'IEnumerable<SelectListItem>"
        // but it does NOT. The error still occurs.
        /*
        public List<SelectListItem> GetListForDropDown()
        {
            List<SelectListItem> PhotoCategoryList = new List<SelectListItem>();

            PhotoCategoryList.Add(new SelectListItem()
            {
                Text = "- No Category -",                
                Value = "0"
            });

            IEnumerable<PhotoCategory> dbPhotoCategoryList = new List<PhotoCategory>();
            dbPhotoCategoryList = _db.PhotoCategory.ToList();

            foreach (PhotoCategory photoCategory in dbPhotoCategoryList)
            {
                PhotoCategoryList.Add(new SelectListItem()
                {
                    //Displays name, but connects to Id behind.
                    Text = photoCategory.Name,
                    //Id is used for url routing in the page
                    Value = photoCategory.Id.ToString()
                });
            }

            return PhotoCategoryList;
        }
        */


        public IEnumerable<SelectListItem> GetListForDropDown()
        {
            return _db.PhotoCategory.Select(i => new SelectListItem()
            {
                Text = i.Name,
                //Id is used for url routing in the page
                Value = i.Id.ToString()
            });
        }




        public void Update(PhotoCategory photoCategory)
        {
            //Gets the first object from the table that has the same id as the one passed in
            var objFromDb = _db.PhotoCategory.FirstOrDefault(s => s.Id == photoCategory.Id);

            objFromDb.Name = photoCategory.Name;

            _db.SaveChanges();
        }



        // **** ToDo **** Deleting a category should change all albums linked to it to the "None" category.
    }
}
