using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLogic.BLL;
using DTO.Models;
using System.Web.Http.Cors;


namespace WebAPI.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FerriesController : ApiController
    {
        private FerryBLL _ferryBLL = new FerryBLL();


        //GET: api/Ferries
        public IHttpActionResult Get()
        {
            return Ok(_ferryBLL.GetAllFerries());
        }

        //GET: api/Ferries/5
        public IHttpActionResult Get(int id)
        {
            var ferry = _ferryBLL.GetFerry(id);
            if (ferry == null)
            {
                return NotFound();
            }
            return Ok(ferry);
        }
    }
}

