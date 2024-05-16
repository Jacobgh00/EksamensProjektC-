using System;
using System.Collections.Generic;
using System.Linq;
using FerryManagementData.Context;
using FerryManagementData.Models;
using DTO.Models;
using FerryManagementData.Mappers;

namespace FerryManagementData.Repository
{
    public class FerryRepository
    {

        //hent færge ud fra id
        public static FerryDTO GetFerry(int id)
        {
            using (var context = new FerryContext())
            {
                var ferry = context.Ferries.Find(id);
                return ferry != null ? FerryMapper.MapToDTO(ferry) : null;
            }
        }

        //hent alle færger
        public static List<FerryDTO> GetAllFerries()
        {
            using (var context = new FerryContext())
            {
                var ferries = context.Ferries.ToList().Select(FerryMapper.MapToDTO).ToList();
                return ferries;
            }
        }

        //tilføj færge
        public static void AddFerry(FerryDTO ferryDto)
        {
            using (var context = new FerryContext())
            {
                var ferry = FerryMapper.MapFromDTO(ferryDto);
                context.Ferries.Add(ferry);
                context.SaveChanges();
            }
        }

        //opdater færge
        public static void UpdateFerry(FerryDTO ferryDto)
        {
            using (var context = new FerryContext())
            {
                var ferry = context.Ferries.Find(ferryDto.FerryID);
                if (ferry != null)
                {
                    ferry.Name = ferryDto.Name;
                    ferry.MaxCars = ferryDto.MaxCars;
                    ferry.MaxGuests = ferryDto.MaxGuests;
                    ferry.PriceGuests = ferryDto.PriceGuests;
                    ferry.PriceCar = ferryDto.PriceCar;
                    ferry.Cars = ferryDto.Cars.Select(CarMapper.MapFromDTO).ToList();
                    ferry.Guests = ferryDto.Guests.Select(GuestMapper.MapFromDTO).ToList();
                    context.SaveChanges();
                }
            }
        }

        //slet færge
        public static void DeleteFerry(int id)
        {
            using (var context = new FerryContext())
            {
                var ferry = context.Ferries.Find(id);
                if (ferry != null)
                {
                    context.Ferries.Remove(ferry);
                    context.SaveChanges();
                }
            }
        }




    }
}