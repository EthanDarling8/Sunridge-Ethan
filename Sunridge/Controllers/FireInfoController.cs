﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class FireInfoController : Controller {
        private readonly IUnitOfWork _unitOfWork;
        private int DisplayNumYears = 3;

        public FireInfoController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        // GET api/<FireController>/5
        [HttpGet("{year}")]
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
        //public void Post([FromBody] string value)
        public IActionResult Post(string Search) {
            var FireList = new List<FireInfo>();
            FireList = _unitOfWork.FireInfo
                .GetAll(n => n.Archived == false && (n.Title.ToLower().Contains(Search.ToLower()) ||
                                                     n.Content.ToLower().Contains(Search.ToLower())))
                .OrderBy(d => d.DisplayDate).Reverse().ToList();
            return Json(new {FireList});
        }
    }
}