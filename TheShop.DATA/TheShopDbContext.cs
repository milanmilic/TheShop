using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheShop.DATA.Model;

namespace TheShop.DATA
{
    public class TheShopDbContext : DbContext
    {
        public TheShopDbContext() : base("TheShopDbContext")
        {
            Database.SetInitializer(new TheShopDbInitializer());
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public class TheShopDbInitializer : DropCreateDatabaseAlways<TheShopDbContext>
        {
            protected override void Seed(TheShopDbContext context)
            {
                //mock data
                var supplier1 = new Supplier { SupplierId = 1, Name = "Supplier1", Articles = new List<Article>() };
                var supplier2 = new Supplier { SupplierId = 2, Name = "Supplier2", Articles = new List<Article>() };
                var supplier3 = new Supplier { SupplierId = 3, Name = "Supplier3", Articles = new List<Article>() };

                var article1Supplier1 = new Article { ArticleId = 1, Name = "Article1", ArticlePrice = 200, IsSold = false, BuyerId = null };
                var article1Supplier2 = new Article { ArticleId = 1, Name = "Article1", ArticlePrice = 250, IsSold = false, BuyerId = null };
                var article1Supplier3 = new Article { ArticleId = 1, Name = "Article1", ArticlePrice = 300, IsSold = false, BuyerId = null };

                var article2Supplier1 = new Article { ArticleId = 2, Name = "Article2", ArticlePrice = 180, IsSold = false, BuyerId = null };
                var article2Supplier2 = new Article { ArticleId = 2, Name = "Article2", ArticlePrice = 150, IsSold = false, BuyerId = null };
                var article2Supplier3 = new Article { ArticleId = 2, Name = "Article2", ArticlePrice = 200, IsSold = false, BuyerId = null };

                supplier1.Articles.Add(article1Supplier1);
                supplier1.Articles.Add(article2Supplier1);

                supplier2.Articles.Add(article1Supplier2);
                supplier2.Articles.Add(article2Supplier2);

                supplier3.Articles.Add(article1Supplier3);
                supplier3.Articles.Add(article2Supplier3);

                List<Buyer> buyerList = new List<Buyer>
                {
                    new Buyer { BuyerId = 1, Name = "Buyer1" },
                    new Buyer { BuyerId = 2, Name = "Buyer2" },
                    new Buyer { BuyerId = 3, Name = "Buyer3" }
                };

                context.Suppliers.Add(supplier1);
                context.Suppliers.Add(supplier2);
                context.Suppliers.Add(supplier3);

                context.Articles.Add(article1Supplier1);
                context.Articles.Add(article1Supplier2);
                context.Articles.Add(article1Supplier3);
                context.Articles.Add(article2Supplier1);
                context.Articles.Add(article2Supplier2);
                context.Articles.Add(article2Supplier3);

                context.Buyers.AddRange(buyerList);

                base.Seed(context);
            }
        }
    }
}
