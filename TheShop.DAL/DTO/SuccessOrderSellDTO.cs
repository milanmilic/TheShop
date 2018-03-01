using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheShop.DAL.DTO
{
    public class SuccessOrderSellDTO
    {
        public TransactionType TransactionType { get; set; }
        public int? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int ArticleId { get; set; }
        public string ArticleName { get; set; }
        public int BuyerId { get; set; }
        public string BuyerName { get; set; }
        public int OrderSellPrice { get; set; }
        public DateTime OrderSellDate { get; set; }
    }

    public enum TransactionType
    {
        Order = 1, Sell = 2
    }
}
