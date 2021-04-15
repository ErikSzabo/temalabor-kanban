using Kanban.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Bll.Models
{
    public class CardDto
    {
        public int ID { get; set; }

        public int ColumnID { get; set; }
        
        [Required] 
        [StringLength(200)] 
        public string Title { get; set; }

        [Required] 
        [StringLength(500)] 
        public string Description { get; set; }

        [Required] 
        public DateTime Deadline { get; set; }

        public CardDto(int iD, int columnID, string title, string description, DateTime deadline)
        {
            ID = iD;
            ColumnID = columnID;
            Title = title;
            Description = description;
            Deadline = deadline;
        }

        public CardDto(Card card)
        {
            ID = card.ID;
            ColumnID = card.ColumnID;
            Title = card.Title;
            Description = card.Description;
            Deadline = card.Deadline;
        }

        public CardDto() { }
    }
}
