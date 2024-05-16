using BusinessLogic.BLL;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CarController : ApiController
    {
        private CarBLL _carBLL = new CarBLL();


        
        // GET: api/Ferry/1/Cars : Det vil sige at vi får alle bilerne tilknyttet en færge
        [HttpGet]
        [Route("api/Ferry/{ferryId}/Cars")]
        public IHttpActionResult GetAllCarsForFerry(int ferryId)
        {
            try
            {
                var cars = _carBLL.GetAllCarsForFerry(ferryId).Select(car =>
                {
                    car.NumberOfGuests = car.Guests?.Count ?? 0;
                    return car;
                }).ToList();

                if (!cars.Any())
                    return NotFound();

                return Ok(cars);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Ferry/5/Cars/1 : Det vil sige at vi får en specifik bil tilknyttet en færge
        [HttpGet]
        [Route("api/Ferry/{ferryId}/Cars/{id}")]
        public IHttpActionResult GetCarForFerry(int ferryId, int id)
        {
            try
            {
                var car = _carBLL.GetCar(id);
                if (car == null || car.FerryID != ferryId)
                    return NotFound();

                return Ok(car);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
