using System;
using TheShop.DAL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;

namespace TheShop
{
	internal class Program
	{
		private static void Main(string[] args)
		{
            ApiService service = new ApiService();

            List<ArticleDTO> articleList = new List<ArticleDTO>();
            List<SupplierDTO> supplierList = new List<SupplierDTO>();
            List<BuyerDTO> buyerList = new List<BuyerDTO>();
            SuccessOrderSellDTO order = new SuccessOrderSellDTO();
            SuccessOrderSellDTO sell = new SuccessOrderSellDTO();
            ArticleDTO articleById = new ArticleDTO();

            bool updateArticleRez = false;


            var db_dataTask = Task.Run(async() =>
            {
                articleList = await ApiService.GetGenericListAsync<ArticleDTO>(service.GetClient(), ApiService.GetArticlesUri);
                supplierList = await ApiService.GetGenericListAsync<SupplierDTO>(service.GetClient(), ApiService.GetSuppliersUri);
                buyerList = await ApiService.GetGenericListAsync<BuyerDTO>(service.GetClient(), ApiService.GetBuyersUri);
            });

            db_dataTask.Wait();

            Console.WriteLine("---------------------Suppliers with their articles ------------------------");
            Console.WriteLine("SupplierId    Name");
            Console.WriteLine("---------------------------------------------------------------------------");

            for (int i = 0; i < supplierList.Count; i++)
            {
                Console.WriteLine($"{supplierList[i].SupplierId}            {supplierList[i].Name} \n");
                Console.WriteLine("ArticleId    Name        Price");
                for (int j = 0; j < supplierList[i].ArticleDTOList.Count; j++)
                {
                    Console.WriteLine($"{supplierList[i].ArticleDTOList[j].ArticleId}            {supplierList[i].ArticleDTOList[j].Name}    {string.Format("{0:C}", supplierList[i].ArticleDTOList[j].Price)} ");
                }
                Console.WriteLine("------------------------------------------------------------------------------");
            }

            Thread.Sleep(1000);
            Console.WriteLine("------------------------Ordering article------------------------------------");
            var db_orderTask = Task.Run(async () =>
            {
                order = await ApiService.GetOrderSellAsync(service.GetClient(), ApiService.GetOrderSellUri, articleList[1].ArticleId, buyerList[2].BuyerId, 200, TransactionType.Order);
            });

            db_orderTask.Wait();
            Console.WriteLine("ArticleId     ArticleName    BuyerId  BuyerName     OrderDate     OrderPrice     SupplierId    SupplierName");
            Console.WriteLine($"{order.ArticleId}             " +
                $"{order.ArticleName}       " +
                $"{order.BuyerId}        " +
                $"{order.BuyerName}        " +
                $"{string.Format("{0:d}", order.OrderSellDate)}     " +
                $"{order.OrderSellPrice}            " +
                $"{order.SupplierId}             " +
                $"{order.SupplierName}");

            Thread.Sleep(1000);
            Console.WriteLine("------------------------Selling article-------------------------------------");
            var db_sellTask = Task.Run(async () =>
            {
                sell = await ApiService.GetOrderSellAsync(service.GetClient(), ApiService.GetOrderSellUri, articleList[1].ArticleId, buyerList[2].BuyerId, 200, TransactionType.Sell);
            });

            db_sellTask.Wait();
            Console.WriteLine("ArticleId     ArticleName    BuyerId  BuyerName      SellDate     SellPrice");
            Console.WriteLine($"{sell.ArticleId}             " +
                $"{sell.ArticleName}       " +
                $"{sell.BuyerId}        " +
                $"{sell.BuyerName}        " +
                $"{string.Format("{0:d}", sell.OrderSellDate)}     " +
                $"{sell.OrderSellPrice}");


            Thread.Sleep(1000);
            ArticleDTO soldArticle = new ArticleDTO();
            soldArticle.ArticleId = sell.ArticleId;
            soldArticle.BuyerId = sell.BuyerId;
            soldArticle.BuyerName = sell.BuyerName;
            soldArticle.IsSold = sell != null ? true : false;
            soldArticle.Name = sell.ArticleName;
            soldArticle.Price = sell.OrderSellPrice;
            soldArticle.SoldDate = sell.OrderSellDate;

            Console.WriteLine("---------------------------Sold article-------------------------------------");
            Console.WriteLine("ArticleId     ArticleName    BuyerId  BuyerName     IsSold      SellDate     SellPrice");
            Console.WriteLine($"{soldArticle.ArticleId}             " +
                $"{soldArticle.Name}       " +
                $"{soldArticle.BuyerId}        " +
                $"{soldArticle.BuyerName}        " +
                $"{soldArticle.IsSold}        " +
                $"{string.Format("{0:d}", soldArticle.SoldDate)}    " +
                $"{soldArticle.Price}");

            var db_updateArticle = Task.Run(async () =>
            {
                updateArticleRez = await ApiService.UpdateArticle(service.GetClient(), ApiService.UpdateArticleUri, soldArticle);
            });

            db_updateArticle.Wait();

            Thread.Sleep(1000);

            if (updateArticleRez)
            {
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("Successfully updated the sold article");
                Console.WriteLine("--------------------------------------");
            }

            var db_getArticleById = Task.Run(async () => 
            {
                articleById = await ApiService.GetGenericById<ArticleDTO>(service.GetClient(), ApiService.GetArticleByIdUri, soldArticle.ArticleId, "articleId");
            });

            db_getArticleById.Wait();

            Console.WriteLine("---------------------------Updated article from db-------------------------------------");
            Console.WriteLine("ArticleId     ArticleName    BuyerId     IsSold      SellDate     SellPrice");
            Console.WriteLine($"{articleById.ArticleId}             " +
                $"{articleById.Name}       " +
                $"{articleById.BuyerId}           " +
                $"{articleById.IsSold}        " +
                $"{string.Format("{0:d}", articleById.SoldDate)}    " +
                $"{articleById.Price}");

            Console.ReadLine();

            //var shopService = new ShopService();

            //try
            //{
            //	//order and sell
            //	shopService.OrderAndSellArticle(1, 20, 10);
            //}
            //catch (Exception ex)
            //{
            //	Console.WriteLine(ex);
            //}

            //try
            //{
            //	//print article on console
            //	var article = shopService.GetById(1);
            //	Console.WriteLine("Found article with ID: " + article.ID);
            //}
            //catch (Exception ex)
            //{
            //	Console.WriteLine("Article not found: " + ex);
            //}

            //try
            //{
            //	//print article on console				
            //	var article = shopService.GetById(12);
            //	Console.WriteLine("Found article with ID: " + article.ID);
            //}
            //catch (Exception ex)
            //{
            //	Console.WriteLine("Article not found: " + ex);
            //}

            //Console.ReadKey();
        }
    }
}