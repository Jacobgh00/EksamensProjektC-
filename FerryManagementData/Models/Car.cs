using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerryManagementData.Models
{
    public class Car
    {
        public int CarID { get; set; }
        public string Name { get; set; }
        public string Numberplate { get; set; }
        public virtual List<Guest> Passengers { get; set; }
        public int FerryID { get; set; }

        public Car()
        {
            Passengers = new List<Guest>();
        }

    
    }
}
