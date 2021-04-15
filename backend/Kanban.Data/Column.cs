using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kanban.Data
{
    public class Column
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [JsonIgnore]
        public int Sort { get; set; }

        [JsonIgnore]
        public ICollection<Card> Cards { get; }
    }
}
