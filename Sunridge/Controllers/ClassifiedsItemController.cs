using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Pages.Owner.Classifieds;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassifiedsItemController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ClassifiedsItemController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
            public IActionResult Get(int id)
            {

            string adminName = User.Identity.Name;
            string ownerId = User.Identity != null && User.Identity.IsAuthenticated && User.Claims.ToList().Count > 0 ? User.Claims.ToList()[0].Value : null;
            //if (roleName == true)
            //{
            //    return Json(new { data = _unitOfWork.ClassifiedsItem.GetAll() });
            //}
            //else
            //{
                //return Json(new { data = _unitOfWork.ClassifiedsItem.GetAll(null, null, "ClassifiedsCategory,ClassifiedsSubCategory") });
                return Json(new { data = _unitOfWork.ClassifiedsItem.GetAll().Where(x => x.OwnerId == ownerId) });
            //}
        }

            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                try
                {
                    var objFromDb = _unitOfWork.ClassifiedsItem.GetFirstOrDefault(u => u.Id == id);

                    if (objFromDb == null)
                    {
                        return Json(new { success = false, message = "Error while deleting" });
                    }

                    //physically remove image (if exists)
                    var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, objFromDb.Images.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }


                    _unitOfWork.ClassifiedsItem.Remove(objFromDb);
                    _unitOfWork.Save();
                }
                catch (Exception)
                {

                    return Json(new { success = false, message = "Error while deleting" });
                }
                return Json(new { success = true, message = "Delete Successful" });
            }

        }
    }


    

