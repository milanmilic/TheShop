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
    public class ArticleRepository : BaseRepository, IArticleRepository, IDisposable
    {
        public IEnumerable<ArticleDTO> GetArticles()
        {
            List<ArticleDTO> articleDTOList = new List<ArticleDTO>();

            try
            {
                var articles_db = ctx.Articles.ToList();


                articles_db.ForEach(item => articleDTOList.Add(new ArticleDTO
                {
                    ArticleId = item.ArticleId,
                    IsSold = item.IsSold,
                    Name = item.Name,
                    Price = item.ArticlePrice,
                    SoldDate = item.SoldDate,
                    BuyerId = item.BuyerId
                }));

                return articleDTOList;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error - {ex.Message} - on getting article list.");
            }
        }

        public ArticleDTO GetArticleById(int articleId)
        {
            ArticleDTO articleDTO = new ArticleDTO();

            try
            {
                var article_db = ctx.Articles.Find(articleId);

                if(article_db != null)
                {
                    articleDTO.ArticleId = article_db.ArticleId;
                    articleDTO.BuyerId = article_db.BuyerId;
                    articleDTO.IsSold = article_db.IsSold;
                    articleDTO.Name = article_db.Name;
                    articleDTO.Price = article_db.ArticlePrice;
                    articleDTO.SoldDate = article_db.SoldDate;
                }

                return articleDTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error - {ex.Message} - on getting article by id.");
            }
        }

        public bool InsertArticle(ArticleDTO article)
        {
            var article_db = ctx.Set<Article>();
            Article newArticle = new Article();

            try
            {
                if(article != null)
                {
                    newArticle.ArticleId = article.ArticleId;
                    newArticle.ArticlePrice = article.Price;
                    newArticle.BuyerId = article.BuyerId;
                    newArticle.IsSold = article.IsSold;
                    newArticle.Name = article.Name;
                    newArticle.SoldDate = article.SoldDate;

                    ctx.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error - {ex.Message} - on inserting new article.");
            }
        }

        public bool UpdateArticle(ArticleDTO article)
        {
            var article_db = ctx.Articles.Where(x => x.ArticleId == article.ArticleId).FirstOrDefault();

            try
            {
                if(article_db != null)
                {
                    article_db.ArticleId = article.ArticleId;
                    article_db.ArticlePrice = article.Price;
                    article_db.BuyerId = article.BuyerId;
                    article_db.IsSold = article.IsSold;
                    article_db.Name = article.Name;
                    article_db.SoldDate = article.SoldDate;

                    ctx.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error - {ex.Message} - on updating existing article.");
            }
        }

        public bool RemoveArticle(int articleId)
        {
            try
            {
                Article article_db = ctx.Articles.Find(articleId);

                if (article_db != null)
                {
                    ctx.Articles.Remove(article_db);

                    ctx.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error - {ex.Message} - on removing existing article.");
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
