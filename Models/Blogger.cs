using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Ex1.Models
{
    public class Blogger
    {       
        public int Id { get; set; }

        List<Innlegg> InnleggItem { get; set;}
    }
}

