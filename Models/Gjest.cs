using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Ex1.Models
{
    public class Gjest
    {       
        public int Id { get; set; }
        
        [Required]
        public string gName { get; set; }
        [Required]
        public string txtTittel { get; set; }
        [Required]
        public string txtMessage { get; set; }
    }
}

