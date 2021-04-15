using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Bll.Models
{
    public class CardUpdateDto
    {

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        public CardUpdateDto(string title, string description, DateTime deadline)
        {
            Title = title;
            Description = description;
            Deadline = deadline;
        }
    }
}
