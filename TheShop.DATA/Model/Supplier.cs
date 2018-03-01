using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheShop.DATA.Model
{
    public class Supplier
    {
        public Supplier()
        {
            this.Articles = new HashSet<Article>();
        }
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
