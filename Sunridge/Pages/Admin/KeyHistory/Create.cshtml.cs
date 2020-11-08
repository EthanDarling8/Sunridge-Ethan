using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using Sunridge.Models.Models;

namespace Sunridge
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public Key Key { get; set; }

        [BindProperty]
        public KeyLot KeyLot { get; set; }

        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int id)
        {
            KeyLot = _unitOfWork.KeyLot.GetFirstOrDefault(kl => kl.LotId == id);
        }

        public IActionResult OnPost(int id)
        {
            IEnumerable<Key> keys = _unitOfWork.Key.GetAll(k => k.Year == Key.Year && k.SerialNumber == Key.SerialNumber);
            
            if (keys.Count() > 0)
            {
                ModelState.AddModelError("DuplicateKey", String.Format("Key {0}-{1} already exists", Key.Year, Key.SerialNumber));
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Key.Add(Key);
                _unitOfWork.Save();

                KeyLot.KeyId = Key.Id; // After saving on _unitOfWork, Key will receive its id
                KeyLot.Issued = true;
                KeyLot.IssueDate = DateTime.Today;

                _unitOfWork.KeyLot.Add(KeyLot);
                _unitOfWork.Save();
            }
            else
            {
                return Page();
            }

            return Redirect("Details?id=" + id);
        }

    }
}