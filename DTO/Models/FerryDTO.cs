using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    public class FerryDTO
    {
        public int FerryID { get; set; }
        public string Name { get; set; }
        public int MaxCars { get; set; }
        public int MaxGuests { get; set; }
        public virtual ICollection<GuestDTO> Guests { get; set; }
        public virtual ICollection<CarDTO> Cars { get; set; }
        public int PriceGuests { get; set; }
        public int PriceCar { get; set; }

        public FerryDTO()
        {
            Guests = new List<GuestDTO>();
            Cars = new List<CarDTO>();
        }

        //laver nogle computed properties her, måske dårlig praksis
        public int TotalGuests => Guests?.Count ?? 0;
        public int TotalCars => Cars?.Count ?? 0;
        public int TotalPrice => (TotalGuests * PriceGuests) + (TotalCars * PriceCar);
    }
}
