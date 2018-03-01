using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TheShop.BL.IRepositories;
using TheShop.DAL.DTO;

namespace TheShop.WEB.Controllers
{
    [RoutePrefix("api/OrderSellApi")]
    public class OrderSellController : ApiController
    {
        private IOrderSellRepository orderSellRepository;

        public OrderSellController(IOrderSellRepository orderSellRepository)
        {
            this.orderSellRepository = orderSellRepository;
        }

        [HttpGet]
        [Route("GetOrderSell")]
        public IHttpActionResult PostOrderSell(int articleId, int buyerId, int orderSellPrice, TransactionType transactionType)
        {
            try
            {
                return Ok(orderSellRepository.OrderSellArticle(articleId, buyerId, orderSellPrice, transactionType));
            }
            catch (Exception ex)
            {
                HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Error getting articles list data. {ex.Message}"),
                    ReasonPhrase = string.Empty
                };
                throw new HttpResponseException(msg);
            }
        }
    }
}
