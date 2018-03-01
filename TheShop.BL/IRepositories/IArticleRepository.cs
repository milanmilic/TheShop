using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheShop.DAL.DTO;

namespace TheShop.BL.IRepositories
{
    public interface IArticleRepository : IDisposable
    {
        IEnumerable<ArticleDTO> GetArticles();
        ArticleDTO GetArticleById(int articleId);
        bool InsertArticle(ArticleDTO article);
        bool UpdateArticle(ArticleDTO article);
        bool RemoveArticle(int articleId);
    }
}
