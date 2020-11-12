using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
    public class ViewCount
    {
        public ViewCount()
        {
            Count = 0;
        }

        public int Id { get; set; }

        public int Count { get; set; }

        public int ClassifiedsItemId { get; set; }
        [ForeignKey("ClassifiedsItemId")]
        public virtual ClassifiedsItem ClassifiedsItem { get; set; }

    }
}
