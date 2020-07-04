using CheapListBackEnd.Models;
using CheapListBackEnd.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CheapListBackEnd.Controllers
{
    public class CitiesController : ApiController
    {
        ICitiesRepository repo;
        public CitiesController(ICitiesRepository ir) => repo = ir;

        //Get All cities 
        public IHttpActionResult Get()
        {
            try
            {
                List<Cities> Datacities = repo.GetCities().ToList();
                return Ok(Datacities);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }


        //Post All cities from API run once)]
        public IHttpActionResult Post(List<Cities> cities)
        {
            try
            {
                repo.PostCities(cities);
                return Ok("Succsess");
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, ex); ;
            }
        }

  

    }
}