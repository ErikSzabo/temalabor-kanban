using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kanban.Models
{
    public class Column
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        public int Sort { get; set; }
        public ICollection<Card> Cards { get; set; }
    }
}
