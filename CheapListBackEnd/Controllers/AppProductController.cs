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
    public class AppProductController : ApiController
    {
        IAppProductRepository repo;
        public AppProductController(IAppProductRepository ir) => repo = ir;

        // GET api/<controller>
        [HttpGet]
        [Route("api/AppProduct/{listID}")]
        public IHttpActionResult Get(int listID)
         {
            try
            {
                List<AppProduct> ProductCart = repo.GetProductCart(listID).ToList();
                return Ok(ProductCart);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]AppProduct appProduct)
        {
            try
            {
                repo.PostAppProduct(appProduct);
                return Ok(appProduct);
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
        [HttpDelete]
        [Route("api/appProduct/{barcode}/{listID}")]
        public IHttpActionResult Delete(string barcode, int listID)
        {
            try
            {
                repo.DeleteProduct(barcode,listID);
                return Ok(barcode);
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, ex); 
            }
        }
        [HttpPut]
        [Route("api/appProduct/UpdateQuantity/{HasAddedQ}")]
        public IHttpActionResult UpdateQuantity([FromBody]AppProduct appProduct, bool HasAddedQ) //action = ture => plus
        {
            try
            {
                repo.UpdateQuantity(appProduct, HasAddedQ);
                return Ok(appProduct);
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, ex); ;
            }
        }


    }
}