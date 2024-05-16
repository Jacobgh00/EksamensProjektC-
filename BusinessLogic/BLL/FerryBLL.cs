using DTO.Models;
using FerryManagementData.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BLL
{
    public class FerryBLL
    {
        // Få alle færger
        public List<FerryDTO> GetAllFerries()
        {
            return FerryRepository.GetAllFerries();
        }

        // få alle færge på id
        public FerryDTO GetFerry(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ferry ID.", nameof(id));

            return FerryRepository.GetFerry(id);
        }

        // tilføj en ny færge
        public void AddFerry(FerryDTO ferry)
        {
            if (ferry == null)
                throw new ArgumentNullException(nameof(ferry), "Ferry cannot be null.");

            if (ferry.MaxCars < 10 || ferry.MaxGuests < 40)
                throw new ArgumentException("Ferry must support at least 10 cars and 40 guests.", nameof(ferry));

            FerryRepository.AddFerry(ferry);
        }

        // Updates the details of an existing ferry
        public void UpdateFerry(FerryDTO ferry)
        {
            if (ferry == null)
                throw new ArgumentNullException(nameof(ferry), "Ferry cannot be null.");

            FerryRepository.UpdateFerry(ferry);
        }

        // Deletes a ferry by its ID
        public void DeleteFerry(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ferry ID.", nameof(id));

            FerryRepository.DeleteFerry(id);
        }

        
    }
}
