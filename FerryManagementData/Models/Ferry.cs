using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerryManagementData.Models
{
    public class Ferry
    {
        public int FerryID { get; set; }
        public string Name { get; set; }
        public int MaxCars { get; set; }
        public int MaxGuests { get; set; }
        public virtual ICollection<Guest> Guests { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
        public int PriceGuests { get; set; }
        public int PriceCar { get; set; }

        public Ferry()
        {
            Guests = new List<Guest>();
            Cars = new List<Car>();
        }
    }
}
