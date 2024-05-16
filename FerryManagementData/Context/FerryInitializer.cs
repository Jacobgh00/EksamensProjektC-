using System;
using System.Collections.Generic;
using System.Data.Entity;
using FerryManagementData.Models;

namespace FerryManagementData.Context
{
    public class FerryInitializer : CreateDatabaseIfNotExists<FerryContext>
    {
        protected override void Seed(FerryContext context)
        {
            Ferry f1 = new Ferry { Name = "Magnolia", MaxCars = 20, MaxGuests = 100, PriceCar = 197, PriceGuests = 99 };
            Ferry f2 = new Ferry { Name = "Titanic", MaxCars = 40, MaxGuests = 200, PriceCar = 197, PriceGuests = 99 };

            Car car = new Car { Name = "Opel", Numberplate = "AB73857" };

            Guest passenger = new Guest { Name = "Jakob", Gender = "Mand", Birthdate = new DateTime(2001, 3, 24) };

            car.Passengers.Add(passenger);
            f1.Guests.Add(passenger);
            f1.Cars.Add(car);

            context.Guests.Add(passenger);
            context.Cars.Add(car);
            context.Ferries.Add(f1);
            context.Ferries.Add(f2);
            context.SaveChanges();

        }
    }
}
