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
        [Route("api/Notifications/{userID}/{listID}")]
        public IHttpActionResult Get(int userID, int listID)
        {

            try
            {
                IEnumerable<Notifications> allNotifications = repo.GetNotifactionsByID(userID, listID).ToList();
                return Ok(allNotifications);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("api/Notifications/getByGroupId/{userID}/{groupID}")]
        public IHttpActionResult getByGroupId(int userID, int groupID)
        {
            try
            {
                IEnumerable<Notifications> allNotifications = repo.GetNotifactionsByGroupID(userID, groupID).ToList();
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

        [HttpPost]
        [Route("api/Notifications/PostNot2MultipleParticipants")]
        public IHttpActionResult PostNot2MultipleParticipants([FromBody] Notifications notification)
        {
            try
            {
                repo.PostNot2MultipleParticipants(notification);
                return Ok("the notification received to the app DB under all the provided ids");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex); ;
            }
        }



        public IHttpActionResult Put([FromBody] List<Notifications> notifications)
        {
            try
            {
                repo.UpdateNotifactions(notifications);
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex); ;
            }
        }


   
    }
}