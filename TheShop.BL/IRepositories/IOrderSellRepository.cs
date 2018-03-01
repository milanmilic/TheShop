using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheShop.DAL.DTO;

namespace TheShop.BL.IRepositories
{
    public interface IOrderSellRepository : IDisposable
    {
        SuccessOrderSellDTO OrderSellArticle(int articleId, int buyerId, int orderSellPrice, TransactionType transactionType);
    }
}
