using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Microsoft.AspNetCore.Razor.Language;
using System.Web;
using System.IO;

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

            if(NewsList.Count() > 0)
            {
                int i = 0;
                foreach(var item in NewsList)
                {
                    NewsList[i].FormatDate = item.DisplayDate.Date.ToLongDateString();
                    NewsList[i].DisplayName = "";
                    if (item.Attachment != null)
                    {
                        NewsList[i].DisplayName = Path.GetFileName(item.Attachment);
                    }
                    i++;
                }
            }

            return Json(new { NewsList });
        }

        // POST api/<NewsController>
        [HttpPost]
        //public void Post([FromBody] string value)
        public IActionResult Post(string Search)
        {
            var NewsList = new List<News>();
            NewsList = _unitOfWork.News.GetAll(n => n.Archived == false && (n.Title.ToLower().Contains(Search.ToLower()) || n.Content.ToLower().Contains(Search.ToLower()))).OrderBy(d => d.DisplayDate).Reverse().ToList();

            if (NewsList.Count() > 0)
            {
                int i = 0;
                foreach (var item in NewsList)
                {
                    NewsList[i].FormatDate = item.DisplayDate.Date.ToLongDateString();
                    NewsList[i].DisplayName = "";
                    if (item.Attachment != null)
                    {
                        NewsList[i].DisplayName = Path.GetFileName(item.Attachment);
                    }
                    i++;
                }
            }

            return Json(new { NewsList });
        }
        
        public IActionResult Delete(int id) {
            var objFromDb = _unitOfWork.News.GetFirstOrDefault(unitOfWork => unitOfWork.Id == id);
            _unitOfWork.News.Remove(objFromDb);
            _unitOfWork.Save();
            return RedirectToPage("/Admin/News/Index");
        }
    }
}
