using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Ex1.Models
{
    public class Innlegg
    {       
        public int Id { get; set; }

        [Required]
        public string txtTittel { get; set; }
        
        [Required]
        public string txtMessage { get; set; }

        public ApplicationUser User { get; set; }
    }
}