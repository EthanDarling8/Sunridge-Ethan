using Sunridge.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Sunridge.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using Sunridge.Utility;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = SD.AdministratorRole)]
    public class DocumentCategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public DocumentCategoryController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }




        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.DocumentCategory.GetAll() });
        }




        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {    
            try
            {
                var objFromDb = _unitOfWork.DocumentCategory.GetFirstOrDefault(c => c.Id == Id);

                //Check category exists
                if (objFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting." });
                }


                //Get all files in category
                IEnumerable<DocumentFile> FileList = new List<DocumentFile>();
                FileList = _unitOfWork.DocumentFile.GetAll(f => f.DocumentCategoryId == Id);

                //Delete files and database record
                if (FileList != null)
                {
                    foreach (DocumentFile file in FileList)
                    {
                        //This concatonates the path of wwwroot with the image path from database.
                        //Remove the slash from the image path
                        var filePath = Path.Combine(_hostingEnvironment.WebRootPath, file.File.TrimStart('\\'));
                        //Remove the file from wwwroot before removing path in database.
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }

                    //Delete file database records
                    _unitOfWork.DocumentFile.RemoveRange(FileList);
                }


                //Get all Sections in Category
                IEnumerable<DocumentSection> SectionList = new List<DocumentSection>();
                SectionList = _unitOfWork.DocumentSection.GetAll(s => s.DocumentCategoryId == Id);

                //Remove SectionText for those sections
                foreach (DocumentSection section in SectionList)
                {
                    //Get all SectionText in Section in Category
                    IEnumerable<DocumentSectionText> SectionTextList = new List<DocumentSectionText>();
                    SectionTextList = _unitOfWork.DocumentSectionText.GetAll(s => s.DocumentSectionId == section.Id);

                    //Delete SectionText database records
                    _unitOfWork.DocumentSectionText.RemoveRange(SectionTextList);
                }

                //Queue delete Section database records
                _unitOfWork.DocumentSection.RemoveRange(SectionList);

                //Queue delete Category database record
                _unitOfWork.DocumentCategory.Remove(objFromDb);

                //Save changes to database
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