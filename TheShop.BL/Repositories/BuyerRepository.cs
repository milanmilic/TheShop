using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheShop.BL.IRepositories;
using TheShop.DAL.DTO;
using TheShop.DATA.Model;

namespace TheShop.BL.Repositories
{
    public class BuyerRepository : BaseRepository, IBuyerRepository, IDisposable
    {
        public IEnumerable<BuyerDTO> GetBuyers()
        {
            List<BuyerDTO> buyerDTOList = new List<BuyerDTO>();

            try
            {
                var buyers_db = ctx.Buyers.ToList();

                buyers_db.ForEach(item => buyerDTOList.Add(new BuyerDTO {
                    BuyerId = item.BuyerId,
                    Name = item.Name
                }));

                return buyerDTOList;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error - {ex.Message} - on getting buyers list.");
            }
        }

        public BuyerDTO GetBuyerById(int buyerId)
        {
            BuyerDTO buyerDTO = new BuyerDTO();

            try
            {
                var buyer_db = ctx.Buyers.Find(buyerId);

                if(buyer_db != null)
                {
                    buyerDTO.BuyerId = buyer_db.BuyerId;
                    buyerDTO.Name = buyer_db.Name;
                }
                return buyerDTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error - {ex.Message} - on getting buyer by id.");
            }
        }

        public bool InsertBuyer(BuyerDTO buyer)
        {
            var buyer_db = ctx.Set<Buyer>();
            Buyer newBuyer = new Buyer();

            try
            {
                if(buyer != null)
                {
                    newBuyer.BuyerId = buyer.BuyerId;
                    newBuyer.Name = buyer.Name;

                    ctx.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error - {ex.Message} - on inserting new buyer.");
            }
        }

        public bool UpdateBuyer(BuyerDTO buyer)
        {
            var buyer_db = ctx.Buyers.Where(x => x.BuyerId == buyer.BuyerId).FirstOrDefault();

            try
            {
                if(buyer_db != null)
                {
                    buyer_db.BuyerId = buyer.BuyerId;
                    buyer_db.Name = buyer.Name;

                    ctx.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error - {ex.Message} - on updating existing buyer.");
            }
        }

        public bool RemoveBuyer(int buyerId)
        {
            try
            {
                Buyer buyer_db = ctx.Buyers.Find(buyerId);

                if(buyer_db != null)
                {
                    ctx.Buyers.Remove(buyer_db);

                    ctx.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error - {ex.Message} - on removing existing buyer.");
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    ctx.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
