using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Utility;

namespace Sunridge.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = SD.AdministratorRole)]
    public class FireInfoController : Controller {
        private readonly IUnitOfWork _unitOfWork;
        private int DisplayNumYears = 3;

        public FireInfoController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        // GET api/<FireController>/5
        [HttpGet("{year}")]
        [Authorize(Roles = SD.AdministratorRole)]
        public IActionResult Get(int year) {
            var FireList = new List<FireInfo>();
            if (year == 0)
                FireList = _unitOfWork.FireInfo
                    .GetAll(n => n.Archived == false && DateTime.Compare(n.DisplayDate.Date, DateTime.Now.Date) <= 0)
                    .OrderBy(d => d.DisplayDate).Reverse().ToList();
            else if (year == 2000) {
                FireList = _unitOfWork.FireInfo
                    .GetAll(n =>
                        n.Archived == false &&
                        n.DisplayDate.Date.Year < DateTime.Now.Date.AddYears(-DisplayNumYears).Year)
                    .OrderBy(d => d.DisplayDate).Reverse().ToList();
            }
            else
                FireList = _unitOfWork.FireInfo.GetAll(n => n.Archived == false && n.DisplayDate.Date.Year == year)
                    .OrderBy(d => d.DisplayDate).Reverse().ToList();

            return Json(new {FireList});
        }

        // POST api/<FireController>
        [HttpPost]
        [Authorize(Roles = SD.AdministratorRole)]
        //public void Post([FromBody] string value)
        public IActionResult Post(string Search) {
            var FireList = new List<FireInfo>();
            FireList = _unitOfWork.FireInfo
                .GetAll(n => n.Archived == false && (n.Title.ToLower().Contains(Search.ToLower()) ||
                                                     n.Content.ToLower().Contains(Search.ToLower())))
                .OrderBy(d => d.DisplayDate).Reverse().ToList();
            return Json(new {FireList});
        }

        public IActionResult Delete(int id) {
            var objFromDb = _unitOfWork.FireInfo.GetFirstOrDefault(unitOfWork => unitOfWork.Id == id);
            _unitOfWork.FireInfo.Remove(objFromDb);
            _unitOfWork.Save();
            return RedirectToPage("/Admin/FireInfo/Index");
        }
    }
}