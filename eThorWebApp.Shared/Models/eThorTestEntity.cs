using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace eThorWebApp.Shared.Models
{

    public class eThorTestEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Column]
        [Required]
        public List<string> HardPropertyList { get; set; } = new List<string>();
    }
}
