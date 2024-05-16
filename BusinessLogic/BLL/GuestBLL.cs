using DTO.Models;
using FerryManagementData.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogic.BLL
{
    public class GuestBLL
    {

        // laver her en validering metode, som tjekker om gæsten er null eller om navnet er tomt
        private void ValidateGuest(GuestDTO guest)
        {
            if (guest == null)
                throw new ArgumentNullException(nameof(guest), "Guest cannot be null.");

            if (string.IsNullOrWhiteSpace(guest.Name))
                throw new ArgumentException("Guest name cannot be empty.", nameof(guest.Name));

        }

        // tilføjer en gæst til en færge
        public void AddGuestToFerry(int ferryId, GuestDTO guest)
        {
            

            var ferry = FerryRepository.GetFerry(ferryId);
            if (ferry == null)
                throw new InvalidOperationException($"Ferry with ID {ferryId} not found.");

            if (ferry.Guests.Count >= ferry.MaxGuests)
                throw new InvalidOperationException($"Ferry '{ferry.Name}' cannot accommodate more guests (Max guests: {ferry.MaxGuests}).");

            guest.FerryID = ferryId;
            GuestRepository.AddGuest(guest);
            
        }

        // henter alle gæster på en færge
        public List<GuestDTO> GetAllGuests(int ferryId)
        {
            return GuestRepository.GetAllGuests(ferryId);
        }

        // henter en gæst
        public GuestDTO GetGuest(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid guest ID.", nameof(id));

            return GuestRepository.GetGuest(id);
        }

        // opdaterer en gæst
        public void UpdateGuest(GuestDTO guest)
        {
            ValidateGuest(guest);
            GuestRepository.UpdateGuest(guest);
        }

        // sletter en gæst
        public void DeleteGuest(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid guest ID.", nameof(id));

            GuestRepository.DeleteGuest(id);
        }

    }
}
