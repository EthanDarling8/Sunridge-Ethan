using Sunridge.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Sunridge.Models;
using System;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DocumentSectionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DocumentSectionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }




        [HttpGet]
        public IActionResult Get(int categoryId)
        {
            return Json(new { data = _unitOfWork.DocumentSection.GetAll(null, null, "DocumentCategory") });
        }




        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                //Get section
                var objFromDb = _unitOfWork.DocumentSection.GetFirstOrDefault(s => s.Id == Id);

                if (objFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting." });
                }

                //Get all SectionText in Section
                IEnumerable<DocumentSectionText> SectionTextList = new List<DocumentSectionText>();
                SectionTextList = _unitOfWork.DocumentSectionText.GetAll(s => s.DocumentSectionId == Id);

                //Queue delete SectionText database records
                _unitOfWork.DocumentSectionText.RemoveRange(SectionTextList);

                //Queue delete Section database record
                _unitOfWork.DocumentSection.Remove(objFromDb);

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