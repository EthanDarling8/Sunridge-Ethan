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

        public int ClassifiedsId { get; set; }

        [NotMapped]
        [ForeignKey("ClassifiedsId")]
        public virtual Classifieds Classifieds { get; set; }

    }
}
