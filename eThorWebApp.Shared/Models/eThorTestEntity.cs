using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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

        public override string ToString()
        {
            return string.Join(System.Environment.NewLine, HardPropertyList.ToArray());
        }
    }
}
