﻿using System;
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



        //får alle færger inklusiv deres biler og gæster
        //GET: api/Ferries
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_ferryBLL.GetAllFerries());
        }

        //får en specifik færge inklusiv dens biler og gæster på baggrund af id
        //GET: api/Ferries/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var ferry = _ferryBLL.GetFerry(id);
            if (ferry == null)
            {
                return NotFound();
            }
            return Ok(ferry);
        }

        //sletter en specifik færge på baggrund af id
        //Delete: api/Ferries/5
        [HttpDelete]
        public IHttpActionResult DeleteFerry(int id)
        {
            try
            {
                var ferry = _ferryBLL.GetFerry(id);
                if (ferry == null)
                {
                    return NotFound();
                }

                _ferryBLL.DeleteFerry(id);
                return Ok("Ferry deleted succesfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}   


