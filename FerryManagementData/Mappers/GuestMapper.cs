using DTO.Models;
using FerryManagementData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerryManagementData.Mappers
{
    public static class GuestMapper
    {
        public static GuestDTO MapToDTO(Guest guest)
        {
            return new GuestDTO
            {
              GuestID = guest.GuestID,
              Name = guest.Name,
              Gender = guest.Gender,
              Birthdate = guest.Birthdate,
              CarID = guest.CarID,
              FerryID = guest.FerryID

            };
        }

        public static Guest MapFromDTO(GuestDTO guestDto)
        {
            return new Guest
            {
                GuestID = guestDto.GuestID,
                Name = guestDto.Name,
                Gender = guestDto.Gender,
                Birthdate = guestDto.Birthdate,
                CarID = guestDto.CarID,
                FerryID = guestDto.FerryID

            };
        }
    }
}
