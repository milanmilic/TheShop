using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TheShop.BL.IRepositories;
using TheShop.DAL.DTO;

namespace TheShop.WEB.Controllers
{
    [RoutePrefix("api/BuyerApi")]
    public class BuyerApiController : ApiController
    {
        private IBuyerRepository buyerRepository;

        public BuyerApiController(IBuyerRepository buyerRepository)
        {
            this.buyerRepository = buyerRepository;
        }

        [HttpGet]
        [Route("GetBuyers")]
        public IHttpActionResult GetBuyers()
        {
            try
            {
                return Ok(buyerRepository.GetBuyers());
            }
            catch (Exception ex)
            {
                HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Error getting buyer list data. {ex.Message}"),
                    ReasonPhrase = string.Empty
                };
                throw new HttpResponseException(msg);
            }
        }

        [HttpGet]
        [Route("GetBuyerById")]
        public IHttpActionResult GetBuyerById(int buyerId)
        {
            try
            {
                return Ok(buyerRepository.GetBuyerById(buyerId));
            }
            catch (Exception ex)
            {
                HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Error getting buyer by Id. {ex.Message}"),
                    ReasonPhrase = string.Empty
                };
                throw new HttpResponseException(msg);
            }
        }

        [HttpPost]
        [Route("InsertBuyer")]
        public IHttpActionResult InsertBuyer([FromBody]BuyerDTO buyer)
        {
            try
            {
                return Ok(buyerRepository.InsertBuyer(buyer));
            }
            catch (Exception ex)
            {
                HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Error inserting buyer in db. {ex.Message}"),
                    ReasonPhrase = string.Empty
                };
                throw new HttpResponseException(msg);
            }
        }

        [HttpPost]
        [Route("UpdateBuyer")]
        public IHttpActionResult UpdateBuyer([FromBody]BuyerDTO buyer)
        {
            try
            {
                return Ok(buyerRepository.UpdateBuyer(buyer));
            }
            catch (Exception ex)
            {
                HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Error updating buyer in db. {ex.Message}"),
                    ReasonPhrase = string.Empty
                };
                throw new HttpResponseException(msg);
            }
        }

        [HttpGet]
        [Route("RemoveBuyer")]
        public IHttpActionResult RemoveBuyer(int buyerId)
        {
            try
            {
                return Ok(buyerRepository.RemoveBuyer(buyerId));
            }
            catch (Exception ex)
            {
                HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Error removing buyer from db. {ex.Message}"),
                    ReasonPhrase = string.Empty
                };
                throw new HttpResponseException(msg);
            }
        }
    }
}
