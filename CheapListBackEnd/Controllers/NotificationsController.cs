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
    public class NotificationsController : ApiController
    {
        INotificationsRepository repo;
        public NotificationsController(INotificationsRepository ir) => repo = ir;

        [HttpGet]
        [Route("api/Notifications/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                IEnumerable<Notifications> allNotifications = repo.GetNotifactionsByID(id).ToList();
                return Ok(allNotifications);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        public IHttpActionResult Post([FromBody] Notifications notifications)
        {
            try
            {
                repo.PostNotifactions(notifications);
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex); ;
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}