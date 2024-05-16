using BusinessLogic.BLL;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Attempting to connect to the database...");

                FerryBLL ferryBLL = new FerryBLL();
                CarBLL carBLL = new CarBLL();

               ferryBLL.AddFerry(new FerryDTO
               {
                    Length = 100,
                    MaxCars = 10,
                    MaxGuests = 100,
                    CarPrice = 100,
                    GuestPrice = 50
                });

                var ferries = ferryBLL.GetFerries();
                // Vælg en færge der har plads til biler
                var ferry = ferries.FirstOrDefault(f => f.MaxCars > 0);
                if (ferry == null)
                {
                    Console.WriteLine("No ferry available with space for cars.");
                    return;
                }

                Console.WriteLine($"Working with Ferry ID: {ferry.FerryId}, Cars: {ferry.Cars.Count}, MaxCars: {ferry.MaxCars}");

                var newCar = new CarDTO
                {
                    Driver = new GuestDTO { Name = "John Doe", Gender = "Male" },
                    Guests = new List<GuestDTO>
            {
                new GuestDTO { Name = "Jane Doe", Gender = "Female" },
                new GuestDTO { Name = "Jim Beam", Gender = "Male" }
            }
                };

                carBLL.AddCarToFerry(newCar, ferry.FerryId);
                Console.WriteLine("Car added successfully to the Ferry!");

                // Print alle biler på færgen efter tilføjelsen
                ferry = ferryBLL.GetFerry(ferry.FerryId); // Opdater færge data
                Console.WriteLine($"After adding, total cars: {ferry.Cars.Count}, MaxCars: {ferry.MaxCars}");
                foreach (var car in ferry.Cars)
                {
                    Console.WriteLine($"Car ID: {car.CarId}, Driver: {car.Driver.Name}, Guests: {string.Join(", ", car.Guests.Select(g => g.Name))}");
                }

                Console.WriteLine("Operation completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }



    }
}