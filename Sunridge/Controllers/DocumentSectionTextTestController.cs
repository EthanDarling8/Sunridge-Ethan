using Sunridge.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Sunridge.DataAccess.Data;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sunridge.Models;
using System;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DocumentSectionTextTestController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        //Bring in an instance of DbContext (maps models to database)
        protected readonly ApplicationDbContext Context;


        public DocumentSectionTextTestController(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            Context = context;
        }


        /*

        //Bring in an instance of DbContext (maps models to database)
        protected readonly DbContext Context;
        //Copy of table in database
        internal DbSet<T> dbset;

        //Instantiate Repository (constructor)
        public Repository(DbContext context)
        {
            Context = context;
            //Gets the table
            this.dbset = context.Set<T>();
        }


                        @"  SELECT DocumentSectionText.Name,
                        DocumentSectionText.DisplayOrder,
	                    DocumentSectionText.DocumentSectionId AS Id,
	                    DocumentSection.Name AS DocumentSectionName,
	                    DocumentCategory.Name AS DocumentCategoryName
                    FROM DocumentSectionText
                    JOIN DocumentSection ON DocumentSectionText.DocumentSectionId = DocumentSection.Id
                    JOIN DocumentCategory ON DocumentSection.DocumentCategoryId = DocumentCategory.Id";


        */

        [HttpGet]
        public IActionResult Get()
        {
            // *********** I think i would need to create a view model that has all the desired columns then run the query on the view model ***********

            string QueryString =
                        @"  SELECT DocumentSectionText.Name,
                                DocumentSectionText.DisplayOrder,
	                            DocumentSectionText.DocumentSectionId AS Id,
	                            DocumentSection.Name AS DocumentSectionName,
	                            DocumentCategory.Name AS DocumentCategoryName
                            FROM DocumentSectionText";

            //DbSet<DocumentSectionText> output = Context.Set<DocumentSectionText>(QueryString);

            /*
            var blogs = Context.Query<Models.DocumentSectionText>(QueryString)
            //.FromSqlRaw("SELECT * FROM dbo.Blogs")
            //.ToList();
            //*/

            var Text = Context.DocumentSectionText.FromSqlRaw(QueryString).Include("DocumentSection ON DocumentSectionText.DocumentSectionId = DocumentSection.Id, DocumentCategory ON DocumentSection.DocumentCategoryId = DocumentCategory.Id").ToList();
            //ToList();
            //var Text = Context..FromSqlRaw(QueryString);


            //return Json(new { data = _unitOfWork.DocumentSectionText.GetAll(null, null, "DocumentSection") });
            return Json(new { data = Text });
        }

        


        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                var objFromDb = _unitOfWork.DocumentSectionText.GetFirstOrDefault(t => t.Id == Id);

                if (objFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting." });
                }

                _unitOfWork.DocumentSectionText.Remove(objFromDb);

                _unitOfWork.Save();

                return Json(new { success = true, message = "Delete Successful." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }
        }
    }
}