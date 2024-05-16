using System;
using System.Collections.Generic;
using System.Linq;
using FerryManagementData.Context;
using FerryManagementData.Models;
using DTO.Models;
using FerryManagementData.Mappers;
using System.Data.Entity;
using System.Diagnostics;

namespace FerryManagementData.Repository
{
    public class CarRepository
    {

        // hent bil med id
        public static CarDTO GetCar(int id)
        {
            using (var context = new FerryContext())
            {
                var car = context.Cars
                    .Include(c => c.Passengers)
                    .FirstOrDefault(c => c.CarID == id);

                return car != null ? CarMapper.MapToDTO(car) : null;
            }
        }

        // hent alle biler for en færge
        public static List<CarDTO> GetAllCarsForFerry(int ferryId)
        {
            using (var context = new FerryContext())
            {
                var cars = context.Cars
                    .Where(c => c.FerryID == ferryId)
                    .Include(c => c.Passengers)
                    .ToList()
                    .Select(CarMapper.MapToDTO)
                    .ToList();

                return cars;
            }
        }

        //tilføj bil
        public static void AddCar(CarDTO carDto)
        {
            using (var context = new FerryContext())
            {
                var car = CarMapper.MapFromDTO(carDto);
                context.Cars.Add(car);
                context.SaveChanges();
            }
        }

        //opdater bil
        public static void UpdateCar(CarDTO carDto)
        {
            using (var context = new FerryContext())
            {
                var car = context.Cars.Find(carDto.CarID);
                if (car != null)
                {

                    car.Name = carDto.Name;
                    car.Numberplate = carDto.Numberplate;
                    car.FerryID = carDto.FerryID;
                    car.Passengers = carDto.Guests.Select(GuestMapper.MapFromDTO).ToList();
                    context.SaveChanges();
                }
            }
        }

        //slet bil
        public static void DeleteCar(int id)
        {
            using (var context = new FerryContext())
            {
                var car = context.Cars.Find(id);
                if (car != null)
                {
                    context.Cars.Remove(car);
                    context.SaveChanges();
                }
            }
        }
    }
}
