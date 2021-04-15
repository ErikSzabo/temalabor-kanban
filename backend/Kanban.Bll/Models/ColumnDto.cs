using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Bll.Models
{
    public sealed record ColumnDto(
        [Required] int ID,
        [Required] [StringLength(100)] string Name
    );
}
