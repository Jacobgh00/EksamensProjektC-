using DTO.Models;
using FerryManagementData.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.BLL
{
    public class CarBLL
    {
        // funktion til at validere en bil
        private void ValidateCar(CarDTO car)
        {
            if (car == null)
                throw new ArgumentNullException(nameof(car), "Car cannot be null.");

            if (string.IsNullOrWhiteSpace(car.Numberplate))
                throw new ArgumentException("License plate cannot be empty.", nameof(car.Numberplate));

        }

        
        // tilføjer en bil til en færge
        public void AddCarToFerry(int ferryId, CarDTO car)
        {
            ValidateCar(car);

            car.FerryID = ferryId;

            CarRepository.AddCar(car);
        }


        // henter alle biler til en færge
        public List<CarDTO> GetAllCarsForFerry(int ferryId)
        {
            return CarRepository.GetAllCarsForFerry(ferryId);
        }



        // henter en bil på baggrund af id
        public CarDTO GetCar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid car ID.", nameof(id));

            return CarRepository.GetCar(id);
        }

        // updates a car
        public void UpdateCar(CarDTO car)
        {
            ValidateCar(car);
            CarRepository.UpdateCar(car);
        }

        // sletter en bil
        public void DeleteCar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid car ID.", nameof(id));

            CarRepository.DeleteCar(id);
        }

        
    }
}
