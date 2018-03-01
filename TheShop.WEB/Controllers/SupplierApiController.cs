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
    [RoutePrefix("api/SupplierApi")]
    public class SupplierApiController : ApiController
    {
        private ISupplierRepository supplierRepository;

        public SupplierApiController(ISupplierRepository supplierRepository)
        {
            this.supplierRepository = supplierRepository;
        }

        [HttpGet]
        [Route("GetSuppliers")]
        public IHttpActionResult GetSuppliers()
        {
            try
            {
                return Ok(supplierRepository.GetSuppliers());
            }
            catch (Exception ex)
            {
                HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Error getting suppliers list data. {ex.Message}"),
                    ReasonPhrase = string.Empty
                };
                throw new HttpResponseException(msg);
            }
        }

        [HttpGet]
        [Route("GetSupplierById")]
        public IHttpActionResult GetSupplierById(int supplierId)
        {
            try
            {
                return Ok(supplierRepository.GetSupplierById(supplierId));
            }
            catch (Exception ex)
            {
                HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Error getting supplier by Id. {ex.Message}"),
                    ReasonPhrase = string.Empty
                };
                throw new HttpResponseException(msg);
            }
        }

        [HttpPost]
        [Route("InsertSupplier")]
        public IHttpActionResult InsertSupplier ([FromBody]SupplierDTO supplier)
        {
            try
            {
                return Ok(supplierRepository.InsertSupplier(supplier));
            }
            catch (Exception ex)
            {
                HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Error inserting supplier in db. {ex.Message}"),
                    ReasonPhrase = string.Empty
                };
                throw new HttpResponseException(msg);
            }
        }

        [HttpPost]
        [Route("UpdateSupplier")]
        public IHttpActionResult UpdateSupplier([FromBody]SupplierDTO supplier)
        {
            try
            {
                return Ok(supplierRepository.UpdateSupplier(supplier));
            }
            catch (Exception ex)
            {
                HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Error updating supplier in db. {ex.Message}"),
                    ReasonPhrase = string.Empty
                };
                throw new HttpResponseException(msg);
            }
        }

        [HttpGet]
        [Route("RemoveSupplier")]
        public IHttpActionResult RemoveSupplier(int supplierId)
        {
            try
            {
                return Ok(supplierRepository.RemoveSupplier(supplierId));
            }
            catch (Exception ex)
            {
                HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Error removing supplier from db. {ex.Message}"),
                    ReasonPhrase = string.Empty
                };
                throw new HttpResponseException(msg);
            }
        }
    }
}
