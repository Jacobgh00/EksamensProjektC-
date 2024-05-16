using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    public class GuestDTO
    {
        public int GuestID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public int? CarID { get; set; }
        public int FerryID { get; set; }
        public GuestDTO() { }

        
    }
}
