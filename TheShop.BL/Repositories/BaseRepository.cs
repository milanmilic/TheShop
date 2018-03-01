using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheShop.DATA;

namespace TheShop.BL.Repositories
{
    public class BaseRepository
    {
        protected TheShopDbContext ctx;

        public BaseRepository()
        {
            ctx = new TheShopDbContext();
        }
    }
}
