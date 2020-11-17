using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Utility;

namespace Sunridge.Pages.Admin.Lot.Inventory
{
    [Authorize(Roles = SD.AdministratorRole)]
    public class Upsert : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;
        public Upsert(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public Models.Inventory InventoryObj { get; set; }

        public IActionResult OnGet(int? id)
        {
            InventoryObj = new Models.Inventory();
            if (id != null)
            {
                InventoryObj = _unitOfWork.Inventory.GetFirstOrDefault(u => u.Id == id);
                if (InventoryObj == null)
                {
                    return NotFound();
                }
            }
            return Page(); //Refreshes page onGet if there's no id passed in
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (InventoryObj.Id == 0) //If there's a new Inventory Item.
            {
                _unitOfWork.Inventory.Add(InventoryObj);
            }
            else
            {
                _unitOfWork.Inventory.Update(InventoryObj);
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}
