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
    public class GuestController : ApiController
    {

        private GuestBLL _guestBLL = new GuestBLL();


        //Get : api/Ferry/1/Guests
        [HttpGet]
        [Route("api/Ferry/{ferryId}/Guests")]
        public IHttpActionResult GetAllGuestsForCar(int ferryId)
        {
            try
            {
                var guests = _guestBLL.GetAllGuests(ferryId);
                if (guests == null || !guests.Any())
                    return NotFound();

                return Ok(guests);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //Get : api/Ferry/1/Guests/1
        [HttpGet]
        [Route("api/Ferry/{ferryId}/Guests/{id}")]
        public IHttpActionResult GetGuest(int ferryId, int id)
        {
            try
            {
                var guest = _guestBLL.GetGuest(id);
                if (guest == null || guest.FerryID != ferryId)
                    return NotFound();

                return Ok(guest);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
