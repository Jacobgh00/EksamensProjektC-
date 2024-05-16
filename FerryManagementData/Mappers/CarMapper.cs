using FerryManagementData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Models;
using System.Runtime.ConstrainedExecution;

namespace FerryManagementData.Mappers
{
    public static class CarMapper
    {
        //mapper Car to CarDTO
        public static CarDTO MapToDTO(Car car)
        {
            return new CarDTO
            {
                CarID = car.CarID,
                Name = car.Name,
                Numberplate = car.Numberplate,
                FerryID = car.FerryID,
                Guests = car.Passengers.Select(GuestMapper.MapToDTO).ToList()

            };
        }

        //mapper CarDTO to Car
        public static Car MapFromDTO(CarDTO carDto)
        {
            return new Car
            {
                CarID = carDto.CarID,
                Name = carDto.Name,
                Numberplate = carDto.Numberplate,
                FerryID = carDto.FerryID,
                Passengers = carDto.Guests.Select(GuestMapper.MapFromDTO).ToList()
            };
        }
    }
}
