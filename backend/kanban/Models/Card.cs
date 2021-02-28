using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace kanban.Models
{
    public class Card
    {
        public int ID { get; set; }

        public int? ParentID { get; set; }
        public int ColumnID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        public Column Column { get; set; }
        public Card Parent { get; set; }
    }
}
