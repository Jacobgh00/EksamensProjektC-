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
        [Required]
        public int CarID { get; set; }

        [Required]

        public string Name { get; set; }
        [Required]
        public string Numberplate { get; set; }

        [Required]
        public virtual List<GuestDTO> Guests { get; set; }

        [Required]
        public int FerryID { get; set; }

        // Computed property til at vise antal gæster for hver bil, måske kan man bare bruge Guests.Count
        public int NumberOfGuests { get; set; }

        public CarDTO()
        {
            Guests = new List<GuestDTO>();
        }

        


    }
}
