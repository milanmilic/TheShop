using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheShop.DATA.Model
{
    public class Article
    {
        public int ArticleId { get; set; }

        public string Name { get; set; }

        public int ArticlePrice { get; set; }
        public bool IsSold { get; set; }

        public DateTime? SoldDate { get; set; }
        public int? BuyerId { get; set; }
        public virtual Buyer Buyer { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
