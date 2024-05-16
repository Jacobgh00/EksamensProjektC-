using System;
using System.Collections.Generic;
using System.Linq;
using FerryManagementData.Context;
using FerryManagementData.Models;
using DTO.Models;
using FerryManagementData.Mappers;

namespace FerryManagementData.Repository
{
    public class GuestRepository
    {
        //hent gæst
        public static GuestDTO GetGuest(int id)
        {
            using (var context = new FerryContext())
            {
                var guest = context.Guests.Find(id);
                return guest != null ? GuestMapper.MapToDTO(guest) : null;
            }
        }


        //hent alle gæster på en færge
        public static List<GuestDTO> GetAllGuests(int ferryId)
        {
            using (var context = new FerryContext())
            {
                var guests = context.Guests
                    .Where(g => g.FerryID == ferryId)
                    .ToList()
                    .Select(GuestMapper.MapToDTO)
                    .ToList();
                return guests;
            }
        }

        //tilføj gæst
        public static void AddGuest(GuestDTO guestDto)
        {
            using (var context = new FerryContext())
            {
                var guest = GuestMapper.MapFromDTO(guestDto);
                context.Guests.Add(guest);
                context.SaveChanges();
                Console.WriteLine("Guest saved to database.");
            }
        }

        //opdater gæst
        public static void UpdateGuest(GuestDTO guestDto)
        {
            using (var context = new FerryContext())
            {
                var guest = context.Guests.Find(guestDto.GuestID);
                if (guest != null)
                {
                    guest.Name = guestDto.Name;
                    guest.Gender = guestDto.Gender;
                    guest.CarID = guestDto.CarID;
                    guest.FerryID = guestDto.FerryID;
                    context.SaveChanges();
                }
            }
        }

        //slet gæst
        public static void DeleteGuest(int id)
        {
            using (var context = new FerryContext())
            {
                var guest = context.Guests.Find(id);
                if (guest != null)
                {
                    context.Guests.Remove(guest);
                    context.SaveChanges();
                }
            }
        }
    }
}
