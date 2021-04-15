using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kanban.Bll.Models
{
    public class CardMoveDto
    {
        [Required]
        public int? ColumnId { get; set; }
        public int? PreviousCardId { get; set; }
    }
}
