using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace kanban.Models
{
    public class Card
    {
        public int ID { get; set; }
        public int ColumnID { get; set; }

        [JsonIgnore]
        public int Sort { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        [JsonIgnore]
        public Column Column { get; set; }
    }
}
