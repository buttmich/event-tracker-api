using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public partial class Events
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Categories")]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }

    }
}
