using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Bll.Models
{
    public class ColumnDto
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ColumnDto(int ID, string name)
        {
            this.ID = ID;
            this.Name = name;
        }
    }
}
