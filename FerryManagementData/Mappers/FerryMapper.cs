using DTO.Models;
using FerryManagementData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerryManagementData.Mappers
{
    public static class FerryMapper
    {
        public static FerryDTO MapToDTO(Ferry ferry)
        {
            return new FerryDTO
            {
                FerryID = ferry.FerryID,
                Name = ferry.Name,
                MaxCars = ferry.MaxCars,
                MaxGuests = ferry.MaxGuests,
                Guests = ferry.Guests.Select(GuestMapper.MapToDTO).ToList(),
                PriceGuests = ferry.PriceGuests,
                PriceCar = ferry.PriceCar,
                Cars = ferry.Cars.Select(CarMapper.MapToDTO).ToList()
            };
        }

        public static Ferry MapFromDTO(FerryDTO ferryDto)
        {
            return new Ferry
            {
                FerryID  = ferryDto.FerryID,
                Name = ferryDto.Name,
                MaxCars = ferryDto.MaxCars,
                MaxGuests = ferryDto.MaxGuests,
                Guests = ferryDto.Guests.Select(GuestMapper.MapFromDTO).ToList(),
                PriceGuests = ferryDto.PriceGuests,
                PriceCar = ferryDto.PriceCar,
                Cars = ferryDto.Cars.Select(CarMapper.MapFromDTO).ToList()
            };
        }
    }
}
