using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheShop.DAL.DTO;
using TheShop.BL.IRepositories;
using TheShop.DATA.Model;

namespace TheShop.BL.Repositories
{
    public class OrderSellRepository : BaseRepository, IOrderSellRepository, IDisposable
    {
        public SuccessOrderSellDTO OrderSellArticle(int articleId, int buyerId, int orderSellPrice, TransactionType transactionType)
        {
            SuccessOrderSellDTO success = new SuccessOrderSellDTO();

            List<Article> RequiredArticleList_db = new List<Article>();

            ArticleDTO chosenArticle = new ArticleDTO();

            try
            {
                var suppliers_db = ctx.Suppliers.Include("Articles").ToList();
                var requiredArticle = ctx.Articles.Find(articleId);
                var requiredBuyer = ctx.Buyers.Find(buyerId);


                if(transactionType == TransactionType.Order)
                {
                    if (requiredArticle != null)
                    {
                        //adding only required articles to list by Id
                        foreach (var suppliers in suppliers_db)
                        {
                            foreach (var articles in suppliers.Articles)
                            {
                                if (articleId == articles.ArticleId)
                                    RequiredArticleList_db.Add(articles);
                            }
                        }
                        //enumerate through list for minimum price
                        foreach (var item in RequiredArticleList_db)
                        {
                            if (item.ArticlePrice == RequiredArticleList_db.Min(x => x.ArticlePrice))
                            {
                                success.ArticleId = item.ArticleId;
                                success.ArticleName = item.Name;
                                success.BuyerId = requiredBuyer.BuyerId;
                                success.BuyerName = requiredBuyer.Name;
                                success.OrderSellDate = DateTime.Now;
                                success.OrderSellPrice = item.ArticlePrice;
                                success.SupplierId = item.Supplier.SupplierId;
                                success.SupplierName = item.Supplier.Name;
                                success.TransactionType = TransactionType.Order;
                            }
                        }
                    }
                }
                else if (transactionType == TransactionType.Sell)
                {
                    if(requiredArticle != null)
                    {
                        success.ArticleId = requiredArticle.ArticleId;
                        success.ArticleName = requiredArticle.Name;
                        success.BuyerId = requiredBuyer.BuyerId;
                        success.BuyerName = requiredBuyer.Name;
                        success.OrderSellDate = DateTime.Now;
                        success.OrderSellPrice = requiredArticle.ArticlePrice;
                        success.TransactionType = TransactionType.Sell;
                    }
                }
                return success;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error - {ex.Message} - on making order or sell.");
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
