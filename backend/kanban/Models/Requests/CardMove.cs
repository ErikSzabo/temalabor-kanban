﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kanban.Models.Requests
{
    public class CardMove
    {
        [Required]
        public int? ColumnId { get; set; }
        public int? PreviousCardId { get; set; }
    }
}
