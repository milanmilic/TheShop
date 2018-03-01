using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheShop.DATA.Model
{
    public class Buyer
    {
        public Buyer()
        {
            this.Articles = new HashSet<Article>();
        }
        public int BuyerId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
