using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sunridge.Models.ViewModels {
    public class OwnerBoardMemberVM {
        public IEnumerable<SelectListItem> OwnerList { get; set; }
        public IEnumerable<SelectListItem> BoardMemberList { get; set; }
        public BoardMember BoardMember { get; set; }
    }
}