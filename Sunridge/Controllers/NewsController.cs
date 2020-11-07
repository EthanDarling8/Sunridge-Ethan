using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private int DisplayNumYears = 3;

        public NewsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/<NewsController>/5
        [HttpGet("{year}")]
        public IActionResult Get(int year)
        {
            var NewsList = new List<News>();
            if(year == 0) 
                NewsList = _unitOfWork.News.GetAll(n => n.Archived == false && DateTime.Compare(n.DisplayDate.Date, DateTime.Now.Date) <= 0).OrderBy(d => d.DisplayDate).Reverse().ToList();
            else if(year == 2000) {
                NewsList = _unitOfWork.News.GetAll(n => n.Archived == false && n.DisplayDate.Date.Year < DateTime.Now.Date.AddYears(-DisplayNumYears).Year).OrderBy(d => d.DisplayDate).Reverse().ToList();
            }
            else
                NewsList = _unitOfWork.News.GetAll(n => n.Archived == false && n.DisplayDate.Date.Year == year).OrderBy(d => d.DisplayDate).Reverse().ToList();
            return Json(new { NewsList });
        }

        // POST api/<NewsController>
        [HttpPost]
        //public void Post([FromBody] string value)
        public IActionResult Post(string Search)
        {
            var NewsList = new List<News>();
            NewsList = _unitOfWork.News.GetAll(n => n.Archived == false && (n.Title.ToLower().Contains(Search.ToLower()) || n.Content.ToLower().Contains(Search.ToLower()))).OrderBy(d => d.DisplayDate).Reverse().ToList();
            return Json(new { NewsList });
        }

    }
}
