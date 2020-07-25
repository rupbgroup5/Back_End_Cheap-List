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
    public class AppListController : ApiController
    {
        IAppListRepository repo;
        public AppListController(IAppListRepository ir) => repo = ir;

        // GET api/<controller>
        [HttpGet]
        [Route("api/appList/{groupID}")]
        public IHttpActionResult Get(int groupID)
       {
            try
            {
                List<AppList> AppList = repo.GetAllList(groupID).ToList();
                return Ok(AppList);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/appList/{groupID}/{listID}")]
        public IHttpActionResult Get(int groupID, int listID)
        {
            try
            {
                AppList appList = repo.GetAppListById(groupID, listID); ;
                return Ok(appList);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody] AppList appList)
        {
            try
            {
                repo.PostAppList(appList);
                return Ok(appList);
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, ex); ;
            }
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put([FromBody] AppList appList)
        {
            try
            {
                repo.UpdateListName(appList);
                return Ok(appList);
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, ex); ;
            }
        }
        
        //[HttpPut]
        //[Route("api/appList/UpdateCityName")]
        //public IHttpActionResult PutCityName(AppList applist)
        //{
        //    try
        //    {
        //        repo.UpdateCityName(applist);
        //        return Ok("Succsess");
        //    }
        //    catch (Exception ex)
        //    {

        //        return Content(HttpStatusCode.BadRequest, ex); ;
        //    }
        //}

        [HttpPut]
        [Route("api/appList/limit/{limit}/{listID}")]
        public IHttpActionResult PutLimitPrice(double limit, int listID)
        {
            try
            {
                repo.UpdateLimitPrice(limit, listID);
                return Ok(limit);
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, ex); ;
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("api/appList/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                repo.DeleteAppList(id);
                return Ok(id);
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, ex); ;
            }
        }


        [HttpPut]
        [Route("api/appList/Location")]
        public IHttpActionResult PutUpdateLocation(AppList appList)
        {
            try
            {
                repo.UpdateLocation(appList);
                return Ok("Succsess");
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, ex); ;
            }
        }
    }
}