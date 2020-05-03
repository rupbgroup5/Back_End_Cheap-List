using CheapListBackEnd.Models;
using CheapListBackEnd.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace CheapListBackEnd.Controllers
{
    //local  path = http://localhost:56794/
    //global path = http://proj.ruppin.ac.il/bgroup5/FinalProject/backEnd/api/AppUsers
    public class AppUsersController : ApiController
    {
        IAppUsersRepository repo;
        // in this way I can use any class which implement IAppUsersRepository
        public AppUsersController(IAppUsersRepository ir) => repo = ir; // Unity config use this constractur automatically 

        // GET api/<controller>
        [HttpGet]
        [Route("api/AppUsers/GetUsers")]
        public IHttpActionResult Get()
        {
            try
            {
                List<AppUser> appUsersList = repo.GetAllAppUsers().ToList();
                return Ok(appUsersList);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpGet]
        [Route("api/AppUsers/GetUser/{userID}")]
        public IHttpActionResult GetUser(int userID)
        {
            // option to modify return base.OK(GetAppUserByName(userName));
            // NOT TESTED yet
            try
            {
                AppUser appUser = repo.GetAppUserByID(userID);
                return appUser.UserID > 0 ? Ok(appUser) :
                throw new EntryPointNotFoundException($"there is no user with the provided id");
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


        [HttpGet]
        [Route("api/AppUsers/SendUserPassword/{fullmailNoDots}")]
        public IHttpActionResult SendUserPassword(string fullmailNoDots)
        /*cant get . so we change it to "_" at our front-end
        * and back to "." here
        */
        {
            string userMail = fullmailNoDots.Replace("_", ".");
            try
            {
                string response = repo.UserForgotPassword(userMail);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpGet]
        [Route("api/AppUsers/AuthenticateUserLogin/{userName}/{userPassword}")]
        public IHttpActionResult AuthenticateUserLogin(string userName, string userPassword)
        {

            try
            {
                AppUser au = repo.AuthenticateUserLogin(userName, userPassword);
                return Ok(au);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpGet]
        [Route("api/AppUsers/GetUserContacts/{userID}")]
        public IHttpActionResult GetUserContacts(int userID)
        {
            try
            {
                List<Contact> contacts = repo.GetUserContacts(userID);
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }

        }


        [HttpGet]
        [Route("api/AppUsers/GetExsistUserSocailID/{socailID}")]
        public IHttpActionResult GetExsistUserSocailID(string socailID)
        {
            try
            {
                AppUser userDetails2Client = repo.GetExsistUserSocailID(socailID);
                return Ok(userDetails2Client);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("api/AppUsers/AuthenticateContact/{phoneNumber}")]
        public IHttpActionResult AuthenticateContact(string phoneNumber)
        {
            try
            {
                AppUser appUser = repo.AuthenticateContact(phoneNumber);
                return Ok(appUser);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("api/AppUsers/IsExpoTokenUpdated/{userID}")]
        public IHttpActionResult IsExpoTokenUpdated(int userID)
        {
            try
            {
                bool response = repo.IsExpoTokenUpdated(userID);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/AppUsers/updateUserExpoToken")]
        public IHttpActionResult UpdateUserExpoToken([FromBody]AppUser user)
        {
            try
            {
                int res = repo.UpdateUserExpoToken(user);
                return res > 0 ? Ok("user's Expo Token Updated in the App DB")
                :
                throw new EntryPointNotFoundException
                ($"there is no user with the provided id ({user.UserID})");
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
        
        [HttpPost]
        [Route("api/AppUsers/PostUser")]
        public IHttpActionResult Post([FromBody]AppUser newUser)
        {
            try
            {
                int newUserID = repo.PostAppUser(newUser);
                return Ok(newUserID);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/AppUsers/SystemPostUser")]
        public IHttpActionResult SystemPostUser([FromBody] AppUser userBySystem)
        {
            try
            {
                repo.PostSystemAppUser(userBySystem);
                return Ok(userBySystem);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }


        // PUT api/<controller>/5
        // col 2 update could be either: UserMail, UserPassword, UserName, User Adress
        //---> need to make a DDL in the client side
        public IHttpActionResult Put([FromBody]UpdateAppUser user2update)
        {
            try
            {
                int res = repo.UpdateFeild(user2update);
                return res > 0 ? Ok($"we have update {user2update.Column2update} to {user2update.NewValue} in the app database") :
                throw new EntryPointNotFoundException($"there is no user with the provided id ({user2update.Id})");
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


        [HttpPost]
        [Route("api/AppUsers/updateUserContacts")]
        public IHttpActionResult UpdateUserContacts([FromBody]AppUser user)
        {
            try
            {
                repo.UpdateUserContactsList(user);
                return Ok("user contacts has been updated");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                int res = repo.DeleteAppUser(id);
                return res > 0 ? Ok($"the user with the id:{id} has been deleted from the app database") :
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
    }
}