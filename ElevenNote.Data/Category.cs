using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Data
{
    public class Category
    {
        public int CategoryID { get; set; }
        
        //[ForeignKey(nameof = "")]
        public int NoteID { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Enter at least 2 characters.")]
        [MaxLength(25, ErrorMessage = "Too many characters.")]
        public string Name { get; set; }

        [Required]
        public DateTimeOffset CreatedUTC { get; set; }
        public DateTimeOffset UpdatedUTC { get; set; }

        public virtual Note Note { get; set; }


    }
}
