using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheShop.DAL.DTO;

namespace TheShop.BL.IRepositories
{
    public interface IBuyerRepository : IDisposable
    {
        IEnumerable<BuyerDTO> GetBuyers();
        BuyerDTO GetBuyerById(int buyerId);
        bool InsertBuyer(BuyerDTO buyer);
        bool UpdateBuyer(BuyerDTO buyer);
        bool RemoveBuyer(int buyerId);
    }
}
