using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sunridge.Models.ViewModels {
    public class OwnerBoardMemberVM {
        public BoardMember BoardMember { get; set; }
        public Owner Owner { get; set; }
        public OwnerBoardMember OwnerBoardMember { get; set; }
        public IEnumerable<SelectListItem> OwnerList { get; set; }
    }
}