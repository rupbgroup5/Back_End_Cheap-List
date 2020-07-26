﻿using CheapListBackEnd.Models;
using CheapListBackEnd.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CheapListBackEnd.Controllers
{
    //http://localhost:56794/api/AppGroups 
    public class AppGroupsController : ApiController
    {
        IAppGroupRepository repo;
        public AppGroupsController(IAppGroupRepository ir) => repo = ir;

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            try
            {
                IEnumerable<AppGroup> AppGroupsList = repo.GetAllGroups().ToList();
                return Ok(AppGroupsList);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }

        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/AppGroups/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
              IEnumerable<AppGroup> appGroupsList = repo.GetAppGroupById(id);
                return Ok(appGroupsList);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

 
        public IHttpActionResult Post( [FromBody] AppGroup appgroup )
        {
            try
            {
                repo.PostAppGroup(appgroup);
                return Ok(appgroup);
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, ex); ;
            }
        }

        public IHttpActionResult PUT ([FromBody] AppGroup appgroup)
        {
            try
            {
                repo.UpdateGroupName(appgroup);
                return Ok(appgroup);
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, ex); ;
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("api/appGroups/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                repo.DeleteAppGroup(id);
                return Ok(id);
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, ex); ;
            }
        }
    }
}