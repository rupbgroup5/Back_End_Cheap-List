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

        // POST api/<controller>
        public IHttpActionResult Post([FromBody] AppGroup appgroup)
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

        // PUT api/<controller>/5
        public IHttpActionResult PUT([FromBody] AppGroup appgroup)
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

        [HttpPut]
        [Route("api/appGroups/RemoveUserFromGroup/{userId}/{groupId}")]
        public IHttpActionResult RemoveUserFromGroup(int userId, int groupId)
        {
            try
            {
                int res = repo.RemoveParticipantFromGroup(userId, groupId);
                return res > 0 ? Ok($"the user with the id:{userId} has been deleted from group {groupId}") :
                throw new EntryPointNotFoundException("there is no user with the provided id");
            }
            catch (EntryPointNotFoundException ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        [Route("api/appGroups/AddUsers2UserInGroup")]
        public IHttpActionResult AddUsers2UserInGroup([FromBody] AppGroup appGroup) 
        {
            try
            {
                int res = repo.AddUsers2UserInGroup(appGroup);
                return res > 0 ?
                Ok($"all new users have added to group where group id: {appGroup.GroupID}") :
                throw new Exception($"the new users haven't added to the  group where group id: {appGroup.GroupID}");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }


        }

    }
}