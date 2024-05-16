using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models
{
    public class CarDTO
    {
        public int CarID { get; set; }
        public string Name { get; set; }
        public string Numberplate { get; set; }
        public virtual List<GuestDTO> Guests { get; set; }
        public int FerryID { get; set; }

        // Computed property til at vise antal gæster for hver bil, måske kan man bare bruge Guests.Count
        public int NumberOfGuests { get; set; }

        public CarDTO()
        {
            Guests = new List<GuestDTO>();
        }

        


    }
}
